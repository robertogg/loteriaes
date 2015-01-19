using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.Constants;
using LoteriaES.Core.CQRS;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.WindowsAzure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoteriaES.Infrastructure.EventStore
{
    public class EventStore:IEventStore
    {
        private const string DatabaseId = "LoteriaES";
        private const string CollectionId = "Events";

        private readonly string _documentDbUri;
        private readonly string _documentDBKey;

        public EventStore()
        {
            _documentDbUri= CloudConfigurationManager.GetSetting(Azure.Configuration.DocumentDbUri);
            _documentDBKey = CloudConfigurationManager.GetSetting(Azure.Configuration.DocumentDbKey);
        }
        private struct EventDescriptor
        {
            public readonly Guid EventId;
            public readonly int Version;
            public readonly IEvent EventData;
            public string EventType;

            public EventDescriptor(Guid id, IEvent eventData, int version)
            {
                EventId = id;
                Version = version;
                EventData = eventData;
                EventType = eventData.GetType().AssemblyQualifiedName;
            }
        }

        public async Task SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
        {
            expectedVersion++;
            var client = new DocumentClient(new Uri(_documentDbUri), _documentDBKey);
            var documentCollection = await GetDocumentCollection(client, DatabaseId, CollectionId);

            foreach (var @event in events)
            {
                @event.AsDynamic().Sender.Version = expectedVersion;
                var eventDescriptor = new EventDescriptor(aggregateId, @event, expectedVersion);
                client.CreateDocumentAsync(documentCollection.DocumentsLink, eventDescriptor).Wait();
            }
        }

        public async Task<List<IEvent>> GetEventsForAggregate(Guid aggregateId)
        {
            var client = new DocumentClient(new Uri(_documentDbUri), _documentDBKey);
            var documentCollection = await GetDocumentCollection(client, DatabaseId, CollectionId);
            var eventDescriptors = client.CreateDocumentQuery(documentCollection.DocumentsLink,
                string.Format("SELECT * FROM Events e WHERE e.EventId ='{0}'", aggregateId))
                .ToList()
                .Select(data => (data.EventData as JObject).ToObject(Type.GetType(data.EventType.ToString())))
                .Cast<IEvent>().ToList();
           
            return eventDescriptors.OrderBy(o=>o.AsDynamic().Sender.Version).ToList();
        }
        private static async Task<DocumentCollection> GetDocumentCollection(DocumentClient client, string databaseId, string collectionId)
        {
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();

            if (database == null)
            {
                database = await client.CreateDatabaseAsync(
                    new Database
                    {
                        Id = databaseId
                    });
            }

            var documentCollection =
                client.CreateDocumentCollectionQuery(database.CollectionsLink)
                    .Where(c => c.Id == collectionId)
                    .ToArray()
                    .FirstOrDefault();
            if (documentCollection == null)
            {
                documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                    new DocumentCollection
                    {
                        Id = collectionId
                    });
            }
            return documentCollection;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.Core.Constants
{
    public sealed class Azure
    {
        public sealed class Configuration
        {
            public const string StorageConnectionString = "LoteriaES.WindowsAzure.Storage.ConnectionString";
            public const string ServiceBusKbEventsQueue = "LoteriaES.WindowsAzure.ServiceBus.KbEventsQueue";
            public const string DocumentDbUri = "LoteriaEs.WindowsAzure.DocumentDB.Uri";
            public const string DocumentDbKey = "LoteriaEs.WindowsAzure.DocumentDB.Key";
            public const string SqlAzureConnectionString = "LoteriaES.SQL.ConnectionString";
        }
    }
}

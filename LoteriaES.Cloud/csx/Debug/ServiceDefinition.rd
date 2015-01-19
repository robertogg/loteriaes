<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="LoteriaES.Cloud" generation="1" functional="0" release="0" Id="2f2712d9-eb22-4d95-9bfb-921aea04ae37" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="LoteriaES.CloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="LoteriaES:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LB:LoteriaES:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="LoteriaES:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/MapLoteriaES:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="LoteriaESInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/MapLoteriaESInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:LoteriaES:Endpoint1">
          <toPorts>
            <inPortMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaES/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapLoteriaES:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaES/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapLoteriaESInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaESInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="LoteriaES" generation="1" functional="0" release="0" software="C:\Proyectos\Eventos\LoteriaES\LoteriaES.Cloud\csx\Debug\roles\LoteriaES" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;LoteriaES&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;LoteriaES&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaESInstances" />
            <sCSPolicyUpdateDomainMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaESUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaESFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="LoteriaESUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="LoteriaESFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="LoteriaESInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="b6d5d804-a78c-42b4-99ef-f3c43bdad1dd" ref="Microsoft.RedDog.Contract\ServiceContract\LoteriaES.CloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="3bc1703b-5e45-4142-b817-61a06664cf26" ref="Microsoft.RedDog.Contract\Interface\LoteriaES:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/LoteriaES.Cloud/LoteriaES.CloudGroup/LoteriaES:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="WindowsAzure1" generation="1" functional="0" release="0" Id="d2abc9e0-917e-4cfc-b3b9-4590751d9e3d" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="WindowsAzure1Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Website:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/WindowsAzure1/WindowsAzure1Group/LB:Website:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Website:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WindowsAzure1/WindowsAzure1Group/MapWebsite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebsiteInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/WindowsAzure1/WindowsAzure1Group/MapWebsiteInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Website:Endpoint1">
          <toPorts>
            <inPortMoniker name="/WindowsAzure1/WindowsAzure1Group/Website/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWebsite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WindowsAzure1/WindowsAzure1Group/Website/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWebsiteInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/WindowsAzure1/WindowsAzure1Group/WebsiteInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Website" generation="1" functional="0" release="0" software="C:\Users\Mikhail_Semichev\Desktop\Azure\WindowsAzure1\csx\Debug\roles\Website" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Website&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Website&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/WindowsAzure1/WindowsAzure1Group/WebsiteInstances" />
            <sCSPolicyFaultDomainMoniker name="/WindowsAzure1/WindowsAzure1Group/WebsiteFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="WebsiteFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WebsiteInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="4990d8b9-ddbf-4209-b57b-089d14988e3a" ref="Microsoft.RedDog.Contract\ServiceContract\WindowsAzure1Contract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="cad13fad-cb3c-4927-9ffc-ad107b4884d2" ref="Microsoft.RedDog.Contract\Interface\Website:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/WindowsAzure1/WindowsAzure1Group/Website:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
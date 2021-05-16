using LinqToDB;
using LinqToDB.Data;

namespace AzureFunctionApp
{
        class MyContext : DataConnection
        {
            public MyContext() : base(ProviderName.SqlServer, @"server=(localdb)\MSSQLLocalDB;database=AzureMachineDb;integrated security=true")
            {
            }
            public ITable<MachineData> MachineDataAccesses => GetTable<MachineData>();
        } 
}

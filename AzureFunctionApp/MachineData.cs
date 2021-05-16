using LinqToDB.Mapping;

namespace AzureFunctionApp
{
    [Table]    class MachineData
    {
        [PrimaryKey, Identity, NotNull]
        public int iD { get; set; }
        [PrimaryKey, NotNull]
        public string MachineName { get; set; }
        [Column]
        public string TimeZone { get; set; }
        [Column]
        public string OsVersion { get; set; }
        [Column]
        public string DotNetVersion { get; set; } 
    }
}

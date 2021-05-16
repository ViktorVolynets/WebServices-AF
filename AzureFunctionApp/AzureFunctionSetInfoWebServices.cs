using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using LinqToDB;

namespace AzureFunctionApp
{
    public static class AzureFunctionSetInfoWebServices
    {
        [FunctionName("FunctionSetAzure")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            //Get data from ws
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            string responseMessage = "request Error";

            MachineData data = new MachineData();

            if (!string.IsNullOrEmpty(requestBody))
            {
                 data = JsonConvert.DeserializeObject<MachineData>(requestBody);
                 log.LogInformation("C# HTTP trigger function receive data from the Web.");

                try
                {
                    using (var con = new MyContext())
                    {
                        if (con.MachineDataAccesses.Any(m => m.MachineName == data.MachineName))
                        {
                            if (!con.MachineDataAccesses.Any(m =>
                             (m.MachineName == data.MachineName
                             && m.TimeZone == data.TimeZone
                             && m.OsVersion == data.OsVersion
                             && m.DotNetVersion == data.DotNetVersion)))
                                {
                                //Update data to table
                                con.MachineDataAccesses.Where(m => m.MachineName == data.MachineName)
                                 .Set(p => p.TimeZone, data.TimeZone)
                                 .Set(p => p.OsVersion, data.OsVersion)
                                 .Set(p => p.DotNetVersion, data.DotNetVersion)
                                 .Update();
                                }
                        }
                        else
                        {
                            con.Insert<MachineData>(data);
                        }
                        con.CommitTransaction();
                    }
                }
                catch (Exception ex)
                {
                    log.LogInformation(ex.Message);
                }
                finally
                {
                    log.LogInformation("Update or insert in DataBase completed");
                    responseMessage = "request completed successfully";
                }
            } 
            return new OkObjectResult(responseMessage);
        }
    }
}

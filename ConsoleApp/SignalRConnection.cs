using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class SignalRConnection
    {
        private HubConnection connection;
        public async void Start()
        {
            var url = "https://localhost:44360/DataHub";

             connection = new HubConnectionBuilder()
                .WithUrl(url, HttpTransportType.WebSockets)
                .WithAutomaticReconnect()
                .Build();

            connection.Closed += async (error) =>
            {
                Console.WriteLine("Disconnected");
                await Task.Delay(70000);
                await connection.StartAsync();
            };

            //Receive a command from the hub
            connection.On<string>("ReceiveData", (key) => OnReceiveData(key));
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
            while (true)
            {
                Console.WriteLine("Please Enter to exit");
                string message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                  break;
                }
            }
        }

        private void OnReceiveData(string key)
        {
            //Validate key matching
            if (key == "123")
            {
                MachineData data = new MachineData();
                var dataString = JsonConvert.SerializeObject(data);
                connection.InvokeAsync("GetData", dataString)
                    .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("There was an error calling send: {0}", task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine($"Data complete:{data.MachineName};{data.TimeZone};{data.OsVersion};{data.DotNetVersion}");
                    }
                }).Wait();
            } else
            {
                Console.WriteLine("The key is incorrect: {0}", key);
            }
        }
    }

  
}

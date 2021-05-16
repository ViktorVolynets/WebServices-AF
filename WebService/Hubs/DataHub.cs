using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebService.Hubs
{
    public class DataHub : Hub
    {
        public List<string> OnlineDevice;
        private static readonly HttpClient client = new HttpClient();
        static object locker = new object();
        private readonly ILogger<DataHub> _logger;
        public DataHub(ILogger<DataHub> log)
        {
            OnlineDevice = new List<string>();
            _logger = log;
        }
        public async void GetData (string dataString)
        {
            var content = new StringContent (dataString);
            
            //Send data to azure function
            try
            {
                using (HttpResponseMessage response = await client
                .PostAsync("http://localhost:7071/api/FunctionSetAzure", content))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation(responseString);
                };
            } 
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }  
        }

        public override async Task OnConnectedAsync()
        {
            lock (locker)
            {
                //Add online device
                OnlineDevice.Add(Context.ConnectionId);
            }     
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            lock (locker)
            {
                //Remove offline device
                OnlineDevice.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}


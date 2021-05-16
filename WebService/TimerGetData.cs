using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;

namespace WebService.Hubs
{
    public class TimerGetData
    {
        private IHubContext<DataHub> hubcontext;
        private Timer timer;
        public TimerGetData(IHubContext<DataHub> hub)
        {
            hubcontext = hub;

            //Create a timer with a five minutes interval.
            TimerCallback tm = new TimerCallback(Callback);
            string key = "123";
            TimeSpan period = TimeSpan.FromMinutes(5);
            timer = new Timer(tm, key, TimeSpan.FromMilliseconds(0), period);
            GC.KeepAlive(timer);
        }

        //Call to send data by console application.
        public void Callback(object obj) => hubcontext.Clients.All.SendAsync("ReceiveData", obj);
    }
}

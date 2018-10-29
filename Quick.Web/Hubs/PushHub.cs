using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Quick.Web.Hubs
{
    public class PushHub : Hub
    {

        public static List<string> SubscribeClientIds { get; set; } = new List<string>(); //存放客户端id集合

        public static readonly IHubConnectionContext<dynamic> Connections = GlobalHost.ConnectionManager.GetHubContext<PushHub>().Clients;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            SubscribeClientIds.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        public void Update()
        {
            SubscribeClientIds.Add(Context.ConnectionId);
        }

        /// <summary>
        /// 推送数据
        /// </summary>
        /// <param name="cb"></param>
        public static void PushData(Action<dynamic> cb)
        {
            cb(Connections.Clients(SubscribeClientIds));
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}
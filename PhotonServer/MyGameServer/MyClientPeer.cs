using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotonHostRuntimeInterfaces;
using Photon.SocketServer;
using MyGameServer.Handler;
using MyGameServer.Tools;
using Common;
using ExitGames.Logging;

namespace MyGameServer
{
    public class MyClientPeer : ClientPeer
    {
        public float x;
        public float y;
        public float z;
        public string m_userName;

        public static readonly ILogger m_log = LogManager.GetCurrentClassLogger();

        public MyClientPeer(InitRequest initRequest):base(initRequest)
        {

        }

        //断开连接 清理工作
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.Instance.m_peerList.Remove(this);
        }

        //客户端请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.m_handlerDic, (OperationCode)operationRequest.OperationCode);

            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandler defaultHandler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.m_handlerDic, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            
        }
    }
}

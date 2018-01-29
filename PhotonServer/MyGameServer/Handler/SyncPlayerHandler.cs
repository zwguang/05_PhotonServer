using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using System.IO;
using System.Xml.Serialization;
using Common;
using MyGameServer.Tools;

namespace MyGameServer.Handler
{
    class SyncPlayerHandler : BaseHandler
    {
        public SyncPlayerHandler()
        {
            m_opCode = Common.OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            //获取所有已登录的在线玩家用户名
            List<string> usernameList = new List<string>();
            foreach(MyClientPeer peerTemp in MyGameServer.Instance.m_peerList)
            {
                if(!String.IsNullOrEmpty(peerTemp.m_userName) && peerTemp != peer)
                {
                    usernameList.Add(peerTemp.m_userName);
                }
            }

            //序列化传输对象
            string usernameListString = XML.Serializer<List<string>>(usernameList);

            //向新的用户发送 所有player的信息
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.UsernameList, usernameListString);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;

            //向已有用户发送 新player的信息
            foreach (MyClientPeer peerTemp in MyGameServer.Instance.m_peerList)
            {
                if (!String.IsNullOrEmpty(peerTemp.m_userName) && peerTemp != peer)
                {
                    EventData ed = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte, Object> data2 = new Dictionary<byte, object>();
                    data2.Add((byte)ParameterCode.Username, peer.m_userName);
                    ed.Parameters = data2;

                    peerTemp.SendEvent(ed, sendParameters);
                }
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}

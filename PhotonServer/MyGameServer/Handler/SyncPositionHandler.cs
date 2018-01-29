using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using MyGameServer.Tools;
using Common;

namespace MyGameServer.Handler
{
    class SyncPositionHandler : BaseHandler
    {
        public SyncPositionHandler()
        {
            m_opCode = Common.OperationCode.SyncPosition;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.x);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.y);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.z);
            peer.x = x;
            peer.y = y;
            peer.z = z;
            MyGameServer.m_log.Info("pos.x = " + x + "   pos.y = " + y + "    pos.z = " + z);
        }
    }
}

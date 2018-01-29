using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;

/// <summary>
/// 所有处理客户端请求的基类
/// </summary>
namespace MyGameServer.Handler
{
    public abstract class BaseHandler
    {
        public OperationCode m_opCode;

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class DefaultHandler: BaseHandler
    {
        public DefaultHandler()
        {
            m_opCode = Common.OperationCode.Default;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}

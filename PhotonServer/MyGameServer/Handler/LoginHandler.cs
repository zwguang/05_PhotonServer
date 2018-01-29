using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using MyGameServer.Tools;
using MyGameServer.Manager;

namespace MyGameServer.Handler
{
    class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            m_opCode = OperationCode.Login;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            UserManager userManager = new UserManager();
            if (userManager.VerifyUser(username, password))
            {
                response.ReturnCode = (short)Common.ReturnCode.Success;
                peer.m_userName = username;
            }
            else
            {
                response.ReturnCode = (short)Common.ReturnCode.Fail;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
        
    }
}

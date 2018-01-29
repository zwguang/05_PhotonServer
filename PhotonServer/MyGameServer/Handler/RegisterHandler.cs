using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using MyGameServer.Tools;
using MyGameServer.Manager;
using Common;
using MyGameServer.Model;

namespace MyGameServer.Handler
{
    class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            m_opCode = Common.OperationCode.Reister;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            UserManager userManager = new UserManager();
            User user = userManager.GetByUserName(username);
            //没有重复的username
            if (user == null)
            {
                User usertemp = new User() { Username = username, Password = password };
                userManager.Add(usertemp);
                response.ReturnCode = (short)Common.ReturnCode.Success;
            }
            else
            {
                response.ReturnCode = (short)Common.ReturnCode.Fail;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

class LoginRequest : Request
{
    [HideInInspector]
    public string m_userName;
    [HideInInspector]
    public string m_passWord;

    
    private LoginPanel m_loginPanel;

    public override void Start()
    {
        base.Start();
        m_loginPanel = GetComponent<LoginPanel>();
    }
    

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, m_userName);
        data.Add((byte)ParameterCode.Password, m_passWord);

        PhotonEngine.Instance.Peer.OpCustom((byte)m_opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        ReturnCode returnCode = (ReturnCode)response.ReturnCode;
        if(returnCode == ReturnCode.Success)
        {
            PhotonEngine.Instance.m_userName = m_userName;
        }
        m_loginPanel.OnLoginResponse((ReturnCode)response.ReturnCode);
        
    }
}

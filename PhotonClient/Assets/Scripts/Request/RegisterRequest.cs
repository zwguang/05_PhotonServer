using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

class RegisterRequest : Request
{
    [HideInInspector]
    public string m_userName;
    [HideInInspector]
    public string m_passWord;

    private RegisterPanel m_registerPanel;

    public override void Start()
    {
        base.Start();

        m_registerPanel = GetComponent<RegisterPanel>();
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
        m_registerPanel.OnConfirmResponse((ReturnCode)response.ReturnCode);
    }
}

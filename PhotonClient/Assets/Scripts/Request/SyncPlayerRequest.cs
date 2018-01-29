using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// 同步其他客户端信息
/// </summary>
class SyncPlayerRequest : Request
{
    private Player m_player;
    public override void Start()
    {
        base.Start();
        m_player = GetComponent<Player>();
    }

    public override void DefaultRequest()
    {
        PhotonEngine.Instance.Peer.OpCustom((byte)m_opCode, null, true);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        String userNameListXML = DictTool.GetValue<byte, object>(response.Parameters, (byte)ParameterCode.UsernameList) as String;
        //反序列化
        List<string> usernameList = XML.Serializer<List<string>>(userNameListXML);
        m_player.OnSyncPlayerResopnse(usernameList);
       
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

/// <summary>
/// 同步坐标位置
/// </summary>
class SyncPositionRequest : Request
{
    [HideInInspector]
    public Vector3 m_syncPos;

    private Player m_player;
    
    public override void Start()
    {
        base.Start();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        //   data.Add((byte)ParameterCode.Position, new Vector3Data() { x= m_syncPos.x, y=m_syncPos.y, z=m_syncPos.z });
        data.Add((byte)ParameterCode.x, m_syncPos.x);
        data.Add((byte)ParameterCode.y, m_syncPos.y);
        data.Add((byte)ParameterCode.z, m_syncPos.z);

        PhotonEngine.Instance.Peer.OpCustom((byte)m_opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        
    }
}


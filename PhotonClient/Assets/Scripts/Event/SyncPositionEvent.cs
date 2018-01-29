using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

class SyncPositionEvent:BaseEvent
{
    private Player m_player;

    public override void Start()
    {
        m_eventCode = EventCode.SyncPosition;

        base.Start();
        m_player = GetComponent<Player>();
    }


    public override void OnEvent(EventData eventData)
    {
        string userDataListString = DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.UserDataList) as string;

        //反序列化
        List<PlayerData> userDataList = XML.Serializer<List<PlayerData>>(userDataListString);
        m_player.OnSyncPlayersDataEvent(userDataList);
    }
}

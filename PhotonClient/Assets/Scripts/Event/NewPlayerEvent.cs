using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class NewPlayerEvent : BaseEvent
{
    private Player m_player;

    public override void Start()
    {
        m_eventCode = EventCode.NewPlayer;

        base.Start();
        m_player = GetComponent<Player>();
    }


    public override void OnEvent(EventData eventData)
    {
        string userName = DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.Username) as string;

        Debug.Log("OnEvent userName = " + userName);
        m_player.OnNewPlayerEvent(userName);
    }

}

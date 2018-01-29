using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class BaseEvent : MonoBehaviour
{
    public EventCode m_eventCode;
    public abstract void OnEvent(EventData eventData);

    public virtual void Start()
    {
        PhotonEngine.Instance.AddEvent(this);
    }

    public void OnDestroy()
    {
        PhotonEngine.Instance.RemoveEvent(this);
    }
}

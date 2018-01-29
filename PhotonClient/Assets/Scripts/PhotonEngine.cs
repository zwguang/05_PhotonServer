using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener {

    private static PhotonEngine m_instance = null;
    public static PhotonEngine Instance
    {
        get
        {
            return m_instance;
        }
    }

    private PhotonPeer m_peer;
    public PhotonPeer Peer
    {
        get
        {
            return m_peer;
        }
    }

    public string m_userName;
    public Dictionary<OperationCode, Request> m_requestDic;
    public Dictionary<EventCode, BaseEvent> m_eventDic;

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(m_instance != this)
        {
            Destroy(this.gameObject); return;
        }
        m_requestDic = new Dictionary<OperationCode, Request>();
        m_eventDic = new Dictionary<EventCode, BaseEvent>();
    }
    
    void Start ()
    {
        //这里使用udp协议，udp不稳定的问题已在photon修复
        //通过Listender接收服务器的响应
        m_peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        m_peer.Connect("192.168.10.84:5055", "MyGame1");//服务器端口 应用名
        
    }

    public void AddRequest(Request request)
    {
        m_requestDic.Add(request.m_opCode, request);
    }
    public void RemoveRequest(Request request)
    {
        m_requestDic.Remove(request.m_opCode);
    }

    public void AddEvent(BaseEvent baseEvent)
    {
        m_eventDic.Add(baseEvent.m_eventCode, baseEvent);
    }
    public void RemoveEvent(BaseEvent baseEvent)
    {
        m_eventDic.Remove(baseEvent.m_eventCode);
    }

    void Update ()
    {
        m_peer.Service();
    }

    private void OnDestroy()
    {
        if(m_peer != null && m_peer.PeerState == PeerStateValue.Connected)
        {
            m_peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    //服务器主动发送的事件
    public void OnEvent(EventData eventData)
    {
        EventCode opCode = (EventCode)eventData.Code;
        BaseEvent baseEvent = DictTool.GetValue<EventCode, BaseEvent>(m_eventDic, opCode);
        Debug.Log("opCode:" + opCode);
        baseEvent.OnEvent(eventData);
    }

    //客户端请求  服务端的回复
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;
        Request request = DictTool.GetValue<OperationCode, Request>(m_requestDic, opCode);

        request.OnOperationResponse(operationResponse);
        
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }
}

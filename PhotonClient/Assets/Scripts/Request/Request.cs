
using Common;
using UnityEngine;
using ExitGames.Client.Photon;

public abstract class Request: MonoBehaviour
{
    public OperationCode m_opCode;
    public abstract void DefaultRequest();

    /// <summary>
    /// 服务端对客户端请求的回复
    /// </summary>
    /// <param name="response"></param>
    public abstract void OnOperationResponse(OperationResponse response);

    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);
    }

    public void OnDestroy()
    {
        PhotonEngine.Instance.RemoveRequest(this);
    }
}

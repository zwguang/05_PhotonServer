using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Update () {

        if (Input.GetMouseButton(1))
        {
            Debug.Log("请求服务器");
            this.SendRequest();
        }
    }
	
    void SendRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 100);
        data.Add(2, "shuliang");
        PhotonEngine.Instance.Peer.OpCustom(1, data, true);
    }
}

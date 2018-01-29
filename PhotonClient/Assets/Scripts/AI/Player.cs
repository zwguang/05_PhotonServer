using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float smoothing = 5f;
    private Vector3 m_lastPosition = Vector3.zero;
    private float m_moveOffset = 0.1f;
    private Dictionary<string, GameObject> m_playerDic = new Dictionary<string, GameObject>();
    public GameObject m_perfabPlayer;
    public GameObject m_player;

    private SyncPositionRequest m_syncPosRequest;
    private SyncPlayerRequest m_syncPlayerRequest;


    // Use this for initialization
    private void Awake()
    {

        m_syncPosRequest = GetComponent<SyncPositionRequest>();
        m_syncPlayerRequest = GetComponent<SyncPlayerRequest>();
    }

    void Start()
    {
        m_player.GetComponent<Renderer>().material.color = Color.green;

        m_syncPlayerRequest.DefaultRequest();

        InvokeRepeating("SyncPosition", 3, 1 / 10f);//3s后开始
    }

    /// <summary>
    /// 跟服务器同步自身位置信息
    /// </summary>
    void SyncPosition()
    {
        if (Vector3.Distance(m_player.transform.position, m_lastPosition) > m_moveOffset)
        {
            m_lastPosition = m_player.transform.position;
            m_syncPosRequest.m_syncPos = m_player.transform.position;
            m_syncPosRequest.DefaultRequest();
        }
    }

    // 移动
    void Update()
    {
        {
            float h = Input.GetAxis(Constant.Horizontal);
            float v = Input.GetAxis(Constant.Vertical);
            Vector3 moveBy = new Vector3(h * smoothing * Time.deltaTime, 0, v * smoothing * Time.deltaTime);
            m_player.transform.Translate(moveBy);
            //m_syncPosRequest.m_syncPos = transform.position + moveBy;
            //m_syncPosRequest.DefaultRequest();
        }
    }

    //创建新加入的客户端
    public void OnSyncPlayerResopnse(List<string> usernameList)
    {
        //创建其他客户端player并设置对应属性
        foreach (string username in usernameList)
        {
            GameObject obj = GameObject.Instantiate(m_perfabPlayer);
            m_playerDic.Add(username, obj);
        }
    }

    //同步新加入的客户端
    public void OnNewPlayerEvent(string username)
    {
        GameObject obj = GameObject.Instantiate(m_perfabPlayer);
        m_playerDic.Add(username, obj);
    }

    //同步所有客户端的信息
    public void OnSyncPlayersDataEvent(List<PlayerData> userDataList)
    {
        foreach (PlayerData playerData in userDataList)
        {
            string username = playerData.Username;
            Vector3Data pos = playerData.Pos;
            GameObject player = DictTool.GetValue<string, GameObject>(m_playerDic, username);
            if (player != null)
            {
                player.transform.position = new Vector3(pos.x, pos.y, pos.z);

            }
        }
    }
}

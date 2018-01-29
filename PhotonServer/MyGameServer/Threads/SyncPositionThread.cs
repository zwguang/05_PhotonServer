using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;
using System.Xml.Serialization;
using System.IO;
using MyGameServer.Tools;

namespace MyGameServer.Threads
{
    class SyncPositionThread
    {
        private Thread thread;

        //启动线程
        public void Run()
        {
            thread = new Thread(UpdatePosition);
            thread.IsBackground = true;
            thread.Start();
        }

        //关闭线程
        public void Stop()
        {
            thread.Abort();
        }

        //更新坐標
        private void UpdatePosition()
        {
            Thread.Sleep(5000);
            while (true)
            {
                Thread.Sleep(200);
                SendPosition();
            }
        }

        //
        private void SendPosition()
        {
            //统计所有player数据
            List<PlayerData> playerDataList = new List<PlayerData>();
            foreach (MyClientPeer peerTemp in MyGameServer.Instance.m_peerList)
            {
                if (!String.IsNullOrEmpty(peerTemp.m_userName))
                {
                    PlayerData playerData = new PlayerData();
                    playerData.Username = peerTemp.m_userName;
                    playerData.Pos = new Vector3Data() { x = peerTemp.x, y = peerTemp.y, z = peerTemp.z };
                    playerDataList.Add(playerData);
                }
            }

            //序列化传输对象
            string playerDataListString = XML.Serializer<List<PlayerData>>(playerDataList);
            Dictionary<byte, Object> data2 = new Dictionary<byte, object>();
            data2.Add((byte)ParameterCode.UserDataList, playerDataListString);


            //发送数据
            foreach (MyClientPeer peerTemp in MyGameServer.Instance.m_peerList)
            {
                if (!String.IsNullOrEmpty(peerTemp.m_userName))
                {
                    EventData ed = new EventData((byte)EventCode.SyncPosition);
                    ed.Parameters = data2;
                    peerTemp.SendEvent(ed, new SendParameters());
                }
            }
        }
    }
}

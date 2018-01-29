using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MyGameServer.Manager;
using Common;
using MyGameServer.Handler;
using MyGameServer.Threads;
namespace MyGameServer
{
    //所有的server端 主类都要继承自
    public class MyGameServer : ApplicationBase
    {
        public static readonly ILogger m_log = LogManager.GetCurrentClassLogger();

        public static MyGameServer Instance
        {
            get;
            private set;
        }
        //存储所有连接的客户端
        public List<MyClientPeer> m_peerList = new List<MyClientPeer>();
        //字典获取对应请求
        public Dictionary<OperationCode, BaseHandler> m_handlerDic = new Dictionary<OperationCode, BaseHandler>();
        private SyncPositionThread m_syncPositionThread = new SyncPositionThread();



        //初始化
        protected override void Setup()
        {
            Instance = this;
            //日志的初始化
            //日志输出目录
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "bin_Win64\\log"); //ApplicationRootPath 为deploy目录下
            //解析配置文件
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//让photon知道
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
            }
            InitHandler();
            m_syncPositionThread.Run();
            m_log.Info("log init complate");
        }

        //当一个客户端请求连接的时候 创建一个peer对象
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            m_log.Info("一个客户端请求连接");
            MyClientPeer peer = new MyClientPeer(initRequest);
            m_peerList.Add(peer);
            return peer;
        }

        //server端关闭的时候
        protected override void TearDown()
        {
            m_syncPositionThread.Stop();
            m_log.Info("MyGame1应用关闭");
        }

        //将所有请求放入请求字典
        private void InitHandler()
        {
            LoginHandler loginHandler = new LoginHandler();
            m_handlerDic.Add(loginHandler.m_opCode, loginHandler);
            DefaultHandler defaultHandler = new DefaultHandler();
            m_handlerDic.Add(defaultHandler.m_opCode, defaultHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            m_handlerDic.Add(registerHandler.m_opCode, registerHandler);
            SyncPositionHandler syncHandler = new SyncPositionHandler();
            m_handlerDic.Add(syncHandler.m_opCode, syncHandler);

            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            m_handlerDic.Add(syncPlayerHandler.m_opCode, syncPlayerHandler);
        }
    }
}

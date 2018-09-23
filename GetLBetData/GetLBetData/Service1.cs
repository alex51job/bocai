using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GetLBetData
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            timer.Interval = 5000;//每5秒执行一次
            timer.Enabled = true;

        }

        private void TimedEvent(object sender, ElapsedEventArgs e)
        {
            MatchDataSync Main = new MatchDataSync();
            string data = Main.GetMatchData();
            this.WriteLog(data);
        }

        protected override void OnStart(string[] args)
        {
            this.WriteLog("\n当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "\n");
            this.WriteLog("足球赔率同步服务：【服务启动】");
        }

        protected override void OnStop()
        {
            this.WriteLog("\n当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "\n");
            this.WriteLog("足球赔率同步服务：【服务停止】");

        }

        protected override void OnShutdown()
        {
            this.WriteLog("\n当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "\n");
            this.WriteLog("足球赔率同步服务：【计算机关闭】");
        }

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {

            //string path = @"C:\log.txt";

            //该日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "   " + msg);
                }
            }
        }
        #endregion
    }
}

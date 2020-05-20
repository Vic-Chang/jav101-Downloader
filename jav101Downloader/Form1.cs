using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.Net.Http;

namespace Jav101Downloader
{
    public partial class Form1 : Form
    {

        /*
         * 功能:
         * 用於下載Jav101之m3u8(影片串流)影片格式。(免費影片，付費有加密)
         * 
         * 實際做法:
         * 輸入網址後，爬網址的內容。內有一段Json字串，解析後可得當下的m3u8檔案。
         * 將檔案網址輸入至 ffmpeg.exe 內進行轉檔、輸出
         */

        /*
         * 付費
         * https://v.jav101.com/play/avid59648a11eca3b
         * 免費
         * https://v.jav101.com/play/avid595f4ee3b2bc1
         */

        readonly string JsonKeySign_start = "var data =";
        readonly string JsonKeySign_end = "}]};";
        string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private BackgroundWorker bw;

        public Form1()
        {
            InitializeComponent();


            //多執行緒與UI可以互動，免使用BackgroundWorker
            Form.CheckForIllegalCrossThreadCalls = false;

            tb_path.ForeColor = Color.Gray;
            tb_path.BackColor = tb_path.BackColor;
            lb_version.Text = "Ver: " + Application.ProductVersion;

            if (NetworkInterface.GetIsNetworkAvailable() == false)
            {
                btn_download.Enabled = false;
                MessageBox.Show("請開啟網路後進行下載", "無網路", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btn_save_path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "請選擇影片下載儲存位置:";
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                SavePath = fbd.SelectedPath;
                tb_path.Text = fbd.SelectedPath;
            }
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            if (tb_url.Text.Contains("v.jav101.com"))
            {
                bw = new BackgroundWorker(); //一定要先初始化，否則會fire多次
                bw.WorkerReportsProgress = true; //是否可回傳控制ProgressBar
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged); //後面可靠bw.ReportProgress回傳資料於此function
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted); //完成後執行該function
                bw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("目前僅提供「啪啪啪研習所(jav101)」下載"
                    + Environment.NewLine + " "
                    + Environment.NewLine + "網址範例:"
                    + Environment.NewLine + "https://v.jav101.com/play/avid592f782546ada", "網址錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lb_version_Click(object sender, EventArgs e)
        {
            Auther moreForm = new Auther();
            moreForm.ShowDialog();
        }

        #region ■ 按下下載確認之BackGroundWork 線程 ■

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            btn_download.Enabled = false;

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8; // 設定Webclient.Encoding
            wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string WebHtmlSource = wc.DownloadString(tb_url.Text);
            try
            {
                #region ■ 解析html Json位置 ■
                int indexJsonStringStart = WebHtmlSource.IndexOf(JsonKeySign_start) + JsonKeySign_start.Length;
                string JsonString = WebHtmlSource.Substring(indexJsonStringStart);
                int indexJsonStringEnd = JsonString.IndexOf(JsonKeySign_end) + JsonKeySign_end.Length - 1; //-1 是因為 ; 號
                JsonString = JsonString.Substring(0, indexJsonStringEnd);
                #endregion

                JObject json = JObject.Parse(JsonString);
                bool isFree = json["video"]["isfree"].ToString() == "1" ? true : false;
                if (isFree != false)
                {
                    string videoTitle = RemoveIllegalPathCharacters(json["video"]["name"].ToString()); //標題
                    string videoUrl = json["video"]["specurl"]["mp4"].ToString(); //影片網址
                    string videoImg = json["video"]["thumb"].ToString(); //影片縮圖
                    string videoLenth = json["video"]["length"].ToString(); //影片長度
                    CallFFmpeg(videoUrl, videoTitle, timeToTike(videoLenth));
                }
                else
                {
                    //停止運行該線程
                    bw.CancelAsync();
                    //設定為停止 bw_RunWorkerCompleted 處理用
                    e.Cancel = true;
                    MessageBox.Show("很抱歉，付費影片不可下載。", "僅供下載免費觀看", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                //停止運行該線程
                bw.CancelAsync();
                //設定為停止 bw_RunWorkerCompleted 處理用
                e.Cancel = true;
                MessageBox.Show("錯誤"
                    + Environment.NewLine
                    + "原因: " + ex.Message, "發生未知錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                MessageBox.Show("下載取消", "已取消下載", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (!(e.Error == null))
            {
                MessageBox.Show("運行錯誤 " + Environment.NewLine + "原因: " + e.Error.Message, "運行錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                MessageBox.Show("下載完成!");
            }

            btn_download.Enabled = true;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            lb_progress.Text = e.ProgressPercentage.ToString() + " %";
        }

        #endregion

        #region ■ FFmpeg處理 ■

        public void CallFFmpeg(string videoUrl, string saveName, double _VideoLenth)
        {
            //FFMPEG 路徑
            string FFMPEG_PATH = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ffmpeg.exe");
            string _savePath = Path.Combine(SavePath, saveName + ".mp4");
            //準備轉檔
            string strParam = " -i \"" + videoUrl + "\" -vcodec copy -bsf:a aac_adtstoasc -copyts -y \"" + _savePath + "\"";
            FFmpegProcess(FFMPEG_PATH, strParam, _VideoLenth);
        }

        /// <summary>
        /// FFmpeg 執行序作業
        /// </summary>
        /// <param name="Path_FFMPEG">FFmpeg完整路徑</param>
        /// <param name="strParam">Command Line參數</param>
        /// <param name="_VideoLenth">FFmpeg該影片長度(用於計算%數)</param>
        public void FFmpegProcess(string Path_FFMPEG, string strParam, double _VideoLenth)
        {
            try
            {
                //開啟一線程
                Process ffmpeg = new Process();

                ffmpeg.StartInfo.UseShellExecute = false;
                ffmpeg.StartInfo.FileName = Path_FFMPEG;
                ffmpeg.StartInfo.Arguments = strParam;

                //把外部程序錯誤輸出寫到StandardError流中
                //PS. FFMPEG 所有的輸出都是 "ErrorData(錯誤輸出流)" ,用StandardOutput抓不到任何訊息
                ffmpeg.StartInfo.RedirectStandardError = true;
                ffmpeg.StartInfo.CreateNoWindow = true;//不創建窗口

                //將 FFMPEG 產生的事件輸出至方法內
                //ffmpeg.ErrorDataReceived += new DataReceivedEventHandler(Output);
                ffmpeg.ErrorDataReceived +=
                    delegate (object sender, DataReceivedEventArgs e)
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            if (e.Data.Contains("bitrate="))
                            {
                                /*
                                 * Exmaple: frame=  129 fps=0.0 q=-1.0 size=     768kB time=00:00:05.37 bitrate=1170.4kbits/s speed=10.3x    
                                */

                                //取得目前處理時間
                                string ProcessTime = e.Data.Substring(e.Data.IndexOf("time=") + 5);
                                ProcessTime = ProcessTime.Substring(0, ProcessTime.IndexOf(" "));

                                //回傳Progress時間進度
                                bw.ReportProgress((int)((timeToTike(ProcessTime) / _VideoLenth) * 100));

                                //取得bit rate
                                string ProcessBitrate = e.Data.Substring(e.Data.IndexOf("bitrate=") + 8);
                                ProcessBitrate = ProcessBitrate.Substring(0, ProcessBitrate.IndexOf(" speed")).Replace("kbits/s", "kb/s");
                                lb_bitrate.Text = ProcessBitrate;
                            }

                            //完成
                            if (e.Data.Contains("muxing overhead"))
                            {
                                bw.ReportProgress(100);
                                lb_bitrate.Text = "0 kb/s";
                            }
                        }
                    };

                ffmpeg.Start();//啟動線程
                ffmpeg.BeginErrorReadLine();//開始異步讀取
                ffmpeg.WaitForExit();//阻塞等待進程結束
                ffmpeg.Close();//關閉進程
                ffmpeg.Dispose();//釋放資源
            }
            catch (Exception ex)
            {
                MessageBox.Show("下載錯誤 " + Environment.NewLine + "原因: " + ex.Message, "轉檔錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 將時間長度轉為秒數
        /// </summary>
        /// <param name="_time">時間 00:01:20 </param>
        /// <returns>秒數 => 80</returns>
        private double timeToTike(string _time)
        {
            return TimeSpan.Parse(_time).TotalSeconds;
        }

        #endregion

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
        
        #region 無用到
        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!String.IsNullOrEmpty(output.Data))
            {
                if (output.Data.Contains("bitrate="))
                {
                    //bw.ReportProgress((i * 10));
                    //textBox3.Text += output.Data.ToString() + Environment.NewLine + Environment.NewLine;
                }
            }
        }
        #endregion
    }
}

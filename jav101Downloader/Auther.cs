using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jav101Downloader
{
    public partial class Auther : Form
    {
        public Auther()
        {
            InitializeComponent();

            #region■ 抓取Facebook資料 ■
            try
            {
                WebClient wc = new WebClient();

                //取得大頭照
                var PROFILE_ID = "YOUR_PROFILE_ID";
                string json_string = wc.DownloadString("http://graph.facebook.com/v2.9/" + PROFILE_ID + "/ picture?redirect=false&type=large");
                JObject json_pic = JObject.Parse(json_string);
                string FacebookPic = json_pic["data"]["url"].ToString();

                //取得Token (APPID)
                var APPID = "YOUR_APP_ID";
                json_string = wc.DownloadString("https://graph.facebook.com/oauth/access_token?client_id=" + APPID + "&client_secret=efb172b6ae4ea3ee7409492bc5731766&grant_type=client_credentials");
                JObject json_FBToken = JObject.Parse(json_string);
                string FBToken = json_FBToken["access_token"].ToString();

                //利用Token取得個人資訊
                json_string = wc.DownloadString(string.Format("https://graph.facebook.com/v2.9/100007792234467?fields=name&access_token={0}", FBToken));
                JObject json_name = JObject.Parse(json_string);
                string FacebookName = json_name["name"].ToString();

                pb_fbpic.Load(FacebookPic);
                pb_fbpic.SizeMode = PictureBoxSizeMode.StretchImage;
                lb_fbname.Text = FacebookName;
            }
            catch
            {
                lb_fbname.Text = "Vic";
            }

            pictureBox_fb_title_image.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_fb_title_image.Image = Properties.Resources.facebook;
            lb_version.Text = Application.ProductVersion;
            #endregion

            #region■ 加上點擊開啟FB ■

            pictureBox_fb_title_image.Click += facebook_open;
            pictureBox_fb_title_image.Cursor = Cursors.Hand;
            pb_fbpic.Click += facebook_open;
            pb_fbpic.Cursor = Cursors.Hand;
            lb_fbname.Click += facebook_open;
            lb_fbname.Cursor = Cursors.Hand;
            panel_facebook.Click += facebook_open;

            #endregion

        }


        private void facebook_open(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/YOUR_PROFILE");
        }
    }
}

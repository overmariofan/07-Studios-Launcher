using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace _07_Studios_Launcher
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
        }

        WebClient client;

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Launcher_MinimumSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "https://simplefileeditor.netlify.com/download/Simple(File)EditorUpdate.exe";
            if (!string.IsNullOrEmpty(url))
            {
                Thread thread = new Thread(() =>
                {
                    Uri uri = new Uri(url);
                    string fileName = System.IO.Path.GetFileName(uri.AbsolutePath);
                    Directory.CreateDirectory("downloads/Simple(File)Editor");
                    client.DownloadFileAsync(uri,Application.StartupPath+"/downloads/Simple(File)Editor/"+fileName);
                });
                thread.Start();
            }
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            { 
                MessageBox.Show("Download complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception)
            {
                MessageBox.Show("Download failed!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double receive = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = receive / total * 100;
                label11.Text = $"Downloaded {string.Format("{0:0.##}",percentage)}%";
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("downloads\\Simple(File)Editor\\Simple(File)EditorUpdate.exe");
            }
            catch
            {
                MessageBox.Show("Simple(File)Editor is not downloaded.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Simple(File)Editor?","Sure?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    File.Delete("downloads\\Simple(File)Editor\\Simple(File)EditorUpdate.exe");
                    Directory.Delete("downloads\\Simple(File)Editor");
                }
                catch
                {
                    MessageBox.Show("Simple(File)Editor is not downloaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

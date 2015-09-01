using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImLost.Launcher
{
    public partial class LaunchForm : Form
    {
        private Dictionary<string, string> cliParameters;

        public LaunchForm()
        {
            InitializeComponent();

            cliParameters = new Dictionary<string, string>();
            cliParameters.Add("sound", "true");
            cliParameters.Add("music", "true");
            cliParameters.Add("speech", "false");
            cliParameters.Add("width", "1280");
            cliParameters.Add("height", "720");
            cliParameters.Add("fullscreen", "false");
            cliParameters.Add("detect", "true");
            cliParameters.Add("lang", "fr-FR");
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == "playButton")
            {
                LaunchGame();
            }
            else
            {
                enabledMusic.Checked = true;
                enabledSound.Checked = true;
                enabledSpeech.Checked = false;

                detectBestResolution.Checked = true;

                screenWidth.Text = "1280";
                screenWidth.Enabled = false;

                screenHeight.Text = "720";
                screenHeight.Enabled = false;

                isFullScreen.Checked = false;
                isFullScreen.Enabled = false;
            }
        }

        private void LaunchGame()
        {
            bool isValid = true;

            if (!detectBestResolution.Checked)
            {
                int width = int.Parse(screenWidth.Text);
                int height = int.Parse(screenHeight.Text);

                if (width < 640 || width > 1920 || height < 480 || height > 1080)
                {
                    MessageBox.Show("Résolution invalide, les résolutions autorisées sont\nMinimum: 640x480\nMaximum: 1920x1080", "Problème de configuration", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    isValid = false;
                }
            }

            if (isValid)
            {
                string directory = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Game" + Path.DirectorySeparatorChar;

                if (!Directory.Exists(directory))
                    directory = Environment.CurrentDirectory + Path.DirectorySeparatorChar;

                string cmdParams = "";

                foreach (KeyValuePair<string, string> keyValue in cliParameters)
                    cmdParams += String.Format(" {0}={1}", keyValue.Key.ToString(), keyValue.Value.ToString());

                Process process = new Process();
                process.StartInfo.Arguments = cmdParams;
                process.StartInfo.WorkingDirectory = directory;
                process.StartInfo.FileName = "Imlost.exe";
                process.Start();
            }
        }

        private void audioCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            switch (checkbox.Name)
            {
                case "enabledSound": cliParameters["sound"] = checkbox.Checked.ToString(); break;
                case "enabledMusic": cliParameters["music"] = checkbox.Checked.ToString(); break;
                case "enabledSpeech": cliParameters["speech"] = checkbox.Checked.ToString(); break;
            }
        }

        private void detectBestResolution_CheckedChanged(object sender, EventArgs e)
        {
            cliParameters["detect"] = detectBestResolution.Checked.ToString();

            screenWidth.Enabled = !detectBestResolution.Checked;
            screenHeight.Enabled = !detectBestResolution.Checked;
            isFullScreen.Enabled = !detectBestResolution.Checked;
        }

        private void isFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            cliParameters["fullscreen"] = isFullScreen.Checked.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}

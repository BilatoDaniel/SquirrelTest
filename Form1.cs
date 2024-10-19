using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Squirrel;

namespace SquirrelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddVersionNumber();
            CheckForUpdate();
        }

        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                this.Text += $" v.{versionInfo.FileVersion}";
            });
        }

        private async void CheckForUpdate()
        {
            try
            {
                using (var manager = await UpdateManager.GitHubUpdateManager("https://github.com/BilatoDaniel/SquirrelTest"))
                {
                    var release = await manager.UpdateApp();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Update fallito: " + ex.ToString());
            }
        }
    }
}

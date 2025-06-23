using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string PakLocation;
        public string MvciLocation;
        public string batLocation;
        public int workflow = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void openPak_FileOk(object sender, CancelEventArgs e)
        {
            switch (workflow++)
            {
                case 0:
                    this.PakLocation = this.openPak.FileName;
                    this.button1.Text = "Select MVCI Location";
                    break;
            }
        }

        private void MakeScript()
        {
            string script = $@"
                                del ""{MvciLocation}\MVCI\Content\Paks\~mods\lk*"" 
                                del ""{MvciLocation}\MVCI\Content\Paks\~mods\*ZZ*Balance*"" 
                                
                                copy ""{PakLocation}"" ""{MvciLocation}\MVCI\Content\Paks\~mods\""
                                ""{MvciLocation}\MVCI\Binaries\Win64\MVCI.exe""
                                pause
    ";
            string fileName = new FileInfo(this.openPak.FileName).Name;
            File.AppendAllText(batLocation + "/" + fileName + ".bat", script);
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            switch (workflow)
            {
                case 0:
                    this.openPak.ShowDialog();
                    break;
                case 1:
                    if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\MARVEL VS. CAPCOM INFINITE"))
                    {
                        this.folderBrowserDialog1.SelectedPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\MARVEL VS. CAPCOM INFINITE";
                    }
                   
                    DialogResult result = this.folderBrowserDialog1.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        this.MvciLocation = this.folderBrowserDialog1.SelectedPath;
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        this.folderBrowserDialog1.SelectedPath = desktopPath;
                        this.button1.Text = "Select Shortcut Location";
                        workflow++;
                    }
                    break;
                case 2:
                    DialogResult result2 = this.folderBrowserDialog1.ShowDialog();

                    if (result2 == DialogResult.OK)
                    {
                        this.batLocation = this.folderBrowserDialog1.SelectedPath;
                        this.button1.Text = "Create Shortcut & exit";
                        workflow++;
                    }
                    break;
                case 3:
                    MakeScript();
                    this.Dispose();
                    break;
            }
            
        }
    }
}

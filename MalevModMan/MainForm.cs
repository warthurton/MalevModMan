using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml.XPath;
//use on file
using System.Threading.Tasks;
using AutoUpdaterDotNET;

namespace Malevolence_Mod_Manager
{
    public partial class MainForm : Form
    {
        private List<ModLoader> _mods;

        private List<ModLoader> _zippedMods;

        public MainForm()
        {
            InitializeComponent();
            
            //autoupdate
            AutoUpdater.Start("http://enemygiant.net/Malevolence%20Mod%20Manager/Update/Malevolence%20Mod%20Manager.xml");

            validateGameDir();

            ModDirToolText.Text = App.Default.modDirectory;
            //load mod manager info

            LoadMods();
            displayInfoHTML();
        }

        private void validateGameDir()
        {
            string message = "Please select your Malevolence directory.";
            int tries = 0;
            //Find the Game version.dat in order to validate the game dir
            while (!File.Exists(App.Default.MSOADirectory + Path.DirectorySeparatorChar + "VERSION.DAT"))
            {
                string temp = GetGameDir(tries);
                //fix the bug where this always shows up!
                if (temp != null)
                {
                    message = temp;
                    tries++;
                    MessageBox.Show(this, message);
                }

            }
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            if (LoadMod() != null)
            {
                ;//MessageBox.Show("Mods Loaded");
            }
            //Process.Start(App.Default.MSOADirectory + @"\MSoA.exe");
            else
            {
                MessageBox.Show("Could not load mods");
            }
        }

        private void UpdateModDirectory(string path)
        {
            App.Default.modDirectory = path;
            App.Default.Save();

            ConfigurationManager.RefreshSection("appSettings");

            LoadMods();
        }

        private void LoadMods()
        {
            string modDir = App.Default.modDirectory;
            
            if (!Directory.Exists(App.Default.modDirectory)) return;

            GameVersion.DoVersionCheck();

            ManageVanilla(modDir);

            //find Mods
            //var mods = new DirectoryInfo(App.Default.modDirectory).GetDirectories();
            var mods = new DirectoryInfo(App.Default.modDirectory).GetFiles();
            //Find all Zipped mods
            var userMods = new DirectoryInfo(App.Default.modDirectory).GetFiles();
            var vanillaZip = new DirectoryInfo(App.Default.modDirectory + Path.DirectorySeparatorChar + "Archive").GetFiles();
            
            //add the vanilla zip
            List<FileInfo> zippedMods = new List<FileInfo>();
            zippedMods.AddRange(userMods);
            zippedMods.AddRange(vanillaZip);
            
            InstalledMods.Items.Clear();

            _mods = new List<ModLoader>();

            _zippedMods = new List<ModLoader>();

            //get the saved applied mod list
            List<string> modList = new List<string>();
            modList.AddRange(App.Default.ModList.Split(','));

            //new zipped mods
            foreach (FileInfo mod in zippedMods)
            {
                //test to see if it's a ZIP archive
                if (mod.Extension == ".zip")
                {
                    ModLoader zipedMod = new ModLoader(mod.Name);
                    ListViewItem item = new ListViewItem();

                    string shortName = Path.GetFileNameWithoutExtension(mod.Name);
                    string zipFileName = mod.Name;

                    item.Text = zipedMod.name;
                    item.Name = zipedMod.name;
                    if (modList.Contains(zipedMod.name))
                    {
                        item.Checked = true;
                    }

                    item.SubItems.Add(zipedMod.version);
                    item.SubItems.Add(zipedMod.author);

                    InstalledMods.Items.Add(item);

                    _zippedMods.Add(zipedMod);
                }
                else
                {
                    return;
                }
                
            }

        }

        private void ManageVanilla(string modDir)
        {
            string ArchivePath = modDir + @"\Archive";
            if (!Directory.Exists(ArchivePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(ArchivePath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                //don't bother hidding sub folders
                Directory.CreateDirectory(ArchivePath + Path.DirectorySeparatorChar + "Vanilla");
                Directory.CreateDirectory(ArchivePath + Path.DirectorySeparatorChar + "Vanilla" + Path.DirectorySeparatorChar + "Info");
            }
            //we need to extract the embedded header PNG file
            Properties.Resources.MSOAlogo.Save(modDir + @"\Archive\Vanilla\Info\MSOAlogo.png");

            //Create the stock Html doc this is dynamic to include the current version number
            if (!File.Exists(modDir + @"\Archive\Vanilla\Info\info.html"))
            {
                using (StreamWriter htmlWrite = File.CreateText(modDir + @"\Archive\Vanilla\Info\info.html"))
                {
                    htmlWrite.Write("<html>\n" +
                    "	<STYLE TYPE=\"text/css\"><!--BODY{color:white;background-color: #272822;font-family:arial;} A:link{color:white} A:visited{color:orange}--></STYLE>\n" +
                    "		<body>\n" +
                    "		<img src=\"MSOAlogo.png\" height=\"190\" width=\"350\">\n" +
                    "		<p><strong><span style=\"color:#ffa500;\">Malevolence: The Sword of Ahkranox</span></strong><br>\n" +
                    "		<span style=\"font-size:10px;\"><version>" + App.Default.GameVersion + "</version></span><br>\n" +
                    "		<span style=\"font-size:10px;\">Author: <author>Visual Outbreak</author></span></p>\n" +
                    "		<p><strong><span style=\"color:#ff0000;\">This is not a Mod!</span></strong> This is the stock game select this to restore your game to the Vanilla (un-moded) version of the game.</p>\n" +
                    "	</body>\n" +
                    "</html>");
                }
            }
        }

        //manage updates to progress
        public void UpdateProgress(int ProgressPercentage)
        {
            //ApplyProgressBar.Value = ProgressPercentage;
            ApplyProgressBar.Increment(ProgressPercentage);
            Console.WriteLine("Progress: " + ApplyProgressBar.Value);

            if (ApplyProgressBar.Value == ApplyProgressBar.Maximum)
            {
                Console.WriteLine("Progress: Done!");
                ApplyProgressBar.Visible = false;
            }

            return;
        }

        public bool getProgressComplete()
        {
            bool progUpdateDone = false;

            if (ApplyProgressBar.Value == ApplyProgressBar.Maximum)
            {
                progUpdateDone = true;
                Console.WriteLine("Progress: Done!");
            }
            return progUpdateDone;
        }

        void ProcessFinished()
        {
            ApplyProgressBar.Visible = false;
            return;
        }

        private bool LoadMod()
        {
            //first get the total file count using mod.files loop

            ListView.CheckedListViewItemCollection selectedMods = InstalledMods.CheckedItems;

            //reset the applied mods list
            App.Default.ModList = null;

            int fileCount = 0;

            ModLoaders loader = new ModLoaders();
            foreach (ModLoader mod in _zippedMods)
            {
                //find file count for progress bar testing selected mods
                if (selectedMods.ContainsKey(mod.name))
                {
                    fileCount = fileCount + mod.zipFiles.Count;
                }

            }

            //set the progress bar total
            ApplyProgressBar.Maximum = fileCount;
            //reset the progress bar
            ApplyProgressBar.Value = 0;
            //ApplyProgressBar.Show();

            foreach (ModLoader mod in _zippedMods)
            {
                //App.Default.ModList.Add(mod.name);
                if (selectedMods.ContainsKey(mod.name))
                {
                    loader.Add(mod);
                    if (App.Default.ModList == null)
                    {
                        App.Default.ModList += mod.name;
                    }
                    else
                    {
                        App.Default.ModList += "," + mod.name;
                    }
                    //subscribe to the progress update
                    mod.ProgressChanged += UpdateProgress;
                    mod.ProcessDone += ProcessFinished;

                }
            }

            App.Default.Save();
            //return whether or not loader was successful
            if (selectedMods.Count == 1 && selectedMods.ContainsKey("Vanilla"))
            {
                return loader.BeginRestore();
            }
            else
            {
                return loader.BeginImport();
            }
            
            
        }

        public static void ImportMod(string zipLocation)
        {
            string modDir = App.Default.modDirectory;

            //Assumes files are in the root of the zip file
            FileInfo fileInf = new FileInfo(zipLocation);
            modDir += @"\"+fileInf.Name.Substring(0,fileInf.Name.IndexOf(".zip", StringComparison.OrdinalIgnoreCase));
            ZipFile.ExtractToDirectory(zipLocation, modDir);

            //Check if we got any files
            FileInfo fInf = new FileInfo(modDir);
            if(Directory.GetFiles(modDir).Length < 1){
                //Nope, no files so presumably there was a zipped folder in the archive.
                //Undo and extract the folder directly.
                Directory.Delete(modDir,true);
                ZipArchive archive = ZipFile.Open(zipLocation, ZipArchiveMode.Read);
                archive.ExtractToDirectory(App.Default.modDirectory);
            }
            
        }

        private string GetGameDir(int tries)
        {


            FolderBrowserDialog foDi = new FolderBrowserDialog();

            DialogResult result = foDi.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selected = foDi.SelectedPath;

                //Find the Game version.dat in order to validate the game dir
                if (!File.Exists(selected + Path.DirectorySeparatorChar + "VERSION.DAT"))
                {
                    GetMessageBoxMessage(tries);
                }
                App.Default.MSOADirectory = selected;

                App.Default.Save();
            }
            else
            {
                MessageBox.Show(this,
                                "You will not be able to use the Malevolence Mod Manager\n" +
                                "until you select a valid directory.");
                Environment.Exit(0);
            }
            return null;
        }

        public static string GetMessageBoxMessage(int tries)
        {
            if (tries < ErrorMessages.messages.Length)
                return ErrorMessages.messages[tries];
            return "";
        }

        private void setModFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GameVersion.DoVersionCheck())
            {
                MessageBox.Show(this,
                                "Please set a valid Game directory first");
                Environment.Exit(0);
            }
            else
            {
                FolderBrowserDialog foDi = new FolderBrowserDialog();
                DialogResult result = foDi.ShowDialog();

                if (result != DialogResult.OK) return;

                UpdateModDirectory(foDi.SelectedPath);
                ModDirToolText.Text = foDi.SelectedPath;
            }
        }

        private void installNewModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open mod folder
            Process.Start(App.Default.modDirectory);
            /*
            OpenFileDialog fiDi = new OpenFileDialog();

            fiDi.Filter = @"Zip Files (.zip)|*.zip|All Files (*.*)|*.*";
            fiDi.FilterIndex = 1;

            fiDi.Multiselect = true;

            DialogResult result = fiDi.ShowDialog();

            if (result == DialogResult.OK)
                foreach (string file in fiDi.FileNames)
                    ImportMod(file);
            */

            LoadMods();
        }

        private void InstalledMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedMod = InstalledMods.SelectedItems;

            ModLoaders loader = new ModLoaders();
            foreach (ModLoader mod in _zippedMods)
            {
                if (selectedMod.ContainsKey(mod.name))
                {
                    htmlDescription.Navigate(mod.htmlinfo);
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                App.Default.AppWindowSize = this.Size;
                App.Default.Save();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (App.Default.AppWindowSize.Width != 0 && App.Default.AppWindowSize.Height != 0)
            {
                this.Size = App.Default.AppWindowSize;
            }
        }

        private void displayInfoHTML()
        {
            string htmlPage = generateMainPage();

            htmlDescription.DocumentText = htmlPage;
        }

        private string generateMainPage()
        {
            //get stats
            int moddedFiles = App.Default.statFiles;

            //get the saved applied mod list
            List<string> modList = new List<string>();
            modList.AddRange(App.Default.ModList.Split(','));

            int modsIntalled = modList.Count;
            //check to see if it's just the Vanilla
            if (modsIntalled == 1 && modList[0] == "Vanilla")
            {
                modsIntalled = 0;
            }

            string appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string value = ("<html>\n" +
                    "	<STYLE TYPE=\"text/css\"><!--BODY{color:white;background-color: #272822;font-family:arial;} A:link{color:white} A:visited{color:orange}--></STYLE>\n" +
                    "		<body>\n" +
                    "		<p><strong><span style=\"color:#ffa500;\">Malevolence Mod Manager</span></strong><br>\n" +
                    "		<span style=\"font-size:10px;\"><version>" + appVersion + "</version></span><br>\n" +
                    "		<span style=\"font-size:10px;\">Author: <author>Kolgrima</author></span></p>\n" +
                    "		<p style=\"font-size:12px;\"><strong><span style=\"color:#ffd700;\">Stats</span></strong><br>\n" +
                    "		Mods Installed: " + modsIntalled + "<br>\n" +
                    "		Modded Files: " + moddedFiles + "</p>\n" +
                    "	</body>\n" +
                    "</html>");
            return value;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayInfoHTML();
        }

        private void setGameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //start game folder selection here
            GetGameDir(0);
            validateGameDir();
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using hap = HtmlAgilityPack;
using Ionic.Zip;

namespace Malevolence_Mod_Manager{
    class ModLoader
    {

        #region public variables
        
        public string name;
        public string modDir;
        public string zipPath;
        public string version;
        public string author;
        public Uri htmlinfo;

        public List<string> files;

        public List<string> zipFiles;
        

        #endregion

        #region Constructors
        
        public ModLoader(string modName)
        {
            //create Vanilla.zip if it dosen't exist
            if (modName == "Vanilla.zip")
            {
                //set the path
                string VanillaZipPath = App.Default.modDirectory + @"\Archive\Vanilla.zip";

                if (!File.Exists(VanillaZipPath))
                {
                    //Add first file and create the zip
                    using (ZipFile zip = new ZipFile())
                    {
                        //zip.AddFile(gamePath, zipFolder);
                        zip.Save(VanillaZipPath);
                    }
                }
                //set the special path for Vanilla.zip
                zipPath = App.Default.modDirectory + Path.DirectorySeparatorChar + "Archive" + Path.DirectorySeparatorChar + modName;
            }
            else
            {
                zipPath = App.Default.modDirectory + Path.DirectorySeparatorChar + modName;
            }

            name = Path.GetFileNameWithoutExtension(modName);
            modDir = App.Default.modDirectory + Path.DirectorySeparatorChar + name;
            modInfoDir = App.Default.modDirectory + Path.DirectorySeparatorChar + "Archive" + Path.DirectorySeparatorChar + name;

            //get mod files
            List<string> f = new List<string>();
            using (ZipFile zippedFile = ZipFile.Read(zipPath))
            {
                foreach (ZipEntry entry in zippedFile)
                {
                    string[] pathParts = entry.FileName.Split('/');
                    //convert case and test
                    if (pathParts[0].ToLower() != "info")
                    {
                        string tempName = entry.FileName;
                        f.Add(tempName);
                    }
                }
            }
            zipFiles = f;
            ManageModInfo();
            LoadModInfo();
        }

        #endregion


        #region public methods

        public event Action<int> ProgressChanged;

        private void OnProgressChanged(int progress)
        {
            var increment = ProgressChanged;
            if (increment != null)
            {
                increment(progress);
            }
        }

        public event Action ProcessDone;

        private void OnProgressDone()
        {
            var endProgress = ProcessDone;
            if (endProgress != null)
            {
                endProgress();
            }
        }

        
        public bool AreCompatible(ModLoader otherMod)
        {
            //REWRITE Are you sure you want this functionality?
            //concider load priority

            //This could be linq, but that's hard to read so I left it like this.
            foreach (string fi in otherMod.zipFiles)
                if (FileExists(fi))
                    return false;

            return true;
        }

        public void MigrateFromGameToMod(ModLoader mod)
        {
            foreach (string file in zipFiles)
            {
                mod.ExtractFromGame(file);
            }
        }

        public void MigrateFromGame()
        {
            foreach (string file in zipFiles)
            {
                ExtractFromGame(file);
            }
        }

        public void MigrateToGame()
        {
            //foreach (string file in files)
            //{
                MovetoGame();

                if (name == "Vanilla")
                {
                    RemoveFromArchive();
                }
            //}
        }

        public void MovetoGame()
        {
            //rewirte for zip files
            try
            {
                //string absPath = ToAbsolutePath(path);
                //FileInfo file = new FileInfo(absPath);
                using (ZipFile zippedFile = ZipFile.Read(zipPath))
                {
                    foreach (ZipEntry entry in zippedFile)
                    {
                        if (zipFiles.Contains(entry.FileName))
                        {
                            //if (!Directory.Exists(gameDir))
                                //Directory.CreateDirectory(gameDir);

                            string gamePath = ToGamePath(entry.FileName);
                            if (File.Exists(gamePath))
                            {
                                //File.Delete(gamePath); not needed
                                //then unpack it to the game
                                entry.Extract(App.Default.MSOADirectory, ExtractExistingFileAction.OverwriteSilently);

                                //add file to the stats for fun but don't count Vanilla
                                if (name != "Vanilla")
                                {
                                    App.Default.statFiles += 1;
                                }
                                
                            }

                            OnProgressChanged(1);
                        }
                    }
                }

            }
            catch (IOException ex)
            {
                DialogResult result = MessageBox.Show(
                    "Couldn't Import file. \nMake sure the file isn't in use in both the mod directory, and the Malevolence directory.\nAlso, Make sure the mod loader is being run as administrator.\nRetry?", "Warning",
                    MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);

                if (result == DialogResult.Retry)
                    MovetoGame();
                else
                {
                    MessageBox.Show("Not all mod files transferred succesfully.\n" +
                                    "It is possible that your Malevolence installation is now corrupt.\n" +
                                    "Try running the game as Vanilla to fix any problems.\n" +
                                    "If that doesn't work, run the Malevolence launcher and just verify your game files.  Malevolence will then re-download any corrupt or missing files.");

                    if (result == DialogResult.Abort)
                    {
                        Environment.Exit(0);
                    }
                }

            }
        }

        public List<string> GetConflictingFiles(ModLoader otherMod)
        {
            //return all files with the same name
            return otherMod.files.Where(FileExists).ToList();
        }

        public void ExtractFromGame(string path)
        {
            try
            {
                //string absPath = ToAbsolutePath(path);
                //FileInfo file = new FileInfo(absPath);

                string gamePath = ToGamePath(path);


                if (File.Exists(gamePath))
                {
                    //if (!Directory.Exists(file.DirectoryName))
                        //Directory.CreateDirectory(file.DirectoryName);

                    //if (!File.Exists(absPath))
                        //File.Copy(gamePath, absPath);

                    //pack zip backup
                    string zipPath = App.Default.modDirectory + @"\Archive\Vanilla.zip";

                    //get the list of entries for vanilla
                    List<string> vanillaEntries = new List<string>();

                    //strip the file name for adding to the zip
                    string[] pathParts = path.Split('/');
                    string zipFolder = "";
                    //make sure were not at root level
                    //TEST THIS!
                    if (pathParts.Length > 2)
                    {
                        zipFolder = pathParts[0];
                        foreach (string folder in pathParts)
                        {
                            zipFolder += "\\" + folder;
                        }
                    }
                    else
                    {
                        zipFolder = pathParts[0];
                    }

                    if (File.Exists(zipPath))
                    {
                        //Console.WriteLine("Path has " + path + " elements");
                        using (ZipFile zip = ZipFile.Read(zipPath))
                        {
                            //check if it has been added already
                            foreach (ZipEntry entry in zip)
                            {
                                vanillaEntries.Add(entry.FileName);
                            }
                            if (!vanillaEntries.Contains(path))
                            {
                                zip.AddFile(gamePath, zipFolder);
                                zip.Save();
                            }
                        }
                    }
                    else
                    {
                        //Add first file and create the zip
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AddFile(gamePath, zipFolder);
                            zip.Save(App.Default.modDirectory + @"\Archive\Vanilla.zip");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                DialogResult result = MessageBox.Show(
                    "Couldn't Extract file " + path +
                    ". \nMake sure the file isn't in use in both the mod directory, and the Malevolence directory.\nAlso, Make sure the mod loader is being run as administrator.\nRetry?", "Warning",
                    MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);

                if (result == DialogResult.Retry)
                    ExtractFromGame(path);
                else
                {
                    MessageBox.Show("Not all mod files transferred succesfully.\n" +
                                    "It is possible that your Malevolence installation is now corrupt.\n" +
                                    "Try running the game as Vanilla to fix any problems.\n" +
                                    "If that doesn't work, run the Malevolence launcher and just verify your game files.  Malevolence will then re-download any corrupt or missing files.");

                    if (result == DialogResult.Abort)
                    {
                        Environment.Exit(0);
                    }
                }

            }
        }

        #endregion

        #region private methods

        private void RemoveFromArchive()
        {
            //try to delete the zip file
            string zipPath = App.Default.modDirectory + @"\Archive\Vanilla.zip";
            if (File.Exists(zipPath))
            {
                //if it's there delete it
                File.Delete(zipPath);

                //re-create the zip
                using (ZipFile zip = new ZipFile())
                {
                    zip.Save(zipPath);
                }
            }
        }

        private void ManageModInfo()
        {
            string ArchivePath = App.Default.modDirectory + @"\Archive";
            string modArchivePath = ArchivePath + Path.DirectorySeparatorChar + name;
            if (!Directory.Exists(ArchivePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(ArchivePath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            if (!Directory.Exists(modArchivePath))
            {
                //don't bother hidding sub folders
                Directory.CreateDirectory(modArchivePath);

                //we need to extract the info files
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    foreach (ZipEntry e in zip)
                    {
                        string[] pathParts = e.FileName.Split('/');
                        //convert case and test
                        if (pathParts[0].ToLower() == "info")
                        {
                            e.Extract(modArchivePath);
                        }
                    }
                }

            }

        }
        
        private string ToAbsolutePath(string relativePath)
        {
            return modDir + relativePath;
        }

        private bool FileExists(string relativePath)
        {
            return zipFiles.Contains(relativePath);
        }

        private string ToRelativePath(string fullPath)
        {
            return fullPath.Replace(modDir, "");
        }


        private string ToGamePath(string relativePath)
        {
            //This simply injects a SeparatorChar between each string. Overkill but this is future proofing
            //so a longer example would be
            //return string.Format("{1}{0}{2}{0}{3}{0}{4}", Path.DirectorySeparatorChar, App.Default.MSOADirectory, "aSubfolder", "LikeMedia",relativePath);
            return string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, App.Default.MSOADirectory, relativePath);
        }

        private List<string> GetFiles(DirectoryInfo directory)
        {
            List<string> theseFiles = new List<string>();
            foreach (DirectoryInfo di in directory.GetDirectories())
                theseFiles.AddRange(GetFiles(di));
            //ignore base and "Info" Directory
            if (directory.FullName != modDir && directory.FullName != modDir + Path.DirectorySeparatorChar + "Info")
                foreach(FileInfo fi in directory.GetFiles())
                    theseFiles.Add(ToRelativePath(fi.FullName));    
            
            return theseFiles;
        }
        
        private void LoadModInfo()
        {
            //path to the info file
            string filePathName = modInfoDir + Path.DirectorySeparatorChar + "Info\\info.html";

            //Use create missing html file if none exist
            if (!File.Exists(filePathName))
            {
                if (!Directory.Exists(modInfoDir + @"\Info"))
                    Directory.CreateDirectory(modInfoDir + @"\Info");

                using (StreamWriter htmlWrite = File.CreateText(filePathName))
                {
                    htmlWrite.Write("<html>\n" +
                    "	<STYLE TYPE=\"text/css\"><!--BODY{color:white;background-color: #272822;font-family:arial;} A:link{color:white} A:visited{color:orange}--></STYLE>\n" +
                    "		<body>\n" +
                    "		<p><strong><span style=\"color:#ffa500;\">" + name + "</span></strong><br>\n" +
                    "		<span style=\"font-size:10px;\"><version>v0.00</version></span><br>\n" +
                    "		<span style=\"font-size:10px;\">Author: <author>Unknown Author</author></span></p>\n" +
                    "		<p><span style=\"color:#ffd700;\"><strong>Attention!</strong></span> This Mod has no description yet.</p>\n" +
                    "	</body>\n" +
                    "</html>");
                }
            }

            //read info file
            //change to html parsing
            if (File.Exists(filePathName))
            {
                //read the HTML
                hap.HtmlWeb gethtml = new hap.HtmlWeb();
                hap.HtmlDocument document = gethtml.Load(filePathName);
                //get out two pieces of info
                hap.HtmlNode versionHtml = document.DocumentNode.SelectSingleNode("//version");
                hap.HtmlNode authorHtml = document.DocumentNode.SelectSingleNode("//author");
                //get the version or use a defualt if html is dosen't use the tags
                if (versionHtml != null)
                {
                    version = versionHtml.InnerText;
                }
                else
                {
                    version = "v0.00";
                }

                if (authorHtml != null)
                {
                    author = authorHtml.InnerText;
                }
                else
                {
                    author = "Unknown Author";
                }
                
            }

            if (File.Exists(filePathName))
            {
                htmlinfo = new Uri(filePathName);
            }
        }
        #endregion

        #region private variables

        private string modInfoDir;

        #endregion

    }
}

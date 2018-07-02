using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malevolence_Mod_Manager
{
    class GameVersion
    {
        public static bool DoVersionCheck()
        {
            bool valid = false;
            string version = "";
            using (StreamReader s = new StreamReader(App.Default.MSOADirectory + @"\version.dat"))
                version = s.ReadLine();
            if (version != "")
            {
                valid = true;
            }

            if (App.Default.GameVersion != version && Directory.Exists(App.Default.modDirectory + @"\Vanilla"))
            {
                Directory.Delete(App.Default.modDirectory + @"\Vanilla", true);
            }

            App.Default.GameVersion = version;
            App.Default.Save();
            return valid;
        }
    }
}

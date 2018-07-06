using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malevolence_Mod_Manager
{
    class ModLoaders
    {
        private List<ModLoader> mods;
        private ModLoader vanilla;

        public ModLoaders()
        {
            mods = new List<ModLoader>();
            vanilla = new ModLoader("Vanilla.zip");
        }

        public void Add(string modName)
        {
            mods.Add(new ModLoader(modName));
        }

        public void Add(ModLoader mod)
        {
            mods.Add(mod);
        }

        public bool BeginImport()
        {
            if (!CanImport()) return false;

            vanilla.MigrateToGame(); //rollback to vanilla install
            //reset stats
            App.Default.statFiles = 0;

            MigrateVanillaFiles(); //figure out what vanilla Files we haven't yet extracted and put them in vanilla
            
            ImportModFiles(); //import the mods

            return true;
        }

        public bool BeginRestore()
        {
            if (!CanImport()) return false;

            vanilla.MigrateToGame(); //rollback to vanilla install
            //reset stats
            App.Default.statFiles = 0;
            return true;
        }

        private void ImportModFiles()
        {
            foreach (ModLoader mod in mods)
            {
                mod.MigrateToGame();
            }
        }

        private void MigrateVanillaFiles()
        {
            foreach (ModLoader mod in mods)
            {
                mod.MigrateFromGameToMod(vanilla);
            }
        }

        private bool CanImport()
        {
            //compare all the mods with eachother
            for (int i = 0; i < mods.Count; i++)
                for (int j = i + 1; j < mods.Count; j++)
                    if (!mods[i].AreCompatible(mods[j]))
                    {
                        MessageBox.Show("Mods "+mods[i]+" and "+mods[j]+" are not compatible.");
                        return false; //return false if they aren't compatible
                    }
            return true; //return true if they are
        }
    }
}

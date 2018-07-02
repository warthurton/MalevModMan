# MalevModMan
Malevolence Mod Manager

Initial release information by Kolgrima on *Malevolence: SoA* forums http://www.malevolencegame.com/forum/threads/malevolence-mod-manager.2416/

---
I’d like to introduce the Malevolence Mod Manager. This will allow you to easily install/uninstall any mods for MSoA. That’s right! Now you can manage all toughs’ mods for malevolence! What? There are not a ton of mods for Malevolence… then you best get to it and create some!

*TODO: Needs new images created*

User Feature list
· Easy install of mods
· Rollback to the vanilla version any time
· Manages updates to the game
· Auto Updates
· Html rich descriptions
· Handles custom locations for both the game and your mod folder
· Works with both Standard and Steam deployments of the game. 

Creator Feature list
· Supports any files in the Game
· Html rich descriptions (as complex or simple as you like)
· Simple zip files no special formats (this way your mod can also be installed by hand)

An additional Note

Currently the mod manager has no priority system for merging mods with like files, that is to say when two mods affect the same file, the manager simply see it as a conflict, and will not allow the two to be installed in tandem. This is currently a shortcoming as just one file conflict stops mods from playing nicely. This will be my next feature I'll be adding to the mod manager, until then this is something to be aware of for both users and modders alike. 

Here’s the Link to Download the Malevolence Mod Manager Installer.

*TODO: Add link to new release*

I’ve attempted to make the tool pretty straight forward, but just in case check out the tutorial below (or just dive right in)

User Tutorial:

1. Install Malevolence Mod Manager
2. You’ll need the .Net 4.5 Framework, for this if you don’t have it.
3. Run Malevolence Mod Manager and set the game folder and the mod folder, the mod folder can be anywhere you like and called whatever you like.
4. Get a mod (or make one) and put it in your mod folder (Do not unpack the zip just place it in the mod folder)
5. Select the mod to read the release notes
6. Check the mod and click the “Apply Selected Mod” button
7. To restore to the vanilla version of the game you’ll need to restart the mod Manager and the Vanilla version will show up. Just select it and click the “Apply Selected Mod” button.

If you’re looking for a mod to install then head over to my Kolgrima’s UI Overhaul thread and grab the latest release of the mod.

Modder tutorial:

1. Make your files
2. Use the same folder structure as the game
3. Create an info.html this uses special “version” and “author” tags for mod information.

HTML:

    <span style="font-size:10px;"><version>v0.01</version></span><br>

    <span style="font-size:10px;">Author: <author>Kolgrima</author></span>


This is so you mod information shows up in the list view (even without these you mod name will still show up in the list)

If you want a template just create zip with your mod name in your mod folder, and an info.html file will be created for your mod in the Archive folder (you’ll need "Show hidden files, and folders" turned on to see the Archive folder).

4. Place all your html document files (images and html file/s) in a folder called “info” it must be called “info” for the manager to both recognize it, and keep it from getting copied to the game folder.
5. Zip up all your files, the end result should look something like this;

*TODO: Needs new images created*

6. Test it out make sure it works.
7. Release it to the hoards.

I have only tested this on three machines as of this post, so I’m sure there will be bugs, feel free to report any you find here and I will try to fix them quickly. I hope you guys find this useful, and Happy Modding!

Cheers


---

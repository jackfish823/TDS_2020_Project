#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TopDownShooterProject2020
{
    // Here will be all of the pathes in content
    public class PathGlobals 
    {
        // Textures

        public const string ZOMBIE_SPAWN_TEXTURE = "2d\\Misc\\Spawner";
        public const string MAIN_CHARACTER_TEXTURE = "2d\\Idle_gun_000";
        public const string GUN_SHOT_TEXTURE = "2d\\Projectiles\\GunShot";
        public const string CURSOR_TEXTURE = "2d\\Misc\\Cursor";

        // Fonts
        public const string ARIAL_FONT = "fonts\\Arial16";
    }
}

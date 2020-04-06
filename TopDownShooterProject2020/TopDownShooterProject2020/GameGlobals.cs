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
    // Happens at load time
    public class GameGlobals
    {
        public static int Score = 0; // Innitialize the score to 0 

        // To scroll (move the screen) i use offset instad of a camera class
        public static PassObject PassProjectile, PassMob, CheckScroll; // Anything can create a projectile ex: unit / sky creating lightnign etc... // #1 delete if camera (the checkscroll)
    }
}

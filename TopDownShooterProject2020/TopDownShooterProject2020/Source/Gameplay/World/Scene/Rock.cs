#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class Rock : SceneItem    
    {    
        public Rock(Vector2 position, Vector2 scale)
            : base("2d\\Scene\\rock", position, new Vector2(100, 100), Globals.oneFrameOnly, scale)
        {

        }
    }
}

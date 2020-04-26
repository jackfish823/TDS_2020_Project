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
    public class SceneItem : Animated2d    
    {    
        public SceneItem(string path, Vector2 position, Vector2 dimensions, Vector2 frames, Vector2 scale)
            : base(path, position, dimensions * scale, frames, Color.White)
        {

        }
    }
}

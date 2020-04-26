#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class GridItem : Animated2d
    {
        public GridItem(string path, Vector2 position, Vector2 dimensions, Vector2 frames)
            : base(path, position, dimensions, frames, Color.Black)
        {

        }

    }
}

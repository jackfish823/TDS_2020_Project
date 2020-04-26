#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class Building: AttackableObject
    {
        public Building(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId) 
            : base (path, position, dimensions, frames, ownerId)
        {

        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {       
           
            base.Update(offset, enemy, grid);
        }
        public override void Draw(Vector2 offeset)
        {
            /*Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.texture.Bounds.Width);
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.dimensions.Y));
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
            */
            base.Draw(offeset);
        }
    }
}

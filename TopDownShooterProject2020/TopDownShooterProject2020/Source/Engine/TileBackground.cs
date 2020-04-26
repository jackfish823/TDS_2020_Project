#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class TileBackground : Basic2d
    {
        public Vector2 bkgDimensions;
        public TileBackground(string path, Vector2 position, Vector2 deminesions, Vector2 bkgDimensions) 
            :base(path, position, new Vector2((float)Math.Floor(deminesions.X), (float)Math.Floor(deminesions.Y)))
        {
            this.bkgDimensions = new Vector2((float)Math.Floor(bkgDimensions.X), (float)Math.Floor(bkgDimensions.Y));
        }

        public override void Draw(Vector2 offset)
        {
            float numX = (float)Math.Ceiling(bkgDimensions.X / dimensions.X);
            float numY = (float)Math.Ceiling(bkgDimensions.Y / dimensions.Y);

            for (int i = 0; i< numX; i++)
            {
                for (int j = 0; j < numY; j++)
                {
                    // Runs until the last tile
                    if(i < numX - 1 && j < numY - 1)
                    {
                        base.Draw(offset + new Vector2(dimensions.X / 2 + dimensions.X * i, dimensions.Y / 2 + dimensions.Y * j));
                    }
                    else
                    {
                        // The amount of space we have left is the space we have minus the space we used
                        float xLeft = Math.Min(dimensions.X, (bkgDimensions.X - (i * dimensions.X)));
                        float yLeft = Math.Min(dimensions.Y, (bkgDimensions.Y - (j * dimensions.X)));
                        // Precentage left will be 1 (100%) or the precentage of the tile left (the smaller)
                        float xPrecentLeft = Math.Min(1, xLeft / dimensions.X);
                        float yPrecentLeft = Math.Min(1, yLeft / dimensions.Y);


                        Globals.spriteBatch.Draw(texture, 
                            new Rectangle((int)(position.X + offset.X + dimensions.X * i), (int)(position.Y + offset.Y + dimensions.X * j), (int)Math.Ceiling(dimensions.X * xPrecentLeft), (int)Math.Ceiling(dimensions.Y * yPrecentLeft)), 
                            new Rectangle(0, 0, (int)xPrecentLeft * texture.Bounds.Width, (int)yPrecentLeft * texture.Bounds.Height), Color.White, rotation, new Vector2(0, 0), new SpriteEffects(), 0);
                    }
                }
            }
        }
    }
}

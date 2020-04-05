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
    public class Basic2d
    {

        public Vector2 position, dimensions;  // Position and Dimantions for the texture

        public Texture2D texture;  // The actual texture (the xna file)
        public Basic2d(string path, Vector2 position, Vector2 dimensions) // Getting path for the xna texture file as string, position (vec2) and dimansions (vec2)
        {
            this.position = position;
            this.dimensions = dimensions;

            this.texture = Globals.content.Load<Texture2D>(path); // Assigning the texture from the given path to texture (Texture2D) wiht content.load func from globals
        }

        public virtual void Update()
        {


        }

        public virtual void Draw()
        {
            if (texture != null) // Creating basic texture, drawn from the middle and drawing
            {
                Globals.spriteBatch.Draw(texture, new Rectangle((int)(position.X), (int)(position.Y), (int)dimensions.X,
                                        (int)dimensions.Y), null, Color.White, 0.0f, new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                                        new SpriteEffects(), 0);
            }

        }
    }
}

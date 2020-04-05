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
    public class MainCharacter : Unit
    {
        public MainCharacter(string path, Vector2 position, Vector2 dimensions) : base(path, position, dimensions)
        {
            speed = 4;
            
        }

        public override void Update()
        {
            if (Globals.keyboard.GetPressed("A"))
            {
                position = new Vector2(position.X - speed, position.Y);
            }

            if (Globals.keyboard.GetPressed("D"))
            {
                position = new Vector2(position.X + speed, position.Y);
            }

            if (Globals.keyboard.GetPressed("W"))
            {
                position = new Vector2(position.X, position.Y - speed);
            }

            if (Globals.keyboard.GetPressed("S"))
            {
                position = new Vector2(position.X, position.Y + speed);
            }

            rotation = Globals.RotateToward(position, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y));
            
            if (Globals.mouse.LeftClick())
            {
                Vector2 v = new Vector2(8.59f, -48.43f);
                v = Vector2.Transform(v, Matrix.CreateRotationZ(rotation));
                GameGlobals.PassProjectile(new GunShot(new Vector2(position.X, position.Y) + v, this, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y)));
            }

            base.Update();
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset, new Vector2(246, 392)); // because the texture isnt square the origin is its head
        }
    }
}

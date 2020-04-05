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
    public class MainCharacter : Basic2d
    {
        public MainCharacter (string path, Vector2 position, Vector2 dimensions) : base (path, position, dimensions)
        {

        }

        public override void Update()
        {       
            if (Globals.keyboard.GetPressed("A"))
            {
                position = new Vector2(position.X - 1, position.Y);           
            }

            if (Globals.keyboard.GetPressed("D"))
            {
                position = new Vector2(position.X + 1, position.Y);
            }

            if (Globals.keyboard.GetPressed("W"))
            {
                position = new Vector2(position.X, position.Y - 1);
            }

            if (Globals.keyboard.GetPressed("S"))
            {
                position = new Vector2(position.X, position.Y + 1);
            }

            base.Update();
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

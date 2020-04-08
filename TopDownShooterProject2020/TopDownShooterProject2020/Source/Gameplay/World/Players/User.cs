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
    public class User : Player // Inhertes from player class - this is the user (gamer) class
    {
        public User(int id) : base(id) // will be 1
        {
            this.mainCharacter = new MainCharacter(PathGlobals.MAIN_CHARACTER_TEXTURE, new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(200, 200), id);
        }

        public override void Update(Player enemy, Vector2 offset)
        {
            base.Update(enemy, offset);
        }

    }
}

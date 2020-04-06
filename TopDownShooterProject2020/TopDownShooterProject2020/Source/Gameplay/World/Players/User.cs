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
        public User() : base()
        {
            this.mainCharacter = new MainCharacter(PathGlobals.MAIN_CHARACTER_TEXTURE, new Vector2(300, 300), new Vector2(200, 200));
        }

        public override void Update(Player enemy, Vector2 offset)
        {
            base.Update(enemy, offset);
        }

    }
}

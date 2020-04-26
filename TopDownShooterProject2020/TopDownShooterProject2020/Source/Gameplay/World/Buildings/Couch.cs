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
    public class Couch : Building
    {
        public Couch(Vector2 position, Vector2 frames, int ownerId)
            : base ("2d\\Buildings\\Couch", position, new Vector2(256, 256), frames, ownerId)
        {
            this.health = 15;
            this.maxHealth = this.health;

            this.hitDistance = 35.0f;
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {       
           
            base.Update(offset, enemy, grid);
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

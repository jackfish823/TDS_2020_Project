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
    public class Turret : Building
    {
        public Turret(Vector2 position, int ownerId)
            : base ("2d\\Buildings\\Turret", position, new Vector2(256, 256), ownerId)
        {
            this.health = 15;
            this.maxHealth = this.health;

            this.hitDistance = 35.0f;
        }

        public override void Update(Vector2 offset, Player enemy)
        {       
           
            base.Update(offset);
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

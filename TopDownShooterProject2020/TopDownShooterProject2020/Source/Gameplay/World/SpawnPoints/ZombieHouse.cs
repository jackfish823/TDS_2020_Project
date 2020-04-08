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
    public class ZombieHouse : SpawnPoint
    {
        public ZombieHouse(Vector2 position, int ownerId) 
            : base ("2d\\Misc\\Spawner", position, new Vector2(120, 120), ownerId)
        {

        }

        public override void Update(Vector2 offset)
        {         
            base.Update(offset);
        }

        public override void SpawnMob()
        {
            int num = Globals.random.Next(0, 100);

            Mob tempMob = null;

            if (num < 50) // ~ 50% chance of spawning cop zombie
            {
                tempMob = new Zombie(this.position, this.ownerId);
            }
            else if (num < 70) // ~ 20% chance of spawning cop zombie
            {
                tempMob = new CopZombie(this.position, this.ownerId);
            }

            if (tempMob != null)
            {
                GameGlobals.PassMob(tempMob);
            }
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

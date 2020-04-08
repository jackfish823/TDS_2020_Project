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
    public class MiniZombieHouse : SpawnPoint
    {
        int maxSpawns, totalSpawns;
        public MiniZombieHouse(Vector2 position, int ownerId)
            : base("2d\\Misc\\mini_zombie_house", position, new Vector2(100, 100), ownerId)
        {
            this.spawnTimer = new BaseTimer(350);
            this.totalSpawns = 0;
            this.maxSpawns = 3;
        }

        public override void Update(Vector2 offset)
        {
            base.Update(offset);
        }

        public override void SpawnMob()
        {
            Mob tempMob = new MiniZombie(this.position, this.ownerId);

            if (tempMob != null)
            {
                GameGlobals.PassMob(tempMob);

                this.totalSpawns++;

                if (this.totalSpawns >= maxSpawns)
                {
                    this.dead = true;
                }
            }


        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

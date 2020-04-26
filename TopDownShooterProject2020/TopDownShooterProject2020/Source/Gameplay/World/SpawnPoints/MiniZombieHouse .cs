#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class MiniZombieHouse : SpawnPoint
    {
        int maxSpawns, totalSpawns;
        public MiniZombieHouse(Vector2 position, Vector2 frames, int ownerId, XElement data)
            : base("2d\\Misc\\mini_zombie_house", position, new Vector2(0, 0), frames, ownerId, data)
        {
            this.spawnTimer = new BaseTimer(350);
            this.totalSpawns = 0;
            this.maxSpawns = 3;
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            base.Update(offset, enemy, grid);
        }

        public override void SpawnMob()
        {
            Mob tempMob = new MiniZombie(this.position, new Vector2(4, 6), this.ownerId);

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

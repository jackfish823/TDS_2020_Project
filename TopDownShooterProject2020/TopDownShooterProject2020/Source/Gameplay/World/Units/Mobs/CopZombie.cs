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
    public class CopZombie : Mob
    {
        public BaseTimer spawnTimer;
        public CopZombie(Vector2 position, int ownerId) 
            : base("2d\\Units\\Mobs\\cop_zombie_walk_000", position, new Vector2(200, 200), ownerId)
        {          
            this.speed = 1;
            this.health = 3;
            this.maxHealth = this.health;
        }

        public override void Update(Vector2 offset, Player enemy)
        {
            if (this.dead)
            {
                SpawnZombieHouse();
            }
            base.Update(offset, enemy);
        }

        public virtual void SpawnZombieHouse()
        {
            GameGlobals.passSpawnPoint(new MiniZombieHouse(this.position, this.ownerId));
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

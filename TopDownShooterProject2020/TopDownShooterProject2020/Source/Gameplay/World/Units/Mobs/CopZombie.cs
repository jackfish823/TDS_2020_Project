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

            // Spawn timer 8 secs but we add at start 4 secs so the first spawn will be at 4 secs
            this.spawnTimer = new BaseTimer(8000);
            this.spawnTimer.AddToTimer(4000);
        }

        public override void Update(Vector2 offset, Player enemy)
        {
            this.spawnTimer.UpdateTimer(); // Updating timer

            if (spawnTimer.Test()) // Testing if timer is done
            {
                SpawnZombieHouse();
                this.spawnTimer.ResetToZero(); // Resetting the timer
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

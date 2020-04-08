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
    public class SpawnPoint : AttackableObject
    {
        public BaseTimer spawnTimer = new BaseTimer(2200); // Timer for the spawns
        public SpawnPoint(string path, Vector2 position, Vector2 dimensions, int ownerId) : base (path, position, dimensions, ownerId)
        {
            dead = false;
            this.health = 3;
            this.maxHealth = this.health;
            hitDistance = 35.0f;
        }

        public override void Update(Vector2 offset)
        {
            this.spawnTimer.UpdateTimer(); // Updating timer

            if(spawnTimer.Test()) // Testing if timer is done
            {
                SpawnMob();
                this.spawnTimer.ResetToZero(); // Resetting the timer
            }
           
            base.Update(offset);
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Zombie(this.position, this.ownerId));
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

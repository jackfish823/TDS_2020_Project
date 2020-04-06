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
    public class SpawnPoint : Basic2d
    {

        public bool dead;

        public float hitDistance;

        public BaseTimer spawnTimer = new BaseTimer(2200); // Timer for the spawns
        public SpawnPoint(string path, Vector2 position, Vector2 dimensions) : base (path, position, dimensions)
        {
            dead = false;

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

        public virtual void GetHit() // For now if unit get hit it dies
        {
            dead = true;
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Zombie(this.position));
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

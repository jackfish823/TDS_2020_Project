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
    public class BasicProjectile : Basic2d // Generic projectile
    {
        public bool done; // Flag to adress if the projectile finished its course for example: hit target, left screen etc

        public float speed; // Projectile speed

        public Vector2 direction; // Projectile direction

        public Unit owner; // Who shoots the projectile

        public BaseTimer timer; // Timer for the life span of the bullet
        public BasicProjectile(string path, Vector2 position, Vector2 dimensions, Unit owner, Vector2 target) // Passing target so as vecotr2 target so its a general place, can also pass Unit for more like point and click
            : base(path, position, dimensions)
        {
            this.done = false;

            this.speed = 5.0f;

            this.owner = owner;

            this.direction = target - this.owner.position; // Getting the directional vector (decremanting 2 vectores) between target and the owner position

            this.direction.Normalize(); // Nomalizing the direction vector to length of 1, so the distance between the target and the owner wont affect the speed of the projectile

            this.rotation = Globals.GetAngle(direction);

            this.timer = new BaseTimer(1200); // 1.2 seconds
        }

        public virtual void Update(Vector2 offset, List<Unit> units) // Passing an offset and list of units so see if it hits somethings, also not overriding basic2d
        {
            this.position += this.direction * this.speed;
            this.timer.UpdateTimer(); // Updating the timer each frame
            if (this.timer.Test()) // So the projectime eventually stop and disappear, also helps with memory
            {
                done = true;
            }

            if (CollisionTest(units)) 
            {
                done = true;
            }
        }
        public virtual bool CollisionTest(List<Unit> units) // Checks if the projectile collide with something
        {
            return false;
        }

        public override void Draw(Vector2 offset)
        { 
            base.Draw(offset);
        }
    }
}   



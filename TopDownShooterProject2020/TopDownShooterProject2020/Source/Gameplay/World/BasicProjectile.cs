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

            this.rotation = Globals.GetAngle(direction); // Getting the angle of the diraction so the sprite will be in the correct direction

            this.timer = new BaseTimer(1200); // 1.2 seconds
        }

        public virtual void Update(Vector2 offset, List<AttackableObject> objects) // Passing an offset and list of units so see if it hits somethings, also not overriding basic2d, objects for short (attackble objects)
        {
            this.position += this.direction * this.speed;
            this.timer.UpdateTimer(); // Updating the timer each frame
            if (this.timer.Test()) // So the projectime eventually stop and disappear, also helps with memory
            {
                done = true;
            }

            if (CollisionTest(objects)) 
            {
                done = true;
            }
        }
        public virtual bool CollisionTest(List<AttackableObject> objects) // Checks if the projectile collide with something, objects for short (attackble objects)
        {
            for (int i = 0; i < objects.Count; i++) // Running all over the units
            {
                if (this.owner.ownerId != objects[i].ownerId && Globals.GetDistance(this.position, objects[i].position) < objects[i].hitDistance) // Calculating the distance between the projectile position and the unit position and comparing to its "hit distance", also checks if the object belongs to the owner, if its like a good thing we can change it to ==
                {
                    objects[i].GetHit(1); // The unit will die
                    return true; // Returning true so the projecitle will end itself
                }
            }

            return false;
        }

        public override void Draw(Vector2 offset)
        {
            // Can clean up and add into a class and add like deafault(makes it 1, and white ect..)
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.texture.Bounds.Width);
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.dimensions.Y));
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(offset);
        }
    }
}   



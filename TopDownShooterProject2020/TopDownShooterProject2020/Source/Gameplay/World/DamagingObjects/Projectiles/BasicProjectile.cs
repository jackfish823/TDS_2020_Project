#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class BasicProjectile : DamagingObject 
    {
        public float speed;
        public BasicProjectile(string path, Vector2 position, Vector2 dimensions, AttackableObject owner) // Passing target so as vecotr2 target so its a general place, can also pass Unit for more like point and click
            : base(path, position, dimensions, owner)
        {
            this.speed = 5.0f;
            this.direction = owner.direction;
            this.rotation = owner.rotation;
        }

        public override void Update(Vector2 offset, List<AttackableObject> objects) // Passing an offset and list of units so see if it hits somethings, also not overriding basic2d, objects for short (attackble objects)
        {
            MoveProjectile();            
            base.Update(offset, objects);
        }

        public virtual void MoveProjectile()
        {
            this.position += this.direction * this.speed;
        }
        public override bool CollisionTest(List<AttackableObject> objects) // Checks if the projectile collide with something, objects for short (attackble objects)
        {
            for (int i = 0; i < objects.Count; i++) // Running all over the units
            {
                if (this.owner.ownerId != objects[i].ownerId && Globals.GetDistance(this.position, objects[i].position) < objects[i].hitDistance) // Calculating the distance between the projectile position and the unit position and comparing to its "hit distance", also checks if the object belongs to the owner, if its like a good thing we can change it to ==
                {
                    objects[i].GetHit(owner, 1); // The unit will die

                    Globals.soundControl.PlaySound("Hit", true);
                    return true; // Returning true so the projecitle will end itself
                }
            }

            return false;
        }

        public override void Draw(Vector2 offset)
        {
            // Can clean up and add into a class and add like deafault(makes it 1, and white ect..)
           // Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
           // Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.texture.Bounds.Width);
           // Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            //Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.dimensions.Y));
            //Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            //Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(offset);
        }
    }
}   



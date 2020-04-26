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
    public class DamagingObject : Basic2d // Generic projectile
    {
        public bool done;
        public BaseTimer timer;


        public AttackableObject owner;
        public DamagingObject(string path, Vector2 position, Vector2 dimensions, AttackableObject owner) // Passing target so as vecotr2 target so its a general place, can also pass Unit for more like point and click
            : base(path, position, dimensions)
        {
            this.done = false;
            this.owner = owner;
            this.timer = new BaseTimer(1200);
        }

        public virtual void Update(Vector2 offset, List<AttackableObject> objects) // Passing an offset and list of units so see if it hits somethings, also not overriding basic2d, objects for short (attackble objects)
        {
            this.timer.UpdateTimer();
            if (this.timer.Test()) // So the projectime eventually stop and disappear, also helps with memory
            {
                done = true;
            }
            if (CollisionTest(objects)) 
            {
                done = true;
            }

            base.Update(offset);
        }

        

        public virtual bool CollisionTest(List<AttackableObject> objects) // Checks if the projectile collide with something, objects for short (attackble objects)
        {

            return false;
        }

        public override void Draw(Vector2 offset)
        {
            // Can clean up and add into a class and add like deafault(makes it 1, and white ect..)
            // Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
            // Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.texture.Bounds.Width);
            // Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            // Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.dimensions.Y));
            // Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            // Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
            base.Draw(offset);
        }
    }
}   



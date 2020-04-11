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
    public class Flame : BasicProjectile // inhereting from BasicProjectile
    {
        public Flame(Vector2 position, Unit owner, Vector2 target) 
            : base("2d\\Projectiles\\flame", position, new Vector2(99,224), owner, target)
        {
            this.speed = 0;
            this.timer = new BaseTimer(100);
        }

        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {
            this.timer.UpdateTimer(); // Updating the timer each frame
            if (this.timer.Test()) // So the projectime eventually stop and disappear, also helps with memory
            {
                done = true;
            }

            CollisionTest(objects);
           
        }

 
        public override bool CollisionTest(List<AttackableObject> objects)
        {
            Vector2 tempdir = this.direction;
            Vector2 temppos = this.position;
            float firelength = 224;
            float fireWidth = 49;

            Vector2 xleft, xright, yleft, yright;

            
            xright = new Vector2(tempdir.Y,tempdir.X*(-1));
            xleft = new Vector2(tempdir.Y * (-1), tempdir.X);

            xright = new Vector2(xright.X * fireWidth, xright.Y * fireWidth);
            Vector2 undervecdir = xright;
            xright = xright + temppos;
            xleft = new Vector2(xleft.X * fireWidth, xleft.Y * fireWidth);
            xleft = xleft + temppos;

            yright = new Vector2(tempdir.X * firelength, tempdir.Y * firelength);
            yleft = new Vector2(tempdir.X * firelength, tempdir.Y * firelength);
            yright = yright + xright;
            yleft = yleft + xleft;

            float bigdist = Globals.GetDistance(xleft, yright) + Globals.GetDistance(xright, yleft);

            for (int i = 0; i < objects.Count; i++) // Running all over the units
            {
                float calcdists = Globals.GetDistance(objects[i].position, xright) + Globals.GetDistance(objects[i].position, xleft) + Globals.GetDistance(objects[i].position, yright) + Globals.GetDistance(objects[i].position, yleft);
                
                if (calcdists - bigdist < 20)
                {
                    objects[i].GetHit(1); // The unit will die
                    return true; // Returning true so the projecitle will end itself
                }
            
            }

            return false;
        }
    
        public override void Draw(Vector2 offset)
        { 
            base.Draw(offset, new Vector2(this.texture.Width / 2, this.texture.Height));
        }
    }
}   



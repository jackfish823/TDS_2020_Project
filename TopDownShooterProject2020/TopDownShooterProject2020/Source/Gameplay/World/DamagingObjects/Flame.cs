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
        Basic2d test2;
        Vector2 xleft, xright, yleft, yright;

        public Flame(Vector2 position, AttackableObject owner) 
            : base("2d\\Projectiles\\flame", position, new Vector2(99,224), owner)
        {
            this.speed = 0;
            this.timer = new BaseTimer(100);
            test2 = new Basic2d("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(1, 1));

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
            float fireWidth = 99;


            
            xright = new Vector2(tempdir.Y,tempdir.X*(-1));
            xleft = new Vector2(tempdir.Y * (-1), tempdir.X);

            xright = new Vector2(xright.X * fireWidth/2, xright.Y * fireWidth/2);
 
            xright = xright + temppos;
            xleft = new Vector2(xleft.X * fireWidth/2, xleft.Y * fireWidth/2);
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
                    objects[i].GetHit(owner, 1); // The unit will die
                    return true; // Returning true so the projecitle will end itself
                }
            
            }

            return false;
        }  
        public override void Draw(Vector2 offset)
        {           
            if(Globals.toggleLinesDebug)
            {
                Globals.DrawLine(test2.texture, xleft, yleft, Color.Red,offset);
                Globals.DrawLine(test2.texture, xright, yright, Color.Red, offset);
                Globals.DrawLine(test2.texture, yleft, yright, Color.Red, offset);
                Globals.DrawLine(test2.texture, xright, xleft, Color.Red, offset);
            }
            base.Draw(offset, new Vector2(this.texture.Width / 2, this.texture.Height));
        }
    }
}   



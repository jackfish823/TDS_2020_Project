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
    public class InvisibleHitbox : DamagingObject
    {
        protected float ticks, currentTick;
        public InvisibleHitbox(Vector2 position, Vector2 dimesions, AttackableObject owner, int milliseconds) 
            : base("2d\\Misc\\solid", position, dimesions, owner)
        {
            ticks = 4;
            currentTick = 0;
        }


        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {           
            base.Update(offset, objects);

            GameGlobals.PassDebugInfo(new CirclePacket(this.position, 20, Color.Green));
            GameGlobals.PassDebugInfo(new LinePacket(this.position, this.position, Color.Red));

            // If it has 3 ticks it will tick at the start end and middle 
            if (timer.Timer >= timer.Msec * (currentTick / (ticks - 1)))
            {
                if(currentTick == 2)
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        if (Globals.GetDistance(objects[i].position, position) <= dimensions.X / 2)
                        {
                            objects[i].GetHit(owner, 1.0f);
                            Console.WriteLine(objects[i] + "hit");
                        }
                    }
                }
                
                currentTick++;
            }
        }
        public override bool CollisionTest(List<AttackableObject> objects)
        {
            return false;
        }
        public override void Draw(Vector2 offset)
        { 
        }
    }
}   



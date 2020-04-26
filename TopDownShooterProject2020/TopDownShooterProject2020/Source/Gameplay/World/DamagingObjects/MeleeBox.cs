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
    public class MeleeBox : DamagingObject // inhereting from BasicProjectile
    {
        float hitrange, hitAngle;
        public MeleeBox(Vector2 position, AttackableObject owner) 
            : base("2d\\Projectiles\\flame", position, new Vector2(99,224), owner)
        {
            hitrange = 150;
            hitAngle = 90;           
        }

        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {
            if (CollisionTest(objects))
            {
                done = true;
            }
        }
        public override bool CollisionTest(List<AttackableObject> objects)
        {
            Vector2 ownerDirection, ownerTargetDirection, ownerPosition, targetPosition;
            float dist;
            float deltaAngle;
            ownerDirection = owner.direction;
            ownerPosition = owner.position;


            for (int i = 0; i < objects.Count; i++) // Running all over the units
            {
                targetPosition = objects[i].position;
                ownerTargetDirection = targetPosition - ownerPosition;
                dist = Globals.GetLength(ownerTargetDirection);
                deltaAngle = (Math.Abs(Globals.GetAngle(ownerDirection) - Globals.GetAngle(ownerTargetDirection))) * 180f / (float)Math.PI;

                if (dist < hitrange)
                {
                    if (deltaAngle <= hitAngle/2)
                    {
                        objects[i].GetHit(owner, 1);
                        Console.WriteLine("Melee hit");
                    }
                }
            }
            return true;
        }
    
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset, new Vector2(this.texture.Width / 2, this.texture.Height));
        }
    }
}   



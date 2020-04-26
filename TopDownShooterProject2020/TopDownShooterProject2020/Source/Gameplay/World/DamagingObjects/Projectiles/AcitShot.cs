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
    public class AcitShot : BasicProjectile // inhereting from BasicProjectile
    {
        public AcitShot(Vector2 position, AttackableObject owner) 
            : base("2d\\Projectiles\\AcidBullet", position, new Vector2(16/1.3f,73/1.3f), owner)
        {
            this.speed = 5;
            this.timer = new BaseTimer(1800);
        }

        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {
            base.Update(offset, objects);
        }

        public override bool CollisionTest(List<AttackableObject> objects) // Checks if the projectile collide with something, objects for short (attackble objects)
        {
            for (int i = 0; i < objects.Count; i++) // Running all over the units
            {
                if (this.owner.ownerId != objects[i].ownerId && Globals.GetDistance(this.position, objects[i].position) < objects[i].hitDistance) // Calculating the distance between the projectile position and the unit position and comparing to its "hit distance", also checks if the object belongs to the owner, if its like a good thing we can change it to ==
                {
                    objects[i].GetHit(owner, 1); // The unit will die
                    GameGlobals.PassEffect(new AcidHit(objects[i].position, new Vector2(65, 39), 198, rotation));

                    return true; // Returning true so the projecitle will end itself
                }
            }

            return false;
        }

        public override void Draw(Vector2 offset)
        { 
            base.Draw(offset);
        }
    }
}   



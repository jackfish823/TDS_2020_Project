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
    public class Knife : BasicWeapon
    {

        public Knife(Unit owner)
            : base("2d\\Weapons\\knife_inventory", owner, new Vector2(170, 208), new Vector2(29, -70))
        {
            this.weaponIcon.dimensions = new Vector2(210 / 1.3f, 49 / 1.3f);

            SetReloadTime(0);
            SetFireDelay(3.78f);
        }


        public override void Update(Vector2 offset)
        {

            if (fireDelay.Test())
            {
                GameGlobals.PassDamaginObject(new KnifeHitBox(owner.position + RotatedVectorTowardsMouse(), owner));

                fireDelay.ResetToZero();
            }
            base.Update(offset);
        }

        public override void Draw(Vector2 offset, Vector2 origin)
        {
            
            base.Draw(offset, origin);
        }
    }
}

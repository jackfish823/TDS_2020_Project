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
    public class Bat : BasicWeapon
    {
        public Bat(Unit owner)
            : base("2d\\Weapons\\bat_inventory", owner, new Vector2(260, 317), new Vector2(29, -70))
        {
            this.weaponIcon.dimensions = new Vector2(314 / 1.5f, 52 / 1.5f);

            SetReloadTime(0);
            SetFireDelay(1.26f);
        }

        public override void Update(Vector2 offset)
        {
            if (fireDelay.Test())
            {                
                GameGlobals.PassDamaginObject(new MeleeBox(owner.position + RotatedVectorTowardsMouse(), owner));

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

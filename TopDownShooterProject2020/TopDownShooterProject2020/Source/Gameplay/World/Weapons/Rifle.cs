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
    public class Rifle : BasicWeapon
    {
        public Rifle(Unit owner)
            : base("2d\\Weapons\\rfl", owner)
        {
            this.magazineSize = 30;
            this.currentBullets = this.magazineSize;
            this.sprayable = true;
           

            SetFireDelay(12f);
            SetReloadTime(1.5f);
        }


        public override void Update(Vector2 offset)
        {
            if (fireDelay.Test() && reloadTime.Test() && currentBullets > 0)
            {
                Vector2 offsetVector = new Vector2(8.59f, -48.43f); // Creating an offset vector so the sprite will come out of the gun 
                offsetVector = Vector2.Transform(offsetVector, Matrix.CreateRotationZ(owner.rotation)); // rotating the vector so it will be correct to any mouse position
                GameGlobals.PassProjectile(new GunShot(new Vector2(owner.position.X, owner.position.Y) + offsetVector, owner, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset));
                currentBullets--;
                fireDelay.ResetToZero();
            }
            else if (reloadTime.Test() && currentBullets <= 0)
            {
                Reload();
            }

            base.Update(offset);
        }

        public override void Draw(Vector2 offset, Vector2 origin)
        {
            base.Draw(offset, origin);
        }
    }
}

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
    public class Pistol : BasicWeapon
    {
        public Pistol(Unit owner)
            : base("2d\\Weapons\\pistol_inventory", owner)
        {
            this.magazineSize = 10;
            this.currentBullets = this.magazineSize;
            this.weaponIcon.dimensions = new Vector2(156/1.7f, 108/1.7f);

            SetFireDelay(2.0f);
            SetReloadTime(1.0f);
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
            if (reloadTime.Test() && currentBullets <= 0)
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

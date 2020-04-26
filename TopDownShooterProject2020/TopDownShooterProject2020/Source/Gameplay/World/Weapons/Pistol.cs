#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
            : base("2d\\Weapons\\pistol_inventory", owner, new Vector2(90, 155), new Vector2(29, -70))
        {
            this.magazineSize = 10;
            this.currentBullets = this.magazineSize;
            this.weaponIcon.dimensions = new Vector2(156/1.7f, 108/1.7f);

            SetFireDelay(2.0f);
            SetReloadTime(1.0f);

            this.fireAnimation = new Animated2d("2d\\pistol_shoot", owner.position + weaponOrigin, new Vector2(150, 150), new Vector2(3, 1), Color.White);
            this.fireAnimation.frameAnimationList.Add(new FrameAnimation(new Vector2(fireAnimation.frameSize.X, fireAnimation.frameSize.Y), fireAnimation.frames, new Vector2(0, 0), 3, 66, 0,
                new Vector2(94, 113), "PistolShoot"));
            this.fireAnimation.SetAnimationByName("PistolShoot");
            this.fireAnimation.frameAnimations = true;


        }


        public override void Update(Vector2 offset)
        {
            if (fireDelay.Test() && reloadTime.Test() && currentBullets > 0)
            {
                Globals.soundControl.PlaySound("Shoot", true);

                this.fireAnimation.position = owner.position + RotatedVectorTowardsMouse();
                this.fireAnimation.rotation = owner.rotation;
                GameGlobals.PassAnimation(this.fireAnimation);

                GameGlobals.PassDamaginObject(new GunShot(new Vector2(owner.position.X, owner.position.Y) + RotatedVectorTowardsMouse(), owner));
                currentBullets--;
                fireDelay.ResetToZero();
            }
            if (reloadTime.Test() && currentBullets <= 0)
            {
                Reload();
            }

            base.Update(offset);
        }
        public override void Reload() // override because i have speical sound, later make it pass sound class to the base and make general
        {
            Globals.soundControl.PlaySound("Reload", true);
            this.reloadTime.ResetToZero();
            this.currentBullets = this.magazineSize;
        }

        public override void Draw(Vector2 offset, Vector2 origin)
        {
            base.Draw(offset, origin);
        }
    }
}

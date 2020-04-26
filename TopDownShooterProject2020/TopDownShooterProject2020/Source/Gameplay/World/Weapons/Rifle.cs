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
    public class Rifle : BasicWeapon
    {

        public Rifle(Unit owner)
            : base("2d\\Weapons\\rifle_inventory", owner, new Vector2(90, 260), new Vector2(26.3f, -122))
        {
            this.magazineSize = 30;
            this.currentBullets = this.magazineSize;
            this.sprayable = true;

            SetFireDelay(8f);
            SetReloadTime(1.5f);

            this.fireAnimation = new Animated2d("2d\\pistol_shoot", owner.position + weaponOrigin, new Vector2(150, 150) ,new Vector2(3, 1), Color.White);
            this.fireAnimation.frameAnimationList.Add(new FrameAnimation(new Vector2(fireAnimation.frameSize.X, fireAnimation.frameSize.Y), fireAnimation.frames, new Vector2(0, 0), 3, 66, 0, 
                new Vector2 (94, 113), "RifleShoot"));
            this.fireAnimation.SetAnimationByName("RifleShoot");
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
            else if (reloadTime.Test() && currentBullets <= 0)
            {
                Reload();
            }           
            base.Update(offset);
        }

        public override void Reload() // override because i have speical sound, later make it pass sound class to the base and make general
        {
            Globals.soundControl.PlaySound("Reload", true);

            //soundEffectreload.Play();
            this.reloadTime.ResetToZero();
            this.currentBullets = this.magazineSize;
        }

        public override void Draw(Vector2 offset, Vector2 origin)
        {

            base.Draw(offset, origin);
        }
    }
}

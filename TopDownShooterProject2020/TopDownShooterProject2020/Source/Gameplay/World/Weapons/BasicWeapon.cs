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
    public abstract class BasicWeapon
    {

        public BaseTimer fireDelay, reloadTime;
        public Unit owner;
        public Basic2d weaponIcon;
        public BasicProjectile projectile;
        public Animated2d fireAnimation; // Sprite sheet of all of the fire animations, why we do that?, we want to have multiple shots like ice, regual ect. and also we cant pull shoot and walk animation so they are sperate so each wep will have his own list of aniamtions
        public int magazineSize, currentBullets;
        public bool sprayable;
        public Vector2 origin; // Because some weapon sprites has different origins and dealing with that graphiclly will be a pain we will give each weapon his origin where his head will be
        public Vector2 weaponOrigin; // The origin of the sprite where the end of the weapon is (where the bullets comes from)



        public BasicWeapon(string iconPath, Unit owner, Vector2 origin, Vector2 weaponOrigin)
        {
            this.owner = owner;
            this.weaponIcon = new Basic2d(iconPath, new Vector2(0, 0), new Vector2(181/1.5f, 58/1.5f));
            this.weaponIcon.rotation = -0.785398f;
            this.sprayable = false;
            this.origin = origin;
            this.weaponOrigin = weaponOrigin;

        }

        public virtual void Update(Vector2 offset)
        {

        }

        // Returns the vector pointing out from the weapon origin in its diraction so it will be correct to any mouse position make deleg?
        public Vector2 RotatedVectorTowardsMouse()
        {
            return Vector2.Transform(this.weaponOrigin, Matrix.CreateRotationZ(owner.rotation));
        }
        protected void SetFireDelay(float fireRate)
        {
            this.fireDelay = new BaseTimer((int)(1000 / fireRate));
            this.fireDelay.AddToTimer(this.fireDelay.Msec);
        }

        protected void SetReloadTime(float reloadTime)
        {
            this.reloadTime = new BaseTimer((int)(reloadTime * 1000));
            this.reloadTime.AddToTimer(this.reloadTime.Msec);
        }

        public virtual void Reload()
        {
            this.reloadTime.ResetToZero();
            this.currentBullets = this.magazineSize;
        }

        public virtual void Draw(Vector2 offset)
        {
        }
        public virtual void Draw(Vector2 offset, Vector2 origin)
        {
            weaponIcon.Draw(offset, origin);
        }
    }
}

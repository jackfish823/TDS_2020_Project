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
        public int magazineSize, currentBullets;
        public bool sprayable;        

        public BasicWeapon(string path, Unit owner)
        {
            this.owner = owner;
            this.weaponIcon = new Basic2d(path, new Vector2(0, 0), new Vector2(181, 58));
            this.sprayable = false;
        }

        public virtual void Update(Vector2 offset)
        {

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

        protected void Reload()
        {
            this.reloadTime.ResetToZero();
            this.currentBullets = this.magazineSize;
        }

        public virtual void Draw(Vector2 offset, Vector2 origin)
        {
            weaponIcon.Draw(offset, origin);
        }
    }
}

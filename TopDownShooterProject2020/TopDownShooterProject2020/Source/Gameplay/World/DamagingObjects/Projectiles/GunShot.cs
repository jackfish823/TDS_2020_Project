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
    public class GunShot : BasicProjectile // inhereting from BasicProjectile
    {
        public GunShot(Vector2 position, AttackableObject owner) 
            : base("2d\\Projectiles\\gun_shot", position, new Vector2(16,73), owner)
        {
            this.speed = 15;
            this.timer = new BaseTimer(500);
        }

        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {

            base.Update(offset, objects);
        }
      
        public override void Draw(Vector2 offset)
        { 
            base.Draw(offset);
        }
    }
}   



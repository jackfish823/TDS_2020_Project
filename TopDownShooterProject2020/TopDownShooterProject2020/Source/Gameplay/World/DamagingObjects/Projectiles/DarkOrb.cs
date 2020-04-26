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
    public class DarkOrb : BasicProjectile // inhereting from BasicProjectile
    {
        Vector2 target;
        public DarkOrb(Vector2 position, AttackableObject owner, Vector2 target) 
            : base("2d\\Projectiles\\syndra_sphere", position, new Vector2(25,25), owner)
        {
            this.target = target;
            this.speed = 15;
            this.timer = new BaseTimer(500);

            this.direction = target - position;
            this.direction.Normalize();
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



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
        public GunShot(Vector2 position, Unit owner, Vector2 target) 
            : base(PathGlobals.GUN_SHOT_TEXTURE, position, new Vector2(5,26), owner, target)
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



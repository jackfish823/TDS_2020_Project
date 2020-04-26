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
    public class FireExplosionHitbox : InvisibleHitbox
    {
        public FireExplosionHitbox(Vector2 position, AttackableObject owner, int milliseconds) 
            : base(position, new Vector2(194, 194), owner, milliseconds)
        {
            GameGlobals.PassEffect(new FlameCircle(position, new Vector2(348, 445), milliseconds));
        }


        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {
            base.Update(offset, objects);
        }
        public override bool CollisionTest(List<AttackableObject> objects)
        {
            return false;
        }
        public override void Draw(Vector2 offset)
        { 
        }
    }
}   



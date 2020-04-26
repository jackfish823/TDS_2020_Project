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
    public class FireExplosion : Skill
    {
        public FireExplosion(AttackableObject owner) 
            : base(owner)
        {
            icon = new Animated2d("2d\\Misc\\fire_explosion_ICON", new Vector2(0, 0), new Vector2(40, 40), Globals.oneFrameOnly, Color.White);
        }

        public override void Targeting(Vector2 offset, Player enemy)
        {   
            if(selectionType == 1)
            {
                targetEffect.position = new Vector2(-1000, -1000);

                TargetingBase(offset);
            }
            else
            {
                if (Globals.mouse.LeftClickRelese())
                {
                    targetEffect.Done = true;

                    TargetingBase(offset);

                }
                else
                {
                    targetEffect.position = Globals.mouse.newMousePosition - offset;
                }
            }
        }

        public virtual void TargetingBase(Vector2 offset)
        {

            GameGlobals.PassDamaginObject(new FireExplosionHitbox(Globals.mouse.newMousePosition - offset, owner, 1122));

            Done = true;
            active = false;
        }

    }
}

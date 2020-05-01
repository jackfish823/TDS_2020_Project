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
    public class Supercharge : Skill
    {
        private float lastSpeed;
        private int lastFireRate;
        public Supercharge(AttackableObject owner) : base(owner)
        {
            targetEffect = null;
            owner.throbbing = true;
            owner.throbColor = Color.Goldenrod;
            owner.throbSpeed = 3;
            owner.throbTimer.Msec = 5000;
            lastSpeed = owner.speed;
            owner.speed = 2 * owner.speed;
            lastFireRate = ((MainCharacter)owner).weapons[0].fireDelay.Msec;
            ((MainCharacter)owner).weapons[0].fireDelay.Msec /= 2;
            ((MainCharacter)owner).frameAnimationList[((MainCharacter)owner).GetAnimationFromName("KnifeShoot")].frameTimer.Msec/=2;
        }

        public override void Update(Vector2 offset, Player enemy)
        {
            if(active && !Done)
            {
                if (!owner.throbbing)
                {
                    Done = true;                   
                    active = false;
                    owner.speed = lastSpeed;
                    ((MainCharacter)owner).weapons[0].fireDelay.Msec = lastFireRate;
                    ((MainCharacter)owner).frameAnimationList[((MainCharacter)owner).GetAnimationFromName("KnifeShoot")].frameTimer.Msec *= 2;

                }
                else
                {
                   
                }
            }
         
        }

     
    }
}

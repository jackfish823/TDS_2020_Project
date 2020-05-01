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
    public class Heal : Skill
    {
        private float currentTick, ticks;
        public Heal(AttackableObject owner) : base(owner)
        {
            targetEffect = null;
            owner.throbbing = true;
            owner.throbColor = Color.Green;
            owner.throbSpeed = 5;
            owner.throbTimer.Msec = 1500;
            currentTick = 0;
            ticks = 4;
        }

        public override void Update(Vector2 offset, Player enemy)
        {
            if(active && !Done)
            {
                if (!owner.throbbing)
                {
                    Done = true;                   
                    active = false;
                }
                else
                {
                    if (owner.health < owner.maxHealth && (owner.throbTimer.Timer >= owner.throbTimer.Msec * (currentTick / (ticks - 1))))
                    {
                        owner.health += 2;
                        if (owner.health > owner.maxHealth) owner.health = owner.maxHealth;
                        Globals.messageList.Add(new Message(new Vector2(owner.position.X + 20, owner.position.Y) + offset, new Vector2(200, 60), "+2hp", 1000, Color.Green, false));
                        currentTick++;
                    }
                }
            }
         
        }

     
    }
}

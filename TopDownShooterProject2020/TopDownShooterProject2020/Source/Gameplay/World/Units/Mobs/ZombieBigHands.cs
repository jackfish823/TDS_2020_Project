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
    public class ZombieBigHands : Mob
    {
        bool shot;
        public ZombieBigHands(Vector2 position, int ownerId) 
            : base("2d\\Units\\Mobs\\zombie_big_hands", position, new Vector2(250, 250), new Vector2(6, 4), ownerId)
        {
            attackRange = 400;

            shot = false;
            this.speed = 2;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 7, 150, 0, new Vector2(513*0.6f, 513 * 0.6f), "Attack"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(1, 1), 6, 100, 0, new Vector2(513 * 0.6f, 513 * 0.6f), "Death"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(1, 2), 9, 166, 0, new Vector2(513 * 0.6f, 513 * 0.6f), "Walk"));
            frameAnimations = true;
            SetAnimationByName("Walk");

        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            base.Update(offset, enemy, grid);
        }
        public override void AI(Player enemy, SquareGrid grid)
        {
            
            if(enemy.mainCharacter != null && (Globals.GetDistance(position, enemy.mainCharacter.position) < attackRange * 0.9f || isAttacking)) 
            {
                if (!isAttacking)
                {
                    shot = false;
                    isAttacking = true;
                    SetAnimationByName("Attack");
                    rotation = Globals.RotateToward(position + Vector2.Transform(new Vector2(0, -100), Matrix.CreateRotationZ(rotation)), enemy.mainCharacter.position);
                }
                else if (currentAnimation == 0)
                {
                    if(frameAnimationList[currentAnimation].CurrentFrame == 3 && !shot)
                    {
                        GameGlobals.PassDamaginObject(new AcitShot(position, this));
                        shot = true;
                    }
                    else if(frameAnimationList[currentAnimation].HasFinished())
                    {
                        SetAnimationByName("Walk");
                        isAttacking = false;
                    }
                }
            }
            else
            {
                // Walk towards the main character
                base.AI(enemy, grid);
            }
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

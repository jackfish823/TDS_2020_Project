#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class Mob : Unit
    {
        // explaing why threding

        public bool currentlyPathing, isAttacking, gaveGold;

        public float attackRange;

        private bool foundRange;
        private Vector2 goTo;


        public BaseTimer rePathTimer = new BaseTimer(200); //every 12 frames
        public Mob(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId) 
            : base(path, position, dimensions, frames, ownerId)
        {
            currentlyPathing = false;
            isAttacking = false;
            speed = 4;
            attackRange = 50;
            gaveGold = false;
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            GameGlobals.PassDebugInfo(new CirclePacket(this.position, 10, Color.Red));
            GameGlobals.PassDebugInfo(new TextPacket(Color.Black, health + " / " + maxHealth, new Vector2(position.X, position.Y - 50)));



            if (!dead)
            {
                AI(enemy, grid);
            }
            else
            {
                if(!gaveGold)
                {
                    GameGlobals.AddToInventory(new Gold(1));
                    gaveGold = true;
                }
                SetAnimationByName("Death");
            }
            base.Update(offset, enemy, grid);
        }

        public virtual void AI(Player enemy, SquareGrid grid) // a walk and hit character ai (not shoot and stuff) he will walk straight towards him, for shooting can add walk as song as its not x distance from him and if yes stop and shoot of not keep going
        {
            rePathTimer.UpdateTimer();
           
            if (pathNodes == null || (pathNodes.Count == 0 && position.X == moveTo.X && position.Y == moveTo.Y) || rePathTimer.Test())  // If it doesnt have a path find it or it needs to repath   
            {
                if(!currentlyPathing)
                {
                    Task repathTask = new Task(() =>
                    {
                        currentlyPathing = true;

                        if (Globals.GetDistance(this.position, enemy.mainCharacter.position) < 200)
                        {
                            pathNodes = FindPath(grid, grid.GetSlotFromPixel(enemy.mainCharacter.position, Vector2.Zero));
                        }
                        else
                        {
                            if (Globals.GetDistance(goTo, enemy.mainCharacter.position) > 150)
                            {
                                goTo = Vector2.Zero;
                                while (grid.GetSlotFromLocation(grid.GetSlotFromPixel(goTo, Vector2.Zero)).filled || goTo == Vector2.Zero)
                                {
                                    int angle = Globals.random.Next(360);
                                    float pathToX = enemy.mainCharacter.position.X + 150f * (float)Math.Cos(angle);
                                    float pathToY = enemy.mainCharacter.position.Y + 150f * (float)Math.Sin(angle);
                                    goTo = new Vector2(pathToX, pathToY);                                   
                                }                                
                            }
                            pathNodes = FindPath(grid, grid.GetSlotFromPixel(goTo, Vector2.Zero));
                        }


                        if (pathNodes.Count > 0)
                        {
                            moveTo = pathNodes[0];
                            pathNodes.RemoveAt(0);
                        }
                        else
                        {
                            Console.WriteLine("Cant reach ");
                        }
                        
                        rePathTimer.ResetToZero();

                        currentlyPathing = false;
                        
                        
                    });

                    repathTask.Start();
                }
            }
            else // If it has a path moving the unit and test for hitting things
            {
                MoveUnit();

                if (Globals.GetDistance(this.position, enemy.mainCharacter.position) < grid.slotDimensions.X) // If the mod hits the mainCharacter
                {
                    enemy.mainCharacter.GetHit(this, 1); // Just an example, can create var inside the mob for its Damage
                    this.dead = true;
                }
            }
            if (Globals.GetDistance(this.position, enemy.mainCharacter.position) > 200)
                GameGlobals.PassDebugInfo(new LinePacket(this.position, goTo, Color.Orange));
            GameGlobals.PassDebugInfo(new LinePacket(this.position, enemy.mainCharacter.position, Color.Green));
            GameGlobals.PassDebugInfo(new LinePacket(this.position, direction * 100 + position, Color.Red));
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset); // because the texture isnt square the origin is its head
        }
    }
}

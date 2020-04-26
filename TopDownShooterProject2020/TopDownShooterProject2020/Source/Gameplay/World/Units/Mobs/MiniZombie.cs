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
    public class MiniZombie : Mob
    {
        public BaseTimer spawnTimer;
        public MiniZombie(Vector2 position, Vector2 frames, int ownerId) 
            : base("2d\\Units\\Mobs\\level1_blue_zombie", position, new Vector2(150, 159), frames, ownerId)
        {          
            this.speed = 2.5f;

            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 9, 132, 0, new Vector2(248, 186), "Attack"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(1, 2), 6, 100, 0, new Vector2(248, 186), "Death"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(3, 3), 9, 66, 0, new Vector2(248, 186), "Walk"));
            frameAnimations = true;
            SetAnimationByName("Walk");
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            base.Update(offset, enemy, grid);
        }

        // Gets a player which it his enemy, goes through all of its buildings and checks if they are turrets, if it finds a turret.  it goes to it and hit it, change it to only priority buildings buy also go for the enemy after (for stuff that doesnt loose the game)
        public override void AI(Player enemy, SquareGrid grid)
        {

            Building temp = null;

            for (int i = 0; i < enemy.buildings.Count(); i++)
            {
                if (enemy.buildings[i].GetType().ToString() == "TopDownShooterProject2020.Turret") // If the enemy.building is turret class, change it in the end (like when we will load the classes form file)
                {
                    temp = enemy.buildings[i];
                }
            }

            if (temp != null)
            {
                if(pathNodes == null || (pathNodes.Count == 0 && position.X == moveTo.X && position.Y == moveTo.Y))  // If it doesnt have a path find it    
                {
                    pathNodes = FindPath(grid, grid.GetSlotFromPixel(temp.position, Vector2.Zero));
                    moveTo = pathNodes[0];
                    pathNodes.RemoveAt(0);
                }
                else // If it has a path moving the unit and test for hitting things
                {
                    MoveUnit();

                    if (Globals.GetDistance(this.position, temp.position) < grid.slotDimensions.X) // If the mod hits the mainCharacter
                    {   
                        temp.GetHit(this, 1); // Just an example, can create var inside the mob for its Damage
                        this.dead = true;
                    }
                }
               
                GameGlobals.PassDebugInfo(new LinePacket(this.position, direction * 100 + position, Color.Red));

            }
        }

        public override void Draw(Vector2 offset)
        {         
            
            base.Draw(offset);
        }
    }
}

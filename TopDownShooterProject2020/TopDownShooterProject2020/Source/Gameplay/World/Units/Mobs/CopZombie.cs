#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class CopZombie : Mob
    {
        public BaseTimer spawnTimer;
        public CopZombie(Vector2 position, int ownerId) 
            : base("2d\\Units\\Mobs\\cop_zombie", position, new Vector2(200, 200), new Vector2(4, 6), ownerId)
        {          
            this.speed = 1;
            this.health = 3;
            this.maxHealth = this.health;

            this.goldDrop = 3;


            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 9, 132, 0, new Vector2(248, 186), "Attack"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(1, 2), 6, 100, 0, new Vector2(248, 186), "Death"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(3, 3), 9, 166, 0, new Vector2(248, 186), "Walk"));
            frameAnimations = true;
            SetAnimationByName("Walk");
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            if (this.dead && !this.done)
            {
                SpawnZombieHouse();
                this.done = true;
            }
            base.Update(offset, enemy, grid);
        }

        public virtual void SpawnZombieHouse()
        {
            GameGlobals.passSpawnPoint(new MiniZombieHouse(this.position, new Vector2(1, 1), this.ownerId, null));
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

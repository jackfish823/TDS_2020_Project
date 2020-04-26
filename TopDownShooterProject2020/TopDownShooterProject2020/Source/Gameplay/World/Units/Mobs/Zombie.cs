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
    public class Zombie : Mob
    {
        public Zombie(Vector2 position, int ownerId) 
            : base("2d\\Units\\Mobs\\level1_green_zombie", position, new Vector2(150, 150), new Vector2(4, 6), ownerId)
        {
          
            this.speed = 2;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 9, 132, 0, new Vector2(248, 186), "Attack"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(1, 2), 6, 100, 0, new Vector2(248, 186), "Death"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(3, 3), 9, 166, 0, new Vector2(248, 186), "Walk"));
            frameAnimations = true;
            SetAnimationByName("Walk");
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            base.Update(offset, enemy, grid);
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

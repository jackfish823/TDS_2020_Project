#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#endregion


namespace TopDownShooterProject2020
{
    public class FlameCircle : BasicEffect
    {
        public FlameCircle(Vector2 position, Vector2 dimensions, int milliseconds)
            : base("2d\\Effects\\flameskill1", position, dimensions, new Vector2(5, 4), milliseconds)
        {
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 17, 66, 0, new Vector2(167, 282), "KnifeWalk"));
            currentAnimation = 0;
            frameAnimations = true;
            noTimer = true;
        }

        public override void Update(Vector2 offset)
        {
            if (frameAnimationList[currentAnimation].HasFinished())
            {

                done = true;
            }
            base.Update(offset);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}

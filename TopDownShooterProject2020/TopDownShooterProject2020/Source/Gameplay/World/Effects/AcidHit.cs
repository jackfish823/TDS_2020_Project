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
    public class AcidHit : BasicEffect
    {
        public AcidHit(Vector2 position, Vector2 dimensions, int milliseconds, float rotation)
            : base("2d\\Effects\\acid_hit", position, dimensions, new Vector2(3, 1), milliseconds)
        {
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 3, 66, 0, new Vector2(32, 4), "Splash"));
            currentAnimation = 0;
            frameAnimations = true;
            noTimer = true;
            this.rotation = rotation;
            
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

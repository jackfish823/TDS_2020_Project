#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class Animated2d : Basic2d    
    {
        public Vector2 frames; // How meny frames on the sheet, x = num of frames in each line, y = num of lines
        public List<FrameAnimation> frameAnimationList = new List<FrameAnimation>(); // List of all of the animations, like walk, run, shoot...
        public bool frameAnimations; // Are we using fame animations, allows to use animated2d like just a basic2d
        public int currentAnimation = 0; // The iteration of which animation we are currently using

        public Animated2d(string path, Vector2 position, Vector2 dimensions, Vector2 frames, Color color)
            : base(path, position, dimensions)
        {
            // Setting frames and frame size witht the property Frame
            Frames = new Vector2(frames.X, frames.Y);

            this.color = color;
        }

        #region Properties
        public Vector2 Frames
        {
            set
            {
                frames = value;
                if (texture != null)
                {
                    // Getting the frame size from the texture 
                    frameSize = new Vector2(texture.Bounds.Width / frames.X, texture.Bounds.Height / frames.Y);
                }
            }
            get
            {
                return frames;
            }            
        }
        #endregion

        public override void Update(Vector2 offset)
        {
            // Checking if we are using the frame animation, the list of frame animations isnt null and if the current animation we are using isnt greater then the list of animations (error check)
            if (frameAnimations && frameAnimationList != null && frameAnimationList.Count > currentAnimation)
            {
                // Updating the current animation
                frameAnimationList[currentAnimation].Update();
            }

            base.Update(offset);
        }

        // Gets the name of the animation and returns the index of it, if it wont find it, it will return -1
        public virtual int GetAnimationFromName(string animationName)
        {
            for(int i = 0; i < frameAnimationList.Count; i++)
            {
                if (frameAnimationList[i].animationName == animationName)
                {
                    return i;
                }
            }
            return -1;
        }


        // Sets animation by name
        public virtual void SetAnimationByName(string animationName)
        {
            int tempAnimation = GetAnimationFromName(animationName);

            if (tempAnimation != -1)
            {
                if (tempAnimation != currentAnimation)
                {
                    frameAnimationList[tempAnimation].Reset();
                }

                currentAnimation = tempAnimation;
            }

        }

        // Checks if we are using frame animations and             , if yes we draw the current animation and if not we just draw a basic2d
        public override void Draw(Vector2 screenShift)
        {
            if (frameAnimations && frameAnimationList[currentAnimation].TotalFrames > 0)
            {
                frameAnimationList[currentAnimation].Draw(texture, dimensions, frameSize, screenShift, position, rotation, color, new SpriteEffects());
            }
            else
            {
                base.Draw(screenShift);
            }
        }





    }
}

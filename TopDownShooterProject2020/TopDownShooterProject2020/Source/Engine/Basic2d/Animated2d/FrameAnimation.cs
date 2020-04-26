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
    public class FrameAnimation
    {
        public bool hasFired; // For a function that we want to call in a middle of an animation, ex: swinging knife, you want it to hit at a certen point and for that theres the hasFired
        protected int totalFrames, currentFrame, maxPasses, currentPass, fireFrame; // Max passes is how meny times the animation will be casted (usually 1 (passing 0))
        public string animationName;
        public Vector2 sheetDimensions, startFrame, sheetFrame, spriteDimensions, origin; // Sheet is the sprite sheet
        public BaseTimer frameTimer; // How long each frame shows for

        public PassObject fireAction;


        // Constructor if the animation doesnt have a fireAction
        public FrameAnimation(Vector2 spriteDimensions, Vector2 sheetDimensions, Vector2 startFrame, int totalFrames, int timePerFrame, int maxPasses, Vector2 origin, string animationName = "")
        {
            this.spriteDimensions = spriteDimensions;
            this.sheetDimensions = sheetDimensions;
            this.startFrame = startFrame;
            this.sheetFrame = new Vector2(startFrame.X, startFrame.Y); // Same as start frame but never changes
            this.totalFrames = totalFrames;
            this.currentFrame = 0;
            this.frameTimer = new BaseTimer(timePerFrame);
            this.maxPasses = maxPasses;
            this.currentPass = 0;
            this.animationName = animationName;
            this.origin = origin;
            this.fireAction = null;
            this.hasFired = false;
            this.fireFrame = 0;
        }

        // Constructor if the animation has a fireAction
        public FrameAnimation(Vector2 spriteDimensions, Vector2 sheetDimensions, Vector2 startFrame, int totalFrames, int timePerFrame, int maxPasses, int fireFrame, PassObject fireAction, Vector2 origin, string animationName = "")
        {
            this.spriteDimensions = spriteDimensions;
            this.sheetDimensions = sheetDimensions;
            this.startFrame = startFrame; // Same as sheet frame but never changes
            this.sheetFrame = new Vector2(startFrame.X, startFrame.Y); 
            this.totalFrames = totalFrames;
            this.currentFrame = 0;
            this.frameTimer = new BaseTimer(timePerFrame);
            this.maxPasses = maxPasses;
            this.currentPass = 0;
            this.animationName = animationName;
            this.origin = origin;
            this.fireAction = fireAction;
            this.hasFired = false;
            this.fireFrame = fireFrame;
        }

        #region Properties
        public int TotalFrames
        {
            get { return totalFrames; }
        }
        public int CurrentFrame
        {
            get { return currentFrame; }
            ///
            set { currentFrame = value;  }
        }
        public int CurrentPass
        {
            get { return currentPass; }
        }
        public int MaxPasses
        {
            get { return maxPasses; }
        }      
        #endregion

        public void Update()
        {
            // If there are more then 1 frame in the animation, update it                 draw the whole update illustration, draw like xy system and the spritesheet
            if (totalFrames > 1)
            {
                frameTimer.UpdateTimer();
                // If the timer get tested and its ready to get to the next frame, and maxPasses is 0 (theres only 1 pass) or currentPass is less then the max
                if (frameTimer.Test() && (maxPasses == 0 || maxPasses > currentPass))
                {
                    // Incremant the frame (move to the next frame)
                    currentFrame++;

                    // When the animation is completed
                    if (currentFrame >= totalFrames)
                    {
                        currentPass++;
                    }

                    // Checking if we are done running 
                    if (maxPasses == 0 || maxPasses > currentPass)
                    {
                        // Moveing to the next frame (incremanting the x)
                        sheetFrame.X += 1;

                        // If the x is overflowed (theres no next frame to the right)
                        if (sheetFrame.X >= sheetDimensions.X)
                        {
                            // Going back to the first x (frame) but going down a line (incremanting the y)
                            sheetFrame.X = 0;
                            sheetFrame.Y += 1;
                        }

                        // If the current frame is greater then the total frames of the animation
                        if (currentFrame >= totalFrames)
                        {
                            // Return to the first frame of the animation (current frame is 0 because it starts in 0 and sheetFrame will be at the start of the fram x,y)
                            currentFrame = 0;
                            hasFired = false;
                            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
                        }
                    }
                    // Reset our timer so we can go back and time our frames
                    frameTimer.Reset();
                }
            }

            // If the fireAction isnt null and we didnt fire it, and fireFrame equals to the current frame, fire it 
            if (fireAction != null && fireFrame == currentFrame && !hasFired)
            {
                fireAction(null);
                hasFired = true;
            }
        }

        // Resetting all of the basic variables
        public void Reset()
        {
            currentFrame = 0;
            currentPass = 0;
            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
            hasFired = false;
        }

        // Test if it the end of the animation, well be using it ex: after a knife swing check if the animation ended and if yes swich back to standing animation
        public bool HasFinished()
        {
            if (currentFrame + 1 >= totalFrames)
            {
                return true;
            }
            return false;
        }

        // Simmilar to the other draw in basic2d, the only thing here is that we use the sprite sheet information and the image dimensions a little different and offseting our target rectange by the sprite sheet 
        public void Draw(Texture2D texture, Vector2 dimensions, Vector2 imageDimansions, Vector2 screenShift, Vector2 position, float rotation, Color color,SpriteEffects spriteEffect)
        {
            Globals.spriteBatch.Draw(texture, new Rectangle((int)((position.X + screenShift.X)), (int)((position.Y + screenShift.Y)), (int)Math.Ceiling(dimensions.X), (int)Math.Ceiling(dimensions.Y)), new Rectangle((int)(sheetFrame.X * imageDimansions.X), (int)(sheetFrame.Y * imageDimansions.Y), (int)imageDimansions.X, (int)imageDimansions.Y), color, rotation, this.origin, spriteEffect, 0);

        }



    }
}

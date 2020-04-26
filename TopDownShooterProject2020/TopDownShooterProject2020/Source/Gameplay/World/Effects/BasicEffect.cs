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
    public class BasicEffect : Animated2d
    {
        protected bool done, noTimer;
        protected BaseTimer timer;      

        public BasicEffect(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int milliseconds) 
            : base (path, position, dimensions, frames, Color.White)
        {
            done = false;
            noTimer = false;
            timer = new BaseTimer(milliseconds);
        }

        #region Properties
        public bool Done { get => done; set => done = value; }
        public BaseTimer Timer { get => timer; set => timer = value; }
        #endregion

        // Basic effect: stay active for as long as the timer is and then be done
        public override void Update(Vector2 offset)
        {
            timer.UpdateTimer();
            if (timer.Test() && !noTimer)
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

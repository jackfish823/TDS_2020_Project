#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion


namespace TopDownShooterProject2020
{
    public class BaseTimer
    {
        public bool gooToGo; // A boolean variable to declare if it can fire right away ~fix explenation~ mostly for testing
        protected int milliseconds;
        protected TimeSpan timer = new TimeSpan();

        public BaseTimer(int milliseconds) // Constructing timer
        {
            this.gooToGo = false;
            this.milliseconds = milliseconds;
        }

        public BaseTimer(int milliseconds, bool rightAway) // Constructing timer with rightAway
        {
            this.gooToGo = rightAway;
            this.milliseconds = milliseconds;
        }

        public int Msec
        {
            get { return this.milliseconds; }
            set { this.milliseconds = value; }
        }

        public int Timer
        {
            get { return (int)this.timer.TotalMilliseconds; }
        }


        public void UpdateTimer() // Takes the timer and adds the elapsed time to it (adds the time it took from the last farm aka last time it called it usually)
        {
            this.timer += Globals.gameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float speed) // Allows you to change the speed ex: function that slows down / speeds up the game
        {
            this.timer += TimeSpan.FromTicks((long)(Globals.gameTime.ElapsedGameTime.Ticks * speed));
        }

        public void AddToTimer(int milliseconds) // Adds in the start of the game ex: using an item that has 20 sec cooldown, waiting 10 then leaving the game, coming back, this adds the 10 secs that left
                                                 // Basically used to load the timer proparly, ex: using an ability and then drinking a cooldown reducer potion can be used for delay also
        {
            this.timer += TimeSpan.FromMilliseconds((long)(milliseconds));
        }

        public bool Test() // Used to check if its time to do whatever ur timing with a goodToGo exception
        {
            if (this.timer.TotalMilliseconds >= this.milliseconds || this.gooToGo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset() // Resets the timer by subtracting
        {
            this.timer = this.timer.Subtract(new TimeSpan(0, 0, this.milliseconds / 60000, this.milliseconds / 1000, this.milliseconds % 1000));
            if (this.timer.TotalMilliseconds < 0)
            {
                this.timer = TimeSpan.Zero;
            }
            this.gooToGo = false;
        }

        public void Reset(int newTimer) // Resets timer with a given value (to be left over)
        {
            this.timer = TimeSpan.Zero;
            this.milliseconds = newTimer;
            this.gooToGo = false;
        }
        public void ResetToZero() // Resets to 0
        {
            this.timer = TimeSpan.Zero;
            this.gooToGo = false;
        }

        public virtual XElement ReturnXML() // Returning xml of the timer to save it
        {
            XElement xml = new XElement("Timer",
                                  new XElement("milliseconds", this.milliseconds),
                                  new XElement("timer", Timer));
            return xml;
        }

        public void SetTimer(TimeSpan time) // Sets the timer to a given TimeSpan
        {
            this.timer = time;
        }

        public virtual void SetTimer(int milliseconds) // Sets the timer to a given int
        {
            this.timer = TimeSpan.FromMilliseconds((long)(milliseconds));
        }

    }
}

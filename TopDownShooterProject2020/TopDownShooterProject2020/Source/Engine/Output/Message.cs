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
using TopDownShooterProject2020.Source.Engine.DataTypes.Forms;
#endregion

namespace TopDownShooterProject2020
{
    public class Message
    {
        public bool done, lockScreen;
        public Vector2 position, dimensions;
        public Color color;
        public TextZone textZone;
        public BaseTimer timer;

        public Message(Vector2 position, Vector2 dimensions, string message, int time, Color color, bool lockScreen)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.color = color;
            this.lockScreen = lockScreen;

            textZone = new TextZone(new Vector2(0, 0), message, (int)(this.dimensions.X * .9), 22, PathGlobals.ARIAL_FONT, color);

            done = false;
            timer = new BaseTimer(time);
        }

        public virtual void Update()
        {
            timer.UpdateTimer();
            if(timer.Test())
            {
                done = true;
            }
            textZone.position = new Vector2(textZone.position.X, textZone.position.Y - 1);

            // Fading the message over time
            textZone.color = color * (float)( .9f * (float)(timer.Msec - (float)timer.Timer) / (float)timer.Msec);
        }

        public virtual void Draw()
        {
            textZone.Draw(new Vector2(position.X - textZone.dimensions.X/2, position.Y));
        }

    }
}

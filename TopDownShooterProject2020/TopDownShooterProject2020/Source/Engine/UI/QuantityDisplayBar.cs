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
    public class QuantityDisplayBar

    {
        private int border;
        private Basic2d bar, barBackground, barShade;
        private Color color;
        private float barSize; // Bar size minus borders

        public Basic2d BarBackground { get => barBackground; }

        public QuantityDisplayBar(Vector2 dimensions, int border, Color color)
        {
            this.border = border;
            this.color = color;

            this.bar = new Basic2d("2d\\Misc\\health_bar_inside", new Vector2(0, 0), new Vector2(dimensions.X - this.border * 2, dimensions.Y - this.border * 2)); // Inner bar
            this.barShade = new Basic2d("2d\\Misc\\shade", new Vector2(0, 0), new Vector2(dimensions.X - this.border * 2, dimensions.Y - this.border * 2)); // Shade bar
            this.barBackground = new Basic2d("2d\\Misc\\health_bar_border", new Vector2(0, 0), new Vector2(dimensions.X, dimensions.Y)); // Background
            this.barSize = this.BarBackground.dimensions.X - border * 2;
        }


        public virtual void Update(float current, float max)    
        {
            this.bar.dimensions = new Vector2(current/max*(this.barSize), this.bar.dimensions.Y); // Getting % of the current thing used in the bar and multiplaying with the bar size minus borders 
        }


        public virtual void Draw(Vector2 offset)
        {
            this.bar.Draw(offset + new Vector2(this.border, this.border), new Vector2(0, 0), Color.Red); // Drawing the bar in between the borders

            this.bar.Draw(offset + new Vector2(this.border, this.border), new Vector2(0, 0), this.color); // Drawing the bar in between the borders

            this.BarBackground.Draw(offset, new Vector2(0, 0), Color.White); // Use color white if i pass a custom background (other then shade (temp art))


        }
    }
}

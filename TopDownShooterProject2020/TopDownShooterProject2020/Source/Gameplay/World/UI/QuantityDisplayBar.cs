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
        public int border;
        public Basic2d bar, barBackground;
        public Color color;
        public float barSize; // Bar size minus borders

        public QuantityDisplayBar(Vector2 dimensions, int border, Color color)
        {
            this.border = border;
            this.color = color;

            this.bar = new Basic2d("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(dimensions.X - this.border * 2, dimensions.Y - this.border * 2)); // Inner bar
            this.barBackground = new Basic2d("2d\\Misc\\shade", new Vector2(0, 0), new Vector2(dimensions.X, dimensions.Y)); // Background
            this.barSize = this.barBackground.dimensions.X - border * 2;
        }


        public virtual void Update(float current, float max)    
        {
            this.bar.dimensions = new Vector2(current/max*(this.barSize), this.bar.dimensions.Y); // Getting % of the current thing used in the bar and multiplaying with the bar size minus borders 
        }


        public virtual void Draw(Vector2 offset)
        {
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue(1.0f); 
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.Black.ToVector4()); // Because the background is black we wanna pass black
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();


            this.barBackground.Draw(offset, new Vector2(0,0), Color.Black); // Use color white if i pass a custom background (other then shade (temp art))

            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(this.color.ToVector4()); // Because whatever the color is bar we wanna pass it
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();

            this.bar.Draw(offset + new Vector2(this.border, this.border), new Vector2(0, 0), this.color); // Drawing the bar in between the borders
        }
    }
}

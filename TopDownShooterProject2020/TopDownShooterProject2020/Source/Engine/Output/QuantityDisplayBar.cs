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
        public indexer border;
        public Basic2d bar, barBckground;
        public Color color;

        public QuantityDisplayBar(Vector2 dimensions, int border, Color color)
        {
            this.border = border;
            this.color = color;
            
        }

     
    }
}

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
    public class TextPacket
    {
        private Vector2 position;
        private string text;
        private Color color;

        public TextPacket(Color color, string text, Vector2 position)
        {
            this.text = text;
            this.color = color;
            this.position = position;
        }
        public TextPacket(Color color, string text)
        {
            this.text = text;
            this.color = color;
            this.position = Vector2.Zero;
        }

        #region Properties
        public Color Color { get => color; set => color = value; }
        public string Text { get => text; set => text = value; }
        public Vector2 Position { get => position; set => position = value; }
        #endregion
    }
}

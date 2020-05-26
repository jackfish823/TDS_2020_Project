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
    public class LinePacket
    {
        private Vector2 source, target;
        private Color color;
        public LinePacket(Vector2 source, Vector2 target, Color color)
        {
            this.Source = source;
            this.Target = target;
            this.color = color;
        }

        #region Properties
        public Vector2 Source { get => source; set => source = value; }
        public Vector2 Target { get => target; set => target = value; }
        public Color Color { get => color; set => color = value; }
        #endregion
    }
}


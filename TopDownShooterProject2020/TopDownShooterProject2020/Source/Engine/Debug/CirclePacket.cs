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
    public class CirclePacket
    {
        private float radius;
        private Vector2 source;
        private Color color;
        public CirclePacket(Vector2 source, float radius, Color color)
        {
            this.source = source;
            this.Radius = radius;
            this.color = color;
        }

        #region Properties
        public Vector2 Source { get => source; set => source = value; }
        public Color Color { get => color; set => color = value; }
        public float Radius { get => radius; set => radius = value; }
        #endregion
    }
}

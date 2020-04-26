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
    public class TargetingCircle : BasicEffect
    {
        public TargetingCircle(Vector2 position, Vector2 dimesions)
            : base("2d\\Misc\\TargetCircle", position, dimesions, Globals.oneFrameOnly, 400)
        {
            noTimer = true;          
        }
    }
}

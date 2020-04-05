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
    public class Unit : Basic2d
    {

        public float speed;
        public Unit(string path, Vector2 position, Vector2 dimensions) : base (path, position, dimensions)
        {
            speed = 2;
        }

        public override void Update()
        {       
           
            base.Update();
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

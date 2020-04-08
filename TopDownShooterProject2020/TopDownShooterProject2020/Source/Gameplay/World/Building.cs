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
    public class Building: AttackableObject
    {
        public Building(string path, Vector2 position, Vector2 dimensions, int ownerId) : base (path, position, dimensions, ownerId)
        {

        }

        public override void Update(Vector2 offset, Player enemy)
        {       
           
            base.Update(offset);
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}

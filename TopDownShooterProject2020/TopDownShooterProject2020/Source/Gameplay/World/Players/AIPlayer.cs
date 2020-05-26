#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class AIPlayer : Player // Inherets from player
    {
        public AIPlayer(int id, XElement data)
            : base(id, data)
        {
           

        }

        public override void Update(Player enemy, Vector2 offset, SquareGrid grid)
        {    
            base.Update(enemy, offset, grid);
        }

        public override void ChangeScore(int score)
        {
            GameGlobals.Score += score;
        }



    }
}

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
            /*this.spawnPoints.Add(new ZombieHouse(new Vector2(40, 40), new Vector2(1, 1), id)); // Adding spawn point #1
             this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(100);

             this.spawnPoints.Add(new ZombieHouse(new Vector2(800, 40), new Vector2(1, 1), id)); // Adding spawn point #2
             this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(200);

             this.spawnPoints.Add(new ZombieHouse(new Vector2(1560, 40), new Vector2(1, 1), id)); // Adding spawn point #3
             this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

             this.spawnPoints.Add(new ZombieHouse(new Vector2(1560, 250), new Vector2(1, 1), id)); // Adding spawn point #4
             this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
*/
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

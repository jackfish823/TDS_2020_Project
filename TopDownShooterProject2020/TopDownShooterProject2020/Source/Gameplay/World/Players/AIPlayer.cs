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
    public class AIPlayer : Player // Inherets from player
    {

        public AIPlayer()
        {
            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(62, 79), new Vector2(120, 120))); // Adding spawn point #1
            this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(576, 67), new Vector2(120, 120))); // Adding spawn point #2
            this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(200);

            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(31, 268), new Vector2(120, 120))); // Adding spawn point #3
            this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(-500);

            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(428, -1), new Vector2(120, 120))); // Adding spawn point #4
        }

        public override void Update(Player enemy, Vector2 offset)
        {
            base.Update(enemy, offset);
        }

        public override void ChangeScore(int score)
        {
            GameGlobals.Score += score;
        }



    }
}

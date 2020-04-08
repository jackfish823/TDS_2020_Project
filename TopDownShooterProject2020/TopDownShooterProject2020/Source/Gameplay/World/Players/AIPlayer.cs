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
        public AIPlayer(int id)
            : base(id)
        {   
            for (int i = 0; i < 4; i++)
            {
                this.spawnPoints.Add(new ZombieHouse(new Vector2(Globals.random.Next(0, Globals.screenWidth), Globals.random.Next(0, Globals.screenHeight)), id)); // Adding spawn point #1
                this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(Globals.random.Next(0, 1000));
            }          
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

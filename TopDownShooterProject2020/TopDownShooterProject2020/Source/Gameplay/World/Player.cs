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
    public class Player
    {
        public MainCharacter mainCharacter; // Creating Main Charecter
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Player()
        {

        }


        public virtual void Update(Player enemy, Vector2 offset)
        {
            if (this.mainCharacter != null) // Wrapping it because ai players wont have a mainCharacter (it will be null) so it wont brak
            {
                this.mainCharacter.Update(offset);
            }


            // Spawn Points
            for (int i = 0; i < this.spawnPoints.Count; i++) // Running all over the SpawnPoints list, not using for each because i might add stuff later
            {
                this.spawnPoints[i].Update(offset);
            }

            // Units
            for (int i = 0; i < this.units.Count; i++) // Running all over the units list here instad of mobs, so even the user could have ally units
            {
                this.units[i].Update(offset, enemy);

                if (this.units[i].dead)
                {
                    ChangeScore(1); //Changes the score (add 1)
                    this.units.RemoveAt(i);
                    i--;
                }
            }


        }

        public virtual void AddUnit(object info) // Passing to the change score delegate
        {
            this.units.Add((Unit)info);
        }

        public virtual void ChangeScore(int score)
        {
        }

        public virtual void Draw(Vector2 offset) 
        {
            if (this.mainCharacter != null) // Wrapping it because ai players wont have a mainCharacter (it will be null) so it wont brak
            {
                this.mainCharacter.Draw(offset);
            }

            // Spawn points
            for (int i = 0; i < this.spawnPoints.Count; i++)
            {
                this.spawnPoints[i].Draw(offset);
            }

            // Units
            for (int i = 0; i < this.units.Count; i++) // Mobs
            {
                this.units[i].Draw(offset);
            }
           
        }


    }
}

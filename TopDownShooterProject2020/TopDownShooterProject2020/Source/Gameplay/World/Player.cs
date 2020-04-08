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
        public int id;
        public MainCharacter mainCharacter; // Creating Main Charecter
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
        public List<Building> buildings = new List<Building>();

        public Player(int id)
        {
            this.id = id;
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

                if (this.spawnPoints[i].dead)
                {
                    this.spawnPoints.RemoveAt(i);
                    i--;
                }
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

            // Buildings
            for (int i = 0; i < this.buildings.Count; i++) // Running all over the units list here instad of mobs, so even the user could have ally units
            {
                this.buildings[i].Update(offset, enemy);

                if (this.buildings[i].dead)
                {
                    ChangeScore(1); //Changes the score (add 1)
                    this.buildings.RemoveAt(i);
                    i--;
                }
            }


        }

        public virtual void AddUnit(object info) 
        {
            Unit tempUnit = (Unit)info; // remove when finalizing
            tempUnit.ownerId = id; // remove when finalizing
            this.units.Add(tempUnit); // remove when finalizing

            //this.units.Add((Unit)info);
        }

        public virtual void AddSpawnPoint(object info) 
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)info; // remove when finalizing
            tempSpawnPoint.ownerId = id; // remove when finalizing
            this.spawnPoints.Add(tempSpawnPoint); // remove when finalizing

            //this.spawnPoints.Add((SpawnPoint)info);
        }
        public virtual void ChangeScore(int score)
        {

        }

        // Returns a list of all of the attackable objects that "this" player has
        public virtual List<AttackableObject> GetAttackableObjects()
        {
            List<AttackableObject> tempObject = new List<AttackableObject>();
            tempObject.AddRange(this.units.ToList<AttackableObject>());
            tempObject.AddRange(this.spawnPoints.ToList<AttackableObject>());
            tempObject.AddRange(this.buildings.ToList<AttackableObject>());


            return tempObject;
        }

        public virtual void Draw(Vector2 offset) 
        {
            if (this.mainCharacter != null) // Wrapping it because ai players wont have a mainCharacter (it will be null) so it wont brak
            {
                this.mainCharacter.Draw(offset);
            }

            // Units
            for (int i = 0; i < this.units.Count; i++) // Mobs
            {
                this.units[i].Draw(offset);
            }

            // Buildings
            for (int i = 0; i < this.buildings.Count; i++) // Mobs
            {
                this.buildings[i].Draw(offset);
            }

            // Spawn points
            for (int i = 0; i < this.spawnPoints.Count; i++)
            {
                this.spawnPoints[i].Draw(offset);
            }
        }


    }
}

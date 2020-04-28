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
    public class Player
    {
        public int id, gold;
        public MainCharacter mainCharacter; // Creating Main Charecter
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();       
        public List<Building> buildings = new List<Building>();
        private Vector2 mainCharacterOriginalFrameSize = new Vector2(874, 826);
        public bool defeated;

        public Player(int id, XElement data)
        {
            this.id = id;
            gold = 100;
            defeated = false;           

            LoadData(data);
        }

        

        public virtual void Update(Player enemy, Vector2 offset, SquareGrid grid)
        {
            if (this.mainCharacter != null) // Wrapping it because ai players wont have a mainCharacter (it will be null) so it wont brak
            {
                this.mainCharacter.Update(offset, enemy, grid);
            }


            // Spawn Points
            for (int i = 0; i < this.spawnPoints.Count; i++) // Running all over the SpawnPoints list, not using for each because i might add stuff later
            {
                this.spawnPoints[i].Update(offset, enemy, grid);

                if (this.spawnPoints[i].dead)
                {
                    this.spawnPoints.RemoveAt(i);
                    i--;
                }
            }

            // Units
            for (int i = 0; i < this.units.Count; i++) // Running all over the units list here instad of mobs, so even the user could have ally units
            {
                this.units[i].Update(offset, enemy, grid);

                if (this.units[i].dead)
                {
                    ChangeScore(1); //Changes the score (add 1)
                    if(this.units[i].frameAnimations)
                    {
                        if (this.units[i].frameAnimationList[units[i].currentAnimation].HasFinished())
                        {
                            this.units.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        this.units.RemoveAt(i);
                        i--;
                    }
                   
                }
            }

            // Buildings
            for (int i = 0; i < this.buildings.Count; i++) // Running all over the units list here instad of mobs, so even the user could have ally units
            {
                this.buildings[i].Update(offset, enemy, grid);

                if (this.buildings[i].dead)
                {
                    ChangeScore(1); //Changes the score (add 1)
                    this.buildings.RemoveAt(i);
                    i--;
                }
            }

            CheckIfDefeated();
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
        public virtual void AddBuilding(object info)
        {
            Building tempBuilding = (Building)info; // remove when finalizing
            tempBuilding.ownerId = id; // remove when finalizing
            this.buildings.Add(tempBuilding); // remove when finalizing          
        }
        public virtual void CheckIfDefeated()
        {
            if(spawnPoints.Count <= 0 && units.Count <= 0)
            {
                defeated = true;
            }
        }
        public virtual void ChangeScore(int score)
        {

        }
        public virtual List<AttackableObject> GetAttackableObjects()// Returns a list of all of the attackable objects that "this" player has
        {
            List<AttackableObject> tempObject = new List<AttackableObject>();
            tempObject.AddRange(this.units.ToList<AttackableObject>());
            tempObject.AddRange(this.spawnPoints.ToList<AttackableObject>());
            tempObject.AddRange(this.buildings.ToList<AttackableObject>());

            if(mainCharacter != null)
            {
                tempObject.Add(mainCharacter);
            }

            return tempObject;
        }
        public virtual void LoadData(XElement data)
        {          
            List<XElement> spawnList = (from t in data.Descendants("SpawnPoint")
                                            select t).ToList<XElement>();

            Type sType = null; // A way to dynamically create a class from a string

            for (int i = 0; i < spawnList.Count; i++)
            {
                sType = Type.GetType("TopDownShooterProject2020." + spawnList[i].Element("type").Value, true); // Gets the type of the spawnlist

                // Activates this instance dynamiclly and put it into spawn point (much more intensive then hard coding), instad of like if spawnPoint is zombieHouse add zombie house else.....
                spawnPoints.Add((SpawnPoint)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(spawnList[i].Element("Position").Element("x").Value, Globals.culture), Convert.ToInt32(spawnList[i].Element("Position").Element("y").Value, Globals.culture)), Globals.oneFrameOnly, id, spawnList[i])));

             }      

            List<XElement> buildingList = (from t in data.Descendants("Building")
                                        select t).ToList<XElement>();

            for (int i = 0; i < buildingList.Count; i++)
            {
                sType = Type.GetType("TopDownShooterProject2020." + buildingList[i].Element("type").Value, true); // Gets the type of the spawnlist

                buildings.Add((Building)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(buildingList[i].Element("Position").Element("x").Value, Globals.culture), Convert.ToInt32(buildingList[i].Element("Position").Element("y").Value, Globals.culture)), Globals.oneFrameOnly, id)));
            }

            if(data.Element("MainCharacter") != null)
            {
                mainCharacter = new MainCharacter("2d\\Units\\male_character", new Vector2(Convert.ToInt32(data.Element("MainCharacter").Element("Position").Element("x").Value, Globals.culture), Convert.ToInt32(data.Element("MainCharacter").Element("Position").Element("y").Value, Globals.culture)), mainCharacterOriginalFrameSize / 3.5f, new Vector2(13, 10), id);
            }
        }
        public virtual void Draw(Vector2 offset) 
        {            
            // Buildings
            for (int i = 0; i < this.buildings.Count; i++) 
            {
                this.buildings[i].Draw(offset);
            }

            if (this.mainCharacter != null) // Wrapping it because ai players wont have a mainCharacter (it will be null) so it wont brak
            {
                this.mainCharacter.Draw(offset);
            }

            // Units
            for (int i = 0; i < this.units.Count; i++)
            {
                this.units[i].Draw(offset);
            }

            // Spawn points
            for (int i = 0; i < this.spawnPoints.Count; i++)
            {
                this.spawnPoints[i].Draw(offset);
            }
        }
    }
}

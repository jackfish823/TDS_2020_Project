#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
using System.Xml;
#endregion

namespace TopDownShooterProject2020
{
    public class World
    {
        // Basic offset for drawing
        public Vector2 offset;

        // User Interface
        public UI ui;

        // Players
        public User user;
        public AIPlayer aIPlayer;

        // The grid
        public SquareGrid grid;

        public CharacterMenu characterMenu;
        public ExitMenu exitMenu;
        public Shop shop;

        public TileBackground background;

        // Projectiles
        public List<DamagingObject> damagingObjects = new List<DamagingObject>();

        //public List<DebugInfo> debugInfoList = new List<DebugInfo>();
        public DebugInfo debugInfo = new DebugInfo();

        public int levelID;

        public List<AttackableObject> attackableObjects = new List<AttackableObject>();

        public List<SceneItem> sceneItems = new List<SceneItem>();

        public List<BasicEffect> effects = new List<BasicEffect>();


        // Weapon animations
        Animated2d weaponAnimation; // find better solutin then updating the timer here
        public List<Animated2d> weaponAnimations = new List<Animated2d>();

        // Reset World (Delegate) #explain more about delegates#
        PassObject ResetWorld, ChangeGameState, ChangePlayState;

        // World Constructor, Gets ResetWorld Function which just creates a new world
        public World(PassObject ResetWorld, int levelID, PassObject ChangeGameState, PassObject ChangePlayState)
        {
            this.levelID = levelID;

            // Reseting The World (creating new) 
            this.ResetWorld = ResetWorld;
            this.ChangeGameState = ChangeGameState;
            this.ChangePlayState = ChangePlayState;

            // Delegates and assigning the functions 
            GameGlobals.PassDamaginObject = PassDamaginObject;
            GameGlobals.PassMob = AddMob;
            GameGlobals.passSpawnPoint = AddSpawnPoint;
            GameGlobals.PassBuilding = AddBuilding;
            GameGlobals.CheckScroll = CheckScroll; // #1 delete when creating camera class
            GameGlobals.PassAnimation = AddWeaponAnimation;
            GameGlobals.PassDebugInfo = AddDebugInfo;
            GameGlobals.PassEffect = AddEffect;
            GameGlobals.PassGold = AddGold;


            LoadData(levelID);

            shop = new Shop(user.mainCharacter);
            characterMenu = new CharacterMenu(user.mainCharacter);
            exitMenu = new ExitMenu(ChangePlayState);


            // For later, offset now is 0
            this.offset = new Vector2(0, 0);

            // User Interface
            ui = new UI(ResetWorld, user.mainCharacter);

            background = new TileBackground("2d\\Tiles\\asphalt_tile_1", new Vector2(-100, -100), new Vector2(128, 128), new Vector2(grid.totalPhysicalDimensions.X, grid.totalPhysicalDimensions.Y));

            XDocument xml = Globals.save.GetFile("\\XML\\LevelsSave.xml");
            
            if(Globals.gameState == GameState.Game)
            {
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(200, 60),
                xml.Element("Root")
                   .Element("Levels")
                   .Element("Level")
                   .Element("name").Value, 3500, Color.GreenYellow, false));
            }

        }

        public virtual void Update()
        {
            // User interface
            ui.Update(this);

            if (!Globals.toggleLinesDebug)
            {
                debugInfo.Clear();
            }
            // If the main character is not dead, Update
            if (!DontUpdate())
            {
                // Updating the weapon antimation timer
                weaponAnimation = user.mainCharacter.weapons[(int)user.mainCharacter.currentWeaponSlot].fireAnimation;
                if (weaponAnimation != null)
                    weaponAnimation.frameAnimationList[weaponAnimation.currentAnimation].frameTimer.UpdateTimer();


                // Clearing every frame, Needs to be optimized its a dirty way
                this.attackableObjects.Clear();

                // Creating a list of all of the attackable objects
                this.attackableObjects.AddRange(this.user.GetAttackableObjects());
                this.attackableObjects.AddRange(this.aIPlayer.GetAttackableObjects());

                // Players
                this.user.Update(this.aIPlayer, offset, grid);
                this.aIPlayer.Update(this.user, offset, grid);


                // Weapon Animations
                for (int i = 0; i < this.weaponAnimations.Count; i++) // Projectiles
                {
                    this.weaponAnimations[i].Update(this.offset);

                    if (this.weaponAnimations[i].frameAnimationList[0].HasFinished())
                        this.weaponAnimations.RemoveAt(i);
                }

                // Damaging objects list
                for (int i = 0; i < this.damagingObjects.Count; i++)
                {
                    this.damagingObjects[i].Update(this.offset, this.attackableObjects); // Needs to be changed later, passing generic units list not only AI


                    if (this.damagingObjects[i].done)
                    {
                        this.damagingObjects.RemoveAt(i);
                        i--;
                    }
                }

                // Effects list
                for (int i = 0; i < this.effects.Count; i++)
                {
                    this.effects[i].Update(this.offset); // Needs to be changed later, passing generic units list not only AI


                    if (this.effects[i].Done)
                    {
                        this.effects.RemoveAt(i);
                        i--;
                    }
                }

                // Scene Items
                for (int i = 0; i < this.sceneItems.Count; i++)
                {
                    sceneItems[i].Update(offset);
                }


            }

            // If the main character is dead, stops updating and displays message
            else
            {
                if (Globals.keyboard.GetPressed("Enter")) //&& (this.user.mainCharacter.dead || this.user.buildings.Count() <= 0)
                {
                    this.ResetWorld(null);
                    GameGlobals.Score = 0;
                }
            }

            characterMenu.Update();
            exitMenu.Update();
            shop.Update();

            // Grid
            if (grid != null)
            {
                grid.Update(offset);
            }

            if (Globals.keyboard.GetSinglePress("Back"))
            {
                this.ResetWorld(null);
                this.ChangeGameState(GameState.MainMenu);

            }

            // To show grid or hide it
            if (Globals.keyboard.GetSinglePress("G"))
            {
                grid.showGrid = !grid.showGrid;
            }

            if (Globals.keyboard.GetSinglePress("C"))
            {
                shop.Active = !shop.Active;
            }

            if (Globals.keyboard.GetSinglePress(GameGlobals.keyBinds.GetKeyByName("Open Inventory")))
            {
                characterMenu.Active = !characterMenu.Active;
                exitMenu.Active = false;
            }


            if (Globals.keyboard.GetSinglePress("Escape"))
            {
                exitMenu.Active = !exitMenu.Active;
                characterMenu.Active = false;
            }

            if (aIPlayer.defeated)
            {
                XDocument xml = Globals.save.GetFile("\\XML\\LevelsSave.xml");

                xml.Element("Root")
                   .Element("Levels")
                   .Elements("Level")
                   .Where(x => x.Attribute("id").Value == levelID.ToString()).FirstOrDefault()
                   .SetElementValue("passed", "true");

                Globals.save.HandleSaveFormates(xml, "LevelsSave.xml");

                Globals.messageList.Add(new DismissibleMessage(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(250, 110), "Good jub", Color.GreenYellow, true, WinConfirm));
                //WinConfirm(null);

            }
        }



        #region Pass / Add Delegates
        public virtual void AddBuilding(object info)
        {
            Building tempBuilding = (Building)info;

            if (this.user.id == tempBuilding.ownerId)
            {
                this.user.AddBuilding(tempBuilding);
            }

            else if (this.aIPlayer.id == tempBuilding.ownerId)
            {
                this.aIPlayer.AddBuilding(tempBuilding);
            }
        }
        public virtual void AddMob(object info) // Adds mob to the unit list 
        {
            Unit tempUnit = (Unit)info;

            if (this.user.id == tempUnit.ownerId)
            {
                this.user.AddUnit(tempUnit);
            }

            else if (this.aIPlayer.id == tempUnit.ownerId)
            {
                this.aIPlayer.AddUnit(tempUnit);
            }
        }
        public virtual void PassDamaginObject(object info) // Adds damaing object to the list 
        {
            this.damagingObjects.Add((DamagingObject)info);
        }
        public virtual void AddEffect(object info)
        {
            this.effects.Add((BasicEffect)info);
        }
        public virtual void AddGold(object info)
        {
            PlayerValuePacket packet = (PlayerValuePacket)info;

            if (user.id == packet.PlayerID)
            {
                user.gold += (int)packet.Value;
            }

            else if (aIPlayer.id == packet.PlayerID)
            {
                aIPlayer.gold += (int)packet.Value;
            }
        }
        public virtual void AddDebugInfo(object info)
        {
            if (info is LinePacket)
                debugInfo.AddLine((LinePacket)info);
            else if (info is TextPacket)
                debugInfo.AddText((TextPacket)info);
            else
                debugInfo.AddCircle((CirclePacket)info);


        }
        public virtual void AddSpawnPoint(object info)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)info;

            if (this.user.id == tempSpawnPoint.ownerId)
            {
                this.user.AddSpawnPoint(tempSpawnPoint);
            }

            else if (this.aIPlayer.id == tempSpawnPoint.ownerId)
            {
                this.aIPlayer.AddSpawnPoint(tempSpawnPoint);
            }
        }
        public virtual void AddWeaponAnimation(object info) // weapon animations 
        {
            this.weaponAnimations.Add((Animated2d)info);
        }
        #endregion
   
        // Camera scrolling #1 delete when making a camera class
        public virtual void CheckScroll(object info)
        {
            Vector2 tempPosition = (Vector2)info;

            if (tempPosition.X < -this.offset.X + (Globals.screenWidth * 0.4f))
            {
                this.offset = new Vector2(offset.X + this.user.mainCharacter.speed, this.offset.Y);
            }

            if (tempPosition.X > -this.offset.X + (Globals.screenWidth * 0.6f))
            {
                this.offset = new Vector2(offset.X - this.user.mainCharacter.speed, this.offset.Y);
            }

            if (tempPosition.Y < -this.offset.Y + (Globals.screenHeight * 0.4f))
            {
                this.offset = new Vector2(offset.X, this.offset.Y + this.user.mainCharacter.speed);
            }

            if (tempPosition.Y > -this.offset.Y + (Globals.screenHeight * 0.6f))
            {
                this.offset = new Vector2(offset.X, this.offset.Y - this.user.mainCharacter.speed);
            }
        }

        public virtual bool DontUpdate()
        {
            if (this.user.mainCharacter.dead || this.user.buildings.Count() == 0 || shop.Active || characterMenu.Active || exitMenu.Active)
            {
                return true;
            }
            return false;
        }
        public void LoadData(int level)
        {
            XDocument xml = XDocument.Load("XML\\Levels\\Level" + level + ".xml");

            // Passing data from xml file to User
            XElement tempElement = null;
            if (xml.Element("Root").Element("User") != null)
            {
                tempElement = xml.Element("Root").Element("User");
            }
            this.user = new User(1, tempElement);

            if (user.mainCharacter != null)
            {
                GameGlobals.AddToInventory = user.mainCharacter.AddToInventory;
            }

            // Passing data from xml file to aIPlayer
            tempElement = null;
            if (xml.Element("Root").Element("AIPlayer") != null)
            {
                tempElement = xml.Element("Root").Element("AIPlayer");
            }

            grid = new SquareGrid(new Vector2(20, 20), new Vector2(-100, -100), new Vector2(Globals.screenWidth + 200, Globals.screenHeight + 200), xml.Element("Root").Element("GridItems"));

            aIPlayer = new AIPlayer(2, tempElement);


            List<XElement> sceneItemList = (from t in xml.Element("Root").Element("Scene").Descendants("SceneItem")
                                            select t).ToList<XElement>();

            Type sType = null;
            for (int i = 0; i < sceneItemList.Count; i++)
            {
                sType = Type.GetType("TopDownShooterProject2020." + sceneItemList[i].Element("type").Value, true); // Gets the type of the spawnlist

                sceneItems.Add((SceneItem)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(sceneItemList[i].Element("Position").Element("x").Value, Globals.culture), Convert.ToInt32(sceneItemList[i].Element("Position").Element("y").Value, Globals.culture)), new Vector2((float)Convert.ToDouble(sceneItemList[i].Element("scale").Value, Globals.culture)))));
            }
        }

        public virtual void WinConfirm(object info)
        {
            ResetWorld(null);
            ChangePlayState(PlayState.LevelsMap);
        }

        // Drawing all the things in world, gets offset and draws compared to the offset
        public virtual void Draw(Vector2 offeset)
        {
            background.Draw(offset);

            grid.DrawGrid(offset);

            debugInfo.Draw(DebugInfoType.Lines, this.offset);

            // Players
            this.user.Draw(this.offset);
            this.aIPlayer.Draw(this.offset);

            // Scene Items
            for (int i = 0; i < this.sceneItems.Count; i++)
            {
                sceneItems[i].Draw(offset);
            }

            // damagingObjects 
            for (int i = 0; i < this.damagingObjects.Count; i++) // Projectiles
            {
                this.damagingObjects[i].Draw(this.offset);
            }

            // Weapon animations
            for (int i = 0; i < this.weaponAnimations.Count; i++) // Projectiles
            {
                this.weaponAnimations[i].Draw(this.offset);
            }

            // Effects
            for (int i = 0; i < this.effects.Count; i++) // Projectiles
            {
                this.effects[i].Draw(this.offset);
            }

            debugInfo.Draw(DebugInfoType.Circles, this.offset);

            debugInfo.Draw(DebugInfoType.Text, this.offset);


            // UI
            this.ui.Draw(this);

            characterMenu.Draw();
            shop.Draw();
            exitMenu.Draw();
        }


    }
}

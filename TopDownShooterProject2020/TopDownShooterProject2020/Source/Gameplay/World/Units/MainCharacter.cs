#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class MainCharacter : Unit
    {
        public enum WeaponSlots
        {
            First,
            Second,
            Third,
            Fourth,
            Bat
        }

        public SkillBar skillBar;

        public List<BasicWeapon> weapons = new List<BasicWeapon>();

        public WeaponSlots currentWeaponSlot = WeaponSlots.First;

        bool sprayCheck;

        Vector2 tempvectest1, tempvectest2;

        private Vector2 lastOffset;
        private SquareGrid lastGrid;

        public Vector2 LastOffset { get => lastOffset; }
        public SquareGrid LastGrid { get => lastGrid; }

        public MainCharacter(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId)
            : base(path, position, dimensions, frames, ownerId)
        {
            this.tempvectest1 = Globals.baseVector;
            this.tempvectest2 = Globals.baseVector;

            this.speed = 3;
            this.health = 20;
            this.maxHealth = this.health;

            this.weapons.Add(new Knife(this));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(10, 7), 6, 132, 0, this.weapons[this.weapons.Count - 1].origin, "KnifeWalk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(11, 3), 8, 132, 0, this.weapons[this.weapons.Count - 1].origin, "KnifeStand"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(1, 5), 8, 33, 0, this.weapons[this.weapons.Count - 1].origin, "KnifeShoot"));


            this.weapons.Add(new Pistol(this));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(4, 7), 6, 132, 0, this.weapons[this.weapons.Count - 1].origin, "PistolWalk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(3, 3), 8, 132, 0, this.weapons[this.weapons.Count - 1].origin, "PistolStand"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(8, 1), 5, 166, 0, this.weapons[this.weapons.Count - 1].origin, "PistolShoot"));


            this.weapons.Add(new Rifle(this));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(3, 8), 6, 132, 0, this.weapons[this.weapons.Count - 1].origin, "RifleWalk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(6, 4), 8, 132, 0, this.weapons[this.weapons.Count - 1].origin, "RifleStand"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(9, 5), 9, 166, 0, this.weapons[this.weapons.Count - 1].origin, "RifleShoot"));


            this.weapons.Add(new Flamethrower(this));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(11, 6), 6, 132, 0, this.weapons[this.weapons.Count - 1].origin, "FlamethrowerWalk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(8, 2), 8, 132, 0, this.weapons[this.weapons.Count - 1].origin,  "FlamethrowerStand"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(12, 0), 9, 166, 0, this.weapons[this.weapons.Count - 1].origin, "FlamethrowerShoot"));

            this.weapons.Add(new Bat(this));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(5, 6), 6, 132, 0, this.weapons[this.weapons.Count - 1].origin, "BatWalk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 2), 8, 132, 0, this.weapons[this.weapons.Count - 1].origin, "BatStand"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), this.frames, new Vector2(0, 0), 12, 66, 0, this.weapons[this.weapons.Count - 1].origin, "BatShoot"));

            // Main character will have framAnimation, current will be 0
            frameAnimations = true;
            currentAnimation = 0;
            // Animation starts at(0,0), length is 4, 66ms = 15fps, maxPass is 0 means it will run forever and its name will be walk

            

       


            skillBar = new SkillBar(new Vector2(80, Globals.screenHeight - 60), 52, 5);

            CurrentSkill = new Skill(this);
            CurrentSkill.Done = true;
            CurrentSkill.Active = false;

            LoadData();
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            lastOffset = offset;
            lastGrid = grid;
            //this.testTimer.UpdateTimer(); // testing restriciton to spam slick
            // Find better solution
            weapons[(int)currentWeaponSlot].fireDelay.UpdateTimer();
            weapons[(int)currentWeaponSlot].reloadTime.UpdateTimer();

            BasicWeapon tempWeapon = weapons[(int)currentWeaponSlot];

            GameGlobals.PassDebugInfo(new CirclePacket(position, 200, Color.Red));
            GameGlobals.PassDebugInfo(new LinePacket(position, direction * 150 + position, Color.Red));
            GameGlobals.PassDebugInfo(new TextPacket(Color.White, "Player Position "+position));
            GameGlobals.PassDebugInfo(new TextPacket(Color.White, "Player Direction " + GetDirectionString(direction)));
            GameGlobals.PassDebugInfo(new TextPacket(Color.White, "Mouse Position "+Globals.mouse.newMousePosition));
            GameGlobals.PassDebugInfo(new TextPacket(Color.White, "Weapon: " + tempWeapon.GetType().Name));
            GameGlobals.PassDebugInfo(new TextPacket(Color.White, "Spawns: " + GameGlobals.spawns));



            if (tempWeapon is Pistol || tempWeapon is Rifle)
            {
                GameGlobals.PassDebugInfo(new LinePacket(position + tempWeapon.RotatedVectorTowardsMouse(), direction * 450 + position + tempWeapon.RotatedVectorTowardsMouse(), Color.PeachPuff));

            }
            else if (tempWeapon is Bat || tempWeapon is Knife)
            {
                tempvectest2 = Vector2.Transform(Globals.baseVector, Matrix.CreateRotationZ(rotation - 0.785398f));
                tempvectest1 = Vector2.Transform(Globals.baseVector, Matrix.CreateRotationZ(rotation + 0.785398f));

                GameGlobals.PassDebugInfo(new LinePacket(position, tempvectest1 * 150 + position, Color.BlueViolet));


                GameGlobals.PassDebugInfo(new LinePacket(position, tempvectest2 * 150 + position, Color.BlueViolet));              
            }


            bool checkScroll = false;

            #region Keypress

            Vector2 tempPos;
            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Left")))
            {
                tempPos = new Vector2(position.X - speed, position.Y);
                if (!grid.GetSlotFromLocation(grid.GetSlotFromPixel(tempPos, Vector2.Zero)).filled)
                {
                    this.position = tempPos;
                    checkScroll = true;
                }
                
            }

            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Right")))
            {
                tempPos = new Vector2(position.X + speed, position.Y);
                if (!grid.GetSlotFromLocation(grid.GetSlotFromPixel(tempPos, Vector2.Zero)).filled)
                {
                    this.position = tempPos;
                    checkScroll = true;
                }

            }

            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Up")))
            {
                tempPos = new Vector2(position.X, position.Y - speed);
                if (!grid.GetSlotFromLocation(grid.GetSlotFromPixel(tempPos, Vector2.Zero)).filled)
                {
                    this.position = tempPos;
                    checkScroll = true;
                }

            }

            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Down")))
            {
                tempPos = new Vector2(position.X, position.Y + speed);
                if (!grid.GetSlotFromLocation(grid.GetSlotFromPixel(tempPos, Vector2.Zero)).filled)
                {
                    this.position = tempPos;
                    checkScroll = true;
                }

            }

            if (Globals.mouse.RightClick() && grid.showGrid)
            {
                Vector2 tempLocation = grid.GetSlotFromPixel(Globals.mouse.newMousePosition, -offset); // Got the location from pixel
                List<GridLocation> locations = grid.GetSlotsFromLocationAndSize(tempLocation, new Vector2(223/20, 107/20));

               if (locations != null && !grid.CheckBlockFilled(locations) && !grid.CheckBlockImpassable(locations))
               {
                    grid.FillBlock(locations);

                    Building tempBuilding = new Couch(new Vector2(0, 0), Globals.oneFrameOnly, ownerId);

                    tempBuilding.position = grid.GetPositionFromLocation(tempLocation) + grid.slotDimensions / 2;

                    GameGlobals.PassBuilding(tempBuilding);
                }
            } 

        

            if (Globals.keyboard.GetSinglePress("T"))
            {
                Globals.toggleLinesDebug = !Globals.toggleLinesDebug;             
            }

            if (Globals.keyboard.GetSinglePress("O"))
            {
                GameGlobals.spawns = !GameGlobals.spawns;
            }
                    //"<Key name=\"Move Right\"><value>D</value></Key>" +

                  
            if (Globals.keyboard.GetPressed("D1"))
            {
                if (currentAnimation != GetAnimationFromName($"{tempWeapon.GetType().Name}Shoot") || frameAnimationList[currentAnimation].HasFinished())
                    currentWeaponSlot = WeaponSlots.First;
            }

            if (Globals.keyboard.GetPressed("D2"))
            {
                if (currentAnimation != GetAnimationFromName($"{tempWeapon.GetType().Name}Shoot") || frameAnimationList[currentAnimation].HasFinished())
                    currentWeaponSlot = WeaponSlots.Second;
            }

            if (Globals.keyboard.GetPressed("D3"))
            {
                if (currentAnimation != GetAnimationFromName($"{tempWeapon.GetType().Name}Shoot") || frameAnimationList[currentAnimation].HasFinished())
                    currentWeaponSlot = WeaponSlots.Third;
            }
            
            if (Globals.keyboard.GetPressed("D4"))
            {
                if (currentAnimation != GetAnimationFromName($"{tempWeapon.GetType().Name}Shoot") || frameAnimationList[currentAnimation].HasFinished())
                    currentWeaponSlot = WeaponSlots.Fourth;
            }

            if (Globals.keyboard.GetPressed("B"))
            {
                if (currentAnimation != GetAnimationFromName($"{tempWeapon.GetType().Name}Shoot") || frameAnimationList[currentAnimation].HasFinished())
                    currentWeaponSlot = WeaponSlots.Bat;
            }

            if (Globals.keyboard.GetSinglePress("R"))
            {
                weapons[(int)currentWeaponSlot].Reload();
            }
            #endregion

            // True when the character moves
            if (checkScroll)
            {
                GameGlobals.CheckScroll(this.position);
                if(currentAnimation != GetAnimationFromName("BatShoot") && currentAnimation != GetAnimationFromName("KnifeShoot") || frameAnimationList[currentAnimation].HasFinished())
                SetAnimationByName($"{tempWeapon.GetType().Name}Walk");               
            }
            // If we are not walking
            else
            {
                if (currentAnimation != GetAnimationFromName("BatShoot") && currentAnimation != GetAnimationFromName("KnifeShoot") || frameAnimationList[currentAnimation].HasFinished())
                SetAnimationByName($"{tempWeapon.GetType().Name}Stand");
            }

            rotation = Globals.RotateToward(position, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset); // #1 delete if camera (the -offset in the end)
                          

                if (tempWeapon.sprayable)
                {
                    if (Globals.mouse.LeftClickHold())
                    {

                        sprayCheck = true;
                    }

                    if (Globals.mouse.LeftClickRelese())
                    {
                        sprayCheck = false;
                    }

                    if (sprayCheck)
                    {
                        weapons[(int)currentWeaponSlot].Update(offset);
                    }

                }
                else
                {
                    if (Globals.mouse.LeftClick())
                    {
                        weapons[(int)currentWeaponSlot].Update(offset);

                        if (tempWeapon is Knife)
                        {
                            SetAnimationByName("KnifeShoot");
                            weapons[(int)currentWeaponSlot].Draw(offset);
                        }
                        else if (tempWeapon is Bat)
                        {
                            SetAnimationByName("BatShoot");
                        }
                    }
                }

            if(CurrentSkill != null)
            {
                CurrentSkill.Update(offset, enemy);
                if (CurrentSkill.Done)
                {
                    CurrentSkill.Reset();
                    CurrentSkill = null;
                }
            }
               
            

            if(Globals.mouse.RightClick() && CurrentSkill != null)
            {
                CurrentSkill.targetEffect.Done = true;
                CurrentSkill.Reset();
                CurrentSkill = null;
            }



            base.Update(offset, enemy, grid);

        }

        // for fun
        public string GetDirectionString(Vector2 direction)
        {
            if (direction.X < 0 && direction.Y < 0)
                return "North";
            if (direction.X < 0 && direction.Y > 0)
                return "West";
            if (direction.X > 0 && direction.Y < 0)
                return "East";
            if (direction.X > 0 && direction.Y > 0)
                return "South";
            else
                return "MEH";
        }

        public virtual void UseItem(object info)
        {
            if(info != null)
            {
                
                if (Inventory.SeachItemByName(((InventoryItem)info).Name) != null)
                {
                    if (Inventory.SeachItemByName(((InventoryItem)info).Name).amount > 0 && skillBar.cooldownTimer.Test())
                    {                       
                        ((InventoryItem)info).Use(this);

                        Inventory.RemoveItemFromInventory(((InventoryItem)info).Name);
                    }

                }
                else
                    Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(200, 60), "You do not have this item!", 1000, Color.LightSeaGreen, false));


            }
        }

        public virtual void LoadData()
        {
            XDocument xml = Globals.save.GetFile("\\XML\\PlayerInventorySave.xml");

            if (xml != null)
            {
                List<XElement> itemsXML = (from t in xml.Descendants("InventoryItem")
                                           select t).ToList<XElement>();
                Type sType = null; 

                for (int i = 0; i < itemsXML.Count; i++)
                {
                    sType = Type.GetType(itemsXML[i].Attribute("type").Value, true); 
                    Inventory.AddToInventory((InventoryItem)(Activator.CreateInstance(sType, Convert.ToInt32(itemsXML[i].Element("amount").Value, Globals.culture))));
                }
            }              
        }
        
      
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);

        }

    }
        
    }

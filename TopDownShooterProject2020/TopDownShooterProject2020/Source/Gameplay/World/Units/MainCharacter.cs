#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



            Skills.Add(new FireExplosion(this));



            skillBar = new SkillBar(new Vector2(80, Globals.screenHeight - 80), 52, 5);

            for(int i = 0; i < Skills.Count; i++)
            {
                if( i < skillBar.slots.Count)
                {
                    skillBar.slots[i].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), setSkill, Skills[i]);
                }
                else
                {
                    break;
                }
            }
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            
            //this.testTimer.UpdateTimer(); // testing restriciton to spam slick
            // Find better solution
            weapons[(int)currentWeaponSlot].fireDelay.UpdateTimer();
            weapons[(int)currentWeaponSlot].reloadTime.UpdateTimer();

            BasicWeapon tempWeapon = weapons[(int)currentWeaponSlot];

            GameGlobals.PassDebugInfo(new LinePacket(position, direction * 150 + position, Color.Red));
            GameGlobals.PassDebugInfo(new TextPacket(Color.Black, "Player Position "+position));
            GameGlobals.PassDebugInfo(new TextPacket(Color.Black, "Player Direction " + GetDirectionString(direction)));
            GameGlobals.PassDebugInfo(new TextPacket(Color.Black, "Mouse Position "+Globals.mouse.newMousePosition));
            GameGlobals.PassDebugInfo(new TextPacket(Color.Black, "Weapon: " + tempWeapon.GetType().Name));
            GameGlobals.PassDebugInfo(new TextPacket(Color.Black, "Spawns: " + GameGlobals.spawns));



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
            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Left")))
            {
                this.position = new Vector2(position.X - speed, position.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Right")))
            {
                this.position = new Vector2(position.X + speed, position.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Up")))
            {
                this.position = new Vector2(position.X, position.Y - speed);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed(GameGlobals.keyBinds.GetKeyByName("Move Down")))
            {
                this.position = new Vector2(position.X, position.Y + speed);
                checkScroll = true;
            }

            if (Globals.mouse.RightClick() && grid.showGrid)
            {
                Vector2 tempLocation = grid.GetSlotFromPixel(Globals.mouse.newMousePosition, -offset); // Got the location from pixel

                GridLocation location = grid.GetSlotFromLocation(tempLocation);
                GridLocation location2 = grid.GetNextSlotFromLocation(tempLocation);
                GridLocation location3 = grid.GetPrevSlotFromLocation(tempLocation);


                if (location != null && !location.filled && !location.impassable && !location2.filled && !location2.impassable && !location3.filled && !location3.impassable)
                {
                    location.SetToFilled(false);
                    location2.SetToFilled(false);
                    location3.SetToFilled(false);

                    Building tempBuilding = new Couch(new Vector2(0, 0), Globals.oneFrameOnly, ownerId);

                    tempBuilding.position = grid.GetPositionFromLocation(tempLocation) + grid.slotDimensions / 2;

                    GameGlobals.PassBuilding(tempBuilding);
                }
            } // add couch and test for fill 3

        

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
                          
            if(currentSkill == null)
            {
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
            }
            else
            {
                currentSkill.Update(offset, enemy);
                if(currentSkill.Done)
                {
                    currentSkill.Reset();
                    currentSkill = null;
                }
            }

            if(Globals.mouse.RightClick() && currentSkill != null)
            {
                currentSkill.targetEffect.Done = true;
                currentSkill.Reset();
                currentSkill = null;
            }


            skillBar.Update(Vector2.Zero);

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

        public virtual void setSkill(object info)
        {
            if(info != null)
            {
                SkillCastTypePacket tempPacket = (SkillCastTypePacket)info;

                currentSkill = tempPacket.skill;
                currentSkill.Active = true;
                currentSkill.selectionType = tempPacket.seletionType;
            }
        }
        

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
            skillBar.Draw(Vector2.Zero);

        }

    }
        
    }

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
    public class MainCharacter : Unit
    {
        //bool testflag = false; // teseting spray
        //BaseTimer testTimer = new BaseTimer(150); // testing restriciton to spam slick Change name to fire rate timer

        // Testing weapon class
        public enum WeaponSlots
        {
            First,
            Second,
            Third
        }

        public List<BasicWeapon> weapons = new List<BasicWeapon>();

        public WeaponSlots currentWeaponSlot = WeaponSlots.First;

        bool sprayCheck;

        //
        public MainCharacter(string path, Vector2 position, Vector2 dimensions, int ownerId) : base(path, position, dimensions, ownerId)
        {
            this.speed = 3;
            this.health = 5;
            this.maxHealth = this.health;

            this.weapons.Add(new Pistol(this));
            this.weapons.Add(new Rifle(this));
            this.weapons.Add(new Flamethrower(this));

        }

        public override void Update(Vector2 offset)
        {
            //this.testTimer.UpdateTimer(); // testing restriciton to spam slick

            // Find better solution
            weapons[(int)currentWeaponSlot].fireDelay.UpdateTimer();
            weapons[(int)currentWeaponSlot].reloadTime.UpdateTimer();


            bool checkScroll = false;

            if (Globals.keyboard.GetPressed("A"))
            {
                this.position = new Vector2(position.X - speed, position.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed("D"))
            {
                this.position = new Vector2(position.X + speed, position.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed("W"))
            {
                this.position = new Vector2(position.X, position.Y - speed);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed("S"))
            {
                this.position = new Vector2(position.X, position.Y + speed);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPressed("D1"))
            {
                currentWeaponSlot = WeaponSlots.First;
            }

            if (Globals.keyboard.GetPressed("D2"))
            {
                currentWeaponSlot = WeaponSlots.Second;
            }

            if (Globals.keyboard.GetPressed("D3"))
            {
                currentWeaponSlot = WeaponSlots.Third;
            }


            if (checkScroll)
            {
                GameGlobals.CheckScroll(this.position);
            }

            rotation = Globals.RotateToward(position, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset); // #1 delete if camera (the -offset in the end)
            

            // testin waapon class

            if (weapons[(int)currentWeaponSlot].sprayable)
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
                }
            }
            

           

            //


            /* spraying no class                           
                if (Globals.mouse.LeftClickHold()) // testing spray
                {
                    this.testflag = true;
                }

                if (Globals.mouse.LeftClickRelese()) // testing spray
                {
                    this.testflag = false;
                }


                if (this.testflag) // testing spray
                {
                    if (this.testTimer.Test())  // timer = fire rate
                    {
                        Vector2 offsetVector = new Vector2(8.59f, -48.43f); // Creating an offset vector so the sprite will come out of the gun 
                        offsetVector = Vector2.Transform(offsetVector, Matrix.CreateRotationZ(rotation)); // rotating the vector so it will be correct to any mouse position
                        GameGlobals.PassProjectile(new GunShot(new Vector2(position.X, position.Y) + offsetVector, this, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset));
                        this.testTimer.ResetToZero();
                    }       
                }
             */

            /* not spraying no class
             if (Globals.mouse.LeftClick())
              {             
                  if (this.test.Test()) // Testing restriciton to spam slick
                  {

                      Vector2 offsetVector = new Vector2(8.59f, -48.43f); // Creating an offset vector so the sprite will come out of the gun 
                      offsetVector = Vector2.Transform(offsetVector, Matrix.CreateRotationZ(rotation)); // rotating the vector so it will be correct to any mouse position
                      GameGlobals.PassProjectile(new GunShot(new Vector2(position.X, position.Y) + offsetVector, this, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset)); // #1 delete if camera (the -offset in the end)
                      this.test.ResetToZero();
                  }

                 // Vector2 offsetVector = new Vector2(8.59f, -48.43f); // Creating an offset vector so the sprite will come out of the gun 
                 // offsetVector = Vector2.Transform(offsetVector, Matrix.CreateRotationZ(rotation)); // rotating the vector so it will be correct to any mouse position
                 // GameGlobals.PassProjectile(new GunShot(new Vector2(position.X, position.Y) + offsetVector, this, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset)); // #1 delete if camera (the -offset in the end)
              }
              */

            base.Update(offset);
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset, new Vector2(246, 392)); // because the texture isnt square the origin is its head
        }
    }
}

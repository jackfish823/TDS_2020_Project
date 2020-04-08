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
        bool testflag = false; // teseting spray
        BaseTimer testTimer = new BaseTimer(150); // testing restriciton to spam slick Change name to fire rate timer
        public MainCharacter(string path, Vector2 position, Vector2 dimensions, int ownerId) : base(path, position, dimensions, ownerId)
        {
            this.speed = 3;
            this.health = 5;
            this.maxHealth = this.health;                     
        }

        public override void Update(Vector2 offset)
        {
            this.testTimer.UpdateTimer(); // testing restriciton to spam slick

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

            if (checkScroll)
            {
                GameGlobals.CheckScroll(this.position);
            }

            rotation = Globals.RotateToward(position, new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y) - offset); // #1 delete if camera (the -offset in the end)

           // if (Globals.mouse.LeftClickHold()) // Testing spray
           // {
           //     this.testflag = true;                
          //  }

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
          
            /*  if (Globals.mouse.LeftClick())
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

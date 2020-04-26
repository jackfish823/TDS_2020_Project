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
#endregion

namespace TopDownShooterProject2020
{
    public class BaseMouse
    {
        public bool dragging, rightDrag;

        public Vector2 newMousePosition, oldMousePosition, firstMousePosition, newMouseAdjustedPos, systemCursorPos, screenLoc; // Vector2's for the mouse positions, newMouseAdjustedPos for zooming, systemCursorPos and screenLoc for bounding the mouse in full screen 

        public MouseState newMouse, oldMouse, firstMouse; // Using 3 mouse states for dragging issues (first mouse get set when you click)

        public BaseMouse() // Constructing mouse and setting basic defaults: setting all mouse states the same starting one
        {
            dragging = false; // Dragging by default is false

            newMouse   = Mouse.GetState(); // Getting from monogames framework the mouse state 
            oldMouse   = newMouse; 
            firstMouse = newMouse; 

            newMousePosition   = new Vector2(newMouse.Position.X, newMouse.Position.Y); // Setting the mouse position as the new mouse position 
            oldMousePosition   = new Vector2(newMouse.Position.X, newMouse.Position.Y); 
            firstMousePosition = new Vector2(newMouse.Position.X, newMouse.Position.Y);

            GetMouseAndAdjust();

        }

        #region Properties
        public MouseState First
        {
            get { return firstMouse; }
        }

        public MouseState New
        {
            get { return newMouse; }
        }

        public MouseState Old
        {
            get { return oldMouse; }
        }
        #endregion

        public void Update()
        {
            GetMouseAndAdjust();

            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePosition = newMousePosition = GetScreenPos(firstMouse);
            }
        }

        public void UpdateOld() // Updates the oldMouse state as newMouse and its position (oldMousePosition)
        {
            oldMouse = newMouse;
            oldMousePosition = GetScreenPos(oldMouse);
        }

        public virtual float GetDistanceFromClick() // Returns the distance between the newMousePosition and the firstMousePosition (using the function GetDistance in globals) 
        {
            return Globals.GetDistance(newMousePosition, firstMousePosition);
        }
        public virtual void GetMouseAndAdjust() // Sets newMouse to the current state (from monogame framework - Mouse class) and sets the newMousePosition to the screen position  the mouse is pointing at
        {
            newMouse = Mouse.GetState();
            newMousePosition = GetScreenPos(newMouse);
        }

        public int GetMouseWheelChange() // Returns the differance between the newMouse state and the oldMouseState so it returns how much it changed
        {
            return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
        }

        public Vector2 GetScreenPos(MouseState mouse) // Gets MouseState and returning a vector2 with its position X and position Y
        {
            return new Vector2(mouse.Position.X, mouse.Position.Y);
        }

        public virtual bool LeftClick() // Checks if the left mouse button of newMouse is pressed and oldMouse is not and also if its in a legal position (inside the window)
                                        // When newMouse left button is pressed and oldMouse isnt, thats a new left click
        {
            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool LeftClickHold() // Checks if the left mouse button of newMouse and oldMouse is pressed and also if its in a legal position (inside the window)
                                            // When both newMouse and oldMouse left button is being pressed, its a hold of the left click
        {
            bool holding = false;

            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }



            return holding;
        }


        public virtual bool LeftClickRelese() // Checks if the left mouse button of newMouse is not pressed and oldMouse is, and also if its in a legal position (inside the window)
                                              // When newMouse left button is not pressed and oldMouse is, thats a relese of the left click 
        {
            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }
        public virtual bool RightClick() // Checks if the Right mouse button of newMouse is pressed and oldMouse is not and also if its in a legal position (inside the window)
                                         // When newMouse Right button is pressed and oldMouse isnt, thats a new Right click
        {
            if (newMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool RightClickHold() // Checks if the Right mouse button of newMouse and oldMouse is pressed and also if its in a legal position (inside the window)
                                             // When both newMouse and oldMouse Right button is being pressed, its a hold of the Right click
        {
            bool holding = false;

            if (newMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;
                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }

            return holding;
        }

        public virtual bool RightClickRelese() // Checks if the Right mouse button of newMouse is not pressed and oldMouse is, and also if its in a legal position (inside the window)
                                               // When newMouse Right button is not pressed and oldMouse is, thats a relese of the Right click 
        {
            if (newMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released && oldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }
        public void SetFirst()
        {

        }
    }
}

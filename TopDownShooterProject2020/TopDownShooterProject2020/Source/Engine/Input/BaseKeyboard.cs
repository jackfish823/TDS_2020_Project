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
    public class BaseKeyboard
    {
        public KeyboardState newKeyboard, oldKeyboard; // 2 keyboard satates: whats being pressed and whats already been pressed

        public List<BaseKey> pressedKeys = new List<BaseKey>(), previousPressedKeys = new List<BaseKey>();

        public BaseKeyboard ()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState(); // Getting the pressed key from monogames framework

            GetPressedKeys();
        }

        public void UpdateOld () // Using at a end of a frame, taking the new state and making it the old state, and preparing it to start the next frame updaing the new state
        {
            oldKeyboard = newKeyboard; // Coping the new keyboard state to old keyboard state

            previousPressedKeys = new List<BaseKey>(); // Creating new list of baseKeys for the previous pressed keys
            
            for (int i = 0; i < pressedKeys.Count; i ++) // Running all over the pressed keys list
            {
                previousPressedKeys.Add(pressedKeys[i]); // Transfering all of the pressed keys to previous pressed keys list
            }
        }

        public bool GetPressed(string key) // Gets a list of BaseKey of keys being pressed (like: a-f-2-g) and a key name and searches if the key name is being pressed (in the pressed keys list), returns true or false
        {
            for (int i = 0; i < pressedKeys.Count; i++) // runs all over the pressed keys list
            {
                if (pressedKeys[i].key == key) // Compares each key in the list to the given key name
                {
                    return true; // Returns true if it found it
                }
            }

            return false; // Return false of it didnt find it
        }


        public virtual void GetPressedKeys()
        {
            pressedKeys.Clear(); // Cleans the list of current pressed keys so we can have a new one

            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++) // Runs all over the newKeyboard state 
            {
                pressedKeys.Add(new BaseKey(newKeyboard.GetPressedKeys()[i].ToString(), 1)); // Adds each pressed key in the newKeyboard state to the pressed keys list
            }


        }
    }
}

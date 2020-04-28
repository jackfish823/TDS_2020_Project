#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class SkillBar
    {
        public float spacer;
        public Vector2 firstPosition;
        public List<InventoryButtonSlot> slots = new List<InventoryButtonSlot>();

        public SkillBar(Vector2 firstPosition, float spacer, int numSlots)
        {
            this.spacer = spacer;
            this.firstPosition = firstPosition;
            
            for(int i = 0; i < numSlots; i++)
            {
                slots.Add(new InventoryButtonSlot(new Vector2(0, 0)));
            }
        }


        public virtual void Update(Vector2 offset)
        {
            for(int i = 0; i <slots.Count; i++)
            {
                if(slots[i].InventoryButton != null)
                {
                    if (((InventoryItem)slots[i].InventoryButton.info).amount <= 0 && ((InventoryItem)slots[i].InventoryButton.info).Name != "Gold")
                    {
                        slots[i].InventoryButton = null;


                    }
                }
                
                slots[i].Update(firstPosition + new Vector2(spacer * i, 0));
            }

            if(Globals.keyboard.GetSinglePress("Q"))
            {
                if(slots.Count > 0 && slots[0].InventoryButton != null)
                {
                    slots[0].InventoryButton.RunButtonClick();  
                }
            }

            if (Globals.keyboard.GetSinglePress("E"))
            {
                if (slots.Count > 1 && slots[1].InventoryButton != null)
                {
                    slots[1].InventoryButton.RunButtonClick();
                }
            }

            if (Globals.keyboard.GetSinglePress("R"))
            {
                if (slots.Count > 2 && slots[2].InventoryButton != null)
                {
                    slots[2].InventoryButton.RunButtonClick();
                }
            }
        }

        public virtual void Draw(Vector2 offset)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Draw(firstPosition + new Vector2(spacer * i, 0));
            }
        }
    }
}

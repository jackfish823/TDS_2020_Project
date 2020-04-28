#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
#endregion


namespace TopDownShooterProject2020
{
    public class Inventory
    {
        private List<InventoryItem> items = new List<InventoryItem>();
        public Inventory()
        {
            Items.Add(new Gold(0));
        }

        public List<InventoryItem> Items { get => items; }

        public void AddToInventory(InventoryItem item)
        {
            InventoryItem tempItem = SeachItemByName(item.Name);

            if (tempItem != null)
            {
                tempItem.amount += item.amount;
                return;
            }           
            
            Items.Add(item);
            DeleteEmptyItems();
        }

        public InventoryItem SeachItemByName(string itemName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (itemName == Items[i].Name)
                {
                    return Items[i];
                }               
            }
            return null;
        }

        private void DeleteEmptyItems()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(Items[i].amount <= 0 && Items[i].Name != "Gold")
                {
                    Items.RemoveAt(i);
                }
            }
        }

        public void RemoveItemFromInventory(string itemName)
        {
            InventoryItem tempItem = SeachItemByName(itemName);

            if (tempItem != null)
            {
                tempItem.amount--;
                DeleteEmptyItems();
            }
        }
    }
}

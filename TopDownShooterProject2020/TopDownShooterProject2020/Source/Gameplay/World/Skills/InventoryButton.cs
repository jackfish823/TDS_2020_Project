#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class InventoryButton : BasicButton
    {
        public Vector2 lastOffset;
        public InventoryItem inventoryItem;
        public InventoryButtonSlot slot;

        public InventoryButton(string path, Vector2 position, Vector2 dimensions, PassObject ButtonClicked, object info) 
            : base ("2d\\Misc\\shade", position, dimensions,"", "", ButtonClicked, info)
        {
            inventoryItem = (InventoryItem)info;
            slot = null;
        }


        public override void Update(Vector2 offset)
        {
            lastOffset = offset;

            if(inventoryItem != null)
            {
                base.Update(offset);
            }
        }

        public override void RunButtonClick()
        {
           
                    ButtonClicked((InventoryItem)info);
            

            Reset();
        }

        public override void Draw(Vector2 offeset)
        {           
            base.Draw(offeset);

            if(inventoryItem != null)
            {
                inventoryItem.Icon.Draw(offeset);
            }

        }
    }
}

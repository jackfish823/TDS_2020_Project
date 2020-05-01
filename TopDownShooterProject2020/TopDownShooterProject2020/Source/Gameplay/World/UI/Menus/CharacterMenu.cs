#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020
{ 
    public class CharacterMenu : BasicMenu
    {
        InventoryItem selectedItem;
        private MainCharacter mainCharacter;
        Color color;
        public CharacterMenu(MainCharacter mainCharacter) : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 500), null)
        {
            this.mainCharacter = mainCharacter;
        }

        public override void Update()
        {
            if (active)
            {
                for (int i = 0; i < mainCharacter.Inventory.Items.Count; i++)
                {
                    if (mainCharacter.Inventory.Items[i].Icon.Hover(topLeft + new Vector2(50, 110) + new Vector2((i % 4) * 80, (i / 4) * 60)))
                    {
                        if (Globals.mouse.LeftClick() && mainCharacter.Inventory.Items[i].Name != "Gold")
                        {
                            selectedItem = mainCharacter.Inventory.Items[i];
                        }
                    }
                }


                if(selectedItem != null)
                {
                    if (Globals.keyboard.GetSinglePress("Q"))
                    {
                        mainCharacter.skillBar.slots[0].InventoryButton = new InventoryButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), mainCharacter.UseItem, selectedItem);
                    }

                    if (Globals.keyboard.GetSinglePress("E"))
                    {
                        mainCharacter.skillBar.slots[1].InventoryButton = new InventoryButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), mainCharacter.UseItem, selectedItem);

                    }

                    if (Globals.keyboard.GetSinglePress("R"))
                    {
                        mainCharacter.skillBar.slots[2].InventoryButton = new InventoryButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), mainCharacter.UseItem, selectedItem);

                    }
                }
                
            }
            else
            {
                selectedItem = null;
            }
            base.Update();
        }

        public override void Close(object info)
        {            
            base.Close(info);
        }

        public override void Draw()
        {
            base.Draw();

            if(Active)
            {
                Globals.CleanShader();
                string tempString = "Inventory";
                Vector2 strDimensions = font.MeasureString(tempString);
                Globals.spriteBatch.DrawString(font, tempString, topLeft + new Vector2(background.dimensions.X / 2 - strDimensions.X / 2, 40), Color.GreenYellow);

                for (int i = 0; i < mainCharacter.Inventory.Items.Count; i++)
                {
                    Color tempColor = Color.White;
                    if (mainCharacter.Inventory.Items[i] == selectedItem) tempColor = Color.Gray;
                    Animated2d tempIcon = new Animated2d("2d\\Misc\\solid", mainCharacter.Inventory.Items[i].Icon.position, new Vector2(50, 50), Globals.oneFrameOnly, tempColor);
                    tempIcon.texture = mainCharacter.Inventory.Items[i].Icon.texture;
                    tempIcon.Draw(topLeft + new Vector2(50, 110) + new Vector2((i % 4) * 80, (i / 4) * 60)); // 4 each row mod the rows and devide the columns classic western type
                    tempString = mainCharacter.Inventory.Items[i].amount +" ";
                    Vector2 strDims = font.MeasureString(tempString);
                    Globals.spriteBatch.DrawString(font, tempString, (topLeft + new Vector2(50, 140)  + new Vector2((i % 4) * 80, (i / 4) * 60)) - new Vector2(strDims.X/2, 0), Color.GreenYellow);

                }

            }
        }
    }
}

#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020
{ 
    public class Shop : BasicMenu
    {
        private MainCharacter mainCharacter;

        private List<InventoryItem> shopItems = new List<InventoryItem>();
        private List<BasicButton> itemButtons = new List<BasicButton>();

        public Shop(MainCharacter mainCharacter) : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(700, 700), null)
        {
            this.mainCharacter = mainCharacter;

            // Everything that the shop sells
            shopItems.Add(new PlasmaCannonItem(1));
            shopItems.Add(new FireExplosionItem(1));
            shopItems.Add(new HealthKitItem(1));
            shopItems.Add(new SuperChargeItem(1));


            for (int i = 0; i < shopItems.Count; i++)
            {
                itemButtons.Add(new BasicButton("2d\\Misc\\solid", topLeft + new Vector2(50, 110) + new Vector2((i % 4) * 80, (i / 4) * 60), new Vector2(40, 40), PathGlobals.ARIAL_FONT, "", Buy, shopItems[i]));
                itemButtons[itemButtons.Count - 1].texture = shopItems[i].Icon.texture;       
            }

        }

        public override void Update()
        {
            if (active)
            {
                for (int i = 0; i < itemButtons.Count; i++)
                {
                    itemButtons[i].Update(Vector2.Zero);
                }

            }
            base.Update();
        }


        private void Buy(object info)
        {
            InventoryItem tempItem = (InventoryItem)info;
            InventoryItem characterGold = mainCharacter.Inventory.SeachItemByName("Gold");
            
            if (characterGold.amount >= tempItem.Price)
            {
                characterGold.amount -= tempItem.Price;

                switch(tempItem.Name)
                {
                    case "Plasma Cannon":
                        mainCharacter.Inventory.AddToInventory(new PlasmaCannonItem(1));
                        break;
                    case "Fire Explosion":
                        mainCharacter.Inventory.AddToInventory(new FireExplosionItem(1));
                        break;
                    case "Health Kit":
                        mainCharacter.Inventory.AddToInventory(new HealthKitItem(1));
                        break;
                    case "Supercharge":
                        mainCharacter.Inventory.AddToInventory(new SuperChargeItem(1));
                        break;
                }
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(500, 60), $"{tempItem.Name} Added to your inventory!", 1000, Color.LightSeaGreen, false));
                Globals.soundControl.PlaySound("PurchaseSound", true);

            }
            else
            {
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(500, 60), "You do not have enough gold!", 1000, Color.LightSeaGreen, false));
            }
        }

        public override void Draw()
        {
            base.Draw();

            if(Active)
            {
                Globals.CleanShader();
                string tempString = "Shop";
                Vector2 strDimensions = font.MeasureString(tempString);
                Globals.spriteBatch.DrawString(font, tempString, topLeft + new Vector2(background.dimensions.X / 2 - strDimensions.X / 2, 40), Color.GreenYellow);

                for (int i = 0; i < itemButtons.Count; i++)
                {
                    itemButtons[i].Draw(Vector2.Zero);
                    tempString = $"{shopItems[i].Price} Gold";
                    strDimensions = font.MeasureString(tempString);
                    Globals.spriteBatch.DrawString(font, tempString, new Vector2(itemButtons[i].position.X - strDimensions.X/2, itemButtons[i].position.Y + 25), Color.GreenYellow);
                }

            }
        }
    }
}

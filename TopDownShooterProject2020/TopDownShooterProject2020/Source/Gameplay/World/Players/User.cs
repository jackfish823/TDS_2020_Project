#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class User : Player // Inhertes from player class - this is the user (gamer) class
    {
        private Vector2 mainCharacterOriginalFrameSize = new Vector2(874, 826);
        public User(int id, XElement data)
            : base(id, data) // will be 1
        {
            //this.mainCharacter = new MainCharacter("2d\\Units\\male_character", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), mainCharacterOriginalFrameSize/3.5f, new Vector2(13, 10), id);

            //this.buildings.Add(new Turret(this.mainCharacter.position + new Vector2(100,178), new Vector2(1, 1), id)); // Eventually the tower pos will be from a save file not like this
        }

        public override void Update(Player enemy, Vector2 offset, SquareGrid grid)
        {
            base.Update(enemy, offset, grid);

            if(Globals.keyboard.GetSinglePress("D0"))
            {
                mainCharacter.Inventory.AddToInventory(new PlasmaCannonItem(1));
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(200, 60), "Added Plasma Cannon", 1000, Color.LightSeaGreen, false));
            }

            if (Globals.keyboard.GetSinglePress("D9"))
            {
                mainCharacter.Inventory.AddToInventory(new FireExplosion(mainCharacter));
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(200, 60), "Added Plasma Cannon", 1000, Color.LightSeaGreen, false));
            }

            if (Globals.keyboard.GetSinglePress("D8"))
            {
                mainCharacter.Inventory.AddToInventory(new Gold(100));
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(200, 60), "Added 100 Gold!", 1000, Color.LightSeaGreen, false));
            }
        }
    }
}

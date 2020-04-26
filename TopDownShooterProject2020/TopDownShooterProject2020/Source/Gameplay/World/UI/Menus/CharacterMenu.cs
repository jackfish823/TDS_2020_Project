#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020
{ 
    public class CharacterMenu : BasicMenu
    {
        private MainCharacter mainCharacter;
        public CharacterMenu(MainCharacter mainCharacter) : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 500), null)
        {
            this.mainCharacter = mainCharacter;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            if(Active)
            {
                Globals.CleanShader();
                string tempString = "Inventory";
                Vector2 strDimensions = font.MeasureString(tempString);
                Globals.spriteBatch.DrawString(font, tempString, topLeft + new Vector2(background.dimensions.X/2 - strDimensions.X/2, 40), Color.GreenYellow);

            }
        }
    }
}

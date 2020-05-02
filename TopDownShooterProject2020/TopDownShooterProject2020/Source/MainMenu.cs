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
    public class MainMenu
    {
        private Basic2d background, title;
        private PassObject PlayClickDelegate, ExitClickDelegate;
        private List<BasicButton> buttons = new List<BasicButton>();

        public MainMenu(PassObject PlayClickDelegate, PassObject ExitClickDelegate)
        {
            this.PlayClickDelegate = PlayClickDelegate;
            this.ExitClickDelegate = ExitClickDelegate;
           
            this.background = new Basic2d("2d\\SplashArts\\MainMenuSplash", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            this.title = new Basic2d("2d\\Misc\\title", new Vector2(Globals.screenWidth / 2, 150), new Vector2(1010, 129));


            this.buttons.Add(new BasicButton("2d\\Misc\\Button_g2", new Vector2(0, 0), new Vector2(406, 67), PathGlobals.ARIAL_FONT, "Play", PlayClickDelegate, GameState.Game, Color.GreenYellow));
            this.buttons.Add(new BasicButton("2d\\Misc\\Button_g2", new Vector2(0, 0), new Vector2(406, 67), PathGlobals.ARIAL_FONT, "Options", PlayClickDelegate, GameState.OptionMenu, Color.GreenYellow));
            this.buttons.Add(new BasicButton("2d\\Misc\\Button_g2", new Vector2(0, 0), new Vector2(406, 67), PathGlobals.ARIAL_FONT, "Exit", ExitClickDelegate, null, Color.GreenYellow));


        }

        public void Update()
        {
            Globals.optionsMenu.prevGameState = GameState.MainMenu;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(new Vector2(327, 503 + 80 * i));
            }


        }

        public void Draw()
        {
            background.Draw(Vector2.Zero);
            title.Draw(Vector2.Zero);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(new Vector2(327, 503 + 80 * i));
            }
        }
    }
}

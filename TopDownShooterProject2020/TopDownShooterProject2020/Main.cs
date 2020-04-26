using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using TopDownShooterProject2020.Source.Engine.Output;

namespace TopDownShooterProject2020
{
    public class Main : Game
    {
        bool lockUpdate;

        GraphicsDeviceManager graphics;

        Gameplay gameplay; // Main gameplay

        MainMenu mainMenu;

        Basic2d cursor; // The Cursor for the mouse

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;

            //graphics.HardwareModeSwitch = false;

            Globals.appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            lockUpdate = false;
        }


        protected override void Initialize()
        {
            Globals.screenWidth = 1600; // Sereen width
            Globals.screenHeight = 900; // screen height

            graphics.PreferredBackBufferWidth = Globals.screenWidth;
            graphics.PreferredBackBufferHeight = Globals.screenHeight;

            graphics.ApplyChanges();

            base.Initialize();
        }    
        protected override void LoadContent() 
        {
            Globals.content = this.Content;                              // Using content from globals clss
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);       // Using sprite batch from globals class


            Globals.save = new Save(1, "League Of Zombies 13");
            Globals.optionsMenu = new OptionsMenu(ApplyOptions);

            SetFullScreen();

            Globals.keyboard = new BaseKeyboard();                       // Adding the keyboard input to the game
            Globals.mouse = new BaseMouse();                             // Adding the mouse input to the game

            cursor = new Basic2d(PathGlobals.CURSOR_TEXTURE, new Vector2(0,0), new Vector2(17,25)); // Setting the cursor to 0,0 so the offset that is the mouse position will take it to the place (illustration)

            Globals.antiAliasingEffect = Globals.content.Load<Effect>("Effects\\AntiAliasingShader");
            Globals.throbEffect = Globals.content.Load<Effect>("Effects\\Throb");


            if (File.Exists(Globals.appDataFilePath + "\\" + Globals.save.gameName + "\\XML\\KeyBinds.xml"))
            {
                GameGlobals.keyBinds = new KeyBindList(Globals.save.GetFile("\\XML\\KeyBinds.xml"));
            }
            else
            {
                // make the file - start xml (basic) (default)
                XDocument keyBindXML = XDocument.Parse("<Root><Keys>" +
                    "<Key name=\"Move Up\"><value>W</value></Key>" +
                    "<Key name=\"Move Left\"><value>A</value></Key>" +
                    "<Key name=\"Move Down\"><value>S</value></Key>" +
                    "<Key name=\"Move Right\"><value>D</value></Key>" +
                    
                    "</Keys></Root>");

                // save the file
                Globals.save.HandleSaveFormates(keyBindXML, "KeyBinds.xml");

                // load the file
                GameGlobals.keyBinds = new KeyBindList(Globals.save.GetFile("\\XML\\KeyBinds.xml"));
            } 

            mainMenu = new MainMenu(ChangeGameState, ExitGame);
            gameplay = new Gameplay(ChangeGameState);                                         // Creating the world

            Window.Title = "League of zombies 5";

            Globals.soundControl = new SoundControl("Audio\\MP Caped Crusader");
            Globals.soundControl.AddSound("Shoot", "Audio\\rifle_shoot_sound", 1);
            Globals.soundControl.AddSound("Reload", "Audio\\rifle_reload_sound", 1);
            Globals.soundControl.AddSound("Hit", "Audio\\Health Hit 3", 1);


            
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            Globals.gameTime = gameTime; // Provides the milliseconds from the last frame and if it lags it sets it to the correct farme

            // inputs update

            Globals.keyboard.Update();
            Globals.mouse.Update();

            ////////////////

            lockUpdate = false;

            for(int i = 0; i < Globals.messageList.Count; i++)
            {
                if(!Globals.messageList[i].done)
                {
                    Globals.messageList[i].Update();
                    if (Globals.messageList[i].lockScreen)
                    {
                        lockUpdate = true;
                    }
                }
                else
                {
                    Globals.messageList.RemoveAt(i);
                    i--;
                }
                
              
            }

            if(!lockUpdate)
            {
                if (Globals.gameState == GameState.MainMenu)
                {
                    mainMenu.Update();
                }
                else if (Globals.gameState == GameState.Game)
                {
                    gameplay.Update();
                }
                else if (Globals.gameState == GameState.OptionMenu)
                {
                    Globals.optionsMenu.Update();
                }
            }         

            // end of frame old inputs update

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            /////////////////////////////////
            

            base.Update(gameTime); // End of frame
        }

        public virtual void ApplyOptions(object info)
        {
            FormOption musicVolume = Globals.optionsMenu.GetOptionValue("Music Volume");
            float musicVolumePrecent = 1.0f;

            if (musicVolume != null)
            {
                musicVolumePrecent = (float)Convert.ToDecimal(musicVolume.value, Globals.culture) / 30f; // 30 max volume
            }


            Globals.soundControl.adjustVolume(musicVolumePrecent);

            SetFullScreen();
        }

        public void SetFullScreen()
        {
            FormOption fullScreen = Globals.optionsMenu.GetOptionValue("Full Screen");
            if(Convert.ToInt32(fullScreen.value, Globals.culture) == 1)
            {
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.IsFullScreen = false;
            }

            graphics.ApplyChanges();
        }
        public virtual void ChangeGameState(object info)
        {
            Globals.gameState = (GameState)info;
            if((GameState)info == GameState.Game)
                Globals.soundControl.changeMusic("Audio\\MP Eerie");
            else
                Globals.soundControl.changeMusic("Audio\\MP Caped Crusader");

        }
        public virtual void ExitGame(object info)
        {
            System.Environment.Exit(0);
        }
        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.CornflowerBlue);


            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); // usually choosing in spritebachmode.Deferred means: "ok you feed me all of your textures and ill going to run the shader form at the end of the frame" and it tried to batch them all together the best it can and make it as efficient it can
                                                                                        // Here we use immediate: it draws it immidiately to the back buffer and that way you can sit there and change out your shader however you want, allows us to do waht we need to to do in order to run our anti aliasing  
            if (Globals.gameState == GameState.MainMenu)
            {
                mainMenu.Draw();
            }
            else if (Globals.gameState == GameState.Game)
            {
                gameplay.Draw();
            }
            else if (Globals.gameState == GameState.OptionMenu)
            {
                Globals.optionsMenu.Draw();
            }

            #region MakeFunction(resets the effect) // last thing we drew was the quantity bar so it got his effect
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue(1.0f); // For texts we just pass in 1 as value so they wont get changed (1 > 0.6) because texts gets funky when doing this on them (we have to pass all of these for the fx to work so just makr it 1 wont change anything)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue(1.0f); // Although we dont wanna change anything we have to pass everything else
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
            #endregion  
           
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.cursor.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.cursor.texture.Bounds.Width);
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.cursor.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.cursor.dimensions.Y));
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();


            for(int i = 0; i < Globals.messageList.Count; i++)
            {
                Globals.messageList[i].Draw();
            }

            cursor.Draw(new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y), new Vector2(0, 0)); // Drawing the mouse with an offset of the mouse position and origin 0,0 (top left) (illustration)

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    #region Main
#if WINDOWS || LINUX
    public static class Program
    {
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
#endif
    #endregion
}

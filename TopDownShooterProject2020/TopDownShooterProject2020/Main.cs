using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterProject2020
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Gameplay gameplay; // Main gameplay

        Basic2d cursor; // The Cursor for the mouse

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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


            Globals.keyboard = new BaseKeyboard();                       // Adding the keyboard input to the game
            Globals.mouse = new BaseMouse();                             // Adding the mouse input to the game

            cursor = new Basic2d(PathGlobals.CURSOR_TEXTURE, new Vector2(0,0), new Vector2(17,25)); // Setting the cursor to 0,0 so the offset that is the mouse position will take it to the place (illustration)

            Globals.antiAliasingEffect = Globals.content.Load<Effect>("Effects\\AntiAliasingShader");

            gameplay = new Gameplay();                                         // Creating the world
        }

      
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime; // Provides the milliseconds from the last frame and if it lags it sets it to the correct farme

            // inputs update

            Globals.keyboard.Update();
            Globals.mouse.Update();

            ////////////////

            Window.Title = $"Mouse X: {Globals.mouse.newMouse.Position.X}, Mouse Y: {Globals.mouse.newMouse.Position.Y}."; // Showing mouse position and the top window title

            gameplay.Update();


            // end of frame old inputs update
            
            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            /////////////////////////////////
            

            base.Update(gameTime); // End of frame
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); // usually choosing in spritebachmode.Deferred means: "ok you feed me all of your textures and ill going to run the shader form at the end of the frame" and it tried to batch them all together the best it can and make it as efficient it can
            // Here we use immediate: it draws it immidiately to the back buffer and that way you can sit there and change out your shader however you want, allows us to do waht we need to to do in order to run our anti aliasing


            gameplay.Draw();

            #region MakeFunction(resets the effect) // last thing we drew was the quantity bar so it got his effect
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue(1.0f); // For texts we just pass in 1 as value so they wont get changed (1 > 0.6) because texts gets funky when doing this on them (we have to pass all of these for the fx to work so just makr it 1 wont change anything)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue(1.0f); // Although we dont wanna change anything we have to pass everything else
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
            #endregion  
           
            // Drawing cursor
            cursor.Draw(new Vector2(Globals.mouse.newMousePosition.X, Globals.mouse.newMousePosition.Y), new Vector2(0, 0)); // Drawing the mouse with an offset of the mouse position and origin 0,0 (top left) (illustration)

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }


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

}

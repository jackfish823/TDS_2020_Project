using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterProject2020
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world; // Main world

        Basic2d cursor; // The Cursor for the mouse

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            Globals.screenWidth = 800; // Sereen width
            Globals.screenHeight = 500; // screen height

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

            cursor = new Basic2d("2d\\Misc\\Cursor", new Vector2(0,0), new Vector2(17,25)); // Setting the cursor to 0,0 so the offset that is the mouse position will take it to the place (illustration)

            world = new World();                                         // Creating the world
        }

      
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime; // Provides the milliseconds from the last frame

            // inputs update

            Globals.keyboard.Update();
            Globals.mouse.Update();

            ////////////////


            world.Update();


            // end of frame old inputs update
            
            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            /////////////////////////////////
            

            base.Update(gameTime); // End of frame
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);



            world.Draw(Vector2.Zero);

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

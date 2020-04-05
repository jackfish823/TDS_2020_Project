using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterProject2020
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            base.Initialize();
        }

      
        protected override void LoadContent() 
        {
            Globals.content = this.Content;                              // Using content from globals clss
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);       // Using sprite batch from globals class

            Globals.keyboard = new BaseKeyboard();
            Globals.mouse = new BaseMouse();

            world = new World();
        }

      
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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



            world.Draw();



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

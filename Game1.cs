using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;





namespace Monogam
{   
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        IntroState introState;
        //GraphicsDeviceManager graphics;
        RenderTarget2D renderTarget;
        SamplerState mysample;
        SpriteBatch spriteBatch;
        static Stack<GameState> GameStateStack;
        float xscale;
        float yscale;
        //Vector2 screenratio;


        bool f1ready = true;
        bool f2ready = true;

        Matrix transformmatrix;


        public void PopState()
        {
            GameStateStack.Pop();
        }

        public void PushState(GameState gameState)
        {
            GameStateStack.Push(gameState);
        }

        public void PeekState()
        {
            GameStateStack.Peek();
        }

        public void ChangeState()
        {
            if (GameStateStack.Peek().NextState != 0)
            {
                if (GameStateStack.Peek().NextState == GameStates.Gameplay)
                {
                    GameStateStack.Pop();
                    GameStateStack.Push(new GameplayState(Content, graphics.GraphicsDevice));
                }
            }
        }

        public Game1()
        {
            this.IsMouseVisible = true;
            IsFixedTimeStep = true; //turn fixed timestep off
            graphics = new GraphicsDeviceManager(this);
           // TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60); //set target framerate
            TargetElapsedTime = new TimeSpan(0,0,0,0, 16); //set target framerate
            graphics.SynchronizeWithVerticalRetrace = false; //turn vsync off
            Content.RootDirectory = "Content";
            GameStateStack = new Stack<GameState>();

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            graphics.IsFullScreen = false;


            //create a samplerstate and enable mipmap levels!
            mysample = new SamplerState();
            mysample.MaxMipLevel = 13;
            mysample.Filter = TextureFilter.Linear;
            // mysample.MipMapLevelOfDetailBias = 1;



        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            GameState.contentManager = Content;

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);


            Console.WriteLine("initialising!");
            base.Initialize();


            xscale = graphics.PreferredBackBufferWidth / 1280;
            yscale = graphics.PreferredBackBufferHeight / 720;

            Console.WriteLine("Initial buffers: " + graphics.PreferredBackBufferWidth + " " + graphics.PreferredBackBufferHeight);

            transformmatrix = Matrix.CreateScale(xscale, yscale, 1);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            introState = new IntroState(Content, "720p", GraphicsDevice);
            PushState(introState);
            // mytext = Content.Load<Texture2D>("Sprites/Scorpion");
            // TODO: use this.Content to load your game content here


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Update(GameTime gameTime)
        {

            //  if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            // Exit();

            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.F1) && f1ready)
            {
                f1ready = false;
                f2ready = true;

                xscale = graphics.PreferredBackBufferWidth / 1920.0f;
                yscale = graphics.PreferredBackBufferHeight / 1080.0f;

                transformmatrix = Matrix.CreateScale(xscale, yscale, 1.0f);

                graphics.PreferredBackBufferWidth = 1920;
                graphics.PreferredBackBufferHeight = 1080;

                graphics.ApplyChanges();

                //reasign render target with updated settings
                renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

                GameStateStack.Peek().ChangeResolution(Content, "1080");

            }


            ///////////////////////////////
            if (kbState.IsKeyDown(Keys.F2) && f2ready)
            {
                f2ready = false;
                f1ready = true;

                Console.WriteLine("current buffers: " + graphics.PreferredBackBufferWidth + " " + graphics.PreferredBackBufferHeight);

                xscale = graphics.PreferredBackBufferWidth / 1280;
                yscale = graphics.PreferredBackBufferHeight / 720;

                transformmatrix = Matrix.CreateScale(xscale, yscale, 1.0f);
                Console.WriteLine("xscale yscale: " + xscale + " " + yscale);

                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;

                //apply resolution change
                graphics.ApplyChanges();

                //reasign render target with updated settings
                renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);


                GameStateStack.Peek().ChangeResolution(Content, "720");

            }

            GameStateStack.Peek().HandleEvents();
            //GameStateStack.Peek().Update(gameTime, transformmatrix);
            GameStateStack.Peek().Update(gameTime);
            ChangeState();

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //set render target as what we draw to, then clear screen
            GraphicsDevice.SetRenderTarget(renderTarget);

            //*** Resolution Independence with Virtual Screen**/
            //draw to render target, note we are using our custom sample state for mipmaps!
            //insert transformationmatrix as the last argument in the begin call to enable scaling, it still has collision issues
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, null);
            GameStateStack.Peek().Draw(spriteBatch);
            //GameStateStack.Peek().Draw(spriteBatch, transformmatrix); //make scaling right
            spriteBatch.End();

            //finish drawing to render target
            GraphicsDevice.SetRenderTarget(null);

            //clear screen then begin drawing render target to screen
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, null, mysample, null, null, null, null);
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();

            //base.Draw(gameTime);
        }
    }
}

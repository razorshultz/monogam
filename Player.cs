using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogam
{
    class Player : Entity
    {
        public Player(ContentManager Content, string name, float x, float y) : base(Content, name, x, y)
        {
            acceleration = new Vector2(0.2f, 0.2f);
            friction = 0.1f;
            maxvelocity = 1.5f;
            screenratio = new Vector2(1.0f, 1.0f);
            mSourceRect = new Rectangle(0, 400, 182, 131);
            mInterval = 200f;
            mTimer = 0f;

            mIdleMaxY = 1065;
            mIdleYFrameIncrement = 133;

            // mTexFilename = "/Sprites/invadersheet";

            mTexFilename = "invadersheet";
        }




        public override void HandleEvents()
        {
            kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Left))
            {
                leftpressed = true;
            }

            if (!kbState.IsKeyDown(Keys.Left))
            {
                leftpressed = false;
            }

            if (kbState.IsKeyDown(Keys.Right))
            {
                rightpressed = true;
            }

            if (!kbState.IsKeyDown(Keys.Right))
            {
                rightpressed = false;
            }

            if (kbState.IsKeyDown(Keys.Up))
            {
                uppressed = true;
            }

            if (!kbState.IsKeyDown(Keys.Up))
            {
                uppressed = false;
            }

            if (kbState.IsKeyDown(Keys.Down))
            {
                downpressed = true;
            }

            if (!kbState.IsKeyDown(Keys.Down))
            {
                downpressed = false;
            }

        }


        //deprecated, the graphicsdevice no longer needs passing in as it's now a globally available static variable
        public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {   //playing around with velocity verlet integration
            Vector2 oldvel;
            oldvel = velocity;

            PlayIdleAnimation(gameTime);
            

            //screenratio = new Vector2(graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight) / new Vector2(1920, 1080);

            //mSourceRect.Y = 0;
            if (leftpressed)
            {
                velocity.X -= acceleration.X;
               // mSourceRect.Y = 267;
            }

            if (rightpressed)
            {
                velocity.X += acceleration.X;
               // mSourceRect.Y = 400;
            }

            if (velocity.X > maxvelocity)
                velocity.X = maxvelocity;

            if (velocity.X < -maxvelocity)
                velocity.X = -maxvelocity;


            if (downpressed)
            {
                velocity.Y += acceleration.Y;
               // mSourceRect.Y = 533;
            }

            if (uppressed)
            {
                velocity.Y -= acceleration.Y;
               // mSourceRect.Y = 533;
            }

            if (velocity.Y > maxvelocity)
                velocity.Y = maxvelocity;

            if (velocity.Y < -maxvelocity)
                velocity.Y = -maxvelocity;


            //friction
            if (velocity.X > 0)
            {
                velocity.X -= friction;
            }

            if (velocity.X < 0)
            {
                velocity.X += friction;
            }

            if (velocity.Y > 0)
            {
                velocity.Y -= friction;
            }

            if (velocity.Y < 0)
            {
                velocity.Y += friction;


            }

            if (!leftpressed && !rightpressed && !uppressed && !downpressed)
            {
                if (velocity.X < 0 && velocity.X > -0.1f)
                    velocity.X = 0;


                if (velocity.X > 0 && velocity.X < 0.1f)
                    velocity.X = 0;



                if (velocity.Y < 0 && velocity.Y > -0.1f)
                    velocity.Y = 0;


                if (velocity.Y > 0 && velocity.Y < 0.1f)
                    velocity.Y = 0;

            }

            Console.WriteLine(velocity.X + " " + velocity.Y);



            //boundary collision
            if (position.X > graphicsDevice.Viewport.Width - mSourceRect.Width)
            {
                velocity.X -= velocity.X + 0.2f;
                
                position.X = graphicsDevice.Viewport.Width - mSourceRect.Width;
            }
            if (position.X < 0)
                velocity.X -= velocity.X - 0.4f;

            if (position.Y < 0)
                velocity.Y -= velocity.Y - 0.4f;

            if (position.Y > graphicsDevice.Viewport.Height - mSourceRect.Height)
            
            {
                velocity.Y -= velocity.Y + 0.4f;
                
                position.Y = graphicsDevice.Viewport.Height - mSourceRect.Height;
            }

            //playing with velocity verlet integration
            position += (oldvel + velocity) * 0.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            position.X = (int)position.X;
            position.Y = (int)position.Y;
        }


        public override void Update(GameTime gameTime)
        {
            //playing around with velocity verlet integration
            Vector2 oldvel;
            oldvel = velocity;

            PlayIdleAnimation(gameTime);


            //screenratio = new Vector2(graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight) / new Vector2(1920, 1080);

            //mSourceRect.Y = 0;
            if (leftpressed)
            {
                velocity.X -= acceleration.X;
                // mSourceRect.Y = 267;
            }

            if (rightpressed)
            {
                velocity.X += acceleration.X;
                // mSourceRect.Y = 400;
            }

            if (velocity.X > maxvelocity)
                velocity.X = maxvelocity;

            if (velocity.X < -maxvelocity)
                velocity.X = -maxvelocity;


            if (downpressed)
            {
                velocity.Y += acceleration.Y;
                // mSourceRect.Y = 533;
            }

            if (uppressed)
            {
                velocity.Y -= acceleration.Y;
                // mSourceRect.Y = 533;
            }

            if (velocity.Y > maxvelocity)
                velocity.Y = maxvelocity;

            if (velocity.Y < -maxvelocity)
                velocity.Y = -maxvelocity;


            //friction
            if (velocity.X > 0)
            {
                velocity.X -= friction;
            }

            if (velocity.X < 0)
            {
                velocity.X += friction;
            }

            if (velocity.Y > 0)
            {
                velocity.Y -= friction;
            }

            if (velocity.Y < 0)
            {
                velocity.Y += friction;


            }

            if (!leftpressed && !rightpressed && !uppressed && !downpressed)
            {
                if (velocity.X < 0 && velocity.X > -0.1f)
                    velocity.X = 0;


                if (velocity.X > 0 && velocity.X < 0.1f)
                    velocity.X = 0;



                if (velocity.Y < 0 && velocity.Y > -0.1f)
                    velocity.Y = 0;


                if (velocity.Y > 0 && velocity.Y < 0.1f)
                    velocity.Y = 0;

            }

            Console.WriteLine(velocity.X + " " + velocity.Y);



            //boundary collision
            if (position.X > Game1.graphics.PreferredBackBufferWidth - mSourceRect.Width)
            {
                velocity.X -= velocity.X + 0.2f;

                position.X = Game1.graphics.PreferredBackBufferWidth - mSourceRect.Width;
            }
            if (position.X < 0)
                velocity.X -= velocity.X - 0.4f;

            if (position.Y < 0)
                velocity.Y -= velocity.Y - 0.4f;

            if (position.Y > Game1.graphics.PreferredBackBufferHeight - mSourceRect.Height)

            {
                velocity.Y -= velocity.Y + 0.4f;

                position.Y = Game1.graphics.PreferredBackBufferHeight - mSourceRect.Height;
            }

            //playing with velocity verlet integration
            position += (oldvel + velocity) * 0.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            position.X = (int)position.X;
            position.Y = (int)position.Y;
        }




        /// <summary>
        /// Idle Animation
        /// </summary>
        /// 
        public override void PlayIdleAnimation(GameTime gameTime)
        {
            mTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (mTimer > mInterval)
                {

                //  mSourceRect.Y +=133;
                // mSourceRect.Y += 81;
                mSourceRect.Y += mIdleYFrameIncrement;

                // mSourceRect.Y += 81;

                // if (mSourceRect.Y > 1065)
                if (mSourceRect.Y > mIdleMaxY)
                    mSourceRect.Y = 1;


                 mTimer = 0f;
            }

            //Console.WriteLine(mTimer + " " + mInterval);
            Console.WriteLine(Game1.graphics.PreferredBackBufferHeight);

        }


        public override void ChangeResolution(ContentManager Content, string NewResHeight)
        {
            // Texture = Content.Load<Texture2D>("Sprites/planet" + NewResHeight + "/" + "bg");

            // Texture = Content.Load<Texture2D>("Sprites/invader/" + NewResHeight + "/" + "invadersheet");

            Texture = Content.Load<Texture2D>("Sprites/" + NewResHeight + "/" + mTexFilename);

            if (NewResHeight == "720")
            {
                SourceRect = new Rectangle(0, 1, 109, 79);
                IdleMaxY = 649;
                IdleYFrameIncrement = 81;
            }
            else if (NewResHeight == "1080")
            {
                SourceRect = new Rectangle(0, 400, 182, 131);
                IdleMaxY = 1065;


                IdleYFrameIncrement = 133;
            }
        }

        public bool DownPressed
        {
            get { return downpressed; }
            set { downpressed = value; }
        }

        public bool UpPressed
        {
            get { return uppressed; }
            set { uppressed = value; }
        }

        public bool LeftPressed
        {
            get { return leftpressed; }
            set { leftpressed = value; }
        }


        public bool RightPressed
        {
            get { return RightPressed; }
            set { rightpressed = value; }
        }

        private bool downpressed;
        private bool uppressed;
        private bool leftpressed;
        private bool rightpressed;

        private float friction;
        private float maxvelocity;

        private Vector2 screenratio;

        private KeyboardState kbState;

    }
}

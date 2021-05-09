using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Monogam
{
    class Entity
    {
        public Entity(ContentManager Content, string name, float x, float y)
        {
            mTexture = Content.Load<Texture2D>("Sprites/" + name);
            position.X = x;
            position.Y = y;
            mFriction = 0;
            this.Content = Content;
            mCurrentResHeight = "720";

            mSourceRect = new Rectangle(0, 0, mTexture.Width, mTexture.Height);

            mTimer = 0f;
            mInterval = 0f;
        }

        public Entity(ContentManager content, string name, float x, float y, Vector2 Accel)
        {
            mTexture = content.Load<Texture2D>(name);
            position.X = x;
            position.Y = y;

            acceleration = Accel;


            mFriction = 0;
        }



        //deprecated, the graphicsdevice no longer needs passing in as it's now a globally available static variable
        public virtual void Update(GameTime gameTime, GraphicsDevice graphicsDev)
        {

        }



        public virtual void Update(GameTime gameTime)
        {

        }

    

        public virtual void HandleEvents()
        {

        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(mTexture, new Vector2((int)position.X, (int)position.Y), Color.White);
            spriteBatch.Draw(mTexture, new Vector2((int)position.X, (int)position.Y), mSourceRect, Color.White);
        }


        //overloaded draw function for specifying size of entity
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 scale)
        {
            spriteBatch.Draw(mTexture, new Vector2((int)position.X, (int)position.Y), mSourceRect, Color.White, 0, Vector2.Zero, new Vector2(scale.X, scale.Y), SpriteEffects.None, 0);
        }

 

        public virtual void ChangeResolution(ContentManager Content, string NewResHeight)
        {
            // Texture = Content.Load<Texture2D>("Sprites/planet" + NewResHeight + "/" + "bg");
            //Texture = Content.Load<Texture2D>("Sprites/planet/" + NewResHeight + "/" + "bg");
            Texture = Content.Load<Texture2D>("Sprites/" + NewResHeight + "/" + mTexFilename);
            SourceRect = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
            mCurrentResHeight = NewResHeight;
        }

        public virtual void PlayIdleAnimation(GameTime gameTime)
        {

        }


        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }



        public Texture2D Texture
        {
            get { return mTexture; }
            // set { mTexture = Content.Load<Texture2D>(value); }
            set { mTexture = value; ; }
            //as for the above, store a static reference to the main game in the base class? or pass contentmanager in as an argument every call?
        }


        public string TexFilename
        {
            get { return mTexFilename; }
            set { mTexFilename = value; }
        }


        public Rectangle SourceRect
        {
            get { return mSourceRect; }
            set { mSourceRect = value; }
        }


        public float Timer
        {
            get { return mTimer; }
            set { mTimer = value; }
        }

        public float Interval
        {
            get { return mInterval; }
            set { mInterval = value; }
        }

        public int IdleMaxY
        {
            get { return mIdleMaxY; }
            set { mIdleMaxY = value; }
        }

        public int IdleYFrameIncrement
        {
          get { return mIdleYFrameIncrement; }
          set { mIdleYFrameIncrement = value; }
        }


        protected float mTimer;
        protected float mInterval;
        protected string mTexFilename;
        protected Texture2D mTexture;
        protected Rectangle mSourceRect;
        protected float mFriction;
        protected string mCurrentResHeight;
        protected int mIdleMaxY;
        protected int mIdleYFrameIncrement;

        protected Vector2 acceleration;
        protected Vector2 velocity;
        protected Vector2 position;

        protected ContentManager Content;

        
        

    }
}

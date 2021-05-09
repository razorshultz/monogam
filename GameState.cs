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
    public class GameState
    {
        public GameState(ContentManager Content, GraphicsDevice graphicsDev)
        {
            nextState = GameStates.None;
            graphicsDevice = graphicsDev;
        }

        public virtual void HandleEvents()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }


 

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }


        public virtual void ChangeResolution(ContentManager Content, string NewResHeight)
        {
            
        }




        public GameStates NextState
        {
            get { return nextState; }
            set { nextState = value; }
        }

        static public ContentManager contentManager { get; set; }
        static private GameStates nextState;
        protected Vector2 screenratio;
        protected GraphicsDevice graphicsDevice;
    }

    public enum GameStates
    {
        None,
        Intro,
        MainMenu,
        Gameplay
    }
}

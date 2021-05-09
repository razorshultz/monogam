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
    class GameplayState : GameState
    {

        public GameplayState(ContentManager Content, GraphicsDevice graphicsDev) : base(Content, graphicsDev)
        {
            this.mContent = Content;
            mPlayer = new Player(Content, "1080/invadersheet", 200, 300);
            mPlayer.TexFilename = "invadersheet";
            BG = new Entity(Content, "720/bg", 0, 0);
            BG.TexFilename = "bg";

            //to do: decide whether anytime an entity is created, to manually call its function to assign the texture filename, or create a constructor to
            //take in the current resolution, and to adjust the entity constructor to just need the texture name, rather than the resolution and name

        }


        public override void HandleEvents()
        {
            mPlayer.HandleEvents();
        }

        public override void Update(GameTime gameTime)
        {
            //mPlayer.Update(gameTime, graphicsDevice);
            mPlayer.Update(gameTime);
            BG.Update(gameTime);
            
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            BG.Draw(spriteBatch);
            mPlayer.Draw(spriteBatch);

        }


        public override void ChangeResolution(ContentManager Content, string NewResHeight)
        {
            BG.ChangeResolution(Content, NewResHeight);
            mPlayer.ChangeResolution(Content, NewResHeight);
        }

        private ContentManager mContent;
        private Player mPlayer;
        private Entity BG;
        
       


    }
}

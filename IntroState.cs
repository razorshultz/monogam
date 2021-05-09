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
     class IntroState : GameState
    {


        public IntroState(ContentManager Content, string name, GraphicsDevice graphicsDev) : base(Content, graphicsDev)
        {
            intropic =  new Entity(Content, "720p", 0, 0);
            graphicsDevice = graphicsDev;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            intropic.Draw(spriteBatch);
        }


      /* public override void Draw(SpriteBatch spriteBatch, Matrix transformmatrix)
        {
            intropic.Draw(spriteBatch);
        }*/




        public override void HandleEvents()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                NextState = GameStates.Gameplay;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void ChangeResolution(ContentManager Content, string NewResHeight)
        {
           intropic.Texture = Content.Load<Texture2D>("Sprites/" +  "720p");
            intropic.SourceRect = new Rectangle(0, 0, intropic.Texture.Width, intropic.Texture.Height);
        }


        private Entity intropic;
        
    }
}

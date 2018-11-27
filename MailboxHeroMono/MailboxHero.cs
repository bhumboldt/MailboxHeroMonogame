using MailboxHeroMono.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono
{
    public class MailboxHero: Game
    {
        private SpriteBatch spriteBatch;

        GraphicsDeviceManager graphics;

        public MailboxHero()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameStateManager.Instance.SetContent(Content);
            GameStateManager.Instance.AddScreen(new MenuGameState(GraphicsDevice));
        }

        protected override void UnloadContent()
        {
            GameStateManager.Instance.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            GameStateManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

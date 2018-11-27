using MailboxHeroMono.GameStates;
using MailboxHeroMono.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.GameState
{
    public class MenuGameState : BaseGameState
    {
        Texture2D _background;
        Texture2D _button;
        SpriteFont _font;

        Button _playGameButton;

        public MenuGameState(GraphicsDevice graphicsDevice): base(graphicsDevice)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _playGameButton.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("menu_sprite");
            _button = content.Load<Texture2D>("button");
            _font = content.Load<SpriteFont>("font");

            _playGameButton = new Button(_button, new Vector2(_graphicsDevice.Viewport.Width / 2f - _button.Width / 2f, _graphicsDevice.Viewport.Height / 2f - _button.Height / 2f), "Play Game", _font, PlayGameAction);
        }

        public override void UnloadContent()
        {
            _background.Dispose();
            _background = null;

            _button.Dispose();
            _button = null;
        }

        public override void Update(GameTime gameTime)
        {
            _playGameButton.Update(gameTime);
        }

        private void PlayGameAction()
        {
            GameStateManager.Instance.AddScreen(new PlayGameState(_graphicsDevice));
        }
    }
}

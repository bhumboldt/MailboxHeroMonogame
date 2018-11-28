using MailboxHeroMono.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.GameState
{
    public class GameOverState: BaseGameState
    {
        string _gameOverText = "Game Over!\nPress r to restart the game, or m to go to the menu";
        int _score;
        SpriteFont _font;

        Vector2 _scorePosition;
        Vector2 _gameOverTextPosition;

        public GameOverState(GraphicsDevice graphicsDevice, int score): base(graphicsDevice)
        {
            _score = score;
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("font");

            var gameOverTextMeasurement = _font.MeasureString(_gameOverText);
            _gameOverTextPosition = new Vector2(_graphicsDevice.Viewport.Width / 2f - gameOverTextMeasurement.X / 2f, _graphicsDevice.Viewport.Height / 2f - gameOverTextMeasurement.Y / 2f);
        }

        public override void Initialize()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _gameOverText, _gameOverTextPosition, Color.Black);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mPressed = keyboardState.IsKeyDown(Keys.M);
            var rPressed = keyboardState.IsKeyDown(Keys.R);
            if (mPressed)
            {
                GameStateManager.Instance.RemoveScreen();
                GameStateManager.Instance.RemoveScreen();
            }
            else if (rPressed)
            {
                GameStateManager.Instance.RemoveScreen();
                GameStateManager.Instance.RemoveScreen();
                GameStateManager.Instance.AddScreen(new PlayGameState(_graphicsDevice));
            }
        }

        public override void UnloadContent()
        {
            
        }
    }
}

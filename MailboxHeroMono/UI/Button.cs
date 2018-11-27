using MailboxHeroMono.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.UI
{
    public class Button
    {
        private Texture2D _texture;
        private Vector2 _position;
        private string _text;
        private SpriteFont _font;
        private ButtonState _mouseOldState = ButtonState.Released;
        private Action _action;

        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font, Action action)
        {
            _texture = texture;
            _position = position;
            _text = text;
            _font = font;
            _action = action;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_texture, _position, Color.White);
            var textLength = _font.MeasureString(_text);
            batch.DrawString(_font, _text, new Vector2(_position.X + _texture.Width / 2f - textLength.X / 2f, _position.Y + _texture.Height / 2f - textLength.Y / 2f), Color.Black);
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var newLeftState = mouseState.LeftButton;
            if (IsMouseInBounds(mouseState) && _mouseOldState == ButtonState.Pressed && newLeftState == ButtonState.Released)
            {
                System.Diagnostics.Debug.WriteLine("Hey, the button has been clicked!");
                _action?.Invoke();
            }
            _mouseOldState = newLeftState;
        }

        public bool IsMouseInBounds(MouseState mouseState)
        {
            return mouseState.X >= _position.X && mouseState.X <= (_position.X + _texture.Width) && mouseState.Y >= _position.Y && mouseState.Y <= (_position.Y + _texture.Height);
        }
    }
}

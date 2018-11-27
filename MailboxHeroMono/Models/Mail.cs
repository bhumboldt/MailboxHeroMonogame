using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.Models
{
    public class Mail
    {
        public Vector2 Position;
        public Rectangle Bounds;

        private Vector2 _velocity;
        private Texture2D _texture;
        private bool _isLeftMail;

        public Mail(bool isLeftMail, Vector2 position, Texture2D texture)
        {
            _isLeftMail = isLeftMail;
            Position = position;
            _texture = texture;

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

            _velocity = new Vector2(15f, 0f);
            _velocity.X = isLeftMail ? -_velocity.X : _velocity.X;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Position += _velocity;
            Bounds.X = (int)Position.X;
            Bounds.Y = (int)Position.Y;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.Models
{
    public class Mailbox
    {
        public Vector2 Position;
        public Rectangle Bounds;
        public bool IsClosed;

        private Vector2 _velocity;
        private Texture2D _texture;

        public Mailbox(Vector2 position, Texture2D texture)
        {
            Position = position;
            _texture = texture;
            _velocity = new Vector2(0f, Constants.Constants.VerticalVelocity);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, 30);
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

        public bool IntersectsWithMail(Mail mail)
        {
            return Bounds.Intersects(mail.Bounds);
        }

        public void CloseMailbox(Texture2D newTexture)
        {
            _texture = newTexture;
            IsClosed = true;
        }
    }
}

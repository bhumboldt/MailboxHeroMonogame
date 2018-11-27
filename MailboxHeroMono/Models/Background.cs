using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.Models
{
    public class Background
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public bool IsBottomBackground;

        private Texture2D _texture;

        public Background(Vector2 position, Texture2D texture)
        {
            Position = position;
            _texture = texture;
            IsBottomBackground = Position.Y == 0;
            Velocity = new Vector2(0f, Constants.Constants.VerticalVelocity);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
        }
    }
}

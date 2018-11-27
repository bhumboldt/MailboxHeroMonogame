using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MailboxHeroMono.Interfaces
{
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);
        void Initialize();
        void LoadContent();
        void UnloadContent();
    }
}

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
    public interface IGameState
    {
        void Initialize();
        void UnloadContent();
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch batch);
    }
}

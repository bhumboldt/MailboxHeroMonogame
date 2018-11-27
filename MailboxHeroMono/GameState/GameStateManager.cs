using MailboxHeroMono.GameStates;
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
    public class GameStateManager
    {
        private static GameStateManager _instance;

        private ContentManager _content;
        private Stack<BaseGameState> _screens = new Stack<BaseGameState>();

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        public void SetContent(ContentManager content)
        {
            _content = content;
        }

        public void AddScreen(BaseGameState screen)
        {
            try
            {
                _screens.Push(screen);
                _screens.Peek().Initialize();
                if (_content != null)
                {
                    _screens.Peek().LoadContent(_content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void RemoveScreen()
        {
            if (_screens.Count > 0)
            {
                try
                {
                    _screens.Pop();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            try
            {
                if (_screens.Count > 0)
                {
                    _screens.Peek().Update(gameTime);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in GameStateManager Update(): {ex.Message}");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (_screens.Count > 0)
                {
                    _screens.Peek().Draw(spriteBatch);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in GameStateManager Draw(): {ex.Message}");
            }
        }

        public void UnloadContent()
        {
            foreach (var screen in _screens)
            {
                screen.UnloadContent();
            }
        }
    }
}

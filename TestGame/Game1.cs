using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Nez.Core
    {
        public Game1(): base(1280, 768, false, false)
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            Window.AllowUserResizing = true;
            var newScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);

            var entityOne = newScene.createEntity("entity-one");
            var mailTruckTexture = newScene.content.Load<Texture2D>("mailtruck");
            entityOne.addComponent(new Sprite(mailTruckTexture));
            scene = newScene;
        }
    }
}

using MailboxHeroMono.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxHeroMono.GameState
{
    public class PlayGameState: BaseGameState
    {
        private const float MAILBOX_DISTANCE = 300f;
        private const int EXP_TO_LEVEL_UP = 256;
        private const int CYCLES_TO_SPAWN_MAILBOX = 200;

        Texture2D player;
        Vector2 playerPos;

        Texture2D backgroundBottom;
        Texture2D backgroundTop;
        Texture2D mailTexture;
        Texture2D rightMailboxOpen;
        Texture2D rightMailboxClosed;
        Texture2D leftMailboxClosed;
        Texture2D leftMailboxOpen;

        Song leftMailSound;
        Song rightMailSound;
        SoundEffectInstance backgroundMusicInstance;

        List<Background> backgrounds;

        SpriteFont font;
        int score = 0;
        int level = 1;

        bool canThrowLeft;
        bool canThrowRight;

        Random random = new Random();

        int levelExp = 0;
        int cyclesSinceLastSpawn = 0;

        List<Mail> mails;
        List<Mailbox> mailBoxes;

        public PlayGameState(GraphicsDevice graphicsDevice): base(graphicsDevice)
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization logic here

            mails = new List<Mail>();
            mailBoxes = new List<Mailbox>();
            canThrowLeft = true;
            canThrowRight = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent(ContentManager content)
        {
            // TODO: use this.Content to load your game content here
            font = content.Load<SpriteFont>("font");

            backgroundTop = content.Load<Texture2D>("background");
            backgroundBottom = content.Load<Texture2D>("background");

            leftMailSound = content.Load<Song>("leftMailSound");
            rightMailSound = content.Load<Song>("rightMailSound");
            var backgroundMusic = content.Load<SoundEffect>("gameMusic");
            backgroundMusicInstance = backgroundMusic.CreateInstance();
            backgroundMusicInstance.IsLooped = true;
            backgroundMusicInstance.Play();

            var topBackground = new Background(new Vector2(0f, -backgroundTop.Height), backgroundTop);
            var bottomBackground = new Background(new Vector2(0f, 0f), backgroundBottom);

            backgrounds = new List<Background>() { bottomBackground, topBackground };

            mailTexture = content.Load<Texture2D>("mail");
            player = content.Load<Texture2D>("mailtruck");
            leftMailboxOpen = content.Load<Texture2D>("open_mailbox");
            rightMailboxOpen = content.Load<Texture2D>("open_mailbox_mirror");
            leftMailboxClosed = content.Load<Texture2D>("mailbox");
            rightMailboxClosed = content.Load<Texture2D>("mailbox_mirror");
            playerPos = new Vector2(_graphicsDevice.Viewport.Width / 2f - player.Width / 2f, _graphicsDevice.Viewport.Height - player.Height - 10f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            for (var i = 0; i < backgrounds.Count; i++)
            {
                backgrounds.ElementAt(i).Update(gameTime);
                if (backgrounds.ElementAt(i).Position.Y >= _graphicsDevice.Viewport.Height)
                {
                    if (i == 0)
                    {
                        backgrounds.ElementAt(i).Position.Y = backgrounds.ElementAt(1).Position.Y - backgroundBottom.Height + 20;
                    }
                    else
                    {
                        backgrounds.ElementAt(i).Position.Y = backgrounds.ElementAt(0).Position.Y - backgroundBottom.Height + 20;
                    }
                }
            }

            cyclesSinceLastSpawn += level;
            SpawnMailbox();

            // TODO: Add your update logic here
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Left) && canThrowLeft)
            {
                var leftMailPosition = new Vector2(playerPos.X - mailTexture.Width, playerPos.Y);
                var mail = new Mail(true, leftMailPosition, mailTexture);
                mails.Add(mail);
                MediaPlayer.Play(leftMailSound);
                canThrowLeft = false;
            }

            if (keyboard.IsKeyDown(Keys.Right) && canThrowRight)
            {
                var rightMailPosition = new Vector2(playerPos.X + player.Width, playerPos.Y);
                var mail = new Mail(false, rightMailPosition, mailTexture);
                mails.Add(mail);
                MediaPlayer.Play(rightMailSound);
                canThrowRight = false;
            }
            foreach (var mail in mails)
            {
                mail.Update(gameTime);
            }

            var mailboxesToRemove = FindMailCollidingWithMailboxes(gameTime);

            mailBoxes.RemoveAll(x => mailboxesToRemove.Contains(x));
            mailboxesToRemove.Clear();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (var background in backgrounds)
            {
                background.Draw(spriteBatch);
            }

            spriteBatch.Draw(player, playerPos, Color.White);

            foreach (var mailbox in mailBoxes)
            {
                mailbox.Draw(spriteBatch);
            }

            foreach (var mail in mails)
            {
                mail.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, $"Score: {score}", new Vector2(25f, 50f), Color.Black);

            spriteBatch.End();
        }

        private void SpawnMailbox()
        {
            if (cyclesSinceLastSpawn >= CYCLES_TO_SPAWN_MAILBOX)
            {
                cyclesSinceLastSpawn = 0;
                var mailboxChance = random.Next(0, 2);
                if (mailboxChance == 0)
                {
                    SpawnLeftMailbox();
                }
                else
                {
                    SpawnRightMailbox();
                }
            }
        }

        private List<Mailbox> FindMailCollidingWithMailboxes(GameTime gameTime)
        {
            var mailboxesToRemove = new List<Mailbox>();

            foreach (var mailbox in mailBoxes)
            {
                mailbox.Update(gameTime);
                var mailToRemove = new List<Mail>();
                foreach (var mail in mails)
                {
                    if (mail.Position.X <= 0)
                    {
                        mailToRemove.Add(mail);
                        canThrowLeft = true;
                        GameOver();
                    }
                    else if (mail.Position.X + mailTexture.Width >= _graphicsDevice.Viewport.Width)
                    {
                        mailToRemove.Add(mail);
                        canThrowRight = true;
                        GameOver();
                    }
                    else if (mailbox.IntersectsWithMail(mail))
                    {
                        mailToRemove.Add(mail);

                        HandleMailboxMailCollision(mail, mailbox);
                    }
                }

                mails.RemoveAll(x => mailToRemove.Contains(x));
                mailToRemove.Clear();
                if (mailbox.Position.Y >= _graphicsDevice.Viewport.Height)
                {
                    if (!mailbox.IsClosed)
                    {
                        GameOver();
                    }
                    mailboxesToRemove.Add(mailbox);
                }
            }

            return mailboxesToRemove;
        }

        private void SpawnLeftMailbox()
        {
            var mailbox = new Mailbox(new Vector2(playerPos.X - leftMailboxOpen.Width - MAILBOX_DISTANCE, -50f), leftMailboxOpen);
            mailBoxes.Add(mailbox);
        }

        private void SpawnRightMailbox()
        {
            var mailbox = new Mailbox(new Vector2(playerPos.X + player.Width + MAILBOX_DISTANCE, -50f), rightMailboxOpen);
            mailBoxes.Add(mailbox);
        }

        private void HandleMailboxMailCollision(Mail mail, Mailbox mailbox)
        {
            score++;
            levelExp += EXP_TO_LEVEL_UP / (2 * level);
            if (levelExp >= EXP_TO_LEVEL_UP)
            {
                levelExp = 0;
                level++;
            }

            if (mail.Position.X > playerPos.X)
            {
                canThrowRight = true;
            }
            else
            {
                canThrowLeft = true;
            }

            if (mailbox.Position.X > playerPos.X)
            {
                mailbox.CloseMailbox(rightMailboxClosed);
            }
            else
            {
                mailbox.CloseMailbox(leftMailboxClosed);
            }
        }

        private void GameOver()
        {
            backgroundMusicInstance.Stop();
            GameStateManager.Instance.AddScreen(new GameOverState(_graphicsDevice, score));
        }

        private void AddLeftMailbox()
        {
            var mailbox = new Mailbox(new Vector2(playerPos.X - leftMailboxOpen.Width - MAILBOX_DISTANCE, 0f), leftMailboxOpen);
            mailBoxes.Add(mailbox);
        }

        private void AddRightMailbox()
        {
            var mailbox = new Mailbox(new Vector2(playerPos.X + player.Width + MAILBOX_DISTANCE, 0f), rightMailboxOpen);
        }
    }
}

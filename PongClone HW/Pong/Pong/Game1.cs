using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        public static List<GameObject> GameObjects = new List<GameObject>();

        public static int screenWidth;
        public static int screenHeight;

        public static int[] score = new int[] { 0, 0 };

        const int PADDLE_OFFSET = 70;

        Player player1;
        Player player2;

        Ball ball;

        Texture2D splashScreen;

        public static SoundEffect sound1;
        public static SoundEffect sound2;

        Texture2D divider;

        bool showSplashScreen = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            this.player1 = new Player(0);
            this.player2 = new Player(1);
            this.ball = new Ball();
            base.Initialize();

            foreach (GameObject gameObject in GameObjects)
                gameObject.Start();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player1.Texture = Content.Load<Texture2D>("Content/Paddle");
            player2.Texture = Content.Load<Texture2D>("Content/Paddle");

            player1.Position = new Vector2(PADDLE_OFFSET, screenHeight / 2 - player1.Texture.Height / 2);
            player2.Position = new Vector2(screenWidth - PADDLE_OFFSET - player2.Texture.Width, screenHeight / 2 - player2.Texture.Height / 2);

            ball.Texture = Content.Load<Texture2D>("Content/Ball");
            ball.Position = new Vector2(screenWidth / 2 - ball.Texture.Width / 2, screenHeight / 2 - ball.Texture.Height / 2);

            splashScreen = Content.Load<Texture2D>("Content/StartScreen");
            divider = Content.Load<Texture2D>("Content/Middle");

            spriteFont = Content.Load<SpriteFont>("Content/SpriteFont1");

            sound1 = Content.Load<SoundEffect>("Content/BallWallCollision");
            sound2 = Content.Load<SoundEffect>("Content/PaddleBallCollision");
        }

        void RestartGame()
        {
            foreach (GameObject gameObject in GameObjects)
                gameObject.Start();
            player1.Position = new Vector2(PADDLE_OFFSET, screenHeight / 2 - player1.Texture.Height / 2);
            player2.Position = new Vector2(screenWidth - PADDLE_OFFSET - player2.Texture.Width, screenHeight / 2 - player2.Texture.Height / 2);
            score[0] = 0;
            score[1] = 0;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            if (!showSplashScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.F5))
                    RestartGame();
                foreach (GameObject gameObject in GameObjects)
                    gameObject.Update();
            }
            else if (showSplashScreen && (Keyboard.GetState().IsKeyDown(Keys.Space) || Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                showSplashScreen = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (!showSplashScreen)
            {
                spriteBatch.Draw(divider, new Rectangle(screenWidth / 2 - divider.Width / 2, screenHeight / 2 - 200, divider.Width, 400), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                foreach (GameObject gameObject in GameObjects)
                    gameObject.Draw(spriteBatch);
                spriteBatch.DrawString(spriteFont, "Score: " + score[0] + " | " + score[1], new Vector2(2, 0), Color.White);
            }
            else
                spriteBatch.Draw(splashScreen, new Vector2(screenWidth / 2 - splashScreen.Width / 2, screenHeight / 2 - splashScreen.Height / 2), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

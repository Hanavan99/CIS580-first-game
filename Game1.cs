using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CIS580_first_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Ball> balls = new List<Ball>();
        private Texture2D playerStill;
        private Texture2D playerMoving;
        private int playerSpeed = 0;
        private int playerX = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Random r = new Random();
            int x = 500;
            for (int i = 0; i < 100; i++)
            {
                Ball b = new Ball(x, 200, (float) r.NextDouble() * 5f - 2.5f, (float)r.NextDouble() * 5f - 2.5f, new Color(r.Next() % 256, r.Next() % 256, r.Next() % 256));
                balls.Add(b);
                x += r.Next() % 500 + 100;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerStill = Content.Load<Texture2D>("player_standing");
            playerMoving = Content.Load<Texture2D>("player_moving");
            // TODO: use this.Content to load your game content here
            foreach (Ball b in balls)
            {
                b.Initialize(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (Ball b in balls)
            {
                b.Update(gameTime, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
                int centerX = (int) b.X + 25;
                int centerY = (int) b.Y + 25;
                if (CheckIntersectCircleRect(centerX, centerY, 25, 200, _graphics.PreferredBackBufferHeight - 100, 50, 100))
                {
                    playerX = 0;
                }
            }

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D)) // moving left
            {
                playerSpeed = 5;
            } else if (state.IsKeyDown(Keys.A))
            {
                playerSpeed = -5;
            } else
            {
                playerSpeed = 0;
            }
            playerX += (int) (playerSpeed * gameTime.ElapsedGameTime.TotalMilliseconds * 0.1);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (Ball b in balls)
            {
                b.Draw(_spriteBatch, -playerX);
            }

            if (playerSpeed == 0)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(playerStill, new Rectangle(200, _graphics.PreferredBackBufferHeight - 100, 50, 100), Color.Black);
                _spriteBatch.End();
            } else
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(playerMoving, new Rectangle(200, _graphics.PreferredBackBufferHeight - 100, 50, 100), Color.Black);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private bool CheckIntersectCircleRect(float circleX, float circleY, float circleRadius, float rectX, float rectY, float rectWidth, float rectHeight)
        {
            // check if center of circle is inside of rect
            if (circleX > rectX && circleX < rectX + rectWidth && circleY > rectY && circleY < rectY + rectHeight)
            {
                return true;
            }
            return false;
        }
    }
}

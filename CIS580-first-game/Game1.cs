using CIS580_content_project;
using CIS580_first_game.Collisions;
using CIS580_first_game.GameState;
using CIS580_first_game.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CIS580_first_game
{
    public class Game1 : Game
    {
        //public static readonly Color[] BallColorList = new Color[] { Color.White, Color.DeepSkyBlue, Color.Red, Color.LightGreen, Color.LightGoldenrodYellow, Color.HotPink };

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<UpdateableObject> objects = new List<UpdateableObject>();
        private World world;
        private Player player;
        private int ballCount = 0;
        private Random r = new Random();
        private double lastMillis = 0;
        private SpriteFont font;
        private SoundEffect levelUp;
        
        private MyGameState gameState;
        private BallModel ballModel;
        private TerrainModel terrainModel;
        private BallColorContent ballColors;

        public const int WorldSize = 1000;

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

            player = new Player(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            objects.Add(player);

            world = new World();

            ballModel = new BallModel();

            gameState = new MyGameState();
            gameState.CameraMatrix = Matrix.CreateTranslation((_graphics.PreferredBackBufferWidth / 2) - (Player.PlayerSpriteSize / 2), 0, 0);
            gameState.WorldSize = WorldSize;

            terrainModel = new TerrainModel();
            for (int i = -20; i < 20; i++)
            {
                TerrainTile t = new TerrainTile(terrainModel, i, r.Next() % 4, _graphics.PreferredBackBufferHeight);
                objects.Add(t);
            }

            base.Initialize();
        }
        
        private void AddBall()
        {
            Ball b = new Ball(r.Next() % (2 * WorldSize - 50) + 25 - WorldSize, 200, (float)r.NextDouble() * 5f - 2.5f, (float)r.NextDouble() * 5f - 2.5f, ballColors.ColorList[r.Next() % ballColors.ColorList.Count], ballModel);
            objects.Add(b);
            world.AddBall(b);
            ballCount++;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AddBall();
            player.LoadContent(Content);
            ballModel.LoadContent(Content);
            terrainModel.LoadContent(Content);
            font = Content.Load<SpriteFont>("default_font");
            levelUp = Content.Load<SoundEffect>("level_up");
            ballColors = Content.Load<BallColorContent>("ballcolors");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            gameState.ViewportHeight = _graphics.PreferredBackBufferHeight;
            gameState.ViewportWidth = _graphics.PreferredBackBufferWidth;

            // handle updating of all updateable objects
            foreach (UpdateableObject o in objects.ToArray())
            {
                o.Update(gameTime, gameState);
                if (o.GetType().Equals(typeof(Ball)) && player.bounds.CollidesWith(((Ball)o).bounds))
                {
                    objects.RemoveAll((o) =>
                    {
                        return o.GetType().Equals(typeof(Ball));
                    });
                    world.Clear();
                    ballCount = 0;
                    AddBall();
                    lastMillis = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            world.Update(gameTime, gameState);

            // end of game checks
            if (gameTime.TotalGameTime.TotalMilliseconds - lastMillis > 10000)
            {
                AddBall();
                lastMillis = gameTime.TotalGameTime.TotalMilliseconds;
                levelUp.Play();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            // draw all updateable objects
            foreach (UpdateableObject o in objects.ToArray())
            {
                o.Draw(gameTime, _spriteBatch, gameState);
            }

            // draw string
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Current Level: " + ballCount, new Vector2(20, 20), Color.Black);
            _spriteBatch.DrawString(font, "Level up in: " + (int) ((lastMillis + 10000 - gameTime.TotalGameTime.TotalMilliseconds) / 1000.0 + 1), new Vector2(20, 40), Color.Black);
            //_spriteBatch.DrawString(font, "Player X: " + player.bounds.X + "; Ball X: " + ((Ball)objects[objects.Count - 1]).bounds.X, new Vector2(20, 40), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace DungeonCrawler
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GUIHandler gui;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random r;

        Floor test;
        Player player;

        List<HealthPotion> HPotions = new List<HealthPotion>();
        List<Enemy> GEnemies = new List<Enemy>();

        MouseHandler m;

        Texture2D wall;
        Texture2D floor;
        Texture2D player_texture;
        Texture2D healthPotion;

        string success = "";

        SoundEffect attack;
        SoundEffect step;

        SpriteFont arial;

        Camera camera;
        FrameRateCounter fps;

        public bool HasMoved = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 

        protected override void Initialize()
        {
            IsMouseVisible = true;

            gui = new GUIHandler();

            Button b = new Button();

            gui.AddButton(b);

            r = new Random();

            // TODO: Add your initialization logic here
            test = new Floor(21, null);

            for (int i = 0; i < 15; i++ )
            {
                Enemy e = new Enemy(test.maze, r, 80);
                GEnemies.Insert(i, e);
            }

            for (int i = 0; i < 5; i++)
            {
                HealthPotion h = new HealthPotion(r, test.maze);
                HPotions.Insert(i, h);
            }

            camera = new Camera(GraphicsDevice.Viewport);

            fps = new FrameRateCounter();

            m = new MouseHandler();

            foreach (HealthPotion i in HPotions)
            {
                if (!i.hasBeenConsumed)
                {
                    test.maze[Convert.ToInt32(i.pos.X), Convert.ToInt32(i.pos.Y)] = "h" + HPotions.IndexOf(i).ToString();
                }
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("Tileable2d.png");
            wall = Content.Load<Texture2D>("Tileable10y.png");
            arial = Content.Load<SpriteFont>("myFont");
            player_texture = Content.Load<Texture2D>("jordeKang.jpg");
            healthPotion = Content.Load<Texture2D>("HealthPotion.png");
            player = new Player(new Vector2(1, 1), new Item(Content.Load<Texture2D>("Front_Head"), Content.Load<Texture2D>("Back_Head"), Content.Load<Texture2D>("Left_Head"), Content.Load<Texture2D>("Right_Head"), 4, 4),
                new Item(Content.Load<Texture2D>("Front_Body"), Content.Load<Texture2D>("Back_Body"), Content.Load<Texture2D>("Left_Body"), Content.Load<Texture2D>("Right_Body"), 6, 2),
                new Item(Content.Load<Texture2D>("Front_Legs"), Content.Load<Texture2D>("Back_Legs"), Content.Load<Texture2D>("Left_Legs"), Content.Load<Texture2D>("Right_Legs"), 4, 2),
                new Item(Content.Load<Texture2D>("Front_Weapon"), Content.Load<Texture2D>("Back_Weapon"), Content.Load<Texture2D>("Left_Weapon"), Content.Load<Texture2D>("Right_Weapon"), 0, 10, "sword", new SwordAbilitySet()));
            gui.Buttons.ElementAt(0).LoadContent(Content, "buttonTexture", Convert.ToInt32(player.playerPos.X + 150), Convert.ToInt32(player.playerPos.Y + 150));

            attack = Content.Load<SoundEffect>("AttackSound");
            step = Content.Load<SoundEffect>("Step");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState k = Keyboard.GetState();

            if (k.IsKeyDown(Keys.E))
            {
                GEnemies.Add(new Enemy(test.maze, new Random(), 50));
                player.Epressed = true;
            }

            m.Update();

            foreach(Button b in gui.Buttons)
            {
                b.Update(Convert.ToInt32(player.playerPos.X) * 32 + 150, Convert.ToInt32(player.playerPos.Y) * 32 + 150);
            }

            if (gui.Buttons.ElementAt(0).hasBeenPressed(m.rect, m.m))
            {
                success = "YES NIGGA";
            }

            player.update(test.maze, gameTime, this, out HasMoved, GEnemies, ref HPotions, attack, step);
            
            if (HasMoved == true)
            {
                foreach (Enemy e in GEnemies)
                {
                    float tempX = e.pos.X;
                    float tempY = e.pos.Y;

                    e.Update(player, test.maze);

                    test.maze[Convert.ToInt32(tempX), Convert.ToInt32(tempY)] = "f";
                    if (e.isAlive)
                    {
                        test.maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "e";
                    }
                }
            }
            
            foreach (Enemy e in GEnemies)
            {
                if (e.pos.X == player.playerPos.X + 1 && e.pos.Y == player.playerPos.Y && player.hasAttacked && !e.hasAttacked)
                {
                    e.attack(player);
                    e.hasAttacked = true;
                }
                else if (e.pos.X == player.playerPos.X - 1 && e.pos.Y == player.playerPos.Y && player.hasAttacked && !e.hasAttacked)
                {
                    e.attack(player);
                    e.hasAttacked = true;
                }
                else if (e.pos.X == player.playerPos.X && e.pos.Y == player.playerPos.Y + 1 && player.hasAttacked && !e.hasAttacked)
                {
                    e.attack(player);
                    e.hasAttacked = true;
                }
                else if (e.pos.X == player.playerPos.X && e.pos.Y == player.playerPos.Y - 1 && player.hasAttacked && !e.hasAttacked)
                {
                    e.attack(player);
                    e.hasAttacked = true;
                }

                foreach (HealthPotion i in HPotions)
                {
                    if (!i.hasBeenConsumed)
                    {
                        test.maze[Convert.ToInt32(i.pos.X), Convert.ToInt32(i.pos.Y)] = "h" + HPotions.IndexOf(i).ToString();
                    }
                }

                HasMoved = false;
            }

                // TODO: Add your update logic here
            camera.Update(gameTime, player.playerPos);
            fps.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            fps.frameCounter++;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            test.DrawLevel(spriteBatch, wall, floor, player);

            foreach (HealthPotion i in HPotions)
            {
                i.Draw(spriteBatch, healthPotion);
            }

            foreach (Enemy e in GEnemies)
            {
                e.draw(spriteBatch, player_texture, arial, player);
            }

            player.drawPlayer(spriteBatch);

            spriteBatch.DrawString(arial, "fps: " + fps.frameRate.ToString(), new Vector2(player.playerPos.X * 32 - 300, player.playerPos.Y * 32 - 160), Color.White);
            spriteBatch.DrawString(arial, "health: " + player.Health, new Vector2(player.playerPos.X * 32 - 300, player.playerPos.Y * 32 + 160), Color.Red);
            spriteBatch.DrawString(arial, m.rect.X + " " + m.rect.Y, new Vector2(player.playerPos.X * 32 + 15, player.playerPos.Y * 32 + 15), Color.Red);
            spriteBatch.DrawString(arial, success, new Vector2(player.playerPos.X * 32 + 15, player.playerPos.Y * 32 + 15), Color.Red);
            foreach (Button b in gui.Buttons)
            {
                b.Draw(spriteBatch, arial);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}

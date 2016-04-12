using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Random r;

        public Floor test;
        public Player player;
        public HiScores hiScores = new HiScores();

        public Score score = new Score();

        public List<HealthPotion> HPotions = new List<HealthPotion>();
        public List<Enemy> GEnemies = new List<Enemy>();
        public Dictionary<int, Item> DroppedItems = new Dictionary<int, Item>();

        public Texture2D wall;
        public Texture2D floor;
        public Texture2D player_texture;
        public Texture2D healthPotion;
        public Texture2D healthBar;
        public Texture2D Border;

        MainMenu mainMenu;

        string success = "";

        public string gameState = "mainMenu";

        public SoundEffect attack;
        public SoundEffect step;

        public SpriteFont arial;

        public Camera camera;
        public FrameRateCounter fps;

        public bool HasMoved = false;

        public int floorNum;
        public int itemsProcessed;

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

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void ChangeWeapons_Update(Item i)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Y))
            {
                string type = DroppedItems[Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1))].Type;

                switch (type)
                {
                    case "head":
                        player.Attack -= player.Equiped[0].attackModifier;
                        player.Block -= player.Equiped[0].blockModifier;
                        //player.HealthPercentage = ((double) player.Health )/ ((double) player.maxHealth);

                        player.Equiped[0] = DroppedItems[Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1))];
                        DroppedItems.Remove(Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1)));
                        if (test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Length == 2)
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = " ";
                        }
                        else
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Remove(1, 1);
                        }

                        player.Health = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier) * player.HealthPercentage);
                        player.maxHealth = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier));
                        player.Attack+= player.Equiped[0].attackModifier;
                        player.Block+= player.Equiped[0].blockModifier;
                        break;
                    case "body":
                        player.Attack-= player.Equiped[1].attackModifier;
                        player.Block-= player.Equiped[1].blockModifier;
                        //player.HealthPercentage = ((double)player.Health) / ((double)player.maxHealth);

                        player.Equiped[1] = DroppedItems[Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1))];
                        DroppedItems.Remove(Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1)));
                        if (test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Length == 2)
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = " ";
                        }
                        else
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Remove(1, 1);
                        }

                        player.Health = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier) * player.HealthPercentage);
                        player.maxHealth = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier));
                        player.Attack+= player.Equiped[1].attackModifier;
                        player.Block+= player.Equiped[1].blockModifier;
                        break;
                    case "legs":
                        player.Attack-= player.Equiped[2].attackModifier;
                        player.Block-= player.Equiped[2].blockModifier;
                        //player.HealthPercentage = ((double)player.Health) / ((double)player.maxHealth);

                        player.Equiped[2] = DroppedItems[Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1))];
                        DroppedItems.Remove(Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1)));
                        if (test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Length == 2)
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = " ";
                        }
                        else
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Remove(1, 1);
                        }
                        player.Health = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier) * player.HealthPercentage);
                        player.maxHealth = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier));
                        player.Attack+= player.Equiped[2].attackModifier;
                        player.Block+= player.Equiped[2].blockModifier;
                        break;
                    case "weapon":
                        player.Attack-= player.Equiped[3].attackModifier;
                        player.Block-= player.Equiped[3].blockModifier;
                        //player.HealthPercentage = ((double)player.Health) / ((double)player.maxHealth);

                        player.Equiped[3] = DroppedItems[Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1))];
                        DroppedItems.Remove(Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1)));
                        if (test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Length == 2)
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = " ";
                        }
                        else
                        {
                            test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Remove(1, 1);
                        }
                        player.Health = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier) * player.HealthPercentage);
                        player.maxHealth = Convert.ToInt32((300 + player.Equiped[0].healthModifier + player.Equiped[1].healthModifier + player.Equiped[2].healthModifier + player.Equiped[3].healthModifier));
                        player.Attack+= player.Equiped[3].attackModifier;
                        player.Block+= player.Equiped[3].blockModifier;
                        break;
                }

                gameState = "inGame";
            }
            else if (keyboard.IsKeyDown(Keys.N))
            {
                DroppedItems.Remove(Convert.ToInt32(test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)].Substring(1, 1)));
                test.Item_Grid[Convert.ToInt32(player.pos.X), Convert.ToInt32(player.pos.Y)] = " ";

                gameState = "inGame";
            }
        }

        public void ChangeWeapons_Draw(Item i, SpriteBatch s)
        {
            GraphicsDevice.Clear(Color.White);

            s.DrawString(arial, "Item to change to: Health Modifier = " + i.healthModifier + " Attack Modifier = " + i.attackModifier + " Block Modifier = " + i.blockModifier, new Vector2(100, 100), Color.Black);
            s.Draw(i.Textures[0], new Rectangle(0, 100, 100, 100), i.color);

            switch (i.Type)
            {
                case "head":
                    s.DrawString(arial, "Item being changed: Health Modifier = " + player.Equiped[0].healthModifier + " Attack Modifier = " + player.Equiped[0].attackModifier + " Block Modifier = " + player.Equiped[0].blockModifier, new Vector2(100, 300), Color.Black);
                    s.Draw(player.Equiped[0].Textures[0], new Rectangle(0, 300, 100, 100), player.Equiped[0].color);
                    break;
                case "body":
                    s.DrawString(arial, "Item being changed: Health Modifier = " + player.Equiped[1].healthModifier + " Attack Modifier = " + player.Equiped[1].attackModifier + " Block Modifier = " + player.Equiped[1].blockModifier, new Vector2(100, 300), Color.Black);
                    s.Draw(player.Equiped[1].Textures[0], new Rectangle(0, 300, 100, 100), player.Equiped[1].color);
                    break;
                case "legs":
                    s.DrawString(arial, "Item being changed: Health Modifier = " + player.Equiped[2].healthModifier + " Attack Modifier = " + player.Equiped[2].attackModifier + " Block Modifier = " + player.Equiped[2].blockModifier, new Vector2(100, 300), Color.Black);
                    s.Draw(player.Equiped[2].Textures[0], new Rectangle(0, 300, 100, 100), player.Equiped[2].color);
                    break;
                case "weapon":
                    s.DrawString(arial, "Item being changed: Health Modifier = " + player.Equiped[3].healthModifier + " Attack Modifier = " + player.Equiped[3].attackModifier + " Block Modifier = " + player.Equiped[3].blockModifier, new Vector2(100, 300), Color.Black);
                    s.Draw(player.Equiped[3].Textures[0], new Rectangle(0, 300, 100, 100), player.Equiped[3].color);
                    break;
            }
        }

        public Item randItem (Random r, Vector2 pos, string Type)
        {
            if (Type == null)
            {
                Item i = new Item();

                i.FloorNum = floorNum;

                i.healthModifier = 0;
                i.attackModifier = 0;
                i.blockModifier = 0;

                i.position = pos;

                string rarity;

                int num = r.Next(0, 100);

                if (num < 60)
                {
                    rarity = "common";
                }

                else if (num < 90)
                {
                    rarity = "rare";
                }
                else
                {
                    rarity = "legendary";
                }

                switch (rarity)
                {
                    case "common":
                        int upgradePoints = 10;

                        i.color = Color.Green;

                        while (upgradePoints > 0)
                        {
                            int rndStat = r.Next(0, 3);
                            switch (rndStat)
                            {
                                case 0:
                                    int modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.healthModifier += modifierValue * floorNum;
                                    break;
                                case 1:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.attackModifier += modifierValue * floorNum;
                                    break;
                                case 2:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.blockModifier += modifierValue * floorNum;
                                    break;
                            }
                        }
                        break;

                    case "rare":
                        upgradePoints = 15;

                        i.color = Color.Blue;

                        while (upgradePoints > 0)
                        {
                            int rndStat = r.Next(0, 3);
                            switch (rndStat)
                            {
                                case 0:
                                    int modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.healthModifier += modifierValue * floorNum;
                                    break;
                                case 1:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.attackModifier += modifierValue * floorNum;
                                    break;
                                case 2:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.blockModifier += modifierValue * floorNum;
                                    break;
                            }
                        }
                        break;

                    case "legendary":
                        upgradePoints = 20;

                        i.color = Color.Red;

                        while (upgradePoints > 0)
                        {
                            int rndStat = r.Next(0, 3);
                            switch (rndStat)
                            {
                                case 0:
                                    int modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.healthModifier += modifierValue * floorNum;
                                    break;
                                case 1:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.attackModifier += modifierValue * floorNum;
                                    break;
                                case 2:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.blockModifier += modifierValue * floorNum;
                                    break;
                            }
                        }
                        break;
                }
                switch (r.Next(0, 4))
                {
                    case 0:
                        i.Type = "legs";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_legs");
                        i.Textures[1] = Content.Load<Texture2D>("back_legs");
                        i.Textures[2] = Content.Load<Texture2D>("left_legs");
                        i.Textures[3] = Content.Load<Texture2D>("right_legs");
                        break;
                    case 1:
                        i.Type = "body";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_body");
                        i.Textures[1] = Content.Load<Texture2D>("back_body");
                        i.Textures[2] = Content.Load<Texture2D>("left_body");
                        i.Textures[3] = Content.Load<Texture2D>("right_body");
                        break;
                    case 2:
                        i.Type = "head";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_head");
                        i.Textures[1] = Content.Load<Texture2D>("back_head");
                        i.Textures[2] = Content.Load<Texture2D>("left_head");
                        i.Textures[3] = Content.Load<Texture2D>("right_head");
                        break;
                    case 3:
                        i.Type = "weapon";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_weapon");
                        i.Textures[1] = Content.Load<Texture2D>("back_weapon");
                        i.Textures[2] = Content.Load<Texture2D>("left_weapon");
                        i.Textures[3] = Content.Load<Texture2D>("right_weapon");
                        i.SAbilities = new SwordAbilitySet(attack, attack, attack);
                        break;
                }
                return i;
            }
            else
            {
                Item i = new Item();

                i.healthModifier = 0;
                i.attackModifier = 0;
                i.blockModifier = 0;

                i.position = pos;

                string rarity;

                int num = r.Next(0, 100);

                if (num < 60)
                {
                    rarity = "common";
                }

                else if (num < 90)
                {
                    rarity = "rare";
                }
                else
                {
                    rarity = "legendary";
                }

                switch (rarity)
                {
                    case "common":
                        int upgradePoints = 10;

                        i.color = Color.Green;

                        while (upgradePoints > 0)
                        {
                            int rndStat = r.Next(0, 3);
                            switch (rndStat)
                            {
                                case 0:
                                    int modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.healthModifier += modifierValue * floorNum;
                                    break;
                                case 1:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.attackModifier += modifierValue * floorNum;
                                    break;
                                case 2:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.blockModifier += modifierValue * floorNum;
                                    break;
                            }
                        }
                        break;

                    case "rare":
                        upgradePoints = 15;

                        i.color = Color.Blue;

                        while (upgradePoints > 0)
                        {
                            int rndStat = r.Next(0, 3);
                            switch (rndStat)
                            {
                                case 0:
                                    int modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.healthModifier += modifierValue * floorNum;
                                    break;
                                case 1:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.attackModifier += modifierValue * floorNum;
                                    break;
                                case 2:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.blockModifier += modifierValue * floorNum;
                                    break;
                            }
                        }
                        break;

                    case "legendary":
                        upgradePoints = 20;

                        i.color = Color.Red;

                        while (upgradePoints > 0)
                        {
                            int rndStat = r.Next(0, 3);
                            switch (rndStat)
                            {
                                case 0:
                                    int modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.healthModifier += modifierValue * floorNum;
                                    break;
                                case 1:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.attackModifier += modifierValue * floorNum;
                                    break;
                                case 2:
                                    modifierValue = r.Next(0, upgradePoints + 1);
                                    upgradePoints -= modifierValue;
                                    i.blockModifier += modifierValue * floorNum;
                                    break;
                            }
                        }
                        break;
                }
                switch (Type)
                {
                    case "legs":
                        i.Type = "legs";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_legs");
                        i.Textures[1] = Content.Load<Texture2D>("back_legs");
                        i.Textures[2] = Content.Load<Texture2D>("left_legs");
                        i.Textures[3] = Content.Load<Texture2D>("right_legs");
                        break;
                    case "body":
                        i.Type = "body";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_body");
                        i.Textures[1] = Content.Load<Texture2D>("back_body");
                        i.Textures[2] = Content.Load<Texture2D>("left_body");
                        i.Textures[3] = Content.Load<Texture2D>("right_body");
                        break;
                    case "head":
                        i.Type = "head";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_head");
                        i.Textures[1] = Content.Load<Texture2D>("back_head");
                        i.Textures[2] = Content.Load<Texture2D>("left_head");
                        i.Textures[3] = Content.Load<Texture2D>("right_head");
                        break;
                    case "weapon":
                        i.Type = "weapon";
                        i.Textures = new Texture2D[4];
                        i.Textures[0] = Content.Load<Texture2D>("front_weapon");
                        i.Textures[1] = Content.Load<Texture2D>("back_weapon");
                        i.Textures[2] = Content.Load<Texture2D>("left_weapon");
                        i.Textures[3] = Content.Load<Texture2D>("right_weapon");
                        i.SAbilities = new SwordAbilitySet(attack, attack, attack);
                        break;
                }
                return i;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMenu = new MainMenu();

            mainMenu.LoadContent(Content.Load<Texture2D>("StartGame.png"), Content.Load<Texture2D>("Exit.png"), Content.Load<Texture2D>("Options.png"), Content.Load<Texture2D>("HiScores.png"), Content.Load<Texture2D>("Arrow.png"));

            healthBar = Content.Load<Texture2D>("RedSquare.png");
            Border = Content.Load<Texture2D>("Border.png");
            arial = Content.Load<SpriteFont>("myFont");

            if (!File.Exists("HiScores.txt"))
            {
                File.Create("HiScores.txt");
            }
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
            switch (gameState)
            {
                case "exit":
                    System.Environment.Exit(1);
                    break;

                case "character_sheet":
                    KeyboardState k = Keyboard.GetState();
                    if (k.IsKeyDown(Keys.C) && player.Cpressed == false)
                    {
                        gameState = "inGame";
                        player.Cpressed = true;
                    }
                    if (k.IsKeyUp(Keys.C))
                    {
                        player.Cpressed = false;
                    }
                    break;
                case "init_game":
                    HPotions = new List<HealthPotion>();
                    GEnemies = new List<Enemy>();
                    IsMouseVisible = true;

                    floorNum = 1;

                    r = new Random();

                    // TODO: Add your initialization logic here
                    test = new Floor(20, null);

                    for (int i = 0; i < 5; i++)
                    {
                        HealthPotion h = new HealthPotion(r, test);
                        HPotions.Insert(i, h);
                    }

                    camera = new Camera();

                    fps = new FrameRateCounter();

                    foreach (HealthPotion i in HPotions)
                    {
                        if (!i.hasBeenConsumed)
                        {
                            test.HP_Grid[Convert.ToInt32(i.pos.X), Convert.ToInt32(i.pos.Y)] = "h" + HPotions.IndexOf(i).ToString();
                        }
                    }

                    floor = Content.Load<Texture2D>("Tileable2d.png");
                    wall = Content.Load<Texture2D>("Tileable10y.png");
                    arial = Content.Load<SpriteFont>("myFont");
                    player_texture = Content.Load<Texture2D>("jordeKang.jpg");
                    healthPotion = Content.Load<Texture2D>("HealthPotion.png");
                    attack = Content.Load<SoundEffect>("AttackSound");
                    step = Content.Load<SoundEffect>("Step");

                    player = new Player(new Vector2(1, 1), new Item(Content.Load<Texture2D>("Front_Head"), Content.Load<Texture2D>("Back_Head"), Content.Load<Texture2D>("Left_Head"), Content.Load<Texture2D>("Right_Head"), 4, 4, 1, "rare"),
             new Item(Content.Load<Texture2D>("Front_Body"), Content.Load<Texture2D>("Back_Body"), Content.Load<Texture2D>("Left_Body"), Content.Load<Texture2D>("Right_Body"), 6, 2, 1, "rare"),
             new Item(Content.Load<Texture2D>("Front_Legs"), Content.Load<Texture2D>("Back_Legs"), Content.Load<Texture2D>("Left_Legs"), Content.Load<Texture2D>("Right_Legs"), 4, 2, 1, "rare"),
             new Item(Content.Load<Texture2D>("Front_Weapon"), Content.Load<Texture2D>("Back_Weapon"), Content.Load<Texture2D>("Left_Weapon"), Content.Load<Texture2D>("Right_Weapon"), 0, 10, 1, "sword", "rare", new SwordAbilitySet(attack, attack, attack)));

                    for (int i = 0; i < floorNum * 3; i++)
                    {
                        Enemy e = new Enemy(test.Wall_Grid, r);

                        e.Equiped.Add(randItem(r, e.pos, "head"));
                        e.Equiped.Add(randItem(r, e.pos, "body"));
                        e.Equiped.Add(randItem(r, e.pos, "legs"));
                        e.Equiped.Add(randItem(r, e.pos, "weapon"));

                        e.UpdateStatistics(floorNum);

                        foreach (Item I in e.Equiped)
                        {
                            e.color = Color.Green;

                            if (e.color == Color.Green && (I.color == Color.Blue || I.color == Color.Red))
                            {
                                e.color = I.color;
                            }
                            if (e.color == Color.Blue && I.color == Color.Red)
                            {
                                e.color = I.color;
                            }
                        }

                        GEnemies.Insert(i, e);
                    }

                    gameState = "inGame";
                    break;

                case "genLevel":

                    floorNum++;

                    GEnemies = new List<Enemy>();
                    HPotions = new List<HealthPotion>();
                    DroppedItems = new Dictionary<int, Item>();

                    camera = new Camera();

                    fps = new FrameRateCounter();

                    for (int i = 0; i < 5; i++)
                    {
                        HealthPotion h = new HealthPotion(r, test);
                        HPotions.Insert(i, h);
                    }

                    test = new Floor(20, null);

                    for (int i = 0; i < floorNum * 3; i++)
                    {
                        Enemy e = new Enemy(test.Wall_Grid, r);

                        e.Equiped.Add(randItem(r, e.pos, "head"));
                        e.Equiped.Add(randItem(r, e.pos, "body"));
                        e.Equiped.Add(randItem(r, e.pos, "legs"));
                        e.Equiped.Add(randItem(r, e.pos, "weapon"));

                        e.UpdateStatistics(floorNum);

                        foreach (Item I in e.Equiped)
                        {
                            e.color = Color.Green;

                            if (e.color == Color.Green && (I.color == Color.Blue || I.color == Color.Red))
                            {
                                e.color = I.color;
                            }
                            if (e.color == Color.Blue && I.color == Color.Red)
                            {
                                e.color = I.color;
                            }
                        }

                        GEnemies.Insert(i, e);
                    }

                    foreach (HealthPotion i in HPotions)
                    {
                        if (!i.hasBeenConsumed)
                        {
                            test.HP_Grid[Convert.ToInt32(i.pos.X), Convert.ToInt32(i.pos.Y)] = "h" + HPotions.IndexOf(i).ToString();
                        }
                    }

                    player.pos = new Vector2(1, 1);

                    gameState = "inGame";
                    break;
                case "mainMenu":
                    mainMenu.Update(ref gameState);
                    break;
                case "loading_test":
                    HPotions = new List<HealthPotion>();
                    GEnemies = new List<Enemy>();
                    IsMouseVisible = true;
                    camera = new Camera();
                    fps = new FrameRateCounter();
                    floorNum = 1;

                    r = new Random();

                    test = new Floor();

                    floor = Content.Load<Texture2D>("Tileable2d.png");
                    wall = Content.Load<Texture2D>("Tileable10y.png");
                    arial = Content.Load<SpriteFont>("myFont");
                    player_texture = Content.Load<Texture2D>("jordeKang.jpg");
                    healthPotion = Content.Load<Texture2D>("HealthPotion.png");
                    attack = Content.Load<SoundEffect>("AttackSound");
                    step = Content.Load<SoundEffect>("Step");

                    player = new Player(new Vector2(1, 1), new Item(Content.Load<Texture2D>("Front_Head"), Content.Load<Texture2D>("Back_Head"), Content.Load<Texture2D>("Left_Head"), Content.Load<Texture2D>("Right_Head"), 4, 4, 1, "rare"),
             new Item(Content.Load<Texture2D>("Front_Body"), Content.Load<Texture2D>("Back_Body"), Content.Load<Texture2D>("Left_Body"), Content.Load<Texture2D>("Right_Body"), 6, 2, 1, "rare"),
             new Item(Content.Load<Texture2D>("Front_Legs"), Content.Load<Texture2D>("Back_Legs"), Content.Load<Texture2D>("Left_Legs"), Content.Load<Texture2D>("Right_Legs"), 4, 2, 1, "rare"),
             new Item(Content.Load<Texture2D>("Front_Weapon"), Content.Load<Texture2D>("Back_Weapon"), Content.Load<Texture2D>("Left_Weapon"), Content.Load<Texture2D>("Right_Weapon"), 0, 10, 1, "sword", "rare", new SwordAbilitySet(attack, attack, attack)));

                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            test.Wall_Grid[j, i] = "f";

                            if (j == 0 || i == 0 || j == 19 || i == 19 || (j == 2 && i == 17) || ((j > 8 && j < 19) && i == 9) || (j == 9 && i > 10) || (i == 11 && (j == 11 || j == 13 || j == 14 || j == 16 || j == 17)) || (i == 12 && (j == 11 || j == 17)) || (i == 13 && (j == 11 || j == 12 || j == 14 || j == 15 || j == 17)) || (i == 14 && (j == 15)) || (i == 15 && (j == 10 || j == 11 || j == 13 || j == 15 || j == 17 || j == 18)) || (i == 16 && (j == 13 || j == 15)) || (i == 17 && (j == 11 || j == 13 || j == 17)) || (i == 18 && (j == 15)))
                            {
                                test.Wall_Grid[j, i] = "w";
                            }

                            if ((i > 1 && i < 7 && j > 9 && j < 17 && !((i == 4 && j < 14))) || (j == 2 && (i < 16 && i > 11)))
                            {

                                Enemy e = new Enemy(new Vector2(j, i), randItem(r, new Vector2(j, i), "head"), randItem(r, new Vector2(j, i), "body"), randItem(r, new Vector2(j, i), "legs"), randItem(r, new Vector2(j, i), "weapon"), false);

                                e.UpdateStatistics(floorNum);

                                foreach (Item I in e.Equiped)
                                {
                                    e.color = Color.Green;

                                    if (e.color == Color.Green && (I.color == Color.Blue || I.color == Color.Red))
                                    {
                                        e.color = I.color;
                                    }
                                    if (e.color == Color.Blue && I.color == Color.Red)
                                    {
                                        e.color = I.color;
                                    }
                                }

                                GEnemies.Insert(GEnemies.Count, e);
                            }

                            if (j == 18 && i == 18)
                            {
                                Enemy e = new Enemy(new Vector2(j, i), randItem(r, new Vector2(j, i), "head"), randItem(r, new Vector2(j, i), "body"), randItem(r, new Vector2(j, i), "legs"), randItem(r, new Vector2(j, i), "weapon"), true);

                                e.UpdateStatistics(floorNum);

                                foreach (Item I in e.Equiped)
                                {
                                    e.color = Color.Green;

                                    if (e.color == Color.Green && (I.color == Color.Blue || I.color == Color.Red))
                                    {
                                        e.color = I.color;
                                    }
                                    if (e.color == Color.Blue && I.color == Color.Red)
                                    {
                                        e.color = I.color;
                                    }
                                }

                                GEnemies.Insert(i, e);
                            }

                            if (i == 17 && (j == 3 || j == 7 || j == 8))
                            {
                                Enemy e = new Enemy(new Vector2(j, i), randItem(r, new Vector2(j, i), "head"), randItem(r, new Vector2(j, i), "body"), randItem(r, new Vector2(j, i), "legs"), randItem(r, new Vector2(j, i), "weapon"), false);

                                e.UpdateStatistics(floorNum);

                                foreach (Item I in e.Equiped)
                                {
                                    e.color = Color.Green;

                                    if (e.color == Color.Green && (I.color == Color.Blue || I.color == Color.Red))
                                    {
                                        e.color = I.color;
                                    }
                                    if (e.color == Color.Blue && I.color == Color.Red)
                                    {
                                        e.color = I.color;
                                    }
                                }

                                GEnemies.Insert(i, e);
                            }

                            if (i == 17 && (j == 4 || j == 6 || j == 8))
                            {

                                HealthPotion h = new HealthPotion();
                                h.pos = new Vector2(j, i);
                                h.rect = new Rectangle(Convert.ToInt32(h.pos.X * 32), Convert.ToInt32(h.pos.Y * 32), 32, 32);
                                HPotions.Insert(HPotions.Count, h);

                            }

                            if (i == 17 && (j == 5 || j == 6 || j == 7))
                            {
                                Item item = randItem(r, new Vector2(j,i), "head");
                                DroppedItems.Add(DroppedItems.Count, item);
                                int index = DroppedItems.First(x => x.Value == item).Key;

                                test.Item_Grid[Convert.ToInt32(item.position.X), Convert.ToInt32(item.position.Y)] = "i" + index;
                            }
                        }
                    }

                    foreach (HealthPotion i in HPotions)
                    {
                        if (!i.hasBeenConsumed)
                        {
                            test.HP_Grid[Convert.ToInt32(i.pos.X), Convert.ToInt32(i.pos.Y)] = "h" + HPotions.IndexOf(i).ToString();
                        }
                    }

                    player.pos = new Vector2(1, 1);

                    gameState = "inGame";

                    break;
                case "inGame":
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (player.Health <= 0)
                    {
                        gameState = "calcHiScore";
                    }


                    k = Keyboard.GetState();

                    if (k.IsKeyDown(Keys.E))
                    {
                        GEnemies.Add(new Enemy(test.Wall_Grid, new Random(), new Item(Content.Load<Texture2D>("Front_Head"), Content.Load<Texture2D>("Back_Head"), Content.Load<Texture2D>("Left_Head"), Content.Load<Texture2D>("Right_Head"), 4, 4, 0, "common"),
            new Item(Content.Load<Texture2D>("Front_Body"), Content.Load<Texture2D>("Back_Body"), Content.Load<Texture2D>("Left_Body"), Content.Load<Texture2D>("Right_Body"), 6, 2, 0, "common"),
            new Item(Content.Load<Texture2D>("Front_Legs"), Content.Load<Texture2D>("Back_Legs"), Content.Load<Texture2D>("Left_Legs"), Content.Load<Texture2D>("Right_Legs"), 4, 2, 0, "common"),
            new Item(Content.Load<Texture2D>("Front_Weapon"), Content.Load<Texture2D>("Back_Weapon"), Content.Load<Texture2D>("Left_Weapon"), Content.Load<Texture2D>("Right_Weapon"), 0, 10, 0, "sword", "common", new SwordAbilitySet(attack, attack, attack)), floorNum));
                        player.Epressed = true;
                    }

                    if (k.IsKeyDown(Keys.C) && player.Cpressed == false)
                    {
                        score.Total(player, this);

                        gameState = "character_sheet";
                        player.Cpressed = true;
                    }


                    player.update(test, gameTime, this, out HasMoved, GEnemies, ref HPotions, attack, step, ref gameState, ref DroppedItems);

                    if (HasMoved == true)
                    {
                        foreach (Enemy e in GEnemies)
                        {
                            float tempX = e.pos.X;
                            float tempY = e.pos.Y;

                            e.Update(player, test, ref DroppedItems, this, r);

                            test.Enemy_Grid[Convert.ToInt32(tempX), Convert.ToInt32(tempY)] = " ";
                            if (e.isAlive)
                            {
                                test.Enemy_Grid[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "e";
                            }
                            else
                            {
                                test.Enemy_Grid[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = " ";

                            }
                        }
                    }

                    foreach (Enemy e in GEnemies)
                    {
                        if (e.pos.X == player.pos.X + 1 && e.pos.Y == player.pos.Y && player.hasAttacked && !e.hasAttacked && e.isAlive && e.isActive)
                        {
                            e.attack(player);
                            e.hasAttacked = true;
                        }
                        else if (e.pos.X == player.pos.X - 1 && e.pos.Y == player.pos.Y && player.hasAttacked && !e.hasAttacked && e.isAlive && e.isActive)
                        {
                            e.attack(player);
                            e.hasAttacked = true;
                        }
                        else if (e.pos.X == player.pos.X && e.pos.Y == player.pos.Y + 1 && player.hasAttacked && !e.hasAttacked && e.isAlive && e.isActive)
                        {
                            e.attack(player);
                            e.hasAttacked = true;
                        }
                        else if (e.pos.X == player.pos.X && e.pos.Y == player.pos.Y - 1 && player.hasAttacked && !e.hasAttacked && e.isAlive && e.isActive)
                        {
                            e.attack(player);
                            e.hasAttacked = true;
                        }

                        foreach (HealthPotion i in HPotions)
                        {
                            if (!i.hasBeenConsumed)
                            {
                                test.HP_Grid[Convert.ToInt32(i.pos.X), Convert.ToInt32(i.pos.Y)] = "h" + HPotions.IndexOf(i).ToString();
                            }
                        }

                        HasMoved = false;
                    }

                    // TODO: Add your update logic here
                    camera.Update(gameTime, player.pos);
                    fps.Update(gameTime);
                    break;
                case "changeItem":
                    ChangeWeapons_Update(DroppedItems[Convert.ToInt32(test.Item_Grid[(int)player.pos.X, (int)player.pos.Y].Substring(1, 1))]);
                    break;
                case "hiScore":
                    k = Keyboard.GetState();

                    if (k.IsKeyDown(Keys.Escape))
                    {
                        gameState = "mainMenu";
                    }
                    break;
                case "GameOver":
                    k = Keyboard.GetState();

                    if (k.IsKeyDown(Keys.M))
                    {
                        gameState = "mainMenu";
                    }
                    break;
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
            switch (gameState)
            {
                case "hiScore":
                    
                    spriteBatch.Begin();
                    spriteBatch.DrawString(arial, "Hi scores hall of eternal glory!", new Vector2(graphics.PreferredBackBufferWidth / 2 - (arial.MeasureString("Hi scores hall of eternal glory!").X + arial.MeasureString("Hi scores hall of eternal glory!").X/2), 50), Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);

                    for (int i = 0; i < 10; i++ )
                    {
                        spriteBatch.DrawString(arial, (i + 1).ToString() + "." + hiScores.GetHiScores()[i], new Vector2(graphics.PreferredBackBufferWidth/2 - arial.MeasureString((i + 1).ToString() + "." + hiScores.GetHiScores()[i]).X, 200 + i*50), Color.White, 0, new Vector2(0,0), 2, SpriteEffects.None, 0);
                    }
                    spriteBatch.End();
                    break;
                case "character_sheet":
                    spriteBatch.Begin();
                    spriteBatch.Draw(player.Equiped[0].Textures[0], new Rectangle(50, 50, 200, 200), player.Equiped[0].color);
                    spriteBatch.Draw(player.Equiped[1].Textures[0], new Rectangle(275, 50, 200, 200), player.Equiped[1].color);
                    spriteBatch.Draw(player.Equiped[2].Textures[0], new Rectangle(50, 275, 200, 200), player.Equiped[2].color);
                    spriteBatch.Draw(player.Equiped[3].Textures[0], new Rectangle(275, 275, 200, 200), player.Equiped[3].color);

                    spriteBatch.DrawString(arial, "                  Base   Head   Body  Legs  Weapon  Total", new Vector2(600, 50), Color.White);
                    spriteBatch.DrawString(arial, "health Modifier:    300    " + player.Equiped[0].healthModifier + "     " + player.Equiped[1].healthModifier + "     " + player.Equiped[2].healthModifier + "      " + player.Equiped[3].healthModifier + "      " + player.maxHealth, new Vector2(600, 150), Color.White);
                    spriteBatch.DrawString(arial, "attack Modifier:           " + player.Equiped[0].attackModifier + "     " + player.Equiped[1].attackModifier + "     " + player.Equiped[2].attackModifier + "      " + player.Equiped[3].attackModifier + "      " + player.Attack, new Vector2(600, 250), Color.White);
                    spriteBatch.DrawString(arial, "block Modifier:            " + player.Equiped[0].blockModifier + "     " + player.Equiped[1].blockModifier + "     " + player.Equiped[2].blockModifier + "      " + player.Equiped[3].blockModifier + "      " + player.Block, new Vector2(600, 350), Color.White);

                    spriteBatch.Draw(healthPotion, new Rectangle(50, 500, 100, 100), Color.White);
                    spriteBatch.DrawString(arial, "= " + player.HealthPotions, new Vector2(150, 500), Color.White, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0);

                    spriteBatch.Draw(Border, new Rectangle(50, 50, 200, 200), Color.White);
                    spriteBatch.Draw(Border, new Rectangle(275, 50, 200, 200), Color.White);
                    spriteBatch.Draw(Border, new Rectangle(50, 275, 200, 200), Color.White);
                    spriteBatch.Draw(Border, new Rectangle(275, 275, 200, 200), Color.White);

                    spriteBatch.Draw(Border, new Rectangle(575, 25, 700, 400), Color.White);

                    spriteBatch.Draw(Border, new Rectangle(50, 475, 300, 150), Color.White);

                    spriteBatch.DrawString(arial, "Current score: " + score.totalScore.ToString(), new Vector2(400, 475), Color.White, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0);

                    spriteBatch.End();
                    break;

                case "calcHiScore":
                    score.Total(player, this);
                    hiScores.AddHiscore(score.totalScore, ref score.position);
                    gameState = "GameOver";
                    break;
                case "GameOver":
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("GameOver.png"), new Vector2(0, 0), Color.Red);
                    spriteBatch.DrawString(arial, "Your score was: " + score.totalScore.ToString(), new Vector2(50, 500), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                    if (score.position == 0)
                    {
                        spriteBatch.DrawString(arial, "You did not make a new hi-score. better luck next time!", new Vector2(50, 550), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.DrawString(arial, "Congratulations! Your position on the scoreboard was:  " + score.position.ToString(), new Vector2(50, 550), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                    }
                    spriteBatch.End();
                    break;
                case "genLevel":
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Loading.png"), new Vector2(0, 0), Color.White);
                    spriteBatch.End();
                    break;
                case "init_game":
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Loading.png"), new Vector2(0, 0), Color.White);
                    spriteBatch.End();
                    break;
                case "mainMenu":
                    spriteBatch.Begin();
                    mainMenu.Draw(spriteBatch, this.graphics);
                    spriteBatch.End();
                    break;
                case "changeItem":
                    spriteBatch.Begin();
                    ChangeWeapons_Draw(DroppedItems[Convert.ToInt32(test.Item_Grid[(int)player.pos.X, (int)player.pos.Y].Substring(1, 1))], spriteBatch);
                    spriteBatch.End();
                    break;
                case "inGame":
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
                    fps.frameCounter++;

                    test.DrawLevel(spriteBatch, wall, floor, Content.Load<Texture2D>("Staircase.png"), player);

                    foreach (Item i in DroppedItems.Values)
                    {
                        i.Draw(spriteBatch);
                    }

                    foreach (HealthPotion i in HPotions)
                    {
                        i.Draw(spriteBatch, healthPotion);
                    }

                    foreach (Enemy e in GEnemies)
                    {
                        e.draw(spriteBatch, player_texture, healthBar, arial, player);
                    }

                    player.drawPlayer(spriteBatch);



                    spriteBatch.DrawString(arial, "fps: " + fps.frameRate.ToString(), new Vector2(player.pos.X * 32 - 300, player.pos.Y * 32 - 160), Color.White);
                    spriteBatch.DrawString(arial, "health potions: " + player.HealthPotions, new Vector2(player.pos.X * 32 - 300, player.pos.Y * 32 - 120), Color.White);
                    spriteBatch.DrawString(arial, "health: " + player.Health + "/" + player.maxHealth, new Vector2(player.pos.X * 32 - 300, player.pos.Y * 32 + 160), Color.Red);
                    spriteBatch.DrawString(arial, success, new Vector2(player.pos.X * 32 + 15, player.pos.Y * 32 + 15), Color.Red);

                    spriteBatch.Draw(healthBar, new Rectangle((int) player.pos.X * 32 - 30, (int) player.pos.Y * 32 + 150, Convert.ToInt32(arial.MeasureString("slash").X * player.Equiped[3].SAbilities.skill1_percentage), Convert.ToInt32(arial.MeasureString("slash").Y)), Color.White);
                    spriteBatch.Draw(healthBar, new Rectangle((int)player.pos.X * 32 + (int)arial.MeasureString("slash").X, (int)player.pos.Y * 32 + 150, Convert.ToInt32(arial.MeasureString("lunge").X * player.Equiped[3].SAbilities.skill2_percentage), Convert.ToInt32(arial.MeasureString("slash").Y)), Color.White);
                    spriteBatch.Draw(healthBar, new Rectangle((int)player.pos.X * 32 + 30 + (int)arial.MeasureString("slash").X + (int)arial.MeasureString("lunge").X, (int)player.pos.Y * 32 + 150, Convert.ToInt32(arial.MeasureString("whirl").X * player.Equiped[3].SAbilities.skill3_percentage), Convert.ToInt32(arial.MeasureString("slash").Y)), Color.White);

                    spriteBatch.DrawString(arial, "slash", new Vector2(player.pos.X * 32 - 30, player.pos.Y * 32 + 150), Color.White);
                    spriteBatch.DrawString(arial, "lunge", new Vector2(player.pos.X * 32 + arial.MeasureString("slash").X, player.pos.Y * 32 + 150), Color.White);
                    spriteBatch.DrawString(arial, "whirl", new Vector2(player.pos.X * 32 + 30 + arial.MeasureString("slash").X + arial.MeasureString("lunge").X, player.pos.Y * 32 + 150), Color.White);

                    spriteBatch.DrawString(arial, "Health Modifier " + player.Health, new Vector2(player.pos.X * 32 + 100, player.pos.Y * 32 - 160), Color.White);
                    spriteBatch.DrawString(arial, "Block Modifier " + player.Block, new Vector2(player.pos.X * 32 + 100, player.pos.Y * 32 - 120), Color.White);
                    spriteBatch.DrawString(arial, "Attack Modifier " + player.Attack, new Vector2(player.pos.X * 32 + 100, player.pos.Y * 32 - 140), Color.White);

                    spriteBatch.End();
                    // TODO: Add your drawing code here
                    break;
            }

            base.Draw(gameTime);
        }
    }
}

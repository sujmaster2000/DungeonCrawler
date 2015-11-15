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
    class Player
    {
        public char direction = 'r';

        bool Apressed = false;
        bool Dpressed = false;
        bool Spressed = false;
        bool Wpressed = false;
        public bool OnePressed = false;
        public bool TwoPressed = false;
        public bool ThreePressed = false;

        Item[] Equiped = new Item[4];

        public Vector2 playerPos;

        public int Health = 300;

        public int AttackDamage = 50;

        public Player(Vector2 Pos, Item Head, Item Body, Item Legs, Item Weapon)
        {
            playerPos = Pos;
            Equiped[0] = Head;
            Equiped[1] = Body;
            Equiped[2] = Legs;
            Equiped[3] = Weapon;

            AttackDamage += Head.attackModifier + Body.attackModifier + Legs.attackModifier + Weapon.attackModifier;
            Health += Head.healthModifier + Body.healthModifier + Legs.healthModifier + Weapon.healthModifier;
        }

        public void update(string[,] Maze, GameTime g, Game1 game,out bool hasPerformedAction, List<Enemy> Enemies, SoundEffect attack, SoundEffect step)
        {
            hasPerformedAction = false;
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.LeftShift))
            {
                if (k.IsKeyDown(Keys.D))
                {
                    direction = 'r';
                    if (Maze[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "w" && Maze[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.X += 1;
                        hasPerformedAction = true;
                        step.Play();
                    }
                }
                else if (k.IsKeyDown(Keys.A))
                {
                    direction = 'l';
                    if (Maze[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "w" && Maze[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.X -= 1;
                        hasPerformedAction = true;
                        step.Play();
                    }
                }
                else if (k.IsKeyDown(Keys.S))
                {
                    direction = 'd';
                    if(Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "w" && Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.Y += 1;
                        hasPerformedAction = true;
                        step.Play();
                    }
                }
                else if (k.IsKeyDown(Keys.W))
                {
                    direction = 'u';
                    if (Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "w" && Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.Y -= 1;
                        hasPerformedAction = true;
                        step.Play();
                    }
                }

            }
            else
            {

                if (k.IsKeyDown(Keys.D))
                {
                    direction = 'r';
                    if (Maze[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "w" && Maze[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "e" && !Dpressed)
                    {
                        playerPos.X += 1;
                        hasPerformedAction = true;
                        Dpressed = true;
                        step.Play();
                    }
                }
                else if (k.IsKeyDown(Keys.A))
                {
                    direction = 'l';
                    if (Maze[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "w" && Maze[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "e" && !Apressed)
                    {
                        playerPos.X -= 1;
                        hasPerformedAction = true;
                        Apressed = true;
                        step.Play();
                    }
                }
                else if (k.IsKeyDown(Keys.S))
                {
                    direction = 'd';
                    if (Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "w" && Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "e" && !Spressed)
                    {
                        playerPos.Y += 1;
                        hasPerformedAction = true;
                        Spressed = true;
                        step.Play();
                    }
                }
                else if (k.IsKeyDown(Keys.W))
                {
                    direction = 'u';
                    if (Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "w" && Maze[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "e" && !Wpressed)
                    {
                        playerPos.Y -= 1;
                        hasPerformedAction = true;
                        Wpressed = true;
                        step.Play();
                    }
                }
                
                else if (k.IsKeyDown(Keys.D1) && !OnePressed)
                {
                    Equiped[3].SAbilities.Skill1(ref Enemies, this, game, Maze);
                }

                else if (k.IsKeyDown(Keys.D2) && !TwoPressed)
                {
                    Equiped[3].SAbilities.Skill2(ref Enemies, this, game, Maze);
                }
                else if (k.IsKeyDown(Keys.D3) && !ThreePressed)
                {
                    Equiped[3].SAbilities.Skill3(ref Enemies, this, game, Maze);
                }

                if (k.IsKeyUp(Keys.D))
                {
                    Dpressed = false;
                    
                }
                if (k.IsKeyUp(Keys.A))
                {
                    Apressed = false;
                    
                }
                if (k.IsKeyUp(Keys.S))
                {
                    Spressed = false;
                    
                }
                if (k.IsKeyUp(Keys.W))
                {
                    Wpressed = false;
                    
                }
                if (k.IsKeyUp(Keys.D1))
                {
                    OnePressed = false;
                    foreach (Enemy e in Enemies)
                    {
                        e.hasAttacked = false;
                    }
                }
                if (k.IsKeyUp(Keys.D2))
                {
                    TwoPressed = false;
                    foreach (Enemy e in Enemies)
                    {
                        e.hasAttacked = false;
                    }
                }
                if (k.IsKeyUp(Keys.D3))
                {
                    ThreePressed = false;
                    foreach (Enemy e in Enemies)
                    {
                        e.hasAttacked = false;
                    }
                }
            }
   
            if (Maze[Convert.ToInt32(playerPos.X), Convert.ToInt32(playerPos.Y)] == "h")
            {
                Health += 50;
            }
        }

        public void drawPlayer(SpriteBatch s)
        {
            switch (direction)
            {
                case 'd':
                    {

                        s.Draw(Equiped[0].Textures[0], playerPos * 32, Color.White);
                        s.Draw(Equiped[1].Textures[0], playerPos * 32, Color.White);
                        s.Draw(Equiped[2].Textures[0], playerPos * 32, Color.White);
                        s.Draw(Equiped[3].Textures[0], playerPos * 32, Color.White);
                        break;
                    }
                case 'u':
                    {
                        s.Draw(Equiped[3].Textures[1], playerPos * 32, Color.White);
                        s.Draw(Equiped[0].Textures[1], playerPos * 32, Color.White);
                        s.Draw(Equiped[1].Textures[1], playerPos * 32, Color.White);
                        s.Draw(Equiped[2].Textures[1], playerPos * 32, Color.White);

                        break;
                    }
                case 'l':
                    {
                        s.Draw(Equiped[0].Textures[2], playerPos * 32, Color.White);
                        s.Draw(Equiped[1].Textures[2], playerPos * 32, Color.White);
                        s.Draw(Equiped[2].Textures[2], playerPos * 32, Color.White);
                        s.Draw(Equiped[3].Textures[2], playerPos * 32, Color.White);
                        break;
                    }
                case 'r':
                    {
                        s.Draw(Equiped[0].Textures[3], playerPos * 32, Color.White);
                        s.Draw(Equiped[1].Textures[3], playerPos * 32, Color.White);
                        s.Draw(Equiped[2].Textures[3], playerPos * 32, Color.White);
                        s.Draw(Equiped[3].Textures[3], playerPos * 32, Color.White);
                        break;
                    }
            }
        }
    }
}

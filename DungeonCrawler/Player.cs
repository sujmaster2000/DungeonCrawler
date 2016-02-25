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
    public class Player
    {
        public char direction = 'r';

        bool Apressed = false;
        bool Dpressed = false;
        bool Spressed = false;
        bool Wpressed = false;
        public bool Epressed = false;
        public bool OnePressed = false;
        public bool TwoPressed = false;
        public bool ThreePressed = false;
        public bool hasAttacked;

        public Item[] Equiped = new Item[4];

        public Vector2 playerPos;

        public int Health = 300;

        public int Attack = 0;

        public double Block = 0;

        public Player()
        {

        }

        public Player(Vector2 Pos, Item Head, Item Body, Item Legs, Item Weapon)
        {
            playerPos = Pos;
            Equiped[0] = Head;
            Equiped[1] = Body;
            Equiped[2] = Legs;
            Equiped[3] = Weapon;

            Attack += Head.attackModifier + Body.attackModifier + Legs.attackModifier + Weapon.attackModifier;
            Health += Head.healthModifier + Body.healthModifier + Legs.healthModifier + Weapon.healthModifier;
            Block += Head.blockModifier + Body.blockModifier + Legs.blockModifier + Weapon.blockModifier;
        }

        public void update(Floor f, GameTime g, Game1 game, out bool hasPerformedAction, List<Enemy> Enemies, ref List<HealthPotion> HPotions,SoundEffect attack, SoundEffect step, ref string GameState)
        {
            hasPerformedAction = false;
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.LeftShift))
            {
                if (k.IsKeyDown(Keys.D))
                {
                    direction = 'r';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.X += 1;
                        hasPerformedAction = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                else if (k.IsKeyDown(Keys.A))
                {
                    direction = 'l';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.X -= 1;
                        hasPerformedAction = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                else if (k.IsKeyDown(Keys.S))
                {
                    direction = 'd';
                    if(f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.Y += 1;
                        hasPerformedAction = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                else if (k.IsKeyDown(Keys.W))
                {
                    direction = 'u';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "e" && Convert.ToInt32(g.TotalGameTime.TotalMilliseconds) % 200 == 0)
                    {
                        playerPos.Y -= 1;
                        hasPerformedAction = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }

            }
            else
            {

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
                if (k.IsKeyUp(Keys.E))
                {
                    Epressed = false;
                }
                if (k.IsKeyUp(Keys.D1))
                {
                    OnePressed = false;
                    hasAttacked = false;
                    foreach (Enemy e in Enemies)
                    {
                        e.hasAttacked = false;
                    }
                }
                if (k.IsKeyUp(Keys.D2))
                {
                    TwoPressed = false;
                    hasAttacked = false;
                    foreach (Enemy e in Enemies)
                    {
                        e.hasAttacked = false;
                    }
                }
                if (k.IsKeyUp(Keys.D3))
                {
                    ThreePressed = false;
                    hasAttacked = false;
                    foreach (Enemy e in Enemies)
                    {
                        e.hasAttacked = false;
                    }
                }
                if (k.IsKeyDown(Keys.D))
                {
                    direction = 'r';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X + 1), System.Convert.ToInt32(playerPos.Y)] != "e" && !Dpressed)
                    {
                        playerPos.X += 1;
                        hasPerformedAction = true;
                        Dpressed = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                else if (k.IsKeyDown(Keys.A))
                {
                    direction = 'l';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X - 1), System.Convert.ToInt32(playerPos.Y)] != "e" && !Apressed)
                    {
                        playerPos.X -= 1;
                        hasPerformedAction = true;
                        Apressed = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                else if (k.IsKeyDown(Keys.S))
                {
                    direction = 'd';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y + 1)] != "e" && !Spressed)
                    {
                        playerPos.Y += 1;
                        hasPerformedAction = true;
                        Spressed = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                else if (k.IsKeyDown(Keys.W))
                {
                    direction = 'u';
                    if (f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "w" && f.Wall_Grid[System.Convert.ToInt32(playerPos.X), System.Convert.ToInt32(playerPos.Y - 1)] != "e" && !Wpressed)
                    {
                        playerPos.Y -= 1;
                        hasPerformedAction = true;
                        Wpressed = true;
                        step.Play();
                        Equiped[3].SAbilities.UpdateCooldown(this);
                    }
                }
                
                else if (k.IsKeyDown(Keys.D1) && !OnePressed)
                {
                    Equiped[3].SAbilities.Skill1(ref Enemies, this, game, f.Wall_Grid);
                    OnePressed = true;
                }

                else if (k.IsKeyDown(Keys.D2) && !TwoPressed)
                {
                    Equiped[3].SAbilities.Skill2(ref Enemies, this, game, f.Wall_Grid);
                    TwoPressed = true;
                }
                else if (k.IsKeyDown(Keys.D3) && !ThreePressed)
                {
                    Equiped[3].SAbilities.Skill3(ref Enemies, this, game, f.Wall_Grid);
                    ThreePressed = true;
                }

            }
   
            if (f.HP_Grid[Convert.ToInt32(playerPos.X), Convert.ToInt32(playerPos.Y)].Substring(0,1) == "h")
            {
                Health += 50;
                HPotions[Convert.ToInt32(f.Wall_Grid[Convert.ToInt32(playerPos.X), Convert.ToInt32(playerPos.Y)].Substring(1, 1))].hasBeenConsumed = true;
                f.HP_Grid[Convert.ToInt32(playerPos.X), Convert.ToInt32(playerPos.Y)] = " ";
            }

            if (f.EntranceExit_Grid[Convert.ToInt32(playerPos.X), Convert.ToInt32(playerPos.Y)] == "exit")
            {
                GameState = "genLevel";
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

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
    public class SwordAbilitySet
    {
        Texture2D Skill1_icon;
        Texture2D Skill2_icon;
        Texture2D Skill3_icon;
        Texture2D Skill4_icon;

        SoundEffect Skill1_Soundeffect;
        SoundEffect Skill2_Soundeffect;
        SoundEffect Skill3_Soundeffect;

        public int Skill1_Cooldown = 0;
        public int Skill2_Cooldown = 0;
        public int Skill3_Cooldown = 0;

        //Initizalizes the textures in the object
        public SwordAbilitySet(Texture2D icon1, Texture2D icon2, Texture2D icon3, Texture2D icon4)
        {
            Skill1_icon = icon1;
            Skill2_icon = icon2;
            Skill3_icon = icon3;
            Skill4_icon = icon4;
        }

        //Initializes the soundeffect
        public SwordAbilitySet(SoundEffect skill1_Soundeffect, SoundEffect skill2_Soundeffect, SoundEffect skill3_Soundeffect)
        {
            Skill1_Soundeffect = skill1_Soundeffect;
            Skill2_Soundeffect = skill2_Soundeffect;
            Skill3_Soundeffect = skill3_Soundeffect;
        }

        public SwordAbilitySet()
        {

        }
        //Skill1 effects
        public void Skill1(ref List<Enemy> Enemies, Player p, Game1 game, string[,] Maze)
        {
            UpdateCooldown(p);
            game.HasMoved = true;
            switch (p.direction)
            {
                case 'd':
                    {
                        foreach (Enemy e in Enemies)
                        {
                            if (new Vector2(p.pos.X, p.pos.Y + 1) == e.pos && e.health > 0)
                            {
                                e.health -= p.Attack;
                            }
                            if (e.health <= 0)
                            {
                                e.isAlive = false;
                                e.health = 0;
                                Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                            }
                        }
                        break;
                    }
                case 'u':
                    {
                        foreach (Enemy e in Enemies)
                        {
                            if (new Vector2(p.pos.X, p.pos.Y - 1) == e.pos && e.health > 0)
                            {
                                e.health -= p.Attack;
                            }
                            if (e.health <= 0)
                            {
                                e.isAlive = false;
                                e.health = 0;
                                Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                            }
                        }
                        break;
                    }
                case 'l':
                    {
                        foreach (Enemy e in Enemies)
                        {
                            if (new Vector2(p.pos.X - 1, p.pos.Y) == e.pos && e.health > 0)
                            {
                                e.health -= p.Attack;
                            }
                            if (e.health <= 0)
                            {
                                e.isAlive = false;
                                e.health = 0;
                                Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                            }
                        }

                        break;
                    }
                case 'r':
                    {
                        foreach (Enemy e in Enemies)
                        {
                            if (new Vector2(p.pos.X + 1, p.pos.Y) == e.pos && e.health > 0)
                            {
                                e.health -= p.Attack;
                            }
                            if (e.health <= 0)
                            {
                                e.isAlive = false;
                                e.health = 0;
                                Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                            }
                        }
                        break;
                    }
            }
            p.OnePressed = true;
            p.hasAttacked = true;

            Skill1_Soundeffect.Play();
        }

        //Skill2 effects
        public void Skill2(ref List<Enemy> Enemies, Player p, Game1 game, string[,] Maze)
        {
            game.HasMoved = true;
            UpdateCooldown(p);
            if (Skill2_Cooldown <= 0)
            {
                switch (p.direction)
                {
                    case 'd':
                        {
                            foreach (Enemy e in Enemies)
                            {
                                if (p.pos.X == e.pos.X && e.pos.Y < p.pos.Y + 5 && e.pos.Y > p.pos.Y && e.health > 0)
                                {
                                    e.health -= p.Attack;
                                }
                                if (e.health <= 0)
                                {
                                    e.isAlive = false;
                                    e.health = 0;
                                    Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                                }
                            }
                            break;
                        }
                    case 'u':
                        {
                            foreach (Enemy e in Enemies)
                            {
                                if (p.pos.X == e.pos.X && e.pos.Y < p.pos.Y && e.pos.Y > p.pos.Y - 5 && e.health > 0)
                                {
                                    e.health -= p.Attack;
                                }
                                if (e.health <= 0)
                                {
                                    e.isAlive = false;
                                    e.health = 0;
                                    Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                                }
                            }
                            break;
                        }
                    case 'l':
                        {
                            foreach (Enemy e in Enemies)
                            {
                                if (p.pos.Y == e.pos.Y && e.pos.X < p.pos.X && e.pos.X > p.pos.X - 5 && e.health > 0)
                                {
                                    e.health -= p.Attack;
                                }
                                if (e.health <= 0)
                                {
                                    e.isAlive = false;
                                    e.health = 0;
                                    Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                                }
                            }

                            break;
                        }
                    case 'r':
                        {
                            foreach (Enemy e in Enemies)
                            {
                                if (p.pos.Y == e.pos.Y && e.pos.X < p.pos.X + 5 && e.pos.X > p.pos.X && e.health > 0)
                                {
                                    e.health -= p.Attack;
                                }
                                if (e.health <= 0)
                                {
                                    e.isAlive = false;
                                    e.health = 0;
                                    Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                                }
                            }
                            break;
                        }
                }
                p.TwoPressed = true;
                p.hasAttacked = true;
                Skill2_Cooldown = 2;

                Skill2_Soundeffect.Play();
            }
        }
        //Skill 3 effects
        public void Skill3(ref List<Enemy> Enemies, Player p, Game1 game, string[,] Maze)
        {
            UpdateCooldown(p);
            game.HasMoved = true;
            if (Skill3_Cooldown <= 0)
            {
                foreach (Enemy e in Enemies)
                {
                    if (e.pos.X < p.pos.X + 3 && e.pos.X > p.pos.X - 3 && e.pos.Y < p.pos.Y + 3 && e.pos.Y > p.pos.Y - 3)
                    {
                        e.health -= p.Attack / 2;
                    }

                    if (e.health <= 0)
                    {
                        e.isAlive = false;
                        e.health = 0;
                        Maze[Convert.ToInt32(e.pos.X), Convert.ToInt32(e.pos.Y)] = "f";
                    }

                }
                p.ThreePressed = true;
                p.hasAttacked = true;
                Skill3_Cooldown = 5;

                Skill3_Soundeffect.Play();
            }
        }
        //Updates the cooldown values for each skill
        public void UpdateCooldown(Player p)
        {
            Skill1_Cooldown -= 1;
            Skill2_Cooldown -= 1;
            Skill3_Cooldown -= 1;

            if (Skill1_Cooldown > 0)
            {
                p.hasAttacked = true;
            }
            if (Skill2_Cooldown > 0)
            {
                p.hasAttacked = true;
            }
            if (Skill3_Cooldown > 0)
            {
                p.hasAttacked = true;
            }
        }
    }
}

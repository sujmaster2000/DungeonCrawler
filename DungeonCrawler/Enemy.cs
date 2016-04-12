using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler
{
    public struct doubleInt
    {
        public int x;
        public int y;
    }
    public class Enemy
    {
        public Vector2 pos;
        public Vector2 prevPos;
        public Rectangle rect;

        public Color color;

        public bool isAlive = true;
        public bool hasAttacked = false;
        public bool hasDroppedItem = false;
        public bool isActive = true;

        public List<Item> Equiped = new List<Item>();
        public List<doubleInt> path = new List<doubleInt>();

        public char direction;

        float maxHealth;
        public float health;
        public int attackDamage;

        public double block;

        public string State = "passive";

        public Enemy (Vector2 Pos, Item Head, Item Body, Item Legs, Item Weapon, bool IsActive)
        {
            pos = Pos;
            rect = new Rectangle(Convert.ToInt32(pos.X * 32), Convert.ToInt32(pos.Y * 32), 32, 32);

            health = 20 + (Head.healthModifier + Body.healthModifier + Legs.healthModifier + Weapon.healthModifier);
            maxHealth = health;
            attackDamage = Head.attackModifier + Body.attackModifier + Legs.attackModifier + Weapon.attackModifier;
            block = Head.blockModifier + Body.blockModifier + Legs.blockModifier + Weapon.blockModifier;

            Equiped.Add(Head);
            Equiped.Add(Body);
            Equiped.Add(Legs);
            Equiped.Add(Weapon);

            isActive = IsActive;
        }

        public Enemy(string[,] Maze, Random r)
        {

            bool validPos = false;

            while (!validPos)
            {
                pos.X = r.Next(1, Maze.GetLength(0) - 1);
                pos.Y = r.Next(1, Maze.GetLength(1) - 1);

                if (Maze[Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)] == "f" && pos.X != 1 && pos.Y != 1)
                {
                    validPos = true;
                }
            }

            rect = new Rectangle(Convert.ToInt32(pos.X * 32), Convert.ToInt32(pos.Y * 32), 32, 32);
        }

        public void UpdateStatistics(int floorNum)
        {
            health = 20 * floorNum + (Equiped[0].healthModifier + Equiped[1].healthModifier + Equiped[2].healthModifier + Equiped[3].healthModifier);
            maxHealth = health;
            attackDamage = Equiped[0].attackModifier + Equiped[1].attackModifier + Equiped[2].attackModifier + Equiped[3].attackModifier;
            block = Equiped[0].blockModifier + Equiped[1].blockModifier + Equiped[2].blockModifier + Equiped[3].blockModifier;
        }

        public Enemy(string[,] Maze, Random r, Item Head, Item Body, Item Legs, Item Weapon, int floorNum)
        {
            bool validPos = false;

            while (!validPos)
            {
                pos.X = r.Next(1, Maze.GetLength(0) - 1);
                pos.Y = r.Next(1, Maze.GetLength(1) - 1);

                if (Maze[Convert.ToInt32(pos.X),Convert.ToInt32(pos.Y)] == "f" && pos.X != 1 && pos.Y != 1)
                {
                    validPos = true;
                }
            }

            rect = new Rectangle(Convert.ToInt32(pos.X * 32), Convert.ToInt32(pos.Y * 32), 32, 32);

            health = 20 * (floorNum - 1) + (Head.healthModifier + Body.healthModifier + Legs.healthModifier + Weapon.healthModifier);
            maxHealth = health;
            attackDamage = Head.attackModifier + Body.attackModifier + Legs.attackModifier + Weapon.attackModifier;
            block = Head.blockModifier + Body.blockModifier + Legs.blockModifier + Weapon.blockModifier;

            Equiped.Add(Head);
            Equiped.Add(Body);
            Equiped.Add(Legs);
            Equiped.Add(Weapon);
        }

        public Enemy()
        {

        }

        public List<doubleInt> getPath (Floor Maze, Vector2 Dest)
        {
            List<doubleInt> path = new List<doubleInt>();


            bool AllTilesChecked = false;

            int[,] maze = new int[Maze.Wall_Grid.GetLength(0), Maze.Wall_Grid.GetLength(1)];

            for (int i = 0; i < Maze.Wall_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.Wall_Grid.GetLength(1); j++)
                {
                    if (Maze.Wall_Grid[j, i] == "w")
                    {
                        maze[j, i] = -1;
                    }
                    else
                    {
                        maze[j, i] = 999; //infinity
                    }
                }
            }

            maze[(int) pos.X, (int) pos.Y] = 0;

            while (!AllTilesChecked)
            {
                AllTilesChecked = true;

                for (int i = 0; i < Maze.Wall_Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Maze.Wall_Grid.GetLength(1); j++)
                    {
                        if (maze[j, i] != -1)
                        {
                            if (j < Maze.Wall_Grid.GetLength(0) - 1)
                            {
                                if (maze[j + 1, i] - 1 > maze[j, i] && maze[j + 1, i] != -1)
                                {
                                    maze[j + 1, i] = maze[j, i] + 1;
                                }
                            }
                            if (j >= 1)
                            {
                                if (maze[j - 1, i] - 1 > maze[j, i] && maze[j - 1, i] != -1)
                                {
                                    maze[j - 1, i] = maze[j, i] + 1;
                                }
                            }
                            if (i < Maze.Wall_Grid.GetLength(1) - 1)
                            {
                                if (maze[j, i + 1] - 1 > maze[j, i] && maze[j, i + 1] != -1)
                                {
                                    maze[j, i + 1] = maze[j, i] + 1;
                                }
                            }
                            if (i >= 1)
                            {
                                if (maze[j, i - 1] - 1 > maze[j, i] && maze[j, i - 1] != -1)
                                {
                                    maze[j, i - 1] = maze[j, i] + 1;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < Maze.Wall_Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Maze.Wall_Grid.GetLength(1); j++)
                    {
                        if (maze[j, i] == 999)
                        {
                            AllTilesChecked = false;
                        }
                    }
                }

            }

            int x = (int) Dest.X;
            int y = (int) Dest.Y;

            path = new List<doubleInt>();

            while (x != pos.X || y != pos.Y)
            {
                doubleInt newNode;

                newNode.x = x;
                newNode.y = y;

                path.Add(newNode);

                if (maze[x + 1, y] != -1 && maze[x + 1, y] + 1 == maze[x, y])
                {
                    x++;

                }
                else if (maze[x - 1, y] != -1 && maze[x - 1, y] + 1 == maze[x, y])
                {

                    x--;

                }
                else if (maze[x, y + 1] != -1 && maze[x, y + 1] + 1 == maze[x, y])
                {
                    y++;
                }
                else if (maze[x, y - 1] != -1 && maze[x, y - 1] + 1 == maze[x, y])
                {
                    y--;
                }
            }


            return path;
        }

        public void Update(Player p, Floor Maze, ref Dictionary<int, Item> droppedItems, Game1 g, Random r)
        {
            if (health <=0 && hasDroppedItem == false)
            {
                if (color == Color.Green)
                {
                    g.score.EnemyScore += g.floorNum;
                }
                else if (color == Color.Blue)
                {
                    g.score.EnemyScore += g.floorNum * 2;
                }
                else if (color == Color.Red)
                {
                    g.score.EnemyScore += g.floorNum * 4;
                }

                isAlive = false;
                health = 0;
                if (r.Next(0, 100) < 100 && Maze.Item_Grid[Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)] == " ")
                {
                    Item item = g.randItem(r, pos, null);
                    droppedItems.Add(droppedItems.Count, item);
                    int index = droppedItems.First(x => x.Value == item).Key;

                    Maze.Item_Grid[Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)] = "i" + index;

                }
                hasDroppedItem = true;
            }

            if (isAlive == true && isActive == true)
            {
                switch (State)
                {

                    case "passive":

                        if (pos.X < p.pos.X + 4 && pos.X > p.pos.X - 4 && pos.Y < p.pos.Y + 4 && pos.Y > p.pos.Y - 4)
                        {
                            path = getPath(Maze, p.pos);
                            State = "investigating";
                        }

                        break;
                    case "investigating":
                        if (Maze.Enemy_Grid[path[path.Count - 1].x, path[path.Count - 1].x] == " ")
                        {
                            prevPos.Y = pos.Y;
                            prevPos.X = pos.X;

                            rect.X = path[path.Count - 1].x * 32;
                            rect.Y = path[path.Count - 1].y * 32;
                            pos.X = path[path.Count - 1].x;
                            pos.Y = path[path.Count - 1].y;

                            path.RemoveAt(path.Count - 1);
                        }
                        if (path.Count == 0)
                        {
                            State = "passive";
                        }

                        if (pos.X < p.pos.X + 3 && pos.X > p.pos.X - 3 && pos.Y < p.pos.Y + 3 && pos.Y > p.pos.Y - 3)
                        {
                            State = "active";
                        }

                        break;
                    case "active":
                        path = getPath(Maze, p.pos);

                        if (path.Count != 1 && Maze.Enemy_Grid[Convert.ToInt32(path[path.Count - 1].x), Convert.ToInt32(path[path.Count - 1].y)] != "e")
                        {
                            prevPos.Y = pos.Y;
                            prevPos.X = pos.X;

                            rect.X = path[path.Count - 1].x * 32;
                            rect.Y = path[path.Count - 1].y * 32;
                            pos.X = path[path.Count - 1].x;
                            pos.Y = path[path.Count - 1].y;
                        }
                        break;
                }
            }   
        }

        public void attack(Player p)
        {
            double toBlock = 100 - (200 / p.Block);

            Random r = new Random();

            double BlockRoll = (double) (r.Next(0,100) - r.NextDouble());

            if (BlockRoll > toBlock)
            {
                p.Health -= attackDamage;
            }
        }

        public void draw(SpriteBatch s, Texture2D t, Texture2D b, SpriteFont f, Player p)
        {
            if (pos.X < p.pos.X + 5 && pos.X > p.pos.X - 5 && pos.Y < p.pos.Y + 5 && pos.Y > p.pos.Y - 5 && isAlive)
            {/*
                s.Draw(t, rect, Color.White);
                s.DrawString(f, health.ToString(), new Vector2(pos.X * 32, pos.Y * 32 + 20), Color.White);*/
                if (prevPos.Y - 1 == pos.Y)
                {
                    direction = 'u'; 
                    
                }
                else if (prevPos.Y + 1 == pos.Y)
                {
                    direction = 'd';
                    
                }
                else if (prevPos.X - 1 == pos.X)
                {
                    direction = 'l';
                    
                }
                else if (prevPos.X + 1 == pos.X)
                {
                    direction = 'r';
                    
                }

                switch (direction)
                {
                    case 'd':
                        {

                            s.Draw(Equiped[0].Textures[0], pos * 32, color);
                            s.Draw(Equiped[1].Textures[0], pos * 32, color);
                            s.Draw(Equiped[2].Textures[0], pos * 32, color);
                            s.Draw(Equiped[3].Textures[0], pos * 32, color);
                            s.Draw(b, new Rectangle(Convert.ToInt32(pos.X) * 32, Convert.ToInt32(pos.Y) * 32 + 26, Convert.ToInt32((health / maxHealth) * 32), 4), Color.White);
                            break;
                        }
                    case 'u':
                        {
                            s.Draw(Equiped[3].Textures[1], pos * 32, color);
                            s.Draw(Equiped[0].Textures[1], pos * 32, color);
                            s.Draw(Equiped[1].Textures[1], pos * 32, color);
                            s.Draw(Equiped[2].Textures[1], pos * 32, color);
                            s.Draw(b, new Rectangle(Convert.ToInt32(pos.X) * 32, Convert.ToInt32(pos.Y) * 32 + 26, Convert.ToInt32((health / maxHealth) * 32), 4), Color.White);
                            //s.DrawString(f, health.ToString(), new Vector2(pos.X * 32, pos.Y * 32 + 16), Color.White);

                            break;
                        }
                    case 'l':
                        {
                            s.Draw(Equiped[0].Textures[2], pos * 32, color);
                            s.Draw(Equiped[1].Textures[2], pos * 32, color);
                            s.Draw(Equiped[2].Textures[2], pos * 32, color);
                            s.Draw(Equiped[3].Textures[2], pos * 32, color);
                            s.Draw(b, new Rectangle(Convert.ToInt32(pos.X) * 32, Convert.ToInt32(pos.Y) * 32 + 26, Convert.ToInt32((health / maxHealth) * 32), 4), Color.White);
                            //s.DrawString(f, health.ToString(), new Vector2(pos.X * 32, pos.Y * 32 + 16), Color.White);
                            break;
                        }
                    case 'r':
                        {
                            s.Draw(Equiped[0].Textures[3], pos * 32, color);
                            s.Draw(Equiped[1].Textures[3], pos * 32, color);
                            s.Draw(Equiped[2].Textures[3], pos * 32, color);
                            s.Draw(Equiped[3].Textures[3], pos * 32, color);
                            s.Draw(b, new Rectangle(Convert.ToInt32(pos.X) * 32, Convert.ToInt32(pos.Y) * 32 + 26, Convert.ToInt32((health / maxHealth) * 32), 4), Color.White);
                            //s.DrawString(f, health.ToString(), new Vector2(pos.X * 32, pos.Y * 32 + 16), Color.White);
                            break;
                        }
                    default:
                            s.Draw(Equiped[0].Textures[0], pos * 32, color);
                            s.Draw(Equiped[1].Textures[0], pos * 32, color);
                            s.Draw(Equiped[2].Textures[0], pos * 32, color);
                            s.Draw(Equiped[3].Textures[0], pos * 32, color);
                            s.Draw(b, new Rectangle(Convert.ToInt32(pos.X) * 32, Convert.ToInt32(pos.Y) * 32 + 26, Convert.ToInt32((health / maxHealth) * 32), 4), Color.White);
                        break;
                }

                switch (State)
                {
                    case "passive":
                        s.DrawString(f, "S", pos * 32, Color.Blue);
                        break;
                    case "investigating":
                        s.DrawString(f, "I", pos * 32, Color.Green);
                        break;
                    case "active":
                        s.DrawString(f, "A!", pos * 32, Color.Red);
                        break;
                }
            }
        }
    }
}

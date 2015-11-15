using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler
{
    class Enemy
    {
        public Vector2 pos;
        public Rectangle rect;
        public bool isAlive = true;
        public bool hasAttacked = false;

        public int health;

        public Enemy (Vector2 Pos, int Health)
        {
            pos = Pos;
            rect = new Rectangle(Convert.ToInt32(pos.X * 32), Convert.ToInt32(pos.Y * 32), 32, 32);
            health = Health;
        }

        public Enemy(string[,] Maze, Random r, int Health)
        {
            bool validPos = false;

            health = Health;

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
        }

        public void Update(Player p, string[,] Maze)
        {
            if (health <=0)
            {
                isAlive = false;
                health = 0;
            }

            if (isAlive == true)
            {
                int[, ,] maze;

                maze = new int[Maze.GetLength(0), Maze.GetLength(1), 3];

                for (int i = 0; i < Maze.GetLength(0); i++)
                {
                    for (int j = 0; j < Maze.GetLength(1); j++)
                    {
                        if (Maze[j, i] == "f" || Maze[j, i] == "h" || Maze[j,i] == "e")
                        {
                            maze[j, i, 0] = 999;
                        }
                        else
                        {
                            maze[j, i, 0] = 1690;
                        }
                    }
                }

                maze[Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y), 0] = 0;

                bool allNodesChecked = false;

                while (!allNodesChecked)
                {
                    //Goes through each point (no priority queue)
                    for (int i = 0; i < maze.GetLength(1); i++)
                    {
                        for (int j = 0; j < maze.GetLength(0); j++)
                        {

                            //Checks the point above below left and right of it (No diagonal edges)
                            if (j - 1 >= 0 && maze[j, i, 0] + 1 < maze[j - 1, i, 0] && maze[j - 1, i, 0] != 1690)
                            {
                                //All edges have weight of 1
                                maze[j - 1, i, 0] = maze[j, i, 0] + 1;
                                //stores in the next coordinate location of the previous one
                                maze[j - 1, i, 1] = j;
                                maze[j - 1, i, 2] = i;
                            }
                            if (j + 1 < maze.GetLength(0) && maze[j, i, 0] + 1 < maze[j + 1, i, 0] && maze[j + 1, i, 0] != 1690)
                            {
                                maze[j + 1, i, 0] = maze[j, i, 0] + 1;
                                maze[j + 1, i, 1] = j;
                                maze[j + 1, i, 2] = i;
                            }
                            if (i - 1 >= 0 && maze[j, i, 0] + 1 < maze[j, i - 1, 0] && maze[j, i - 1, 0] != 1690)
                            {
                                maze[j, i - 1, 0] = maze[j, i, 0] + 1;
                                maze[j, i - 1, 1] = j;
                                maze[j, i - 1, 2] = i;
                            }
                            if (i + 1 < maze.GetLength(1) && maze[j, i, 0] + 1 < maze[j, i + 1, 0] && maze[j, i + 1, 0] != 1690)
                            {
                                maze[j, i + 1, 0] = maze[j, i, 0] + 1;
                                maze[j, i + 1, 1] = j;
                                maze[j, i + 1, 2] = i;
                            }
                        }
                    }
                    //checks to see if any nodes still have 'infinite' distance
                    allNodesChecked = true;
                    
                    for (int i = 0; i < maze.GetLength(1); i++)
                    {
                        for (int j = 0; j < maze.GetLength(0); j++)
                        {
                            if (maze[j, i, 0] == 999)
                            {
                                allNodesChecked = false;
                            }
                        }
                    }
                }

                string towritetofile = "";

                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {

                        towritetofile += maze[j, i, 0];
                        for (int k = 0; k < 5 - maze[j, i, 0].ToString().Length; k++)
                        {
                            towritetofile += " ";
                        }

                    }
                    towritetofile += System.Environment.NewLine;
                }

                System.IO.File.WriteAllText("maze.txt", towritetofile);

                int[,] path = new int[maze[Convert.ToInt32(p.playerPos.X), Convert.ToInt32(p.playerPos.Y), 0], 2];

                //endpoint coordinates
                int sX = Convert.ToInt32(p.playerPos.X);
                int sY = Convert.ToInt32(p.playerPos.Y);

                //starts at the end and works backwards to the origin
                for (int i = maze[Convert.ToInt32(p.playerPos.X), Convert.ToInt32(p.playerPos.Y), 0] - 1; i > -1; i--)
                {
                    path[i, 0] = sX;
                    path[i, 1] = sY;

                    int SX = sX;

                    sX = maze[sX, sY, 1];
                    sY = maze[SX, sY, 2];
                }
                try
                {
                    if (path.GetLength(0) != 1 && Maze[Convert.ToInt32(path[0, 0]), Convert.ToInt32(path[0, 1])] != "e")
                    {
                        rect.X = path[0, 0] * 32;
                        rect.Y = path[0, 1] * 32;
                        pos.X = path[0, 0];
                        pos.Y = path[0, 1];
                    }
                }
                catch(Exception)
                {

                }
            }   
        }

        public void attack(Player p)
        {
            p.Health -= health/4;
        }

        public void draw(SpriteBatch s, Texture2D t, SpriteFont f, Player p)
        {
            if (pos.X < p.playerPos.X + 5 && pos.X > p.playerPos.X - 5 && pos.Y < p.playerPos.Y + 5 && pos.Y > p.playerPos.Y - 5)
            {
                s.Draw(t, rect, Color.White);
                s.DrawString(f, health.ToString(), new Vector2(pos.X * 32, pos.Y * 32 + 20), Color.White);
            }
        }
    }
}

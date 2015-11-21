using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DungeonCrawler
{
    class Floor
    {
        public string[,] maze;

        public static void GenLevel(out string[,] Maze, int size, string Seed)
        {
            if (size % 2 == 0)
            {
                Maze = new string[size + 1, size + 1];
            }
            else
            {
                Maze = new string[size, size];
            }

            if (Seed != null)
            {
                int seed = Seed.GetHashCode();

                int[, ,] prevCoord = new int[size, size, 2];

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Maze[i, j] = "w";
                    }
                }
                //■
                bool areUnvisitedTiles = true;
                Random r = new Random(seed);

                int direction = 1;

                int currX = 1;
                int currY = 1;

                bool isFirstGo = true;

                while (areUnvisitedTiles)
                {
                    Maze[currX, currY] = "f";

                    bool validDirectionChosen = false;

                    bool visited1 = false;
                    bool visited2 = false;
                    bool visited3 = false;
                    bool visited4 = false;

                    while (!validDirectionChosen)
                    {
                        direction = r.Next(1, 5);
                        if (direction == 1)
                        {
                            if (currY != size - 2)
                            {
                                if (Maze[currX, currY + 2] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX, currY + 2, 0] = currX;
                                    prevCoord[currX, currY + 2, 1] = currY;

                                }
                                else
                                    visited1 = true;
                            }
                            else
                                visited1 = true;
                        }
                        else if (direction == 2)
                        {
                            if (currX != size - 2)
                            {
                                if (Maze[currX + 2, currY] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX + 2, currY, 0] = currX;
                                    prevCoord[currX + 2, currY, 1] = currY;

                                }
                                else
                                    visited2 = true;
                            }
                            else
                                visited2 = true;
                        }
                        else if (direction == 3)
                        {
                            if (currY != 1)
                            {
                                if (Maze[currX, currY - 2] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX, currY - 2, 0] = currX;
                                    prevCoord[currX, currY - 2, 1] = currY;
                                }
                                else
                                    visited3 = true;
                            }
                            else
                                visited3 = true;
                        }
                        else if (direction == 4)
                        {
                            if (currX != 1)
                            {
                                if (Maze[currX - 2, currY] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX - 2, currY, 0] = currX;
                                    prevCoord[currX - 2, currY, 1] = currY;
                                }
                                else
                                    visited4 = true;
                            }
                            else
                                visited4 = true;
                        }

                        if (!validDirectionChosen && visited1 && visited2 && visited3 && visited4)
                        {
                            int TempX = currX;
                            int TempY = currY;

                            currX = prevCoord[TempX, TempY, 0];
                            currY = prevCoord[TempX, TempY, 1];
                        }

                        if (!validDirectionChosen && visited1 && visited2 && visited3 && visited4 && currX == 0 && currY == 0 && isFirstGo == false)
                        {
                            direction = 0;
                            break;
                        }
                    }

                    int tempX = currX;
                    int tempY = currY;

                    switch (direction)
                    {
                        case (1):
                            {
                                currX = tempX;
                                currY = tempY + 2;
                                Maze[tempX, tempY + 1] = "f";
                                break;
                            }
                        case (2):
                            {
                                currX = tempX + 2;
                                currY = tempY;
                                Maze[tempX + 1, tempY] = "f";
                                break;
                            }
                        case (3):
                            {
                                currX = tempX;
                                currY = tempY - 2;
                                Maze[tempX, tempY - 1] = "f";
                                break;
                            }
                        case (4):
                            {
                                currX = tempX - 2;
                                currY = tempY;
                                Maze[tempX - 1, tempY] = "f";
                                break;
                            }

                        case (0):
                            {
                                areUnvisitedTiles = false;
                                break;
                            }
                    }
                    isFirstGo = false;

                    if (!isFirstGo && currX == 0 && currY == 0)
                    {
                        areUnvisitedTiles = false;
                    }

                }
            }
            else
            {
                int[, ,] prevCoord = new int[size, size, 2];

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Maze[i, j] = "w";
                    }
                }
                //■
                bool areUnvisitedTiles = true;
                Random r = new Random();

                int direction = 1;

                int currX = 1;
                int currY = 1;

                bool isFirstGo = true;

                while (areUnvisitedTiles)
                {
                    Maze[currX, currY] = "f";

                    bool validDirectionChosen = false;

                    bool visited1 = false;
                    bool visited2 = false;
                    bool visited3 = false;
                    bool visited4 = false;

                    while (!validDirectionChosen)
                    {
                        direction = r.Next(1, 5);
                        if (direction == 1)
                        {
                            if (currY != size - 2)
                            {
                                if (Maze[currX, currY + 2] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX, currY + 2, 0] = currX;
                                    prevCoord[currX, currY + 2, 1] = currY;

                                }
                                else
                                    visited1 = true;
                            }
                            else
                                visited1 = true;
                        }
                        else if (direction == 2)
                        {
                            if (currX != size - 2)
                            {
                                if (Maze[currX + 2, currY] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX + 2, currY, 0] = currX;
                                    prevCoord[currX + 2, currY, 1] = currY;

                                }
                                else
                                    visited2 = true;
                            }
                            else
                                visited2 = true;
                        }
                        else if (direction == 3)
                        {
                            if (currY != 1)
                            {
                                if (Maze[currX, currY - 2] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX, currY - 2, 0] = currX;
                                    prevCoord[currX, currY - 2, 1] = currY;
                                }
                                else
                                    visited3 = true;
                            }
                            else
                                visited3 = true;
                        }
                        else if (direction == 4)
                        {
                            if (currX != 1)
                            {
                                if (Maze[currX - 2, currY] == "w")
                                {
                                    validDirectionChosen = true;
                                    prevCoord[currX - 2, currY, 0] = currX;
                                    prevCoord[currX - 2, currY, 1] = currY;
                                }
                                else
                                    visited4 = true;
                            }
                            else
                                visited4 = true;
                        }

                        if (!validDirectionChosen && visited1 && visited2 && visited3 && visited4)
                        {
                            int TempX = currX;
                            int TempY = currY;

                            currX = prevCoord[TempX, TempY, 0];
                            currY = prevCoord[TempX, TempY, 1];
                        }

                        if (!validDirectionChosen && visited1 && visited2 && visited3 && visited4 && currX == 0 && currY == 0 && isFirstGo == false)
                        {
                            direction = 0;
                            break;
                        }
                    }

                    int tempX = currX;
                    int tempY = currY;

                    switch (direction)
                    {
                        case (1):
                            {
                                currX = tempX;
                                currY = tempY + 2;
                                Maze[tempX, tempY + 1] = "f";
                                break;
                            }
                        case (2):
                            {
                                currX = tempX + 2;
                                currY = tempY;
                                Maze[tempX + 1, tempY] = "f";
                                break;
                            }
                        case (3):
                            {
                                currX = tempX;
                                currY = tempY - 2;
                                Maze[tempX, tempY - 1] = "f";
                                break;
                            }
                        case (4):
                            {
                                currX = tempX - 2;
                                currY = tempY;
                                Maze[tempX - 1, tempY] = "f";
                                break;
                            }

                        case (0):
                            {
                                areUnvisitedTiles = false;
                                break;
                            }
                    }
                    isFirstGo = false;

                    if (!isFirstGo && currX == 0 && currY == 0)
                    {
                        areUnvisitedTiles = false;
                    }

                }
            }
        }

        public Floor(int Size, string Seed)
        {
            GenLevel(out maze, Size, Seed);
        }

        public void DrawLevel(SpriteBatch s, Texture2D wall, Texture2D floor, Player p)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (j < p.playerPos.X + 5 && j > p.playerPos.X - 5 && i < p.playerPos.Y + 5 && i > p.playerPos.Y - 5)
                    {
                        switch (maze[j, i].Substring(0,1))
                        {
                            case "w":
                                s.Draw(wall, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                                break;
                            case "f":
                                s.Draw(floor, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                                break;
                            case "h":
                                s.Draw(floor, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                                break;
                            case "e":
                                s.Draw(floor, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                                break;
                        }
                    }

                    else
                    {
                        switch (maze[j, i].Substring(0,1))
                        {
                            case "w":
                                s.Draw(wall, new Rectangle(j * 32, i * 32, 32, 32), Color.DarkGray);
                                break;
                            case "f":
                                s.Draw(floor, new Rectangle(j * 32, i * 32, 32, 32), Color.DarkGray);
                                break;
                            case "h":
                                s.Draw(floor, new Rectangle(j * 32, i * 32, 32, 32), Color.DarkGray);
                                break;
                            case "e":
                                s.Draw(floor, new Rectangle(j * 32, i * 32, 32, 32), Color.DarkGray);
                                break;
                        }
                    }
                }
            }
        }
    }
}

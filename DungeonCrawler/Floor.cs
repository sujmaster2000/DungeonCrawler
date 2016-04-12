using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DungeonCrawler
{
    public class Floor
    {
        public string[,] Wall_Grid;
        public string[,] HP_Grid;
        public string[,] Enemy_Grid;
        public string[,] Item_Grid;
        public string[,] EntranceExit_Grid;

        public List<Enemy> enemies;

        public struct doubleInt
        {
            public int x;
            public int y;
        }

        //Generates a Maze with randomized krusukal's algorithm

        public Floor()
        {
            EntranceExit_Grid = new string[20, 20];
            HP_Grid = new string[20, 20];
            Item_Grid = new string[20, 20];
            Enemy_Grid = new string[20, 20];
            Wall_Grid = new string[20, 20];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    HP_Grid[j, i] = " ";
                }
            }


            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Item_Grid[j, i] = " ";
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Wall_Grid[j, i] = " ";
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    EntranceExit_Grid[j, i] = " ";
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Enemy_Grid[j, i] = " ";
                }
            }
        }

        public static void KrusukalLevel(out string[,] Maze, ref string[,] EntranceExit_Grid, ref string[,] HP_Grid, ref string[,] Item_Grid, ref string[,] Enemy_Grid,int size, string seed)
        {
            if (size % 2 == 0)
            {
                Maze = new string[size + 1, size + 1];
            }
            else
            {
                Maze = new string[size, size];
            }


            int[,] maze = new int[size, size];

            EntranceExit_Grid = new string[Maze.GetLength(0), Maze.GetLength(1)];
            HP_Grid = new string[Maze.GetLength(0), Maze.GetLength(1)];
            Item_Grid = new string[Maze.GetLength(0), Maze.GetLength(1)];
            Enemy_Grid = new string[Maze.GetLength(0), Maze.GetLength(1)];

            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    HP_Grid[j, i] = " ";
                }
            }


            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    Item_Grid[j, i] = " ";
                }
            }


            for (int i = 0; i < Maze.GetLength(0); i++ )
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    EntranceExit_Grid[j, i] = " ";
                }
            }

            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    Enemy_Grid[j, i] = " ";
                }
            }

            List<doubleInt> edges = new List<doubleInt>();

            int counter = 0;

            Random r = new Random();

            if (seed != null)
            {
                r = new Random(seed.GetHashCode());
            }

            bool isComplete = false;

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i % 2 != 0 || j % 2 != 0)
                    {
                        maze[j, i] = -1;
                    }
                    else
                    {
                        maze[j, i] = counter;
                        counter++;
                    }
                }
            }

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (j != maze.GetLength(0) - 1 && i != maze.GetLength(1) - 1)
                    {
                        if (maze[j, i] == -1 && (maze[j + 1, i] != -1 || maze[j, i + 1] != -1))
                        {
                            doubleInt n;

                            n.x = j;
                            n.y = i;

                            edges.Add(n);
                        }
                    }
                }
            }

            while (!isComplete)
            {
                doubleInt currEdge = edges[r.Next(0, edges.Count)];

                if (maze[currEdge.x, currEdge.y + 1] == -1)
                {
                    if (maze[currEdge.x - 1, currEdge.y] != maze[currEdge.x + 1, currEdge.y])
                    {
                        maze[currEdge.x, currEdge.y] = maze[currEdge.x - 1, currEdge.y];
                        for (int i = 0; i < maze.GetLength(0); i++)
                        {
                            for (int j = 0; j < maze.GetLength(1); j++)
                            {
                                if (maze[j, i] == maze[currEdge.x + 1, currEdge.y])
                                {
                                    maze[j, i] = maze[currEdge.x, currEdge.y];
                                }
                            }
                        }
                    }
                    else
                    {
                        edges.Remove(currEdge);
                    }
                }

                else if (maze[currEdge.x, currEdge.y - 1] != maze[currEdge.x, currEdge.y + 1])
                {
                    maze[currEdge.x, currEdge.y] = maze[currEdge.x, currEdge.y - 1];
                    for (int i = 0; i < maze.GetLength(0); i++)
                    {
                        for (int j = 0; j < maze.GetLength(1); j++)
                        {
                            if (maze[j, i] == maze[currEdge.x, currEdge.y + 1])
                            {
                                maze[j, i] = maze[currEdge.x, currEdge.y];
                            }
                        }
                    }
                }
                else
                {
                    edges.Remove(currEdge);
                }

                for (int i = 0; i < maze.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < maze.GetLength(1) - 1; j++)
                    {
                        if (maze[j, i] != -1 && maze[j + 1, i] != -1 && maze[j, i] != maze[j + 1, i])
                        {
                            for (int k = 0; k < maze.GetLength(0); k++)
                            {
                                for (int l = 0; l < maze.GetLength(1); l++)
                                {
                                    if (maze[l, k] == maze[j + 1, i])
                                    {
                                        maze[l, k] = maze[j, i];
                                    }
                                }
                            }
                        }
                        else if (maze[j, i] != -1 && maze[j, i + 1] != -1 && maze[j, i] != maze[j, i + 1])
                        {
                            for (int k = 0; k < maze.GetLength(0); k++)
                            {
                                for (int l = 0; l < maze.GetLength(1); l++)
                                {
                                    if (maze[l, k] == maze[j, i + 1])
                                    {
                                        maze[l, k] = maze[j, i];
                                    }
                                }
                            }
                        }
                    }
                }

                if (edges.Count == 0)
                {
                    isComplete = true;
                }
            }

            

            if (size % 2 == 0)
            {
                for (int i = 0; i < maze.GetLength(1); i++)
                {
                    for (int j = 0; j < maze.GetLength(0); j++)
                    {
                        if (maze[j, i] == -1)
                        {
                            Maze[j + 1, i + 1] = "w";
                        }
                        else
                        {
                            Maze[j + 1, i + 1] = "f";
                        }
                    }
                }

                for (int i = 0; i < Maze.GetLength(0); i++)
                {
                    Maze[i, 0] = "w";
                    Maze[0, i] = "w";
                }
            }

            else
            {
                for (int i = 0; i < Maze.GetLength(0); i++)
                {
                    Maze[i, 0] = "w";
                    Maze[0, i] = "w";
                }
                for (int i = 0; i < maze.GetLength(1); i++)
                {
                    for (int j = 0; j < maze.GetLength(0); j++)
                    {
                        if (maze[j, i] == -1)
                        {
                            Maze[j + 1, i + 1] = "w";
                        }
                        else
                        {
                            Maze[j + 1, i + 1] = "f";
                        }
                    }
                }
            }

            bool exitIsPlaced = false;

            while (!exitIsPlaced)
            {
                int x = r.Next(0, Maze.GetLength(0));
                int y = r.Next(0, Maze.GetLength(1));

                if (Maze[x, y] != "w")
                {
                    int AdjacentWallCount = 0;
                    
                    if (Maze[x + 1, y] == "w")
                    {
                        AdjacentWallCount++;
                    }
                    if (Maze[x - 1, y] == "w")
                    {
                        AdjacentWallCount++;
                    }
                    if (Maze[x, y + 1] == "w")
                    {
                        AdjacentWallCount++;
                    }
                    if (Maze[x, y - 1] == "w")
                    {
                        AdjacentWallCount++;
                    }
                    if (AdjacentWallCount == 3)
                    {
                        EntranceExit_Grid[x, y] = "exit";
                        exitIsPlaced = true;
                    }

                }
            }
        }

        //Generates the level via depth-first search

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

        //Constructor

        public Floor(int Size, string Seed)
        {
            KrusukalLevel(out Wall_Grid, ref EntranceExit_Grid, ref HP_Grid, ref Item_Grid, ref Enemy_Grid, Size, Seed);
        }

        //Draws the level onto the screen

        public void DrawLevel(SpriteBatch s, Texture2D wall, Texture2D floor, Texture2D Exit, Player p)
        {
            for (int i = 0; i < Wall_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Wall_Grid.GetLength(1); j++)
                {

                    if (j < p.pos.X + 5 && j > p.pos.X - 5 && i < p.pos.Y + 5 && i > p.pos.Y - 5)
                    {
                        switch (Wall_Grid[j, i].Substring(0,1))
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

                        if (EntranceExit_Grid[j, i] == "exit")
                        {
                            s.Draw(Exit, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                        }
                    }

                    else
                    {
                        switch (Wall_Grid[j, i].Substring(0,1))
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

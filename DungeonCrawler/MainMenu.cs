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
    class MainMenu
    {
        Texture2D startGame;
        Texture2D exitGame;
        Texture2D options;
        Texture2D hiScores;
        Texture2D arrow;

        bool WPressed;
        bool SPressed;
        bool TPressed;

        int currOption = 0;

        int tCount = 0;

        public MainMenu ()
        {

        }

        public void LoadContent (Texture2D StartGame, Texture2D ExitGame, Texture2D Options, Texture2D HiScores, Texture2D Arrow)
        {
            startGame = StartGame;
            exitGame = ExitGame;
            options = Options;
            hiScores = HiScores;
            arrow = Arrow;
        }

        public void Update(ref string GameState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && currOption > 0 && WPressed == false)
            {
                currOption -= 1;
                WPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && currOption < 3 && SPressed == false)
            {
                currOption += 1;
                SPressed = true;
            }
            
            else if (Keyboard.GetState().IsKeyDown(Keys.T) && !TPressed)
            {
                tCount++;
                TPressed = true;
            }
                 

            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switch (currOption)
                {
                    case 0:
                        GameState = "init_game";
                        break;
                    case 1:
                        GameState = "hiScore";
                        break;
                    case 2:
                        GameState = "options";
                        break;
                    case 3:
                        GameState = "exit";
                        break;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                WPressed = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.S))
            {
                SPressed = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.T))
            {
                TPressed = false;
            }

            if (tCount == 3)
            {
                GameState = "loading_test";
            }
        }

        public void Draw(SpriteBatch s, GraphicsDeviceManager g)
        {
            s.Draw(arrow, new Rectangle(g.PreferredBackBufferWidth / 2 - 364, g.PreferredBackBufferHeight / 2 - 128 + 64 * currOption, 64, 64), Color.White);
            s.Draw(startGame, new Rectangle(g.PreferredBackBufferWidth / 2 - 300, g.PreferredBackBufferHeight / 2 - 128, 600, 64), Color.White);
            s.Draw(hiScores, new Rectangle(g.PreferredBackBufferWidth / 2 - 300, g.PreferredBackBufferHeight / 2 - 64, 600, 64), Color.White);
            s.Draw(options, new Rectangle(g.PreferredBackBufferWidth / 2 - 300, g.PreferredBackBufferHeight / 2, 600, 64), Color.White);
            s.Draw(exitGame, new Rectangle(g.PreferredBackBufferWidth / 2 - 300, g.PreferredBackBufferHeight / 2 + 64, 600, 64), Color.White);
        }
    }
}

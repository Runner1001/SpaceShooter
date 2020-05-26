using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    static class Game
    {
        static Player player;
        static Background bg;
        public static int NumJoysticks;

        public static Window window;
        //static float totalTime;
        static float gravity;

        public static float DeltaTime { get { return window.deltaTime; } }
        public static float Gravity { get { return gravity; } }

        static Game()
        {
            window = new Window(1280, 720, "Run!", false);
            gravity = 300.0f;
            /*
            GfxManager.AddTexture("player", "Assets/player_ship.png");
            GfxManager.AddTexture("bg", "Assets/spaceBg.png");
            GfxManager.AddTexture("bullets", "Assets/beams.png");
            GfxManager.AddTexture("enemy", "Assets/enemy_ship.png");
            GfxManager.AddTexture("enemy2", "Assets/redEnemy_ship.png");
            GfxManager.AddTexture("fireGlobe", "Assets/fireGlobe.png");
            GfxManager.AddTexture("blueLaser", "Assets/blueLaser.png");
            GfxManager.AddTexture("playerBar", "Assets/loadingBar_bar.png");
            GfxManager.AddTexture("barFrame", "Assets/loadingBar_frame.png");
            GfxManager.AddTexture("powerUp_Nrg", "Assets/powerUp_battery.png");
            */
            

        }

        public static void Play()
        {
            GfxManager.Load();
            player = new Player("player", new Vector2(window.Width / 2, window.Height / 2));
            bg = new Background("bg", Vector2.Zero, -220);

            string[] joysticks = Game.window.Joysticks;

            for (int i = 0; i < joysticks.Length; i++)
            {
                if (joysticks[i] != null && joysticks[i] != "Unmapped Controller")
                    NumJoysticks++;
            }
            while (window.opened)
            {

                //totalTime += GfxTools.Win.deltaTime;
                Console.SetCursorPosition(0, 0);
                Console.Write((1 / window.deltaTime)+"                   ");
               
                //Input
                if (window.GetKey(KeyCode.Esc))
                    break;
                if(player.IsActive)
                    player.Input();

                //Update
                PhysicsManager.Update();
                UpdateManager.Update();
                PhysicsManager.CheckCollisions();
                SpawnManager.Update();


                //Draw
                DrawManager.Draw();

                

                window.Update();
            }
        }
    }
}

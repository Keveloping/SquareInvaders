using System;
using Aiv.Draw;

namespace SquareInvaders_22_Dicembre_2020 {
    static class Game {
        static Window window;
        static Player player;
        public static float totalTime;
        static Ufo ufo;

        public const int distToSide = 20;

        public static float gravity = 555f;

        public static float DeltaTime { get { return window.DeltaTime; } }

        static Game () {
            window = new Window (800 , 600 , "Space Invaders" , PixelFormat.RGB);
            GfxTools.Init (window);
            EnemyMgr.Init (24 , 3);

            Vector2 playerPos;
            playerPos.X = window.Width / 2;
            playerPos.Y = window.Height * 0.95f;

            ufo = new Ufo(new Vector2(0 - 100,100), new Vector2(500, 0));
            player = new Player (playerPos);
        }

        public static Player GetPlayer () {
            return player;
        }

        public static Ufo GetUfo() {
            return ufo;
        }

        public static void Play () {
            while (window.IsOpened && !EnemyMgr.Landed && !EnemyMgr.AllGone () && player.IsAlive) {
                Console.SetCursorPosition (0 , 0);
                Console.WriteLine ((1 / GfxTools.Win.DeltaTime));
                totalTime += GfxTools.Win.DeltaTime;
                Console.WriteLine($"Time : {totalTime}");
                Console.WriteLine($"TimeUfo : {Ufo.time}");
                GfxTools.Clean ();

                //Input
                if (window.GetKey (KeyCode.Esc))
                    return;

                player.Input ();

                //Update
                player.Update ();
                ufo.Update();
                EnemyMgr.Update();

                //Draw
                player.Draw ();
                Ufo.Draw();
                EnemyMgr.Draw ();
                Hearts.Draw();
                Score.Draw();
                Barriers.Draw();

                window.Blit ();
            }
        }

        public static float GetTotalTime()
        {
            return totalTime;
        }
    }
}

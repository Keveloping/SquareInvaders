
namespace SquareInvaders_22_Dicembre_2020 {
    static class EnemyMgr {

        static Alien[] aliens;
         static int numAliens;
        static int numRows;
        static int aliensPerRow;
        static int alienWidth;
        static int alienHeight;
        static int numAlives;
        static int numVisibles;
        static AlienBullet[] bullets;

        public static bool Landed;

        public static void Init (int numOfAliens , int numOfRows) {
            numAliens = numOfAliens;
            numRows = numOfRows;
            numAlives = numAliens;
            numVisibles = numAliens;
            aliensPerRow = numAliens / numRows;

            aliens = new Alien[numAliens];

            int startX = 40;
            int posY = 100;
            int dist = 5;
            alienWidth = 55;
            alienHeight = 40;

            Color green = new Color (0 , 255 , 0);

            for (int i = 0; i < aliens.Length; i++) {
                if (i != 0 && i % aliensPerRow == 0) {
                    posY += alienHeight + dist;
                }
                int alienX = startX + (i % aliensPerRow) * (alienWidth + dist);
                aliens[i] = new Alien (new Vector2 (alienX , posY) , new Vector2 (200 , 0) , alienWidth , alienHeight , green);
                if (i >= numOfAliens - aliensPerRow) {
                    aliens[i].CanShoot = true;
                }
            }

            bullets = new AlienBullet[aliensPerRow];
            for (int i = 0; i < bullets.Length; i++) {
                bullets[i] = new AlienBullet ();
            }

        }

        public static void Update () {
            bool endReached = false;
            float tempOverFlowX = 0;
            float overflowX = 0;
            for (int i = 0; i < aliens.Length; i++) {
                if (aliens[i].IsVisible) {
                    if (aliens[i].Update (ref tempOverFlowX)) {
                        endReached = true;
                        overflowX = tempOverFlowX;
                    }
                }
            }

            if (endReached) {
                for (int i = 0; i < aliens.Length; i++) {
                    if (aliens[i].IsAlive) {
                        aliens[i].Translate (new Vector2 (-overflowX , 20));
                        aliens[i].Velocity.X = -aliens[i].Velocity.X;
                        if (aliens[i].Position.Y >= GfxTools.Win.Height * 0.90f) {
                            Landed = true;
                        }
                    }
                }
            }


            Player player = Game.GetPlayer ();
            for (int i = 0; i < bullets.Length; i++) {
                if (bullets[i].IsAlive) {
                    bullets[i].Update ();
                    if (bullets[i].Collides (player.GetPosition (), player.GetRay ())) {
                        player.OnHit ();
                        bullets[i].IsAlive = false;
                    }
                }
            }

            //Alien hitta le barriere
            for(int i=0; i<bullets.Length; i++) {
                if (bullets[i].IsAlive) {
                    bullets[i].Update();
                    if (Barriers.AlienBulletCollideBarrier(bullets[i], Barriers.FirstBarrier)) {
                        Barriers.OnHit();
                        bullets[i].IsAlive = false;
                    }
                }
            }
            

        }

        public static void Draw () {
            for (int i = 0; i < aliens.Length; i++) {
                if (aliens[i].IsVisible) {
                    aliens[i].Draw ();
                }
            }
            for (int i = 0; i < bullets.Length; i++) {
                if (bullets[i].IsAlive) {
                    bullets[i].Draw ();
                }
            }
        }

        public static void Shoot (Alien shooter) {
            AlienBullet b = GetFreeBullet ();
            if (b != null) {
                b.Shoot (new Vector2 (shooter.Position.X , shooter.Position.Y + shooter.GetHeight () / 2 + 15) , new Vector2 (0 , 250));
            }
        }

        private static AlienBullet GetFreeBullet () {
            for (int i = 0; i < bullets.Length; i++) {
                if (!bullets[i].IsAlive) {
                    return bullets[i];
                }
            }
            return null;
        }

        private static void IncAliensSpeed (float percentage) {
            for (int i = 0; i < aliens.Length; i++) {
                aliens[i].Velocity.X *= percentage;
            }
        }

        public static bool CollideWithBullet (Bullet bullet) {
            for (int i = 0; i < aliens.Length; i++) {
                if (aliens[i].IsAlive) {
                    if (bullet.Collides (aliens[i].Position , aliens[i].GetWidth () / 2)) {
                        IncAliensSpeed (1.05f);
                        aliens[i].OnHit ();
                        if (aliens[i].CanShoot) {
                            int prevAlienIndex = i - aliensPerRow;
                            while (prevAlienIndex >= 0) {
                                if (aliens[prevAlienIndex].IsAlive) {
                                    aliens[prevAlienIndex].CanShoot = true;
                                    break;
                                }

                                prevAlienIndex -= aliensPerRow;
                            }
                        }
                        return true;
                    }
                }

            }
            return false;
        }

        public static void OnAlienDisappears () {
            numVisibles--;
        }

        public static bool AllGone () {
            return numVisibles <= 0;
        }

        public static int GetNumVisibles()
        {
            return numVisibles;
        }

    }
}

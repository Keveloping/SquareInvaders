using Aiv.Draw;

namespace SquareInvaders_22_Dicembre_2020 {
    class Player {

        SpriteObj spriteplayer;
        Vector2 position;

        float speed;
        const float maxSpeed = 730.0f;
        int distToSide;

        Bullet[] bullets; //pool of bullets
        float counter;
        float shootDelay;  

        int hp = 3;

        public bool IsAlive;

        public Player (Vector2 pos) {

            position = pos;
            spriteplayer = new SpriteObj("Assets/player.png", position);
            pos.X += spriteplayer.GetWidth() / 2;
            pos.Y += spriteplayer.GetHeight() / 2;
            distToSide = Game.distToSide;
            shootDelay = 0.2f;

            bullets = new Bullet[30];

            Color bulletCol = new Color (255 , 255 , 255);
            for (int i = 0; i < bullets.Length; i++) {
                bullets[i] = new Bullet (10 , 20 , bulletCol);
            }
            IsAlive = true;
        }

        private Bullet GetFreeBullet () {
            for (int i = 0; i < bullets.Length; i++) {
                if (!bullets[i].IsAlive) {
                    return bullets[i];
                }
            }
            return null;
        }
        public void Shoot () {
            Bullet b = GetFreeBullet ();
            if (b != null) {
                b.Shoot (new Vector2 (position.X + spriteplayer.GetWidth()/2, position.Y - spriteplayer.GetHeight()/3) , new Vector2 (0 , -3000));
            }
        }

        public void Input () {
            counter += GfxTools.Win.DeltaTime;

            if (GfxTools.Win.GetKey (KeyCode.Right) || GfxTools.Win.GetKey (KeyCode.D)) {
                speed = maxSpeed;
            } else if (GfxTools.Win.GetKey (KeyCode.Left) || GfxTools.Win.GetKey (KeyCode.A)) {
                speed = -maxSpeed;
            } else {
                speed = 0;
            }

            if (GfxTools.Win.GetKey (KeyCode.Space)) {
                if (counter >= shootDelay) {
                    Shoot ();
                    counter = 0;
                }
            }
        }

        public void Update () {
            float deltaX = speed * GfxTools.Win.DeltaTime;
            position.X += deltaX;
            float maxX = spriteplayer.GetPosition().X + spriteplayer.GetWidth() + distToSide;
            float minX = spriteplayer.GetPosition().X - distToSide;

            if (maxX > GfxTools.Win.Width - distToSide) {
                float overflowX = maxX - (GfxTools.Win.Width - distToSide);
                position.X -= overflowX;
                deltaX -= overflowX;
            } else if (minX < distToSide) {
                float overflowX = minX - distToSide;
                position.X -= overflowX;
                deltaX -= overflowX;
            }

            spriteplayer.Translate(deltaX, 0);
 

            for (int i = 0; i < bullets.Length; i++) {
                if (bullets[i].IsAlive) {
                    bullets[i].Update ();

                    //  AGGIUNTA CHECK COLLISIONI
                    if (EnemyMgr.CollideWithBullet (bullets[i])) {
                        bullets[i].IsAlive = false;
                        ScoreCounter.AlienScoreInc();
                    }
                    else if(Ufo.CollideWithBullet(bullets[i],Ufo.spriteobj))
                        {
                        bullets[i].IsAlive = false;
                        ScoreCounter.UfoScoreInc();
                    }
                    else if(Barriers.CollideFirstBarrier(bullets[i],Barriers.FirstBarrier)) {
                        bullets[i].IsAlive = false;
                    }
                }
            }
        }

        public void Draw () {

            spriteplayer.Draw();

            for (int i = 0; i < bullets.Length; i++) {
                if (bullets[i].IsAlive)
                    bullets[i].Draw ();
            }
        }

        public Vector2 GetPosition () {
            return position;
        }

        public void OnHit () {
            hp--;
            if (hp <= 0) {
                IsAlive = false;
            }
        }

        public float GetRay () {
            return spriteplayer.GetWidth() / 2;
        }

        public int GetHp()
        {
            return hp;
        }

    }
}

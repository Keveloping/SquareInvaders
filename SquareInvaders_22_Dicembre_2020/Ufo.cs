using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders_22_Dicembre_2020 {
    class Ufo {

        public static Vector2 position;
        public Vector2 velocity;
        public Animation animation;
        public static SpriteObj spriteobj;
        static float ufocounter;
        public static float time;

        public static bool isAlive;
        public static bool isVisible;

        public Ufo(Vector2 pos, Vector2 vel) {
            position = pos;
            velocity = vel;
            isAlive = false;
            isVisible = true;
            ufocounter = RandomGenerator.GetRandom(1, 2);

            spriteobj = new SpriteObj("Assets/ship_0.png", position);
            string[] animationFiles = { "Assets/ship_0.png", "Assets/ship_1.png", "Assets/ship_2.png", "Assets/ship_3.png", "Assets/ship_4.png" };
            animation = new Animation(animationFiles, spriteobj, 12);

        }

        public void Update() {

            time += GfxTools.Win.DeltaTime;
            if (time > ufocounter) {

                isAlive = true;
            }
            if (isAlive) {
                animation.Update();

                float deltaX = velocity.X * GfxTools.Win.DeltaTime;
                position.X += deltaX;

                if (spriteobj != null)
                    spriteobj.Translate(deltaX, 0);

                #region Avanti ed indietro
                //if (position.X - spriteobj.GetWidth() >= GfxTools.Win.Height)
                //{
                //    velocity.X = -velocity.X;
                //    spriteobj.Translate(deltaX, 0);
                //}
                //if (position.X < 0)
                //{
                //    velocity.X = -velocity.X;
                //    spriteobj.Translate(deltaX, 0);
                //}
                #endregion

                if (position.X > GfxTools.Win.Width) {
                    ufocounter = RandomGenerator.GetRandom(3, 4);
                    isAlive = false;
                    time = 0;
                }
            }
            else {
                animation.Update();
                spriteobj.SetPosition(new Vector2(0 - spriteobj.GetWidth(), 70));
                position.X = 0 - spriteobj.GetWidth();
                position.Y = 0;

            }


        }

        public static void Draw() {

            spriteobj.Draw();

        }

        public static bool OnHit() {
            isAlive = false;
            isVisible = false;
            ufocounter = RandomGenerator.GetRandom(5, 6);
            time = 0;
            return true;
        }

        public void Translate(float deltaX, float deltaY) {
            position.X += deltaX;
            position.Y += deltaY;
        }

        public static bool CollideWithBullet(Bullet bullet, SpriteObj _spriteobj) {
            if (bullet.Collides(new Vector2(position.X + _spriteobj.GetWidth() / 2, position.Y + _spriteobj.GetWidth() / 2), _spriteobj.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            return false;
        }

    }
}

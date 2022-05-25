using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders_22_Dicembre_2020 {
    static class Barriers {
        static Vector2 position = new Vector2(160, 465);
        public static SpriteObj FirstBarrier = new SpriteObj("Assets/barrier.png", position);
        public static SpriteObj SecondBarrier = new SpriteObj("Assets/barrier.png", new Vector2(position.X + 200, position.Y));
        public static SpriteObj ThirdBarrier = new SpriteObj("Assets/barrier.png", new Vector2(position.X + 400, position.Y));

        public static int hp = 2;

        static bool isAlive = true;

        public static void Draw() {

            FirstBarrier.Draw();
            SecondBarrier.Draw();
            ThirdBarrier.Draw();
        }

        public static bool OnHit() {
            hp--;
            if (hp <= 0) {

            isAlive = false;
            }
            isAlive = true;
            return true;
        }

        public static float GetRay() {
            return FirstBarrier.GetWidth() / 2;
        }

        public static bool CollideFirstBarrier(Bullet bullet, SpriteObj _barrier) {
            if (bullet.Collides(new Vector2(position.X + _barrier.GetWidth()/2,465 ),_barrier.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            if (bullet.Collides(new Vector2((position.X + 200) + _barrier.GetWidth() / 2, 465), _barrier.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            if (bullet.Collides(new Vector2((position.X + 400) + _barrier.GetWidth() / 2, 465), _barrier.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            return false;

        }
        public static bool AlienBulletCollideBarrier(AlienBullet bullet, SpriteObj _barrier) {
            if (bullet.Collides(new Vector2(position.X + _barrier.GetWidth() / 2, 465), _barrier.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            if (bullet.Collides(new Vector2((position.X + 200) + _barrier.GetWidth() / 2, 465), _barrier.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            if (bullet.Collides(new Vector2((position.X + 400) + _barrier.GetWidth() / 2, 465), _barrier.GetWidth() / 2)) {
                OnHit();
                return true;
            }
            return false;

        }




    }
}

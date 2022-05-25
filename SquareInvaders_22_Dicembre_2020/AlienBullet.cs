
namespace SquareInvaders_22_Dicembre_2020 {
    class AlienBullet {

        Vector2 velocity;
        SpriteObj sprite; //sprite del proiettile
        Animation animation;

        public Vector2 Position;
        public bool IsAlive;

        public AlienBullet () {
            Position = new Vector2 (0 , 0);
            velocity = Position;
            sprite = new SpriteObj ("Assets/alienBullet_0.png" , Position);
            string[] animationFiles = { "Assets/alienBullet_0.png" , "Assets/alienBullet_1.png" };
            animation = new Animation (animationFiles, sprite, 12);
        }

        public void Shoot (Vector2 startPos , Vector2 startVelocity) {
            Position = startPos;
            sprite.SetPosition (new Vector2 (Position.X - sprite.GetWidth () / 2 , Position.Y - sprite.GetHeight () / 2));
            IsAlive = true;
            velocity = startVelocity;
        }

        public void Update () {

            animation.Update ();

            float deltaX = velocity.X * GfxTools.Win.DeltaTime;
            Position.X += deltaX;

            float deltaY = velocity.Y * GfxTools.Win.DeltaTime;
            Position.Y += deltaY;

            if (sprite != null) sprite.Translate (deltaX , deltaY);

            if (Position.Y - sprite.GetHeight () / 2 >= GfxTools.Win.Height) {
                IsAlive = false;
            }
        }

        public void Draw () {
            sprite.Draw ();
        }

        public bool Collides (Vector2 center, float ray) {
            Vector2 dist = Position.Sub (center);
            return (dist.GetLength () <= sprite.GetWidth () / 2 + ray);
        }

        public bool CollideWithBullet(Bullet bullet, SpriteObj _spriteobj) {
            if (bullet.Collides(new Vector2(Position.X + _spriteobj.GetWidth() / 2, Position.Y + _spriteobj.GetWidth() / 2), _spriteobj.GetWidth() / 2)) {
                Ufo.OnHit();
                return true;
            }
            return false;
        }

    }
}

namespace SquareInvaders_22_Dicembre_2020 {
    class Bullet {

        int width;
        int height;
        Vector2 velocity;
        Color color;

        public Vector2 Position;
        public bool IsAlive;

        public Bullet (int w , int h , Color col) {
            Position = new Vector2 (0 , 0);
            velocity = Position;
            width = w;
            height = h;
            color = col;
        }

        public void Update () {
            Position.X += velocity.X * GfxTools.Win.DeltaTime;
            Position.Y += velocity.Y * GfxTools.Win.DeltaTime;

            if (Position.Y + height / 2 < 0 || Position.Y - height / 2 >= GfxTools.Win.Height) {
                IsAlive = false;
            }
        }

        public void Draw () {
            GfxTools.DrawRect ((int) (Position.X - width / 2) , (int) (Position.Y - height / 2) , width , height , color.R , color.G , color.B);
        }

        public void Shoot (Vector2 startPos , Vector2 startVelocity) {
            Position = startPos;
            velocity = startVelocity;
            IsAlive = true;
        }

        public int GetWidth () {
            return width;
        }

        public bool Collides (Vector2 center , float ray) {
            Vector2 dist = Position.Sub (center);
            return (dist.GetLength () <= width / 2 + ray);
        }

        public Vector2 GetPosition()
        {
            return Position;
        }
    }
}

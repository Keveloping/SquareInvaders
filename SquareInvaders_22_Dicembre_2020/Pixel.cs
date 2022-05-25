namespace SquareInvaders_22_Dicembre_2020 {
    class Pixel {
        Vector2 position;
        Vector2 velocity;
        int width;
        Color color;

        private bool isGravityAffected;
        private bool isVisible;


        public bool GetIsVisible () {
            return isVisible;
        }

        public void SetGravityAffected (bool value) {
            isGravityAffected = value;
        }

        public Vector2 GetPosition () {
            return position;
        }

        public void SetPosition (Vector2 position) {
            this.position = position;
        }

        public Vector2 GetVelocity () {
            return velocity;
        }

        public void SetVelocity (Vector2 velocity) {
            this.velocity = velocity;
        }

        public Pixel (Vector2 pos , int w , Color col) {
            position = pos;
            width = w;
            color = col;
            isVisible = true;
        }

        public void Draw () {
            GfxTools.DrawRect ((int) position.X , (int) position.Y , width , width , color.R , color.G , color.B);
        }

        public void Translate (float x , float y) {
            position.X += x;
            position.Y += y;
        }

        public void Update () {
            if (isGravityAffected) {
                velocity.Y += Game.gravity * GfxTools.Win.DeltaTime;
            }

            position.X += velocity.X * GfxTools.Win.DeltaTime;
            position.Y += velocity.Y * GfxTools.Win.DeltaTime;

            if (position.Y >= GfxTools.Win.Height || position.X + width < 0 || position.X >= GfxTools.Win.Width) {
                isVisible = false;
            }
        }
    }
}

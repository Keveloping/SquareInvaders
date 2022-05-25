namespace SquareInvaders_22_Dicembre_2020 {
    class Rect {
        Vector2 position;
        int width;
        int height;
        Color color;

        public Rect (float xPos , float yPos , int w , int h , Color col) {
            this.position.X = xPos;
            this.position.Y = yPos;
            this.width = w;
            this.height = h;
            this.color = col;
        }

        public void Translate (float x , float y) {
            position.X += x;
            position.Y += y;
        }

        public void Draw () {
            GfxTools.DrawRect ((int) position.X , (int) position.Y , width , height , color.R , color.G , color.B);
        }
    }
}

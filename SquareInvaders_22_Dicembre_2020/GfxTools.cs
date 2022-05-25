using Aiv.Draw;

namespace SquareInvaders_22_Dicembre_2020 {
    static class GfxTools {

        //Reference disponibile praticamente ovunque nel codice della finestra del gioco (singleton).
        public static Window Win;

        //Inizializza la classe statica
        public static void Init (Window window) {
            Win = window;
        }


        public static void Clean () {
            for (int i = 0; i < Win.Bitmap.Length; i++) {
                Win.Bitmap[i] = 0;
            }
        }

        public static void PutPixel (int x , int y , byte r , byte g , byte b) {
            if (x < 0 || x >= Win.Width || y < 0 || y >= Win.Height)
                return;
            int index = (y * Win.Width + x) * 3;
            Win.Bitmap[index] = r;
            Win.Bitmap[index + 1] = g;
            Win.Bitmap[index + 2] = b;
            Win.Bitmap[index + 2] = b;
        }

        public static void DrawHorizontalLine (int x , int y , int width , byte r , byte g , byte b) {
            for (int i = 0; i < width; i++) {
                PutPixel (x + i , y , r , g , b);
            }
        }

        public static void DrawVerticalLine (int x , int y , int height , byte r , byte g , byte b) {
            for (int i = 0; i < height; i++) {
                PutPixel (x , y + i , r , g , b);
            }
        }

        public static void DrawRect (int x , int y , int width , int height , byte r , byte g , byte b) {
            for (int i = 0; i < height; i++) {
                DrawHorizontalLine (x , y + i , width , r , g , b);
            }
        }

        public static void DrawSprite (Sprite sprite, int spriteX, int spriteY) {
            int x; //la x del pixel che sto per disegnare
            int y; //la y del pixel che sto per disegnare

            for (int i = 0; i < sprite.Width; i++) {
                for (int j = 0; j < sprite.Height; j++) {

                    x = spriteX + i;
                    y = spriteY + j;

                    if (x < 0 || x >= Win.Width || y < 0 || y >= Win.Height) {
                        continue;
                    }

                    int windowIndex = (y * Win.Width + x) * 3;
                    int spriteIndex = (j * sprite.Width + i) * 4;

                    byte winR = Win.Bitmap[windowIndex];
                    byte winG = Win.Bitmap[windowIndex + 1];
                    byte winB = Win.Bitmap[windowIndex + 2];

                    byte spriteR = sprite.Bitmap[spriteIndex];
                    byte spriteG = sprite.Bitmap[spriteIndex + 1];
                    byte spriteB = sprite.Bitmap[spriteIndex + 2];
                    byte spriteA = sprite.Bitmap[spriteIndex + 3];
                    float alpha = spriteA / 255;


                    byte blendedR = (byte) (spriteR * alpha + winR * (1 - alpha));
                    byte blendedG = (byte) (spriteG * alpha + winG * (1 - alpha));
                    byte blendedB = (byte) (spriteB * alpha + winB * (1 - alpha));

                    Win.Bitmap[windowIndex] = blendedR;
                    Win.Bitmap[windowIndex + 1] = blendedG;
                    Win.Bitmap[windowIndex + 2] = blendedB;


                }
            }

        }


    }
}

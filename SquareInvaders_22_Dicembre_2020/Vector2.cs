using System;

namespace SquareInvaders_22_Dicembre_2020 {
    struct Vector2 {
        public float X;
        public float Y;

        //Costruttore
        public Vector2 (float x , float y) {
            X = x;
            Y = y;
        }

        //Sottrazione
        public Vector2 Sub (Vector2 vec) {
            return new Vector2 (X - vec.X , Y - vec.Y);
        }

        public static Vector2 operator + (Vector2 a , Vector2 b) {
            return new Vector2 (a.X + b.X , a.Y + b.Y);
        }

        public static Vector2 operator - (Vector2 a , Vector2 b) {
            return new Vector2 (a.X - b.X , a.Y - b.Y);
        }

        public static Vector2 Sub (Vector2 a , Vector2 b) {
            return new Vector2 (a.X - b.X , a.Y - b.Y);
        }

        //Lunghezza del vettore
        public float GetLength () {
            return (float) Math.Sqrt (X * X + Y * Y);
        }

        public float GetLenghtSquared () {
            return X * X + Y * Y;
        }

    }
}

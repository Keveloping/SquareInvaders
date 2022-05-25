using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders_22_Dicembre_2020
{
    static class Score
    {
        static SpriteObj score = new SpriteObj("Assets/numbers_0.png", position);
        static Vector2 position = new Vector2(200,30);
        static string[] scoreFiles = { "Assets/numbers_0.png", "Assets/numbers_1.png", "Assets/numbers_2.png", "Assets/numbers_3.png", "Assets/numbers_4.png", "Assets/numbers_5.png", "Assets/numbers_6.png", "Assets/numbers_7.png", "Assets/numbers_8.png", "Assets/numbers_9.png" };
        static int[] numbers = new int[6];

        public static void Draw()
        {

            for (int i = 0; i < numbers.Length; i++) {

                score = new SpriteObj(scoreFiles[numbers[i]],new Vector2(position.X + 15 * i, position.Y));
                score.Draw();
            }
   
        }

        public static void AddPoints() {
            int tmp = ScoreCounter.GetTotalScore();
            numbers[0] = tmp / 100000;
            tmp -= 100000 * numbers[0];

            numbers[1] = tmp / 10000;
            tmp -= 10000 * numbers[1];

            numbers[2] = tmp / 1000;
            tmp -= 1000 * numbers[2];

            numbers[3] = tmp / 100;
            tmp -= 100 * numbers[3];

            numbers[4] = tmp / 10;
            tmp -= 10 * numbers[4];

            numbers[5] = tmp / 1;

        }


    }

}

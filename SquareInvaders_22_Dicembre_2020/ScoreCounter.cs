using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders_22_Dicembre_2020
{
    static class ScoreCounter
    {
        public static int totalscore = 0;


        public static void AlienScoreInc() {
            totalscore += 10;
            Score.AddPoints();
        }

        public static void UfoScoreInc() {
            totalscore += 100;
            Score.AddPoints();
        }

        public static int GetTotalScore() {
            return totalscore;
        }
    }
}

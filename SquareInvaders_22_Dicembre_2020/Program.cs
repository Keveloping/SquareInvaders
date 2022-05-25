using System;

namespace SquareInvaders_22_Dicembre_2020 {
    class Program {
        static void Main (string[] args) {
            Game.Play ();
            Console.WriteLine ("Partita finita. Premi invio per terminare");
            Console.WriteLine($"Score : {ScoreCounter.totalscore - (int)Game.totalTime * 3}");
            Console.ReadLine ();
        }
    }
}

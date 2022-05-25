using System;

namespace SquareInvaders_22_Dicembre_2020
{
    static class Hearts
    {
        static SpriteObj heart;
        static Vector2 position = new Vector2(600, 20);


        public static void Draw()
        {
            for(int i=0; i < Game.GetPlayer().GetHp();i++)
            {
                heart = new SpriteObj("Assets/heart.png", new Vector2(position.X + 45 * i, position.Y));
                heart.Draw();
            }
        }
    }
}

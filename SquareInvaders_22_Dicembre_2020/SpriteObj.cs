using Aiv.Draw;
using System;

namespace SquareInvaders_22_Dicembre_2020 {
    class SpriteObj {

        Sprite sprite;
        Vector2 position;

        public int GetWidth () {
            return sprite.Width;
        }

        public int GetHeight () {
            return sprite.Height;
        }

        public Vector2 GetPosition () {
            return position;
        }

        public void SetPosition (Vector2 position) {
            this.position = position;
        }

        public SpriteObj (string fileName /*PATH RELATIVO*/, Vector2 spritePosition) {
            sprite = new Sprite (fileName);
            position = spritePosition;
        }

        public  void Translate (float deltaX, float deltaY) {
            position.X += deltaX;
            position.Y += deltaY;
        }

        public void Draw () {
            GfxTools.DrawSprite (sprite ,(int) Math.Round (position.X) , (int) Math.Round (position.Y));
        }

        public void SetSprite (Sprite sprite) {
            this.sprite = sprite;
        }

    }
}

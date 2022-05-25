using Aiv.Draw;

namespace SquareInvaders_22_Dicembre_2020 {
    class Animation {

        Sprite[] sprites;
        SpriteObj owner;
        int numFrames;
        float frameDuration;
        float counter;

        private bool Loop;
        private bool IsPlaying;

        private int currentFrame;

        public Animation (string[] files, SpriteObj owner, float fps) {
            //Potete fare un altro costruttore in overloading che accetta anche altri due bool come parametri per il Loop e se parte in play
            Loop = true;
            IsPlaying = true;

            numFrames = files.Length;
            this.owner = owner;

            sprites = new Sprite[numFrames];
            for (int i = 0; i < numFrames; i++) {
                sprites[i] = new Sprite (files[i]);
            }

            counter = 0;
            SetCurrentFrame (0);

            if (fps > 0) {
                frameDuration = 1 / fps;
            } else {
                frameDuration = 0;
            }

        }

        public void Update () {
            if (owner != null && IsPlaying) {
                counter += GfxTools.Win.DeltaTime;

                if (counter >= frameDuration) {
                    counter = 0;
                    SetCurrentFrame (currentFrame + 1);
                }
            }
        }

        private void SetCurrentFrame (int value) {
            currentFrame = value;

            if (currentFrame >= numFrames) {
                OnAnimationEnd ();
            } else {
                owner.SetSprite (sprites[currentFrame]);
            }

        }

        private void OnAnimationEnd () {
            if (Loop) {
                SetCurrentFrame (0);
            } else {
                Pause ();
            }
        }

        public void Play () {
            IsPlaying = true;
        }

        public void Stop () {
            SetCurrentFrame (0);
            IsPlaying = false;
        }

        public void Pause () {
            IsPlaying = false;
        }

        public void Restart () {
            SetCurrentFrame (0);
            Play ();
        }

    }
}

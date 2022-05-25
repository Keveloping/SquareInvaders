
namespace SquareInvaders_22_Dicembre_2020 {
    class Alien {

        int width;
        int height;

        Color color;
        //Rect sprite;
        Pixel[] sprite;
        int distToSide;
        float nextShoot;

        int visiblePixel;

        public Vector2 Velocity;
        public Vector2 Position;


        public bool IsVisible;
        public bool CanShoot;
        public bool IsAlive;

        public Alien (Vector2 pos , Vector2 vel , int w , int h , Color col) {
            Position = pos;
            Velocity = vel;
            width = w;
            height = h;
            color = col;
            distToSide = Game.distToSide;
            //sprite = new Rect (Position.X - width / 2 , Position.Y - height / 2 , width , height , color);
            IsAlive = true;
            IsVisible = true;


            //Array di byte che mi dice, su un quadrato, quali sono i pixel da disengare (1) 
            //e dove invece non c'è nessn pixel da disengare (0)
            byte[] pixelArr = {  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0
            };

            int numPixels = 0;
            //calcolo quanti sono i pixel effettivi del mio disegno
            for (int i = 0; i < pixelArr.Length; i++) {
                if (pixelArr[i] == 1)
                    numPixels++;
            }

            sprite = new Pixel[numPixels];
            //verticalPixel sono quante righe di pixel ci sono nell'array di byte
            int verticalPixel = 8;
            //horizontalPixel sono quante colonne di pixel ci sono nell'array di byte
            int horizontalPixel = 11;
            //capisco la size di ogni pixel (in pixel dello schermo, basandomi sulla risoluzione)
            int pixelSize = height / verticalPixel;
            //adatto la width dell'alieno in base alla pixelSize
            width = horizontalPixel * pixelSize;

            //setto la posizione iniziale del primo pixel in a sinistra
            float startPosX = Position.X - (float) width / 2;
            //setto la posizione della prima riga
            float posY = Position.Y - height / 2;

            //contatore dell'array di pixel
            int sp = 0;
            for (int i = 0; i < pixelArr.Length; i++) {
                //Capisco dall'array unidimensionale quando va a capo nella matrice che dovrebbe crearsi
                if (i != 0 && i % horizontalPixel == 0)
                    posY += pixelSize;
                //il pixel esiste solo se l'array di byte che rappresenta il mio "disegno" è 1. Se è 0 NON è un pixel nero, ma semplicemente un'assenza di pixel
                if (pixelArr[i] != 0) {
                    //calcolo la x del pixel attuale
                    float pixelX = startPosX + (i % horizontalPixel) * (pixelSize);
                    //creo il nuovo pixel
                    sprite[sp] = new Pixel (new Vector2 (pixelX , posY) , pixelSize , color);
                    sp++;
                }
            }

            visiblePixel = numPixels;
            nextShoot = RandomGenerator.GetRandom (2 , 12);
        }

        public bool Update (ref float overFlowX) {
            bool endReached = false;

            if (IsAlive) {
                float deltaX = Velocity.X * GfxTools.Win.DeltaTime;
                float deltaY = Velocity.Y * GfxTools.Win.DeltaTime;
                Position.X += deltaX;
                Position.Y += deltaY;

                float maxX = Position.X + width / 2;
                float minX = Position.X - width / 2;

                if (maxX > GfxTools.Win.Width - distToSide) {
                    overFlowX = maxX - (GfxTools.Win.Width - distToSide);
                    endReached = true;
                } else if (minX < distToSide) {
                    overFlowX = minX - distToSide;
                    endReached = true;
                }
                TranslateSprite (new Vector2 (deltaX , deltaY));

                if (CanShoot) {
                    nextShoot -= GfxTools.Win.DeltaTime;
                    if (nextShoot <= 0) {
                        EnemyMgr.Shoot (this);
                        nextShoot = RandomGenerator.GetRandom (2 , 12);
                    }
                }
            } else if (IsVisible) {
                for (int i = 0; i < sprite.Length; i++) {
                    if (sprite[i].GetIsVisible ()) {
                        sprite[i].Update ();
                        if (!sprite[i].GetIsVisible ()) {
                            visiblePixel--;
                            if (visiblePixel <= 0) {
                                IsVisible = false;
                                EnemyMgr.OnAlienDisappears ();
                            }
                        }
                    }
                }
            }

            return endReached;
        }

        public void Draw () {
            //sprite.Draw ();
            for (int i = 0; i < sprite.Length; i++) {
                sprite[i].Draw ();
            }
        }

        public void Translate (Vector2 transVect) {
            Position.X += transVect.X;
            Position.Y += transVect.Y;
            TranslateSprite (transVect);
        }

        private void TranslateSprite (Vector2 transVect) {
            //sprite.Translate (transVect.X , transVect.Y);
            for (int i = 0; i < sprite.Length; i++) {
                sprite[i].Translate (transVect.X , transVect.Y);
            }
        }

        public int GetWidth () {
            return width;
        }

        public int GetHeight () {
            return height;
        }

        public bool OnHit ()   {
            IsAlive = false;
            for (int i = 0; i < sprite.Length; i++) {
                Vector2 pixelVel = sprite[i].GetPosition () - Position; //ottieni la direzione uscente come se l'alieno esplodesse dal centro
                pixelVel.X *= RandomGenerator.GetRandom (4 , 15);
                pixelVel.Y *= RandomGenerator.GetRandom (4 , 23);
                sprite[i].SetVelocity (pixelVel);
                sprite[i].SetGravityAffected (true);
            }

            return true;
        }

    }
}

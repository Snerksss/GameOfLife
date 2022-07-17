namespace GameOfLifeForms {
    internal class Game {

        Pixel[,] pixelList;
        List<CordPixel> alivePixel;
        int pixelCount;
        int pixelSize;

        public Size Resolution { get; set; }

        public int PixelCount {
            get { return pixelCount; }
            set { pixelCount = value; }
        }

        internal void load(int pixelCount, int pixelSize) {
            alivePixel = new List<CordPixel>();
            this.pixelCount = pixelCount;
            pixelList = new Pixel[pixelCount, pixelCount];
            this.pixelSize = pixelSize;

            for (int i = 0; i < pixelCount; i++) {
                for (int j = 0; j < pixelCount; j++) {
                    pixelList[i, j] = generatePixel(i, j);
                }
            }
        }

        public void set(int x, int y) {
            if (pixelList[x, y].IsAlive == false) {
                pixelList[x, y].IsAlive = true;
                alivePixel.Add(new CordPixel(x, y));
            } else {
                pixelList[x, y].IsAlive = false;
                for (int i = 0; i < alivePixel.Count; i++) {
                    if (alivePixel[i].x == x && alivePixel[i].y == y) {
                        alivePixel.RemoveAt(i);
                    }
                }

            }
        }

        public Pixel generatePixel(int x, int y) {

            Pixel pixel = new Pixel(x, y);

            List<CordPixel> roundPixel = new List<CordPixel>();

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (i != 0 || j != 0) {
                        int roundX = x + i;
                        int roundY = y + j;
                        if (roundX < 0) {
                            roundX = pixelCount - 1;
                        } else {
                            if (roundX > pixelCount - 1) {
                                roundX = 0;
                            }
                        }
                        if (roundY < 0) {
                            roundY = pixelCount - 1;
                        } else {
                            if (roundY > pixelCount - 1) {
                                roundY = 0;
                            }
                        }

                        roundPixel.Add(new CordPixel(roundX, roundY));
                    }
                }
            }

            pixel.RoundPixel = roundPixel;
            return pixel;
        }

        public void update() {
            List<Pixel> pixelToCheck = new List<Pixel>();

            foreach (CordPixel pixel in alivePixel) {
                Pixel pixelID = this.pixelList[pixel.x, pixel.y];
                if (!pixelToCheck.Contains(pixelID)) {
                    pixelToCheck.Add(pixelID);
                }
                foreach (CordPixel roundPixel in pixelID.RoundPixel) {
                    Pixel roundPixelID = this.pixelList[roundPixel.x, roundPixel.y];
                    if (!pixelToCheck.Contains(roundPixelID)) {
                        pixelToCheck.Add(roundPixelID);
                    }
                }
            }

            Pixel[,] newUpdatedGameField = new Pixel[pixelCount, pixelCount];

            for (int i = 0; i < pixelCount; i++) {
                for (int j = 0; j < pixelCount; j++) {
                    newUpdatedGameField[i, j] = this.pixelList[i, j];
                }
            }

            this.alivePixel = new List<CordPixel>();

            foreach (Pixel pixel in pixelToCheck) {
                Pixel pixelTmp = new Pixel(pixel.X, pixel.Y);
                pixelTmp.IsAlive = pixel.IsAlive;
                pixelTmp.RoundPixel = pixel.RoundPixel;
                if (pixelTmp.checkAlive(this.pixelList)) {
                    this.alivePixel.Add(new CordPixel(pixelTmp.X, pixelTmp.Y));
                }
                newUpdatedGameField[pixel.X, pixel.Y] = pixelTmp;
            }

            this.pixelList = newUpdatedGameField;
        }


        public void draw(Graphics gfx) {
            gfx.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, pixelSize * pixelCount, pixelSize * pixelCount));
            foreach (CordPixel pixel in alivePixel) {
                gfx.FillRectangle(new SolidBrush(Color.Black), new Rectangle(
                    this.pixelSize * pixel.x,
                    this.pixelSize * pixel.y,
                    this.pixelSize,
                    this.pixelSize));
            }

        }

        public void unload() {

        }

        public void reset() {
            load(this.pixelCount, this.pixelSize);
        }

    }
    public class Pixel {
        private int x;
        private int y;
        private Boolean isAlive;

        private List<CordPixel> roundPixel;

        public Pixel(int x, int y) {
            this.x = x;
            this.y = y;
            isAlive = false;
            roundPixel = null;
        }

        public List<CordPixel> RoundPixel {
            get { return roundPixel; }
            set { roundPixel = value; }
        }

        public int X {
            get { return x; }
        }

        public int Y {
            get { return y; }
        }

        public Boolean IsAlive {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Boolean checkAlive(Pixel[,] pixelList) {
            int numberOfAliveCells = 0;
            foreach (CordPixel pixel in roundPixel) {
                if (pixelList[pixel.x, pixel.y].isAlive) {
                    numberOfAliveCells++;
                }
            }
            if (isAlive) {
                if (numberOfAliveCells != 2 && numberOfAliveCells != 3)
                    isAlive = false;
            } else {
                if (numberOfAliveCells == 3) {
                    isAlive = true;
                }
            }
            return isAlive;
        }
    }

    public class CordPixel {
        public int x;
        public int y;

        public CordPixel(int x, int y) { this.x = x; this.y = y; }
    }
}

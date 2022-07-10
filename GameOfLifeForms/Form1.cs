namespace GameOfLifeForms {
    public partial class GameField : Form {
        Pixel[,] pixelList;
        List<CordPixel> alivePixel;
        int pixelSize;
        Boolean firstRun;
        Boolean run;
        public GameField() {
            InitializeComponent();
            firstRun = false;
            run = false;
            alivePixel = new List<CordPixel>();
        }

        private void GameField_Load(object sender, EventArgs e) {
            resizeField();
        }



        private void numberOfPixels_ValueChanged(object sender, EventArgs e) {
            resizeField();
        }

        private void resizeField() {


            int pixelCount = Decimal.ToInt32(numberOfPixels.Value);

            int maxSize = 0;

            if (this.Size.Width + 100 <= this.Size.Height) {
                maxSize = this.Size.Width - 36;
            } else {
                maxSize = this.Size.Height - 136;
            }

            int distance = maxSize % pixelCount;

            pictureBox1.Height = pictureBox1.Width = maxSize - distance;

            this.Location = new Point(5, 10);
            this.pixelSize = pictureBox1.Height / Decimal.ToInt32(numberOfPixels.Value);





            frameGenerator();
        }

        private void generateRoundPixel(int x, int y, int pixelCount) {
            Pixel pixel = pixelList[x, y];

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
            pixelList[x, y] = pixel;
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {

            int pixelCount = Decimal.ToInt32(numberOfPixels.Value);

            if (!firstRun) {
                pixelList = new Pixel[pixelCount, pixelCount];

                for (int i = 0; i < pixelCount; i++) {
                    for (int j = 0; j < pixelCount; j++) {
                        pixelList[i,j] = new Pixel(i, j);
                    }
                }

                for (int i = 0; i < pixelCount; i++) {
                    for (int j = 0; j < pixelCount; j++) {
                        generateRoundPixel(i, j, pixelCount);
                    }
                }
                firstRun = true;
            }
            label1.Visible = numberOfPixels.Visible = false;

            button3.BackColor = Color.Gray;
            button3.IsAccessible = false;
            button3.Enabled = false;

            run = true;

            this.pixelList[5,1].IsAlive = true;
            alivePixel.Add(new CordPixel(5,1));

            this.pixelList[6, 2].IsAlive = true;
            alivePixel.Add(new CordPixel(6,2));

            this.pixelList[7, 3].IsAlive = true;
            alivePixel.Add(new CordPixel(7,3));

            this.pixelList[6, 3].IsAlive = true;
            alivePixel.Add(new CordPixel(6,3));

            this.pixelList[6, 0].IsAlive = true;
            alivePixel.Add(new CordPixel(6,0));

            this.pixelList[8, 2].IsAlive = true;
            alivePixel.Add(new CordPixel(8,2));

            while (run) {
            //for(int l = 0;l < 3; l++) { 
               

                List<Pixel> pixelToCheck = new List<Pixel>();

                foreach(CordPixel pixel in alivePixel) {
                    Pixel pixelID = this.pixelList[pixel.x, pixel.y];
                    pixelToCheck.Add(pixelID);
                    foreach(CordPixel roundPixel in pixelID.RoundPixel) {
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
                    if (pixel.checkAlive(this.pixelList)) {
                        this.alivePixel.Add(new CordPixel(pixel.X, pixel.Y));
                    }
                    newUpdatedGameField[pixel.X, pixel.Y] = pixel;
                }

                this.pixelList = newUpdatedGameField;
                frameGenerator();
            }

            

        }

        private void button2_Click(object sender, EventArgs e) {
            button3.BackColor = Color.Blue;
            button3.Enabled = true;
            run = false;
        }

        private void button3_Click(object sender, EventArgs e) {
            if (run == false) {
                label1.Visible = numberOfPixels.Visible = true;
                firstRun = false;
                pixelList = new Pixel[Decimal.ToInt32(numberOfPixels.Value), Decimal.ToInt32(numberOfPixels.Value)];
                alivePixel = new List<CordPixel>();
            }
        }

        private void frameGenerator() {
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            using (SolidBrush cellBrush = new SolidBrush(Color.White)) {
                g.Clear(Color.Black);
                foreach (CordPixel pixel in alivePixel) {
                    g.FillRectangle(cellBrush, new Rectangle(
                        this.pixelSize * pixel.x,
                        this.pixelSize * pixel.y,
                        this.pixelSize,
                        this.pixelSize));
                }
                pictureBox1.Image = (Bitmap)bmp.Clone();
                pictureBox1.Update();
            }
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
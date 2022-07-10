namespace GameOfLifeForms
{
    public partial class GameField : Form
    {
        int[,] gameField;
        int pixelSize;
        Boolean run;
        public GameField()
        {
            InitializeComponent();
            run = false;
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            resizeField();
            
        }

        

        private void numberOfPixels_ValueChanged(object sender, EventArgs e)
        {
            resizeField();
            
        }

        private void resizeField()
        {
            int pixelCount = Decimal.ToInt32(numberOfPixels.Value);
            int widthHeight = pictureBox1.Height;
            int distance = widthHeight % pixelCount;
            int additionalSize = pixelCount - distance;
            if (distance > pixelCount / 2)
            {
                additionalSize = pixelCount - distance;
            }
            else
            {
                additionalSize = - distance;
            }
            pictureBox1.Height = pictureBox1.Width = pictureBox1.Height + additionalSize;
            this.Size = new Size(this.Size.Width + additionalSize, this.Size.Height + additionalSize);
            this.Location = new Point(5, 10);
            this.pixelSize = pictureBox1.Height / Decimal.ToInt32(numberOfPixels.Value);
            gameField = new int[Decimal.ToInt32(numberOfPixels.Value), Decimal.ToInt32(numberOfPixels.Value)];
            
            frameGenerator();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = numberOfPixels.Visible = false;

            button3.BackColor = Color.Gray;
            button3.IsAccessible = false;
            button3.Enabled = false;

            run = true;

            gameField[1, 1] = 1;

            gameField[20, 4] = 1;

            frameGenerator();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Blue;
            button3.Enabled = true;
            run = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(run == false)
            {
                label1.Visible = numberOfPixels.Visible = true;
            }
        }

        private void frameGenerator()
        {
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            using (SolidBrush cellBrush = new SolidBrush(Color.DarkOrange))
            {
                g.Clear(Color.Black);
                for(int i = 0; i < numberOfPixels.Value; i++)
                {
                    for(int j = 0; j < numberOfPixels.Value; j++)
                    {
                        if(gameField[i, j] != 0)
                        {
                            g.FillRectangle(cellBrush, new Rectangle(this.pixelSize * i, this.pixelSize * j, this.pixelSize, this.pixelSize));
                        }
                    }
                }
                pictureBox1.Image = (Bitmap)bmp.Clone();
            }
        }
    }
}
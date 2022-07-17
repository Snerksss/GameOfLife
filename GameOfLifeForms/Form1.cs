namespace GameOfLifeForms {
    public partial class GameField : Form {

        System.Windows.Forms.Timer graphicsTimer;
        GameLoop gameLoop = null;


        public GameField() {
            InitializeComponent();
            Paint += GameField_Paint;
            // Initialize graphicsTimer
            graphicsTimer = new System.Windows.Forms.Timer();
            graphicsTimer.Interval = 1000 / 120;
            graphicsTimer.Tick += GraphicsTimer_Tick;
        }

        private void GameField_Load(object sender, EventArgs e) {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(50, 50);
            // Initialize Game
            Game myGame = new Game();
            myGame.Resolution = new Size(resolution.Width, resolution.Height);

            // Initialize & Start GameLoop
            gameLoop = new GameLoop();
            gameLoop.Load(myGame, this.Size.Width, 100, Decimal.ToInt32(numericUpDown1.Value));

        }

        private void GameField_Paint(object sender, PaintEventArgs e) {
            if (gameLoop != null) {

                gameLoop.Draw(e.Graphics);
            }
        }

        private void GraphicsTimer_Tick(object sender, EventArgs e) {
            // Refresh Form1 graphics
            Invalidate();
        }

        private void numberOfPixels_ValueChanged(object sender, EventArgs e) {
            resizeField();
        }

        private void resizeField() {

        }

        private void button1_Click(object sender, EventArgs e) {
            gameLoop.Start();

            // Start Graphics Timer
            graphicsTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e) {
            gameLoop.Stop();
        }

        private void GameField_Click(object sender, EventArgs e) {
            var relativePoint = this.PointToClient(Cursor.Position);
            double x = ((double)relativePoint.X / this.Size.Width) * 100;
            double y = ((double)relativePoint.Y / this.Size.Width) * 100;
            gameLoop.set((int)x, (int)y);
            this.Invalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            gameLoop.setSpeed(Decimal.ToInt32(numericUpDown1.Value));
        }

        private void button3_Click(object sender, EventArgs e) {
            gameLoop.reset();
            this.Invalidate();
        }
    }
}
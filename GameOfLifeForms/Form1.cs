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

            // Initialize Game
            Game myGame = new Game();
            myGame.Resolution = new Size(resolution.Width, resolution.Height);

            // Initialize & Start GameLoop
            gameLoop = new GameLoop();
            gameLoop.Load(myGame);
            gameLoop.Start(this.Size.Width, this.Size.Height, 80);

            // Start Graphics Timer
            graphicsTimer.Start();
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
    }  
}
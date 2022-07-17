namespace GameOfLifeForms {
    internal class GameLoop {
        private Game _myGame;
        public bool Running { get; private set; }

        public int Speed { get; private set; }

        public void Load(Game gameObj, int width, int pixelCount, int speed) {
            _myGame = gameObj;
            _myGame.load(pixelCount, (width) / pixelCount);
            Speed = speed;
        }

        public async void Start() {
            Running = true;

            DateTime _previousGameTime = DateTime.Now;

            while (Running) {

                _myGame.update();

                await Task.Delay(Speed);
            }
        }

        public void set(int x, int y) {
            _myGame.set(x, y);
        }

        public void Stop() {
            Running = false;
        }


        public void Draw(Graphics gfx) {
            _myGame.draw(gfx);
        }

        public void setSpeed(int speed) {
            Speed = speed;
        }
    }
}


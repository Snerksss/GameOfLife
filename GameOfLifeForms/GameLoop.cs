namespace GameOfLifeForms {
    internal class GameLoop {
        private Game gameObj;
        public bool Running { get; private set; }

        public int Speed { get; private set; }

        public void Load(Game gameObj, int width, int pixelCount, int speed) {
            this.gameObj = gameObj;
            this.gameObj.load(pixelCount, (width) / pixelCount);
            Speed = speed;
        }

        public async void Start() {
            Running = true;

            DateTime _previousGameTime = DateTime.Now;

            while (Running) {

                gameObj.update();

                await Task.Delay(Speed);
            }
        }

        public void set(int x, int y) {
            gameObj.set(x, y);
        }

        public void Stop() {
            Running = false;
        }


        public void Draw(Graphics gfx) {
            gameObj.draw(gfx);
        }

        public void reset() {
            gameObj.reset();
        }

        public void setSpeed(int speed) {
            Speed = speed;
        }
    }
}


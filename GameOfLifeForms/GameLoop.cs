using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeForms {
    internal class GameLoop {
        private Game _myGame;
        public bool Running { get; private set; }

        public void Load(Game gameObj) {
            _myGame = gameObj;
        }

        public async void Start(int width, int height, int pixelCount) {
            if (_myGame == null)
                throw new ArgumentException("Game not loaded!");

            _myGame.load(pixelCount, width, height, (height - 20)/pixelCount);

            
            Running = true;

            
            DateTime _previousGameTime = DateTime.Now;
            
            while (Running) {
                
                _myGame.update();
                
                await Task.Delay(100);
            }
        }

        public void Stop() {
            Running = false;
            _myGame?.unload();
        }

        
        public void Draw(Graphics gfx) {
            _myGame.draw(gfx);
        }
    }
}


using System;
using BlindMan.Enitities;

namespace BlindMan
{
    public class GameModel
    {
        public Player Player { get; private set; }

        private GameState _gameState;
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                GameStateChanged?.Invoke(value);
            }
        }

        public event Action<GameState> GameStateChanged;

        public GameModel()
        {
            Player = new Player(0, 200, 240, 300);
        }
        
        public void Update(float deltaTime)
        {
            Player.Update(deltaTime);
        }
    }
}
﻿using System;
using System.Windows.Forms;
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
        
        public event Action LeftKeyDown;
        public event Action RightKeyDown;
        public event Action UpKeyDown;
        public event Action DownKeyDown;

        public GameModel()
        {
            Player = new Player(0, 200, 40, 40, this);
        }
        
        public void Update(float deltaTime)
        {
            Player.Update(deltaTime);
        }

        public void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                case Keys.Left:
                    LeftKeyDown?.Invoke();
                    break;
                case Keys.D:
                case Keys.Right:
                    RightKeyDown?.Invoke();
                    break;
                case Keys.W:
                case Keys.Up:
                    UpKeyDown?.Invoke();
                    break;
                case Keys.S:
                case Keys.Down:
                    DownKeyDown?.Invoke();
                    break;
            }
        }
    }
}
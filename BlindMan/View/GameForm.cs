﻿using System.Drawing;
using System.Windows.Forms;
using BlindMan.Domain;
using BlindMan.View.Controls;

namespace BlindMan.View
{
    public class GameForm : Form
    {
        private GameModel gameModel;
        
        public GameForm(GameModel gameModel)
        {
            this.gameModel = gameModel;
            Text = GameSettings.GameName;
            Size = new Size(GameSettings.GameWidth, GameSettings.GameHeight);

            gameModel.GameStateChanged += SetState;
            gameModel.GameState = GameState.Menu;
        }

        private void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.Menu:
                    SetControl(new MenuControl(gameModel));
                    break;
                case GameState.Game:
                    SetControl(new GameControl(gameModel));
                    break;
                case GameState.GameWon:
                    gameModel.GameState = GameState.Menu;
                    break;
            }
        }

        private void SetControl(BaseControl control)
        {
            foreach (Control _control in Controls)
            {
                _control.Dispose();
            }
            Controls.Clear();
            Controls.Add(control);
        }
    }
}
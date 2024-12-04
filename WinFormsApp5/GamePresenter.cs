using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp5
{
    public class GamePresenter //Управляет логикой и синхронизацией между Model и View.
    {
        private readonly GameModel _model;
        private readonly IGameView _view;

        public GamePresenter(GameModel model, IGameView view) 
        {
            _model = model;
            _view = view;

            _view.SetMenuOptions(_model.IsHardMode, _model.IsComputerFirst);
        }

        public void SetDifficulty(bool isHardMode)
        {
            _model.IsHardMode = isHardMode;
        }

        public void SetFirstMove(bool isComputerFirst)
        {
            _model.IsComputerFirst = isComputerFirst;
        }

        public void NewGame()
        {
            _model.ResetBoard();
            _view.ResetBoard();

            if (_model.IsComputerFirst)
            {
                ComputerMove();
            }
        }

        public void PlayerMove(int x, int y)
        {
            if (!_model.GameActive || _model.Board[x, y] != null)
                return;

            MakeMove(x, y, "X");

            if (!_model.GameActive)
                return;

            ComputerMove();
        }

        private void ComputerMove()
        {
            if (!_model.GameActive)
                return;

            var emptyCells = new List<(int, int)>();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (_model.Board[i, j] == null)
                        emptyCells.Add((i, j));

            if (emptyCells.Count == 0)
                return;

            (int x, int y) move = emptyCells[new Random().Next(emptyCells.Count)];
            MakeMove(move.x, move.y, "O");
        }

        private void MakeMove(int x, int y, string player)
        {
            _model.Board[x, y] = player;
            _view.SetButtonImage(x, y, player, player == "X" ? @"Images2\photo_5345997896035724545_m.jpg" : @"Images2\photo_5345997896035724544_m.jpg");

            if (_model.CheckWin())
            {
                _view.ShowMessage(player == "X" ? "Нолики выиграли!" : "Крестики выиграли!");
                _model.GameActive = false;
                return;
            }

            if (_model.IsDraw())
            {
                _view.ShowMessage("Ничья!");
                _model.GameActive = false;
                return;
            }

            _model.IsXTurn = !_model.IsXTurn;
            _view.UpdateTurnIndicator();
        }
    }
}

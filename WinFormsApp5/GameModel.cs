using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp5
{
    public class GameModel
    {
        public string[,] Board { get; private set; } = new string[3, 3];
        public bool IsXTurn { get; set; } = true;
        public bool IsHardMode { get; set; } = false;
        public bool IsComputerFirst { get; set; } = false;
        public bool GameActive { get; set; } = false;

        public GameModel()
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Board[i, j] = null;

            IsXTurn = true;
            GameActive = true;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] != null && Board[i, 0] == Board[i, 1] && Board[i, 0] == Board[i, 2])
                    return true;

                if (Board[0, i] != null && Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i])
                    return true;
            }

            if (Board[0, 0] != null && Board[0, 0] == Board[1, 1] && Board[0, 0] == Board[2, 2])
                return true;

            if (Board[0, 2] != null && Board[0, 2] == Board[1, 1] && Board[0, 2] == Board[2, 0])
                return true;

            return false;
        }

        public bool IsDraw()
        {
            return Board.Cast<string>().All(cell => cell != null);
        }
    }
}

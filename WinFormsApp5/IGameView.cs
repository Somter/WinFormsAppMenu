using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp5
{
    public interface IGameView //Визуальное представление, включая меню.
    {
        void ResetBoard();
        void SetButtonImage(int x, int y, string player, string imagePath);
        void UpdateTurnIndicator();
        void ShowMessage(string message);
        void SetMenuOptions(bool isHardMode, bool isComputerFirst);
    }


}

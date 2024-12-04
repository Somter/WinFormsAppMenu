namespace WinFormsApp5
{
    public partial class Form1 : Form, IGameView
    {
        private readonly GamePresenter _presenter;
        private readonly Button[,] buttons = new Button[3, 3];

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var model = new GameModel();
            _presenter = new GamePresenter(model, this);

            InitializeGameField();
            InitializeMenu();
        }
        private void Form1_Load(object sender, EventArgs e) { }
        private void InitializeGameField()
        {
            buttons[0, 0] = button1;
            buttons[0, 1] = button2;
            buttons[0, 2] = button3;
            buttons[1, 0] = button4;
            buttons[1, 1] = button5;
            buttons[1, 2] = button6;
            buttons[2, 0] = button7;
            buttons[2, 1] = button8;
            buttons[2, 2] = button9;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = i, y = j;
                    buttons[i, j].Click += (sender, e) => _presenter.PlayerMove(x, y);
                }
            }
        }

        private void InitializeMenu()
        {
            MenuStrip menuStrip = new MenuStrip();

            ToolStripMenuItem menuGame = new ToolStripMenuItem("Игра");
            ToolStripMenuItem newGameItem = new ToolStripMenuItem("Новая игра");
            newGameItem.Click += (sender, e) => _presenter.NewGame();

            ToolStripMenuItem menuFirstMove = new ToolStripMenuItem("Первый ход");
            ToolStripMenuItem firstComputerMoveItem = new ToolStripMenuItem("Первым ходит компьютер");
            firstComputerMoveItem.CheckOnClick = true;
            firstComputerMoveItem.CheckedChanged += (sender, e) =>
            {
                _presenter.SetFirstMove(firstComputerMoveItem.Checked);
            };

            ToolStripMenuItem menuDifficulty = new ToolStripMenuItem("Сложность");
            ToolStripMenuItem easyLevelItem = new ToolStripMenuItem("Легкий уровень");
            ToolStripMenuItem hardLevelItem = new ToolStripMenuItem("Сложный уровень");

            easyLevelItem.Click += (sender, e) =>
            {
                _presenter.SetDifficulty(false);
                easyLevelItem.Checked = true;
                hardLevelItem.Checked = false;
            };

            hardLevelItem.Click += (sender, e) =>
            {
                _presenter.SetDifficulty(true);
                hardLevelItem.Checked = true;
                easyLevelItem.Checked = false;
            };

            easyLevelItem.Checked = true;

            menuGame.DropDownItems.Add(newGameItem);
            menuFirstMove.DropDownItems.Add(firstComputerMoveItem);
            menuDifficulty.DropDownItems.Add(easyLevelItem);
            menuDifficulty.DropDownItems.Add(hardLevelItem);

            menuStrip.Items.Add(menuGame);
            menuStrip.Items.Add(menuFirstMove);
            menuStrip.Items.Add(menuDifficulty);

            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }

        public void ResetBoard()
        {
            foreach (Button button in buttons)
            {
                button.BackgroundImage = null;
                button.Tag = null;
            }
        }

        public void SetButtonImage(int x, int y, string player, string imagePath)
        {
            buttons[x, y].BackgroundImage = Image.FromFile(imagePath);
            buttons[x, y].BackgroundImageLayout = ImageLayout.Stretch;
            buttons[x, y].Tag = player;
        }

        public void UpdateTurnIndicator()
        {
            this.Text = "Крестики нолики";
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void SetMenuOptions(bool isHardMode, bool isComputerFirst){}
    }
}
using Game_Center.Projects.Tic_Tac_Toe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Center.Projects.Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for TicTacToe.xaml
    /// </summary>
    public partial class TicTacToe : Window
    {
        private TicTacToeModel _gameScene;
        public TicTacToe()
        {
            InitializeComponent();
            _gameScene = new TicTacToeModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button != null && string.IsNullOrEmpty(button.Content as string))
            {
                button.Content = _gameScene.CurrentPlayer.ToString();
                int row = Grid.GetRow(button);
                int column = Grid.GetColumn(button);
                _gameScene.GameBoard[row, column] = _gameScene.CurrentPlayer;

                if (_gameScene.CheckForWin())
                {
                    MessageBox.Show($"{_gameScene.CurrentPlayer} wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetGame();
                }
                else
                {
                    if (_gameScene.IsBoardFull())
                    {
                        MessageBox.Show("It's a draw!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetGame();
                    }
                    else
                    {
                        _gameScene.CurrentPlayer = _gameScene.CurrentPlayer == 'X' ? 'O' : 'X';
                    }
                }
            }
        }
        private void ResetGame()
        {
            _gameScene.CurrentPlayer = 'X';
            _gameScene.GameBoard = new char[3, 3];

            // Clear the game board
            foreach (Button button in MainGrid.Children)
            {
                button.Content = "";
            }
        }
    }
}

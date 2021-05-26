using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Summative___Connect_3_and_Connect_4
{
    public partial class Form1 : Form
    {
        private Rectangle[] boardColumns;
        private int[,] board;
        private int turn;
        WindowsMediaPlayer music = new WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
            boardColumns = new Rectangle[7];
            board = new int[6, 7];
            turn = 1;
            music.URL = "Burn.M4A";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 24, 24, 340, 300);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        boardColumns[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int columnIndex = ColumnNumber(e.Location);
            if (columnIndex != -1)
            {
                int rowIndex = EmptyRow(columnIndex);
                if (rowIndex != -1)
                {
                    board[rowIndex, columnIndex] = turn;
                    if (turn == 1)
                    {
                        Graphics g = CreateGraphics();
                        g.FillEllipse(Brushes.Red, 32 + 48 * columnIndex, 32 + 48 * rowIndex, 32, 32);
                    }
                    else if (turn == 2)
                    {
                        Graphics g = CreateGraphics();
                        g.FillEllipse(Brushes.Yellow, 32 + 48 * columnIndex, 32 + 48 * rowIndex, 32, 32);
                    }
                    int winner = WinnerPlayer(turn);
                    if (winner != -1)
                    {
                        if (turn == 1)
                        {
                            if (textBox1.Text == "")
                            {
                                MessageBox.Show("Congratulations! " + " Red Wins");
                            }
                            else
                            {
                                MessageBox.Show("Congratulations! " + textBox1.Text + " Wins");
                            }
                        }
                        else if (turn == 2)
                        {
                            if (textBox2.Text == "")
                            {
                                MessageBox.Show("Congratulations! " + " Yellow Wins");
                            }
                            else
                            {
                                MessageBox.Show("Congratulations! " + textBox2.Text + " Wins");
                            }
                        }
                        Application.Restart();
                    }
                    if (turn == 1)
                    {
                        turn = 2;
                    }
                    else
                    {
                        turn = 1;
                    }
                }
            }
        }
        private int WinnerPlayer(int playerToCheck)
        {
            // vertical win check
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (AllNumbersEqual(playerToCheck, board[row, column], board[row + 1, column], board[row + 2, column], board[row + 3, column]))
                    {
                        return playerToCheck;
                    }
                }
            }
            // horizontal win check
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1) - 3; column++)
                {
                    if (AllNumbersEqual(playerToCheck, board[row, column], board[row, column + 1], board[row, column + 2], board[row, column + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            // top left diagonal win check (\)
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int column = 0; column < board.GetLength(1) - 3; column++)
                {
                    if (AllNumbersEqual(playerToCheck, board[row, column], board[row + 1, column + 1], board[row + 2, column + 2], board[row + 3, column + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            // top right diagonal win check (/)
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int column = 3; column < board.GetLength(1); column++)
                {
                    if (AllNumbersEqual(playerToCheck, board[row, column], board[row + 1, column - 1], board[row + 2, column - 2], board[row + 3, column - 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            return -1;
        }
        private bool AllNumbersEqual(int toCheck, params int[] numbers)
        {
            foreach (int num in numbers)
            {
                if (num != toCheck)
                {
                    return false;
                }
            }
            return true;
        }
        private int ColumnNumber(Point mouse)
        {
            for (int i = 0; i < boardColumns.Length; i++)
            {
                if ((mouse.X >= boardColumns[i].X) && (mouse.Y >= boardColumns[i].Y))
                {
                    if ((mouse.X <= boardColumns[i].X + boardColumns[i].Width) && (mouse.Y <= boardColumns[i].Y + boardColumns[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int EmptyRow(int col)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (board[i, col] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void PlayConnect3Button_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Hide();
            Form Connect3 = new Form2();
            Connect3.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            music.controls.play();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace B21_Ex05_ReverseTicTacToe
{
    public delegate void PlayerMadeMoveDelegate(int i_RowIndex, int i_ColumnIndex);

    public delegate void MessageBoxResultDelegate(DialogResult i_DialogResult);

    public partial class BoardForm : Form
    {
        public event MessageBoxResultDelegate ResultFromMessageBox; 

        public event PlayerMadeMoveDelegate FillCell;

        private Label m_Player1NameAndScore;
        private Label m_Player2NameAndScore;
        private const int k_VerticalPadding = 65;
        private const int k_HorizontalPadding = 20;
        private const int k_ButtonWidthAndHeight = 50;
        private const int k_PaddingBetweenButtons = 10;
        private Button[,] m_ButtonBoard;

        public BoardForm(int i_Dimension, string i_Player1Name, string i_Player2Name, int i_ScorePlayer1, int i_ScorePlayer2)
        {
            InitializeComponent(i_Dimension, i_Player1Name, i_Player2Name, i_ScorePlayer1, i_ScorePlayer2);
            GameManager.TextChanged += gameManagerTextChanged;
            GameManager.TurnStarted += gameManagerTurnStarted;
            GameManager.GameOver += gameManagerGameOver;
        }

        private void gameManagerGameOver(GameManager.eGameStatus i_GameStatus, string i_WinnerName)
        {
            string bodyMessage = string.Empty;
            DialogResult dialogResult;
            if (i_GameStatus.Equals(GameManager.eGameStatus.Win))
            {
                bodyMessage = string.Format("{0} Won!!{1} Another Round?", i_WinnerName, Environment.NewLine);
            }
            else 
            {
                bodyMessage = string.Format("Tie !! {0} Another Round?", Environment.NewLine);
            }

            GameManager.GameOver -= gameManagerGameOver;
            GameManager.TextChanged -= gameManagerTextChanged;
            GameManager.TurnStarted -= gameManagerTurnStarted;
            dialogResult = MessageBox.Show(bodyMessage, "A " + i_GameStatus + "!", MessageBoxButtons.YesNo,
            MessageBoxIcon.Information);
            if (ResultFromMessageBox != null)
            {
                ResultFromMessageBox.Invoke(dialogResult);
            }
        }

        private void gameManagerTurnStarted(GameManager.eTurn i_CurrentPlayer)
        {
            if (i_CurrentPlayer.Equals(GameManager.eTurn.Player1))
            {
                m_Player1NameAndScore.Font = new Font(m_Player1NameAndScore.Font, FontStyle.Bold);
                m_Player2NameAndScore.Font = new Font(m_Player2NameAndScore.Font, FontStyle.Regular);
            }
            else 
            {
                m_Player2NameAndScore.Font = new Font(m_Player2NameAndScore.Font, FontStyle.Bold);
                m_Player1NameAndScore.Font = new Font(m_Player1NameAndScore.Font, FontStyle.Regular);
            }
        }

        private void gameManagerTextChanged(string i_TextInCell, int i_Row, int i_Column)
        {
            m_ButtonBoard[i_Row, i_Column].Text = i_TextInCell;
            m_ButtonBoard[i_Row, i_Column].Enabled = false;
        }

        private void InitializeComponent(int i_Dimension, string i_Player1Name, string i_Player2Name, int i_ScorePlayer1, int i_ScorePlayer2)
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            int formWidth = (i_Dimension * k_ButtonWidthAndHeight) + (k_PaddingBetweenButtons * (i_Dimension + 1));
            this.ClientSize = new System.Drawing.Size(formWidth, formWidth + 30);
            this.Text = "Reversed Tic Tac Toe";
            createButtunBoard(i_Dimension);
            setAndCenterLabels(i_Player1Name, i_Player2Name, i_ScorePlayer1, i_ScorePlayer2);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonBoard_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            string[] indices = currentButton.Name.Split(' ');
            int row = int.Parse(indices[0]);
            int column = int.Parse(indices[1]);
            if (FillCell != null)
            {
                FillCell.Invoke(row, column);
            }
        }

        private void createButtunBoard(int i_Dimension)
        {
            m_ButtonBoard = new Button[i_Dimension, i_Dimension];
            for (int i = 0; i < i_Dimension; i++)
            {
                for (int j = 0; j < i_Dimension; j++)
                {
                    m_ButtonBoard[i, j] = new Button();
                    int currentButtonLocationX = i * k_ButtonWidthAndHeight + (i + 1) * k_PaddingBetweenButtons;
                    int currentButtonLocationY = j * k_ButtonWidthAndHeight + (j + 1) * k_PaddingBetweenButtons;
                    m_ButtonBoard[i, j].Location = new Point(currentButtonLocationX, currentButtonLocationY);
                    m_ButtonBoard[i, j].Size = new Size(k_ButtonWidthAndHeight, k_ButtonWidthAndHeight); 
                    m_ButtonBoard[i, j].Text = string.Empty;
                    m_ButtonBoard[i, j].Name = String.Format("{0} {1}", i, j);
                    this.Controls.Add(m_ButtonBoard[i, j]);
                    m_ButtonBoard[i, j].Click += buttonBoard_Click;
                }
            }
        }

        private void setAndCenterLabels(string i_Player1Name, string i_Player2Name, int i_ScorePlayer1, int i_ScorePlayer2)
        {
            m_Player1NameAndScore = new Label();
            m_Player2NameAndScore = new Label();
            m_Player1NameAndScore.Text = String.Format("{0}: {1}", i_Player1Name, i_ScorePlayer1);
            m_Player2NameAndScore.Text = String.Format("{0}: {1}", i_Player2Name, i_ScorePlayer2);
            m_Player1NameAndScore.AutoSize = false;
            m_Player2NameAndScore.AutoSize = true;
            m_Player1NameAndScore.TextAlign = ContentAlignment.TopRight;
            m_Player1NameAndScore.Font = new Font(m_Player1NameAndScore.Font, FontStyle.Bold);
            int totalLabelWidth = m_Player1NameAndScore.Width + m_Player2NameAndScore.Width;
            int player1LabelPositionX = (Width - totalLabelWidth) / 2 - k_HorizontalPadding;
            int player2LabelPositionX = m_Player1NameAndScore.Left + m_Player1NameAndScore.Width + k_HorizontalPadding;
            int playersLabelPositionY = Height - k_VerticalPadding;
            m_Player1NameAndScore.Location = new Point(player1LabelPositionX, playersLabelPositionY);
            m_Player2NameAndScore.Location = new Point(player2LabelPositionX, playersLabelPositionY);
            Controls.Add(m_Player1NameAndScore);
            Controls.Add(m_Player2NameAndScore);
        }
    }
}

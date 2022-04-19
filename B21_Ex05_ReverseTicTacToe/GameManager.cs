using System;
using System.Windows.Forms;

namespace B21_Ex05_ReverseTicTacToe
{
    public delegate void StartPlayerTurnDelegate(GameManager.eTurn i_CurrentPlayer);

    public delegate void TextInCellSetterDelegate(string i_TextInCell, int i_Row, int i_Column);

    public delegate void GameOverStatusDelegate(GameManager.eGameStatus i_GameStatus, string i_WinnerName);

    public class GameManager
    {
        public enum eGameMode
        {
            PlayerVsPlayer = 'P',
            PlayerVsComputer = 'C'
        }

        public enum eGameStatus
        {
            Playing,
            Win,
            Tie
        }

        public enum eTurn
        {
            Player1,
            Player2,
            Computer
        }

        public static event StartPlayerTurnDelegate TurnStarted;

        public static event TextInCellSetterDelegate TextChanged;

        public static event GameOverStatusDelegate GameOver;

        private eGameMode m_GameMode;
        private Board m_Board;
        private string m_Player1Name;
        private string m_Player2Name;
        private int m_BoardDimension;
        private eTurn m_CurrentTurn = eTurn.Player1;
        private Board.eCellType m_CurrentPlayerCellType = Board.eCellType.X;
        private int m_Player1Score = 0;
        private int m_Player2OrComputerScore = 0;
        private eGameStatus m_CurrentGameStatus = eGameStatus.Playing;
        private BoardForm m_BoardForm; 

        public GameManager()
        {

        }

        public void GameInitializer()
        {
            SettingsForm settingForms = new SettingsForm();
            settingForms.StartGame += settingForms_StartGame;
            settingForms.ShowDialog();
        }

        private void settingForms_StartGame(string i_PlayerOneName, string i_PlayerTwoName, int i_BoardDimension,
            bool i_IsTwoPlayerMode)
        {
            m_Player1Name = i_PlayerOneName;
            m_Player2Name = i_PlayerTwoName;
            m_BoardDimension = i_BoardDimension;
            if (i_IsTwoPlayerMode)
            {
                m_GameMode = eGameMode.PlayerVsPlayer;
            }
            else
            {
                m_GameMode = eGameMode.PlayerVsComputer;
            }
            
            m_Board = new Board(m_BoardDimension);
            start();
        }

        private void start()
        {
            m_BoardForm = new BoardForm(m_BoardDimension, m_Player1Name, m_Player2Name, m_Player1Score,
                m_Player2OrComputerScore);
            m_CurrentTurn = eTurn.Player1;
            m_CurrentPlayerCellType = Board.eCellType.X;
            m_BoardForm.FillCell += BoardFormFillCell;
            m_BoardForm.ResultFromMessageBox += HandleDialogResult;
            m_BoardForm.ShowDialog();
        }

        private void BoardFormFillCell(int i_RowIndex, int i_ColumnIndex)
        {
            if (TextChanged != null)
            {
                TextChanged.Invoke(m_CurrentPlayerCellType.ToString(), i_RowIndex, i_ColumnIndex);
            }
            
            setAndPrepareBoard(i_RowIndex, i_ColumnIndex, m_CurrentPlayerCellType);
            updatesIfGameOver();
            if (m_CurrentTurn.Equals(eTurn.Computer))
            {
                makeComputerTurn();
            }
        }

        private void updatesIfGameOver()
        {
            if (CheckIfSomeoneWon() || IsBoardFull())
            {
                Tie();
                if (CheckIfSomeoneWon())
                {
                    m_CurrentGameStatus = eGameStatus.Win;
                }

                PresentScoreAndAskForAnotherRound();
            }
        }

        private void makeComputerTurn()
        {
            Random rand = new Random();
            int row = rand.Next(m_BoardDimension);
            int col = rand.Next(m_BoardDimension);
            while (!m_Board.IsCellAvailable(row, col))
            { 
                row = rand.Next(m_BoardDimension);
                col = rand.Next(m_BoardDimension);
            }
            
            if (TextChanged != null)
            {
                TextChanged.Invoke(m_CurrentPlayerCellType.ToString(), row, col);
            }

            setAndPrepareBoard(row,col, m_CurrentPlayerCellType);
            updatesIfGameOver();
        }

        public void AdjustScore()
        {
            if (m_CurrentGameStatus.Equals(eGameStatus.Win))
            {
                if (m_CurrentTurn.Equals(eTurn.Player1))
                {
                    m_Player1Score++;
                }
                else
                {
                    m_Player2OrComputerScore++;
                }
            }
        }

        private void setAndPrepareBoard(int i_Row, int i_Column, Board.eCellType i_CurrentCellType)
        {
            m_Board.SetCellValue(i_Row, i_Column, i_CurrentCellType);
            prepareNextTurn();
        }

        private void prepareNextTurn()
        {
            if (m_CurrentTurn.Equals(eTurn.Player1))
            {
                switch (m_GameMode)
                {
                    case eGameMode.PlayerVsComputer:
                        m_CurrentTurn = eTurn.Computer;
                        break;

                    case eGameMode.PlayerVsPlayer:
                        m_CurrentTurn = eTurn.Player2;
                        break;
                }

                m_CurrentPlayerCellType = Board.eCellType.O;
            }
            else
            {
                m_CurrentTurn = eTurn.Player1;
                m_CurrentPlayerCellType = Board.eCellType.X;
            }

            if(TurnStarted != null)
            {
                TurnStarted.Invoke(m_CurrentTurn);
            }
        }

        public bool IsBoardFull()
        {

            return m_Board.IsBoardFull();
        }

        public bool CheckIfSomeoneWon()
        {

            return m_Board.CheckIfLostDiagonally() || m_Board.CheckIfLostInColumn() || m_Board.CheckIfLostInRow();
        }

        public void PresentScoreAndAskForAnotherRound()
        {
            
            string winnerName = m_Player2Name;
            if (m_CurrentTurn.Equals(eTurn.Player1))
            { 
                winnerName = m_Player1Name;
            }

            if (GameOver != null)
            {
                GameOver.Invoke(m_CurrentGameStatus, winnerName);
            }

        }

        private void HandleDialogResult(DialogResult dialogResult)
        {
            m_BoardForm.Hide();
            if (dialogResult == DialogResult.Yes)
            {
                m_Board.CleanBoard();
                AdjustScore();
                m_BoardForm.FillCell -= BoardFormFillCell;
                m_BoardForm.ResultFromMessageBox -= HandleDialogResult;
                start();
            }
            else
            {
                Application.Exit();
            }
        }

        public void Tie()
        {
            m_CurrentGameStatus = eGameStatus.Tie;
        }
    }
}



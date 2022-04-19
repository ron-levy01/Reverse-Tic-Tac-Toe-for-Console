namespace B21_Ex05_ReverseTicTacToe
{
    public class Board
    {
        public enum eCellType
        {
            Empty = ' ',
            X = 'X',
            O = 'O'
        }

        private int m_Dimension;
        private eCellType[,] m_Board;
        private int m_EmptyCellsCounter;

        public Board(int i_Dimesion)
        {
            m_Dimension = i_Dimesion;
            m_Board = new eCellType[m_Dimension, m_Dimension];
            makeBoardEmpty();
            m_EmptyCellsCounter = m_Dimension * m_Dimension;
        }

        private void makeBoardEmpty()
        {
            for (int i = 0; i < m_Dimension; i++)
            {
                for (int j = 0; j < m_Dimension; j++)
                {
                    m_Board[i, j] = eCellType.Empty;
                }
            }
        }

        public void SetCellValue(int i_Row, int i_Column, eCellType i_value)
        {
            m_Board[i_Row, i_Column] = i_value;
            m_EmptyCellsCounter--;
        }

        public bool IsCellAvailable(int i_Row, int i_Column)
        {

            return m_Board[i_Row, i_Column].Equals(eCellType.Empty);
        }

        public int Dimension
        {
            get
            {
                
                return m_Dimension;
            }
        }

        public bool IsBoardFull()
        {
            
            return m_EmptyCellsCounter == 0;
        }

        public bool CheckIfLostDiagonally()
        {
            bool mainDiagonalFullOfO = SameTypeCounterInMainDiagonally(eCellType.O) == m_Dimension;
            bool mainDiagonalFullOfX = SameTypeCounterInMainDiagonally(eCellType.X) == m_Dimension;
            bool secondDiagonalFullOfO = SameTypeCounterInSecondDiagonally(eCellType.O) == m_Dimension;
            bool secondDiagonalFullOfX = SameTypeCounterInSecondDiagonally(eCellType.X) == m_Dimension;

            return mainDiagonalFullOfO || mainDiagonalFullOfX || secondDiagonalFullOfO || secondDiagonalFullOfX;
        }

        public bool CheckIfLostInRow()
        {
            bool foundFullRow = false;

            for (int i = 0; i < m_Dimension; i++)
            {
                foundFullRow = isRowFull(i);
                if (foundFullRow)
                {
                    break;
                }
            }

            return foundFullRow;
        }

        public bool CheckIfLostInColumn()
        {
            bool foundFullColumn = false;

            for (int i = 0; i < m_Dimension; i++)
            {
                foundFullColumn = isColumnFull(i);
                if (foundFullColumn)
                {
                    break;
                }
            }

            return foundFullColumn;
        }

        private bool isRowFull(int i_row)
        {
            bool isFullOfO = SameTypeCounterInRow(i_row, eCellType.O) == m_Dimension;
            bool isFullOfX = SameTypeCounterInRow(i_row, eCellType.X) == m_Dimension;

            return isFullOfO || isFullOfX;
        }

        private bool isColumnFull(int i_Column)
        {
            bool isFullOfO = SameTypeCounterInColumn(i_Column, eCellType.O) == m_Dimension;
            bool isFullOfX = SameTypeCounterInColumn(i_Column, eCellType.X) == m_Dimension;

            return isFullOfO || isFullOfX;
        }

        public void CleanBoard()
        {
            m_Board = new eCellType[m_Dimension, m_Dimension];
            m_EmptyCellsCounter = m_Dimension * m_Dimension;
            makeBoardEmpty();

        }

        public int SameTypeCounterInRow(int i_Row, eCellType i_CellType)
        {
            int sameTypeCounterInRow = 0;

            for (int i = 0; i < m_Dimension; i++)
            {
                if (m_Board[i_Row, i].Equals(i_CellType))
                {
                    sameTypeCounterInRow++;
                }
            }

            return sameTypeCounterInRow;
        }

        public int SameTypeCounterInColumn(int i_Column, eCellType i_CellType)
        {
            int sameTypeCounterInColumn = 0;

            for (int i = 0; i < m_Dimension; i++)
            {
                if (m_Board[i, i_Column].Equals(i_CellType))
                {
                    sameTypeCounterInColumn++;
                }
            }

            return sameTypeCounterInColumn;
        }

        public int SameTypeCounterInMainDiagonally(eCellType i_CellType)
        {
            int sameTypeCounterInMainDiagonally = 0;

            for (int i = 0; i < m_Dimension; i++)
            {
                if (m_Board[i, i].Equals(i_CellType))
                {
                    sameTypeCounterInMainDiagonally++;
                }
            }

            return sameTypeCounterInMainDiagonally;
        }

        public int SameTypeCounterInSecondDiagonally(eCellType i_CellType)
        {
            int sameTypeCounterInSecondDiagonally = 0;

            for (int i = 0; i < m_Dimension; i++)
            {
                if (m_Board[i, m_Dimension - i - 1].Equals(i_CellType))
                {
                    sameTypeCounterInSecondDiagonally++;
                }
            }

            return sameTypeCounterInSecondDiagonally;
        }
    }
}

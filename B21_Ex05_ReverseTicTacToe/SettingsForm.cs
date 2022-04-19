using System;
using System.Windows.Forms;

namespace B21_Ex05_ReverseTicTacToe
{ 
    public delegate void GameSettingDelegate(string i_PlayerOneName, string i_PlayerTwoName, int i_BoardDimension , bool i_IsTwoPlayerMode);

    public class SettingsForm : Form
    {
        public event GameSettingDelegate StartGame;
        private TextBox m_Player1TextBox;
        private TextBox m_Player2TextBox;
        private Label m_PlayersLabel;
        private Label m_BoardSizeLabel;
        private CheckBox m_Player2CheckBox;
        private NumericUpDown m_NumericUpDownRows;
        private Label m_RowsLabel;
        private NumericUpDown m_NumericUpDownCols;
        private Label m_ColsLabel;
        private Label m_Player1Label;
        private Button m_StartButton;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_Player1TextBox = new System.Windows.Forms.TextBox();
            this.m_Player2TextBox = new System.Windows.Forms.TextBox();
            this.m_PlayersLabel = new System.Windows.Forms.Label();
            this.m_BoardSizeLabel = new System.Windows.Forms.Label();
            this.m_Player2CheckBox = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.m_RowsLabel = new System.Windows.Forms.Label();
            this.m_NumericUpDownCols = new System.Windows.Forms.NumericUpDown();
            this.m_ColsLabel = new System.Windows.Forms.Label();
            this.m_StartButton = new System.Windows.Forms.Button();
            this.m_Player1Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // player1TextBox
            // 
            this.m_Player1TextBox.Location = new System.Drawing.Point(118, 49);
            this.m_Player1TextBox.Name = "m_Player1TextBox";
            this.m_Player1TextBox.Size = new System.Drawing.Size(108, 22);
            this.m_Player1TextBox.TabIndex = 0;
            // 
            // player2TextBox
            // 
            this.m_Player2TextBox.Enabled = false;
            this.m_Player2TextBox.Location = new System.Drawing.Point(118, 81);
            this.m_Player2TextBox.Name = "m_Player2TextBox";
            this.m_Player2TextBox.Size = new System.Drawing.Size(108, 22);
            this.m_Player2TextBox.TabIndex = 1;
            this.m_Player2TextBox.Text = "[Computer]";
            // 
            // playersLabel
            // 
            this.m_PlayersLabel.AutoSize = true;
            this.m_PlayersLabel.Location = new System.Drawing.Point(12, 18);
            this.m_PlayersLabel.Name = "m_PlayersLabel";
            this.m_PlayersLabel.Size = new System.Drawing.Size(59, 17);
            this.m_PlayersLabel.TabIndex = 3;
            this.m_PlayersLabel.Text = "Players:";
            // 
            // boardSizeLabel
            // 
            this.m_BoardSizeLabel.AutoSize = true;
            this.m_BoardSizeLabel.Location = new System.Drawing.Point(12, 120);
            this.m_BoardSizeLabel.Name = "m_BoardSizeLabel";
            this.m_BoardSizeLabel.Size = new System.Drawing.Size(81, 17);
            this.m_BoardSizeLabel.TabIndex = 5;
            this.m_BoardSizeLabel.Text = "Board Size:";
            // 
            // player2CheckBox
            // 
            this.m_Player2CheckBox.AutoSize = true;
            this.m_Player2CheckBox.Location = new System.Drawing.Point(28, 81);
            this.m_Player2CheckBox.Name = "m_Player2CheckBox";
            this.m_Player2CheckBox.Size = new System.Drawing.Size(86, 21);
            this.m_Player2CheckBox.TabIndex = 6;
            this.m_Player2CheckBox.Text = "Player 2:";
            this.m_Player2CheckBox.UseVisualStyleBackColor = true;
            this.m_Player2CheckBox.CheckedChanged += new System.EventHandler(this.player2CheckBox_CheckedChanged);
            // 
            // numericUpDownRows
            // 
            this.m_NumericUpDownRows.Location = new System.Drawing.Point(77, 153);
            this.m_NumericUpDownRows.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.m_NumericUpDownRows.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_NumericUpDownRows.Name = "m_NumericUpDownRows";
            this.m_NumericUpDownRows.Size = new System.Drawing.Size(37, 22);
            this.m_NumericUpDownRows.TabIndex = 7;
            this.m_NumericUpDownRows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_NumericUpDownRows.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // rowsLabel
            // 
            this.m_RowsLabel.AutoSize = true;
            this.m_RowsLabel.Location = new System.Drawing.Point(25, 155);
            this.m_RowsLabel.Name = "m_RowsLabel";
            this.m_RowsLabel.Size = new System.Drawing.Size(46, 17);
            this.m_RowsLabel.TabIndex = 8;
            this.m_RowsLabel.Text = "Rows:";
            // 
            // numericUpDownCols
            // 
            this.m_NumericUpDownCols.Location = new System.Drawing.Point(189, 153);
            this.m_NumericUpDownCols.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.m_NumericUpDownCols.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_NumericUpDownCols.Name = "m_NumericUpDownCols";
            this.m_NumericUpDownCols.Size = new System.Drawing.Size(37, 22);
            this.m_NumericUpDownCols.TabIndex = 9;
            this.m_NumericUpDownCols.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_NumericUpDownCols.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // colsLabel
            // 
            this.m_ColsLabel.AutoSize = true;
            this.m_ColsLabel.Location = new System.Drawing.Point(140, 155);
            this.m_ColsLabel.Name = "m_ColsLabel";
            this.m_ColsLabel.Size = new System.Drawing.Size(43, 17);
            this.m_ColsLabel.TabIndex = 10;
            this.m_ColsLabel.Text = "Cols: ";
            // 
            // startButton
            // 
            this.m_StartButton.Location = new System.Drawing.Point(15, 188);
            this.m_StartButton.Name = "m_StartButton";
            this.m_StartButton.Size = new System.Drawing.Size(211, 23);
            this.m_StartButton.TabIndex = 11;
            this.m_StartButton.Text = "Start!";
            this.m_StartButton.UseVisualStyleBackColor = true;
            this.m_StartButton.Click += startButton_Click;
            // 
            // player1Label
            // 
            this.m_Player1Label.AutoSize = true;
            this.m_Player1Label.Location = new System.Drawing.Point(25, 52);
            this.m_Player1Label.Name = "m_Player1Label";
            this.m_Player1Label.Size = new System.Drawing.Size(64, 17);
            this.m_Player1Label.TabIndex = 12;
            this.m_Player1Label.Text = "Player 1:";
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(245, 223);
            this.Controls.Add(this.m_Player1Label);
            this.Controls.Add(this.m_StartButton);
            this.Controls.Add(this.m_ColsLabel);
            this.Controls.Add(this.m_NumericUpDownCols);
            this.Controls.Add(this.m_RowsLabel);
            this.Controls.Add(this.m_NumericUpDownRows);
            this.Controls.Add(this.m_Player2CheckBox);
            this.Controls.Add(this.m_BoardSizeLabel);
            this.Controls.Add(this.m_PlayersLabel);
            this.Controls.Add(this.m_Player2TextBox);
            this.Controls.Add(this.m_Player1TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string playerTwo;
            if (m_Player1TextBox.Text != string.Empty && m_Player2TextBox.Text != string.Empty)
            {
                playerTwo = m_Player2CheckBox.Checked ? m_Player2TextBox.Text : "Computer";
                Hide();
                OnStartGame(playerTwo);
            }
        }

        private void player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                this.m_Player2TextBox.Enabled = true;
                this.m_Player2TextBox.Text = string.Empty;
            }
            else
            {
                this.m_Player2TextBox.Enabled = false;
                this.m_Player2TextBox.Text = "[Computer]";
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int boardSize = (int)(sender as NumericUpDown).Value;
            this.m_NumericUpDownRows.Value = boardSize;
            this.m_NumericUpDownCols.Value = boardSize;
        }

        public void OnStartGame(string i_PlayerTwoName)
        {
            if (StartGame != null)
            {
                StartGame.Invoke(m_Player1TextBox.Text, i_PlayerTwoName, (int)m_NumericUpDownRows.Value, m_Player2CheckBox.Checked);
            }
        }
    }
}

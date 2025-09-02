using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.BullsAndCows
{
    public class FormMainGame : Form
    {
        public event Action<eGameStatus, int> GameEnded;
        public event Action<List<eGuessOption>, List<eGuessUnitState>, int> GuessSubmitted;

        //Data Members
        private const int k_LengthOfGuess = 4;
        private GameLogic m_GameLogic;
        private Button[] m_SecretButtons;
        private Button[,] m_GuessButtons;
        private Button[] m_SubmitButtons;
        private Button[,] m_ResultButtons;
        private List<eGuessOption> m_ComputerSequence;
        private List<eGuessOption>[] m_UserGuesses;
        private int m_MaxGuesses;
        private int m_CurrentGuess;

        public FormMainGame(int i_MaxGuesses)
        {
            m_MaxGuesses = i_MaxGuesses;
            m_GameLogic = new GameLogic();
            m_ComputerSequence = m_GameLogic.GenerateComputerInput();
            m_CurrentGuess = 0;
            initializeUserGuesses();
            GameEnded += submitButton_Click;
            GuessSubmitted += formMainGame_GuessSubmitted;
            initializeComponent();
            createSecretButtons();
            createGuessAndResultRows();
        }

        private void initializeUserGuesses()
        {
            m_UserGuesses = new List<eGuessOption>[m_MaxGuesses];
            for (int i = 0; i < m_MaxGuesses; i++)
            {
                m_UserGuesses[i] = new List<eGuessOption>();
                for (int j = 0; j < k_LengthOfGuess; j++)
                {
                    m_UserGuesses[i].Add(eGuessOption.Purple);
                }
            }
        }

        private void initializeComponent()
        {
            this.Text = "Bool Pgia";
            this.Size = new Size(350, 50 + (m_MaxGuesses + 1) * 45);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void createSecretButtons()
        {
            m_SecretButtons = new Button[k_LengthOfGuess];
            for (int i = 0; i < k_LengthOfGuess; i++)
            {
                m_SecretButtons[i] = createButton(40, 40, new Point(10 + i * 45, 10), Color.Black, false, null);
                this.Controls.Add(m_SecretButtons[i]);
            }
        }

        private void createGuessAndResultRows()
        {
            m_GuessButtons = new Button[m_MaxGuesses, k_LengthOfGuess];
            m_SubmitButtons = new Button[m_MaxGuesses];
            m_ResultButtons = new Button[m_MaxGuesses, k_LengthOfGuess];
            for (int row = 0; row < m_MaxGuesses; row++)
            {
                for (int col = 0; col < k_LengthOfGuess; col++)
                {
                    m_GuessButtons[row, col] = createButton(40, 40, new Point(10 + col * 45, 60 + row * 45), Color.LightGray, row == 0, guessButton_Click);
                    m_GuessButtons[row, col].Tag = new Point(row, col);
                    this.Controls.Add(m_GuessButtons[row, col]);
                }

                m_SubmitButtons[row] = createButton(30, 40, new Point(200, 60 + row * 45), SystemColors.Control, false, submitButton_Click);
                m_SubmitButtons[row].Text = "-->";
                m_SubmitButtons[row].Tag = row;
                this.Controls.Add(m_SubmitButtons[row]);
                for (int col = 0; col < k_LengthOfGuess; col++)
                {
                    m_ResultButtons[row, col] = createButton(15, 15, new Point(240 + (col % 2) * 20, 65 + row * 45 + (col / 2) * 20), Color.LightGray, false, null);
                    this.Controls.Add(m_ResultButtons[row, col]);
                }
            }
        }

        private Button createButton(int i_Width, int i_Height, Point i_Location, Color i_Color, bool i_Enabled, EventHandler onClick)
        {
            Button button = new Button();
            button.Size = new Size(i_Width, i_Height);
            button.Location = i_Location;
            button.BackColor = i_Color;
            button.Enabled = i_Enabled;
            if (onClick != null)
            {
                button.Click += onClick;
            }

            return button;
        }

        private void guessButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;
            int row = position.X;
            int col = position.Y;
            List<eGuessOption> usedColorsList = new List<eGuessOption>();
            eGuessOption[] usedColors;
            FormColorPicker colorPicker;
            eGuessOption selectedColor;

            for (int i = 0; i < k_LengthOfGuess; i++)
            {
                if (i != col && m_GuessButtons[row, i].BackColor != Color.LightGray)
                {
                    usedColorsList.Add(m_UserGuesses[row][i]);
                }
            }

            usedColors = usedColorsList.ToArray();
            colorPicker = new FormColorPicker(usedColors);
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorPicker.SelectedColor;
                clickedButton.BackColor = ColorManager.GetSystemColor(selectedColor);
                m_UserGuesses[row][col] = selectedColor;
                updateSubmitButtonState(row);
            }

            colorPicker.Dispose();
        }

        private void updateSubmitButtonState(int i_RowIndex)
        {
            if (isRowComplete(i_RowIndex))
            {
                m_SubmitButtons[i_RowIndex].Enabled = true;
            }
            else
            {
                m_SubmitButtons[i_RowIndex].Enabled = false;
            }
        }

        private bool isRowComplete(int i_RowIndex)
        {
            bool isRowComplete = true;

            for (int i = 0; i < k_LengthOfGuess; i++)
            {
                if (m_GuessButtons[i_RowIndex, i].BackColor == Color.LightGray)
                {
                    isRowComplete = false;
                    break;
                }
            }

            return isRowComplete;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Button submitButton = sender as Button;
            int row = (int)submitButton.Tag;
            List<eGuessOption> guess = m_UserGuesses[row];
            List<eGuessUnitState> feedback;
            eGameStatus gameStatus;

            feedback = m_GameLogic.ProcessUserGuess(guess, m_ComputerSequence);
            m_CurrentGuess++;
            formMainGame_GuessSubmitted(guess, feedback, m_CurrentGuess);
            gameStatus = m_GameLogic.CheckIfWonOrLost(feedback, m_CurrentGuess, m_MaxGuesses);
            if (gameStatus != eGameStatus.Pending)
            {
                submitButton_Click(gameStatus, m_CurrentGuess);
            }
        }

        private void displayFeedback(int i_RowIndex, List<eGuessUnitState> i_FeedbackForUserGuess)
        {
            int feedbackIndex = 0;

            for (int i = 0; i < i_FeedbackForUserGuess.Count; i++)
            {
                if (i_FeedbackForUserGuess[i] == eGuessUnitState.Bull && feedbackIndex < k_LengthOfGuess)
                {
                    m_ResultButtons[i_RowIndex, feedbackIndex].BackColor = Color.Black;
                }
                else if (i_FeedbackForUserGuess[i] == eGuessUnitState.Cow && feedbackIndex < k_LengthOfGuess)
                {
                    m_ResultButtons[i_RowIndex, feedbackIndex].BackColor = Color.Yellow;
                }

                feedbackIndex++;
            }
        }

        private void setRowEnabled(int i_RowIndex, bool i_isEnabled)
        {
            for (int col = 0; col < k_LengthOfGuess; col++)
            {
                m_GuessButtons[i_RowIndex, col].Enabled = i_isEnabled;
            }
        }

        private void endGame(eGameStatus i_GameStatus)
        {
            string message;

            revealSecretSequence();
            for (int row = 0; row < m_MaxGuesses; row++)
            {
                setRowEnabled(row, false);
                m_SubmitButtons[row].Enabled = false;
            }

            if (i_GameStatus == eGameStatus.Win)
            {
                message = String.Format("You won! You guessed the code in {0} guesses", m_CurrentGuess);
            }
            else
            {
                message = String.Format("You lost! You reached the maximum number of guesses: {0}", m_MaxGuesses);
            }

            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void revealSecretSequence()
        {
            for (int i = 0; i < k_LengthOfGuess; i++)
            {
                m_SecretButtons[i].BackColor = ColorManager.GetSystemColor(m_ComputerSequence[i]);
            }
        }

        private void formMainGame_GuessSubmitted(List<eGuessOption> i_UserGuess, List<eGuessUnitState> i_Feedback, int i_GuessNumber)
        {
            int currentRow = i_GuessNumber - 1;

            displayFeedback(currentRow, i_Feedback);
            setRowEnabled(currentRow, false);
            m_SubmitButtons[currentRow].Enabled = false;
            if (i_GuessNumber < m_MaxGuesses)
            {
                setRowEnabled(i_GuessNumber, true);
            }
        }

        private void submitButton_Click(eGameStatus gameStatus, int guessCount)
        {
            endGame(gameStatus);
        }
    }
}
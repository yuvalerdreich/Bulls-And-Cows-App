using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.BullsAndCows
{
    public class FormGameSettings : Form
    {
        private const int k_MinGuesses = 4;
        private const int k_MaxGuesses = 10;
        private const int k_FormWidth = 288;
        private const int k_FormHeight = 98;
        private const int k_CounterButtonWidth = 200;
        private const int k_CounterButtonHeight = 30;
        private const int k_CounterButtonX = 50;
        private const int k_CounterButtonY = 20;
        private const int k_StartButtonWidth = 100;
        private const int k_StartButtonHeight = 30;
        private const int k_StartButtonX = 94;
        private const int k_StartButtonY = 60;
        private Button m_ButtonCounter;
        private Button m_ButtonStart;
        private int m_NumOfGuesses;

        public FormGameSettings()
        {
            m_NumOfGuesses = k_MinGuesses;
            initializeComponent();
            initControls();
        }

        private void initializeComponent()
        {
            this.ClientSize = new Size(k_FormWidth, k_FormHeight);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Bool Pgia";
        }

        private void initControls()
        {
            m_ButtonCounter = new Button();
            m_ButtonCounter.Text = $"Number of chances: {m_NumOfGuesses}";
            m_ButtonCounter.Size = new Size(k_CounterButtonWidth, k_CounterButtonHeight);
            m_ButtonCounter.Location = new Point(k_CounterButtonX, k_CounterButtonY);
            m_ButtonCounter.Click += buttonCounter_Click;
            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start Game";
            m_ButtonStart.Size = new Size(k_StartButtonWidth, k_StartButtonHeight);
            m_ButtonStart.Location = new Point(k_StartButtonX, k_StartButtonY);
            m_ButtonStart.Click += buttonStart_Click;
            this.Controls.Add(m_ButtonCounter);
            this.Controls.Add(m_ButtonStart);
        }

        private void buttonCounter_Click(object sender, EventArgs e)
        {
            m_NumOfGuesses++;
            if (m_NumOfGuesses > k_MaxGuesses)
            {
                m_NumOfGuesses = k_MinGuesses;
            }

            m_ButtonCounter.Text = $"Number of chances: {m_NumOfGuesses}";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            FormMainGame mainGame = new FormMainGame(m_NumOfGuesses);

            mainGame.ShowDialog();
            mainGame.Dispose();
            this.Close();
        }
    }
}
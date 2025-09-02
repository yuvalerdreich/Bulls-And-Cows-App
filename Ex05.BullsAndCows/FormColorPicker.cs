using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.BullsAndCows
{
    public class FormColorPicker : Form
    {
        private readonly GameLogic r_GameLogic;
        private const int k_NumOfColorsPerRow = 4;
        private const int k_ColorButtonSize = 40;
        private const int k_ColorButtonSpacing = 45;
        private const int k_FormPadding = 10;
        private const int k_FormWidth = 280;
        private const int k_FormHeight = 180;
        private Button[] m_ColorButtons;
        private eGuessOption m_SelectedColor;
        private readonly eGuessOption[] r_UsedColors;

        public eGuessOption SelectedColor
        {
            get 
            { 
                return m_SelectedColor; 
            }
        }

        public FormColorPicker(eGuessOption[] i_UsedColors)
        {
            r_GameLogic = new GameLogic();  
            r_UsedColors = i_UsedColors;
            initializeComponent();
            initControls();
        }

        private void initializeComponent()
        {
            this.Text = "Pick a Color";
            this.Size = new Size(k_FormWidth, k_FormHeight);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void initControls()
        {
            eGuessOption[] allColors = ColorManager.GetAllColors();
            m_ColorButtons = new Button[allColors.Length];

            for (int i = 0; i < allColors.Length; i++)
            {
                m_ColorButtons[i] = new Button();
                m_ColorButtons[i].Size = new Size(k_ColorButtonSize, k_ColorButtonSize);
                m_ColorButtons[i].Location = new Point((i % k_NumOfColorsPerRow) * k_ColorButtonSpacing + k_FormPadding, (i / k_NumOfColorsPerRow) * k_ColorButtonSpacing + k_FormPadding);
                m_ColorButtons[i].BackColor = ColorManager.GetSystemColor(allColors[i]);
                m_ColorButtons[i].Tag = allColors[i];
                m_ColorButtons[i].Click += colorButton_Click;
                this.Controls.Add(m_ColorButtons[i]);
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            eGuessOption colorClicked = (eGuessOption)clickedButton.Tag;
            List<eGuessOption> usedColorsList = new List<eGuessOption>(r_UsedColors);

            if (r_GameLogic.CountColorAppearances(usedColorsList, colorClicked) > 0)
            {
                MessageBox.Show("Your input must consist of unique colors. Select a different color.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            m_SelectedColor = colorClicked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
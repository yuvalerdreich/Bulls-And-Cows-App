using System;
using System.Windows.Forms;

namespace Ex05.BullsAndCows
{
    public class Program
    {
        public static void Main()
        {
            FormGameSettings gameSettings;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gameSettings = new FormGameSettings();
            Application.Run(gameSettings);
        }
    }
}
using System;
using System.Drawing;

namespace Ex05.BullsAndCows
{
    public static class ColorManager
    {
        public static Color GetSystemColor(eGuessOption i_Color)
        {
            Color resultColor;

            switch (i_Color)
            {
                case eGuessOption.Purple:
                    resultColor = Color.Purple;
                    break;
                case eGuessOption.Red:
                    resultColor = Color.Red;
                    break;
                case eGuessOption.Green:
                    resultColor = Color.Green;
                    break;
                case eGuessOption.Cyan:
                    resultColor = Color.Cyan;
                    break;
                case eGuessOption.Blue:
                    resultColor = Color.Blue;
                    break;
                case eGuessOption.Yellow:
                    resultColor = Color.Yellow;
                    break;
                case eGuessOption.Brown:
                    resultColor = Color.Brown;
                    break;
                case eGuessOption.White:
                    resultColor = Color.White;
                    break;
                default:
                    resultColor = Color.Gray;
                    break;
            }

            return resultColor;
        }

        public static eGuessOption[] GetAllColors()
        {
            return (eGuessOption[])Enum.GetValues(typeof(eGuessOption));
        }
    }
}
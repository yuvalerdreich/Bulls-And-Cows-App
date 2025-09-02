using System;
using System.Collections.Generic;

namespace Ex05.BullsAndCows
{
    public class GameLogic
    {
        private readonly Random r_Random = new Random();
        private const int k_LengthOfGuess = 4;

        public List<eGuessOption> GenerateComputerInput()
        {
            List<eGuessOption> generatedSequence = new List<eGuessOption>();
            eGuessOption randomColor;
            eGuessOption[] possibleColors = (eGuessOption[])Enum.GetValues(typeof(eGuessOption));

            while (generatedSequence.Count < k_LengthOfGuess)
            {
                do
                {
                    randomColor = possibleColors[r_Random.Next(possibleColors.Length)];
                } while (generatedSequence.Contains(randomColor));

                generatedSequence.Add(randomColor);
            }

            return generatedSequence;
        }

        public int CountColorAppearances(List<eGuessOption> i_UserGuess, eGuessOption i_ColorToCheck)
        {
            int count = 0;

            foreach (eGuessOption color in i_UserGuess)
            {
                if (color == i_ColorToCheck)
                {
                    count++;
                }
            }

            return count;
        }

        public List<eGuessUnitState> ProcessUserGuess(List<eGuessOption> i_UserGuess, List<eGuessOption> i_ComputerSequence)
        {
            List<eGuessUnitState> feedback = new List<eGuessUnitState>();

            for (int i = 0; i < i_UserGuess.Count; i++)
            {
                if (i_ComputerSequence.Contains(i_UserGuess[i]))
                {
                    if (i_ComputerSequence[i] == i_UserGuess[i])
                    {
                        feedback.Add(eGuessUnitState.Bull);
                    }
                    else
                    {
                        feedback.Add(eGuessUnitState.Cow);
                    }
                }
            }

            return feedback;
        }

        public eGameStatus CheckIfWonOrLost(List<eGuessUnitState> i_Feedback, int i_CurrentGuessNumber, int i_MaxGuesses)
        {
            eGameStatus status = eGameStatus.Pending;
            int bullCount = 0;

            foreach (eGuessUnitState state in i_Feedback)
            {
                if (state == eGuessUnitState.Bull)
                {
                    bullCount++;
                }
            }

            if (bullCount == k_LengthOfGuess)
            {
                status = eGameStatus.Win;
            }
            else if (i_CurrentGuessNumber >= i_MaxGuesses)
            {
                status = eGameStatus.Loss;
            }

            return status;
        }
    }
}
# Bulls and Cows - Windows Forms Game
This project is a Windows Forms application written in **C# (.NET Framework)** that implements the classic game *Bulls and Cows*
---

## 🎮 Game Overview
- The computer randomly generates a secret sequence of **4 unique colors**.
- The player has a limited number of attempts (between 4 and 10) to guess the sequence.
- After each guess:
  - **Black Peg (Bull):** Correct color in the correct position.
  - **Yellow Peg (Cow):** Correct color in the wrong position.
- The game ends when:
  - The player guesses the entire sequence (**Win**), or
  - The player runs out of attempts (**Loss**).
- At the end, the secret sequence is revealed.

---

## 🖥️ Features
- **Windows Forms UI** with multiple forms:
  - **FormGameSettings**: Choose the number of attempts and start the game.
  - **FormMainGame**: Play the guessing game with visual feedback.
  - **FormColorPicker**: Select colors from a palette of 8 unique options.
- **Validation** ensures that each guess contains unique colors.
- **Dynamic feedback** using colored indicators (black and yellow).
- **Clean separation** of enums (`eGameStatus`, `eGuessOption`, `eGuessUnitState`) and utility classes (`ColorManager`).

---

## 📂 Project Structure
- `Program.cs` – Entry point, launches the settings form.
- `FormGameSettings.cs` – Game initialization and settings screen.
- `FormMainGame.cs` – Main game logic and board UI.
- `FormColorPicker.cs` – Dialog for selecting colors.
- `ColorManager.cs` – Maps game color enums to system colors.
- `eGameStatus.cs` – Game state enum (Win, Loss, Pending).
- `eGuessOption.cs` – Possible color choices.
- `eGuessUnitState.cs` – Guess result (Bull, Cow).

---

## 🚀 How to Run
1. Open the solution in **Visual Studio**.
2. Build the project
3. Run the program
4. Choose the number of attempts and play the game.

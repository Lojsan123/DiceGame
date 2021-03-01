using System;

namespace DiceGame
{
    public class DiceGuessingGame
    {
        private int _score;
        private Dice _dice;
        private int _correctGuessCounter;
        
        public DiceGuessingGame()
        {
            _dice = new Dice();
        }

        public int Score 
        {
            get { return _score; }
            set { _score = value; }
        }

        public int CorrectGuessCounter 
        {
            get { return _correctGuessCounter; }
            set { _correctGuessCounter = value; }
        }

        public void GameLoop(int lastThrownValue = 0)
        {
            if (lastThrownValue == 0)
            {
                _dice.PreviousValue = _dice.RollDice();

            }
            else 
            {
                _dice.PreviousValue = lastThrownValue;
            }
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"Dice rolled: {_dice.PreviousValue}");
            Console.WriteLine($"Guess if next throw is higher or lower? For lower: Lower, for higher: Higher.");
            var guess = Console.ReadLine();
            _dice.CurrentValue = _dice.RollDice();
            var continueGame = PlayGame(_dice.CurrentValue, _dice.PreviousValue, guess);
            Console.WriteLine($"Dice rolled: {_dice.CurrentValue} which is {(continueGame? "correct" : "wrong")}");
            Console.WriteLine($"Your score is: {Score}");
            if (continueGame)
            {
                GameLoop(_dice.CurrentValue);

            }
            else 
            {
                Console.WriteLine($"Game Over!");
                return;
            }
        }

        public bool CurrentDiceRollIsHigher(int currentValue, int previousValue)
        {
            return currentValue > previousValue;
        }

        public bool CurrentDiceRollIsLower(int currentValue, int previousValue)
        {
            return currentValue < previousValue; 
        }

        public bool IsDiceRollEqual(int currentValue, int previousValue)
        {
            return (currentValue == previousValue);
        }

        public bool CheckIfGuessIsRight(int curretValue, int previousValue, string guess)
        {
            if (IsDiceRollEqual(curretValue, previousValue)) 
            {
                return false;
            }
            if (guess == "Higher") 
            {
                return CurrentDiceRollIsHigher(curretValue, previousValue);
            }
            else if (guess == "Lower") 
            {
                return CurrentDiceRollIsLower(curretValue, previousValue);
            }
            
            return false;
        }

        public bool PlayGame(int currentValue, int previousValue, string guess)
        {
            if (CheckIfGuessIsRight(currentValue, previousValue, guess)) 
            {
                CorrectGuessCounter++;
                ScoreHandler();
                return true;
            }
            return false;
        }

        private void ScoreHandler()
        {
            if (CorrectGuessCounter % 3 == 0)
            {
                Score += 3;
            }
            else
            {
                Score++;
            }
        }
    }
}
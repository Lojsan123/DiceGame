using System;
using System.Collections.Generic;

namespace DiceGame
{
    public class Dice
    {
        private int _currentValue;
        private int _previousValue;
        public Dice()
        {
        }

        public int CurrentValue
        {
            get { return _currentValue; }
            set { _currentValue = value; }
        }

        public int PreviousValue
        {
            get { return _previousValue; }
            set { _previousValue = value; }
        }

        public int RollDice(int optionalSeed=0)
        {
            if (optionalSeed != 0)
            {
                Random random = new Random(optionalSeed);
                return random.Next(1, 7);
            }
            else {
                Random random = new Random();
                return random.Next(1, 7);
            }
            
        }
    }
}
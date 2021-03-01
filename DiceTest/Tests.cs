using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiceGame;

namespace DiceTest
{
    [TestClass]
    public class Tests
    {
        private Dice _dice;
        private DiceGuessingGame _diceGuessingGame;

        [TestInitialize]
        public void SetUp()
        {
            _dice = new Dice();
            _diceGuessingGame = new DiceGuessingGame();
        }

        [TestMethod]
        public void Roll_Dice_Result_Should_Be_Two() 
        {
            _dice.CurrentValue = _dice.RollDice(3);
            Assert.AreEqual(2, _dice.CurrentValue);
        }

        [TestMethod]
        public void Should_Return_True_When_Current_Value_Is_Higher_Than_Previous() 
        {
            var currentValue = _dice.RollDice(123);
            var previousValue = _dice.RollDice(4);
            var result = _diceGuessingGame.CurrentDiceRollIsHigher(currentValue, previousValue);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Should_Return_True_When_Current_Value_Is_Lower_Than_Previous()
        {
            var currentValue = _dice.RollDice(4);
            var previousValue = _dice.RollDice(123);
            var result = _diceGuessingGame.CurrentDiceRollIsLower(currentValue, previousValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_Return_True_When_Current_Value_Is_Equal_To_Previous() 
        {
            var currentValue = _dice.RollDice(4);
            var previousValue = _dice.RollDice(4);
            var result = _diceGuessingGame.IsDiceRollEqual(currentValue, previousValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_Return_True_If_Higher_Guess_Is_Right() 
        {
            var currentValue = _dice.RollDice(123);
            var previousValue = _dice.RollDice(4);
            var result = _diceGuessingGame.CheckIfGuessIsRight(currentValue, previousValue, "Higher");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_Return_True_If_Lower_Guess_Is_Right()
        {
            var currentValue = _dice.RollDice(4);
            var previousValue = _dice.RollDice(123);
            var result = _diceGuessingGame.CheckIfGuessIsRight(currentValue, previousValue, "Lower");

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("Higher")]
        [DataRow("Lower")]
        public void Should_Return_False_Guess_If_Result_Is_Equal(string guess) 
        {
            var currentValue = _dice.RollDice(4);
            var previousValue = _dice.RollDice(4);
            var result = _diceGuessingGame.CheckIfGuessIsRight(currentValue, previousValue, guess);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Game_Should_Continue_If_Guess_Is_Right() 
        {
            var currentValue = _dice.RollDice(4);
            var previousValue = _dice.RollDice(123);
            var result = _diceGuessingGame.PlayGame(currentValue, previousValue, "Lower" );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_Get_One_Point_If_Guess_Is_Right() 
        {
            _diceGuessingGame.Score = 0;

            var currentValue = _dice.RollDice(1);
            var previousValue = _dice.RollDice(4);
            var result = _diceGuessingGame.PlayGame(currentValue, previousValue, "Lower");

            Assert.AreEqual(1, _diceGuessingGame.Score);

        }

        [TestMethod]
        [DataRow(0, 0, 5)]
        [DataRow(3, 5, 10)]
        [DataRow(6, 10, 15)]
        public void Third_Time_Correct_Should_Give_Three_Points(int startGuessCounter, int startScore, int expected) 
        {
            _diceGuessingGame.CorrectGuessCounter = startGuessCounter;
            _diceGuessingGame.Score = startScore;

            var currentValue = _dice.RollDice(123);
            var previousValue = _dice.RollDice(1);


            _diceGuessingGame.PlayGame(currentValue, previousValue, "Higher");
            _diceGuessingGame.PlayGame(currentValue, previousValue, "Higher");
            _diceGuessingGame.PlayGame(currentValue, previousValue, "Higher");

            Assert.AreEqual(expected, _diceGuessingGame.Score);

        }

       
        
    }
}

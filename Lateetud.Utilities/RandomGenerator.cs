using System;
using System.Text;

namespace Lateetud.Utilities
{
    public class RandomGenerator
    {
        #region private properties
        private string RandomId { get; set; }
        private int minNumber { get; set; }
        private int maxNumber { get; set; }
        private int NoOfChars { get; set; }
        private bool lowerCase { get; set; }
        private int NoOfChars1 { get; set; }
        private bool lowerCase1 { get; set; }
        private int NoOfChars2 { get; set; }
        private bool lowerCase2 { get; set; }
        #endregion

        #region RandomGenerator
        public RandomGenerator()
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = 1000;
            this.maxNumber = 9999;
            this.NoOfChars = 4;
            this.lowerCase = true;
            this.NoOfChars1 = 4;
            this.lowerCase1 = true;
            this.NoOfChars2 = 2;
            this.lowerCase2 = false;
        }
        public RandomGenerator(int minNumber, int maxNumber)
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            this.NoOfChars = 4;
            this.lowerCase = true;
            this.NoOfChars1 = 4;
            this.lowerCase1 = true;
            this.NoOfChars2 = 2;
            this.lowerCase2 = false;
        }
        public RandomGenerator(int NoOfChars, bool lowerCase)
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = 1000;
            this.maxNumber = 9999;
            this.NoOfChars = NoOfChars;
            this.lowerCase = lowerCase;
            this.NoOfChars1 = 4;
            this.lowerCase1 = true;
            this.NoOfChars2 = 2;
            this.lowerCase2 = false;
        }
        public RandomGenerator(int minNumber, int maxNumber, int NoOfChars, bool lowerCase)
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            this.NoOfChars = NoOfChars;
            this.lowerCase = lowerCase;
            this.NoOfChars1 = 4;
            this.lowerCase1 = true;
            this.NoOfChars2 = 2;
            this.lowerCase2 = false;
        }
        public RandomGenerator(int NoOfChars1, bool lowerCase1, int NoOfChars2, bool lowerCase2)
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = 1000;
            this.maxNumber = 9999;
            this.NoOfChars = 4;
            this.lowerCase = true;
            this.NoOfChars1 = NoOfChars1;
            this.lowerCase1 = lowerCase1;
            this.NoOfChars2 = NoOfChars2;
            this.lowerCase2 = lowerCase2;
        }
        public RandomGenerator(int minNumber, int maxNumber, int NoOfChars1, bool lowerCase1, int NoOfChars2, bool lowerCase2)
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            this.NoOfChars = 4;
            this.lowerCase = true;
            this.NoOfChars1 = NoOfChars1;
            this.lowerCase1 = lowerCase1;
            this.NoOfChars2 = NoOfChars2;
            this.lowerCase2 = lowerCase2;
        }
        public RandomGenerator(int minNumber, int maxNumber, int NoOfChars, bool lowerCase, int NoOfChars1, bool lowerCase1, int NoOfChars2, bool lowerCase2)
        {
            this.RandomId = Guid.NewGuid().ToString();
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            this.NoOfChars = NoOfChars;
            this.lowerCase = lowerCase;
            this.NoOfChars1 = NoOfChars1;
            this.lowerCase1 = lowerCase1;
            this.NoOfChars2 = NoOfChars2;
            this.lowerCase2 = lowerCase2;
        }
        #endregion

        #region Generate a random number between two numbers  
        public string RandomGuid()
        {
            return this.RandomId;
        }
        public int RandomNumber()
        {
            return RandomNumber(this.minNumber, this.maxNumber);
        }
        public int RandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            return random.Next(minNumber, maxNumber);
        }
        #endregion

        #region Generate a random string with a given size  
        public string RandomString()
        {
            return RandomString(this.NoOfChars, this.lowerCase);
        }
        public string RandomString(int NoOfChars, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < NoOfChars; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        #endregion

        #region Generate a random password  
        public string RandomPassword()
        {
            return this.RandomPassword(this.minNumber, this.maxNumber, this.NoOfChars1, this.lowerCase1, this.NoOfChars2, this.lowerCase2);
        }
        public string RandomPassword(int minNumber, int maxNumber)
        {
            return this.RandomPassword(minNumber, maxNumber, this.NoOfChars1, this.lowerCase1, this.NoOfChars2, this.lowerCase2);
        }
        public string RandomPassword(int NoOfChars1, bool lowerCase1, int NoOfChars2, bool lowerCase2)
        {
            return this.RandomPassword(this.minNumber, this.maxNumber, NoOfChars1, lowerCase1, NoOfChars2, lowerCase2);
        }
        public string RandomPassword(int minNumber, int maxNumber, int NoOfChars1, bool lowerCase1, int NoOfChars2, bool lowerCase2)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(NoOfChars1, lowerCase1));
            builder.Append(RandomNumber(minNumber, maxNumber));
            builder.Append(RandomString(NoOfChars2, lowerCase2));
            return builder.ToString();
        }
        #endregion
    }
}

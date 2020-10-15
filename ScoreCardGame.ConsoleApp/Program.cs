//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="---">
//     Copyright (c) ---. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ScoreCardGame.ConsoleApp
{
    using System;
    using ScoreCardGame.Library;

    /// <summary>
    /// The program to simulate/test the utilities.
    /// </summary>
    internal class Program
    {
        #region Private Methods

        /// <summary>
        /// The entry point of the program.
        /// </summary>
        private static void Main()
        {
            string[] cards = { "0", "J", "K", "1", "2", "K", "K", "3", "4", "K", "5", "K", "6", "7", "K" };
            int result = Utilities.ScoreHand(cards);

            Console.WriteLine("Result: {0}", result);
            Console.ReadLine();
        }

        #endregion Private Methods
    }
}
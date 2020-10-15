//-----------------------------------------------------------------------
// <copyright file="UtilitiesTest.cs" company="---">
//     Copyright (c) ---. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ScoreCardGame.Test
{
    using System;
    using Library;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Testing the utilities library.
    /// </summary>
    [TestClass]
    public class UtilitiesTest
    {
        #region Public Methods

        /// <summary>
        /// Testing the <see cref="Utilities.ScoreHand()"/> with multiple and invalid hands.
        /// Expecting <see cref="ArgumentException"/> to be thrown
        /// </summary>
        /// <param name="cards">The cards (hand)</param>
        [TestMethod]
        [DataRow(new[] { "A", "3", "Z", "J" })]
        [DataRow(new[] { "1", "j" })]
        [DataRow(new[] { "1", "2", "11" })]
        [DataRow(new[] { "A", "0", "J" })]
        [ExpectedException(typeof(ArgumentException), "Invalid hand")]
        public void ScoreHand_MultipleHands_Invalid_ThrowsException(string[] cards)
        {
            //// Act
            Utilities.ScoreHand(cards);
        }

        /// <summary>
        /// Testing the <see cref="Utilities.ScoreHand()"/>
        /// with multiple hands and successfully asserting the expected result.
        /// </summary>
        /// <param name="cards">The cards (hand)</param>
        /// <param name="expected">The expected result.</param>
        [TestMethod]
        [DataRow(new[] { "J", "K", "1", "2", "K", "K", "3", "4", "K", "5", "K", "6", "7", "K" }, 5)]
        [DataRow(new[] { "9", "J", "K", "1", "2", "K", "K", "3", "4", "K", "5", "K", "6", "7", "K" }, 5)]
        [DataRow(new[] { "3", "5" }, 8)]
        [DataRow(new[] { "J", "3", "5" }, 8)]
        [DataRow(new[] { "K", "3", "5" }, 8)]
        [DataRow(new[] { "3", "5", "K" }, 8)]
        [DataRow(new[] { "3", "5", "Q" }, 8)]
        [DataRow(new[] { "3", "5", "Q", "A" }, 8)]
        [DataRow(new[] { "9", "A", "3", "Q", "Q", "2" }, 25)]
        [DataRow(new[] { "K", "3", "5", "J", "A", "Q", "8", "K" }, 0)]
        [DataRow(new[] { "2", "A", "A", "A" }, 16)]
        [DataRow(new[] { "2", "A", "A", "A", "J" }, 0)]
        [DataRow(new[] { "4", "K", "3", "K", "5", "K", "Q" }, 9)]
        [DataRow(new[] { "A", "3", "J", "J" }, 0)]
        [DataRow(new[] { "K", "J", "3", "K" }, 0)]
        [DataRow(new[] { "2", "K", "J", "3", "K" }, 2)]
        [DataRow(new[] { "K", "J", "3", "K", "10" }, 10)]
        [DataRow(new[] { "4", "J", "3", "K", "10" }, 13)]
        [DataRow(new[] { "4", "Q", "3", "K", "10" }, 18)]
        [DataRow(new[] { "4", "Q", "3", "J", "10" }, 10)]
        public void ScoreHand_MultipleHands_Success(string[] cards, int expected)
        {
            //// Act
            int result = Utilities.ScoreHand(cards);

            //// Assert
            Assert.AreEqual(expected, result, string.Join(",", cards));
        }

        #endregion Public Methods
    }
}
//-----------------------------------------------------------------------
// <copyright file="Utilities.cs" company="---">
//     Copyright (c) ---. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ScoreCardGame.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Utilities library
    /// </summary>
    public class Utilities
    {
        #region Public Methods

        /// <summary>
        /// Scores the hand, based on the business logic.
        /// </summary>
        /// <param name="cards">The cards.</param>
        /// <returns>The score</returns>
        public static int ScoreHand(string[] cards)
        {
            List<string> cardList = cards.ToList();

            ValidateHand(cardList);

            HandleKings(cardList);

            HandleJacks(cardList);

            //// At this stage we should not have any "Jacks" or "Kings".

            int result = 0;

            for (int i = 0; i < cardList.Count; i++)
            {
                string card = cardList[i];

                if (card == "Q")
                {
                    result = HandleQueenScore(cardList, i, result);
                }
                else if (card == "A")
                {
                    result = HandleAceScore(cardList, card, ref i, result);
                }
                else
                {
                    //// Handle numbers.
                    //// It assumes only (valid) numbers will be available here.
                    result += int.Parse(card);
                }
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the Ace ('A') score.
        /// Doubles the previous card
        /// Keeps doubling while the next card is still an Ace ('A')
        /// </summary>
        /// <param name="cardList">The list of cards.</param>
        /// <param name="card">The current card.</param>
        /// <param name="index">The current index. This can be updated (by reference)</param>
        /// <param name="result">The current result</param>
        /// <returns>The updated result.</returns>
        private static int HandleAceScore(List<string> cardList, string card, ref int index, int result)
        {
            if (index > 0 && cardList[index - 1] != "Q")
            {
                int prevCard = int.Parse(cardList[index - 1]);
                result -= prevCard;

                while (card == "A")
                {
                    prevCard *= 2;

                    if (index + 1 >= cardList.Count ||
                        cardList[index + 1] != "A")
                    {
                        break;
                    }

                    index++;
                    card = cardList[index];
                }

                result += prevCard;
            }

            return result;
        }

        /// <summary>
        /// Handles the Jacks ('J') cards.
        /// Try to find the last Jack and remove all elements before that index.
        /// </summary>
        /// <param name="cardList">The list of cards.</param>
        private static void HandleJacks(List<string> cardList)
        {
            int jackIndex = cardList.LastIndexOf("J");

            if (jackIndex >= 0)
            {
                cardList.RemoveRange(0, jackIndex + 1);
            }
        }

        /// <summary>
        /// Handles the King ('K') cards.
        /// Try to find the pairs of Kings and remove all cards between each pair.
        /// </summary>
        /// <param name="cardList">The list of cards.</param>
        private static void HandleKings(List<string> cardList)
        {
            List<int> kingIndexes = new List<int>();

            for (int i = 0; i < cardList.Count; i++)
            {
                if (cardList[i] == "K")
                {
                    kingIndexes.Add(i);
                }
            }

            if (kingIndexes.Count % 2 != 0)
            {
                //// Remove the last King, because this cannot form a pair
                cardList.RemoveAt(kingIndexes.Last());
                kingIndexes.RemoveAt(kingIndexes.Count - 1);
            }

            for (int i = kingIndexes.Count - 1; i >= 0; i -= 2)
            {
                cardList.RemoveRange(kingIndexes[i - 1], kingIndexes[i] - kingIndexes[i - 1] + 1);
            }
        }

        /// <summary>
        /// Handles Queen the Queen score
        /// Adds '1' there is a following card and it is not an Ace ('A').
        /// It assumes that no Kings ('K') or Jacks ('J') are available.
        /// </summary>
        /// <param name="cardList">The list of cards.</param>
        /// <param name="index">The current index</param>
        /// <param name="result">The current result.</param>
        /// <returns>The updated result.</returns>
        private static int HandleQueenScore(List<string> cardList, int index, int result)
        {
            if (index + 1 < cardList.Count && cardList[index + 1] != "A")
            {
                result++;
            }

            return result;
        }

        /// <summary>
        /// Checks if the hand is valid.
        /// It will throw an exception if not valid.
        /// </summary>
        /// <param name="cardList">The cards</param>
        private static void ValidateHand(List<string> cardList)
        {
            //// Validate hand
            foreach (string card in cardList)
            {
                if (card == "A" ||
                    card == "J" ||
                    card == "Q" ||
                    card == "K")
                {
                    continue;
                }

                if (int.TryParse(card, out int value))
                {
                    if (value >= 1 && value <= 10)
                    {
                        continue;
                    }
                }

                throw new ArgumentException("Invalid hand");
            }
        }

        #endregion Private Methods
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public abstract class PokerHand
    {
        protected Card[] myHand, playerCards, myDistinct;
        protected HandAndFactor handAndFactor;
        protected static List<Card> _deck = new List<Card>(52);
        protected Card[][] sublists = new Card[10][];
        public abstract bool exists(out HandAndFactor handAndFactor);
        public abstract double couldExist(int lives);
        public static Dictionary<Card, List<Hand>> uniqueFactors = new Dictionary<Card, List<Hand>>();
        
        public PokerHand(Card[] _playerCards)
        {
            playerCards = new Card[2];
            _playerCards.CopyTo(playerCards, 0);
            myHand = new Card[playerCards.Length + Poker.communityCards.Count];

            playerCards.CopyTo(myHand, 0);
            Poker.communityCards.CopyTo(myHand, playerCards.Length);

            myHand = myHand.OrderBy(item => item.myValue).ToArray();

            myDistinct = myHand.DistinctBy(value => value.myValue).ToArray();
        }

        public static void setDeck()
        {
            _deck.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(cardType)).Length; i++)
                for (int j = 0; j < Enum.GetNames(typeof(cardValue)).Length; j++)
                    _deck.Add(new Card(i, j + 2));
        }

        protected static bool repetitionOrPairs(Card[] myHand, out HandAndFactor handAndKicker)
        {
            cardValue Kicker;
            int[] indicesOfTripleRepeatedValues = new int[2];

            Card[] myDistinct = myHand.DistinctBy(value => value.myValue).ToArray();

            int[] counters = new int[myDistinct.Length];

            for (int i = 0; i < myDistinct.Length; i++)
                counters[i] = myHand.Count(value => value.myValue == myDistinct[i].myValue);

            indicesOfTripleRepeatedValues[0] = Array.FindIndex(counters, counter => counter == 3);

            if (indicesOfTripleRepeatedValues[0] != -1)
            {
                indicesOfTripleRepeatedValues[1] = Array.FindIndex(counters, indicesOfTripleRepeatedValues[0] + 1, counter => counter >= 2);
                //full house
                if (indicesOfTripleRepeatedValues[1] != -1)
                {
                    handAndKicker = new HandAndFactor(Hand.FullHouse, myDistinct[indicesOfTripleRepeatedValues[0]].myValue);
                    return true;
                }
                //straight
                else if (straightExists(myHand, out Kicker))
                {
                    handAndKicker = new HandAndFactor(Hand.Straight, Kicker);
                    return true;
                }
                //three of a kind
                else
                {
                    handAndKicker = new HandAndFactor(Hand.ThreeOfAkind, myDistinct[indicesOfTripleRepeatedValues[0]].myValue);
                    return true;
                }
            }
            //straight
            else if (straightExists(myHand, out Kicker))
            {
                handAndKicker = new HandAndFactor(Hand.Straight, Kicker);
                return true;
            }
            else
            {
                handAndKicker = new HandAndFactor();
                return false;
            }
        }

        protected static bool straightExists(Card[] myHand, out cardValue endValue)
        {
            for (int i = 0; i < myHand.Length; i++)
            {
                for (int j = i + 1; j < myHand.Length; j++)
                {
                    if (myHand[i].myValue == myHand[j].myValue)
                        myHand = Helper.RemoveAt(myHand, j);
                }
            }

            return FormSequence(myHand, out endValue);
        }

        protected static bool FormSequence(Card[] allCardsWithThisSuit, out cardValue endValue)
        {
            int straightCounter = 1;
            cardValue seqStart = allCardsWithThisSuit[0].myValue;
            endValue = cardValue.Two;

            for (int i = 1; i < allCardsWithThisSuit.Length; i++)
            {
                if (allCardsWithThisSuit[i].myValue == ++seqStart)
                {
                    straightCounter++;
                    endValue = allCardsWithThisSuit[i].myValue;
                    continue;
                }
                else
                {
                    straightCounter = 1;
                    if (i == allCardsWithThisSuit.Length - 4)
                        return false;
                    seqStart = allCardsWithThisSuit[i].myValue;
                }
            }

            if (straightCounter >= 5)
                return true;
            else
                return false;
        }

        protected static List<KeyValuePair<cardValue, int>> repeatedValues(Card[] myHand)
        {
            List<KeyValuePair<cardValue, int>> counters = new List<KeyValuePair<cardValue, int>>(13);

            for (int i = 0; i < 13; i++)
                counters.Add(new KeyValuePair<cardValue, int>((cardValue)i + 2, myHand.Count(value => value.myValue == (cardValue)i + 2)));

            counters.RemoveAll(count => count.Value < 2);

            return counters;
        }

        protected static int sameSuit(Card[] myHand)
        {
            int[] counters = new int[4];
            counters[(int)cardType.Spades] = myHand.Count(type => type.myType == cardType.Spades);
            counters[(int)cardType.Hearts] = myHand.Count(type => type.myType == cardType.Hearts);
            counters[(int)cardType.Diamonds] = myHand.Count(type => type.myType == cardType.Diamonds);
            counters[(int)cardType.Clubs] = myHand.Count(type => type.myType == cardType.Clubs);

            int khara = Array.FindIndex(counters, count => count >= myHand.Length - 2);
            if (khara != -1)
                return khara;
            else
                return -1;
        }

        protected static int sameSuit(Card[] myHand, out int suitCount)
        {
            int[] counters = new int[4];
            counters[(int)cardType.Spades] = myHand.Count(type => type.myType == cardType.Spades);
            counters[(int)cardType.Hearts] = myHand.Count(type => type.myType == cardType.Hearts);
            counters[(int)cardType.Diamonds] = myHand.Count(type => type.myType == cardType.Diamonds);
            counters[(int)cardType.Clubs] = myHand.Count(type => type.myType == cardType.Clubs);

            int khara = Array.FindIndex(counters, count => count >= myHand.Length - 2);
            if (khara != -1)
            {
                suitCount = counters[khara];
                return khara;
            }
            else
            {
                suitCount = 0;
                return -1;
            }
        }

        protected void setSublists(int sIndex)
        {
            Card[] mboy = { new Card(sIndex, 14), new Card(sIndex, 2), new Card(sIndex, 3), new Card(sIndex, 4), new Card(sIndex, 5) };

            for (int i = 0; i < 9; i++)
            {
                sublists[i] = new Card[5];
                if (sIndex == -1)
                    sublists[i] = _deck.GetRange(i, 5).ToArray();
                else
                    sublists[i] = _deck.GetRange(i + (sIndex * 13), 5).ToArray();
            }
            sublists[9] = mboy;
        }

        protected void addUniqueFactors(List<KeyValuePair<cardValue, int>> repeated, Hand hand)
        {
            List<cardType> neededTypes = new List<cardType>();
            List<cardType> repeatedCardsTypes = new List<cardType>();

            foreach (KeyValuePair<cardValue, int> kvp in repeated)
            {
                Card[] repeatedCards = myHand.Where(val => val.myValue == kvp.Key).ToArray();
                for (int i = 0; i < repeatedCards.Length; i++)
                    repeatedCardsTypes.Add(repeatedCards[i].myType);

                neededTypes = Helper.allTypes.Except(repeatedCardsTypes).ToList();

                for (int i = 0; i < neededTypes.Count; i++)
                {
                    Card c = new Card(neededTypes[i], repeated[0].Key);
                    List<Hand> dummy = new List<Hand>(1) { hand };

                    if (!uniqueFactors.Keys.Contains(c))
                        uniqueFactors.Add(c, dummy);
                    else if (!uniqueFactors[c].Contains(hand))
                        uniqueFactors[c].Add(hand);
                }
            }
        }

        protected void addUniqueFactors(Card[] cards, Hand hand)
        {
            List<cardType> neededTypes = new List<cardType>();
            List<cardType> repeatedCardsTypes = new List<cardType>();

            foreach (Card card in cards)
            {
                for (int i = 0; i < cards.Length; i++)
                    repeatedCardsTypes.Add(cards[i].myType);

                neededTypes = Helper.allTypes.Except(repeatedCardsTypes).ToList();

                for (int i = 0; i < neededTypes.Count; i++)
                {
                    Card c = new Card(neededTypes[i], cards[0].myValue);

                    List<Hand> dummy = new List<Hand>(1) { hand };

                    if (!uniqueFactors.Keys.Contains(c))
                        uniqueFactors.Add(c, dummy);
                    else if (!uniqueFactors[c].Contains(hand))
                        uniqueFactors[c].Add(hand);
                }
            }
        }

        protected void addUniqueFactors(Card card, Hand hand)
        {
            List<cardType> neededTypes = new List<cardType>();
            List<cardType> repeatedCardsTypes = new List<cardType>() { card.myType };

            neededTypes = Helper.allTypes.Except(repeatedCardsTypes).ToList();

            for (int i = 0; i < neededTypes.Count; i++)
            {
                Card c = new Card(neededTypes[i], card.myValue);

                List<Hand> dummy = new List<Hand>(1) { hand };

                if (!uniqueFactors.Keys.Contains(c))
                    uniqueFactors.Add(c, dummy);
                else if (!uniqueFactors[c].Contains(hand))
                    uniqueFactors[c].Add(hand);
            }
        }

        protected void getNeededCardsForFlush(int sIndex, Hand hand)
        {
            cardValue[] neededValues = Helper.allValues.Except(myHand.Select(val => val.myValue)).ToArray();
            Card[] neededCardsWithThisSuit = new Card[neededValues.Length];
            List<Hand> dummy = new List<Hand>(1) { hand };

            for (int i = 0; i < neededValues.Length; i++)
                neededCardsWithThisSuit[i] = new Card((cardType)sIndex, neededValues[i]);

            for (int i = 0; i < neededCardsWithThisSuit.Length; i++)
            {
                if (!uniqueFactors.Keys.Contains(neededCardsWithThisSuit[i]))
                    uniqueFactors.Add(neededCardsWithThisSuit[i], dummy);
                else if (!uniqueFactors[neededCardsWithThisSuit[i]].Contains(hand))
                    uniqueFactors[neededCardsWithThisSuit[i]].Add(hand);
            }
        }

    }
}
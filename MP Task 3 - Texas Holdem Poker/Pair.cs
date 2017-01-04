using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class Pair : PokerHand
    {
        public Pair(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            if (PairsExist(myHand, out handAndFactor))
                return true;
            else
                return false;
        }

        public override double couldExist(int toBeDrawn)
        {
            double case1, case2 = 0, case3 = 0, unSeenCards = Dealer.Deck.Count - myHand.Length, probability = 0;

            double outs = myDistinct.Length * 3;
            if(toBeDrawn == 2)
            {
                case2 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                case3 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));

                outs = 13 - myDistinct.Length;
                case1 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));

                probability += (case1 + case2 + case3);
            }
            else
                probability += (outs / unSeenCards);

            return probability;
        }

        private static bool PairsExist(Card[] myHand, out HandAndFactor handAndKicker)
        {
            int[] indicesOfPairs = new int[2];

            Card[] myDistinct = myHand.DistinctBy(value => value.myValue).ToArray();

            int[] counters = new int[myDistinct.Length];

            for (int i = 0; i < myDistinct.Length; i++)
                counters[i] = myHand.Count(value => value.myValue == myDistinct[i].myValue);

            indicesOfPairs[0] = Array.FindIndex(counters, counter => counter == 2);

            if (indicesOfPairs[0] != -1)
            {
                //pair
                indicesOfPairs[1] = Array.FindIndex(counters, indicesOfPairs[0] + 1, counter => counter == 2);
                if (indicesOfPairs[1] != -1)
                {
                    //two pairs
                    handAndKicker = new HandAndFactor(Hand.TwoPairs, myDistinct[indicesOfPairs[1]].myValue);
                    return true;
                }
                else
                {
                    handAndKicker = new HandAndFactor(Hand.OnePair, myDistinct[indicesOfPairs[0]].myValue);
                    return true;
                }
            }
            handAndKicker = new HandAndFactor();
            return false;
        }

    }
}
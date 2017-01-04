using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class FullHouse :PokerHand
    {
        public FullHouse(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            if (PokerHand.repetitionOrPairs(myHand, out handAndFactor) && handAndFactor.myHand == Hand.FullHouse)
                return true;
            else
                return false;
        }

        public override double couldExist(int toBeDrawn)
        {
            if (myDistinct.Length == myHand.Length)
                return 0;

            double case2, case3, unSeenCards = Dealer.Deck.Count - myHand.Length, probability = 0;
            List<KeyValuePair<cardValue, int>> counters = repeatedValues(myHand);

            List<KeyValuePair<cardValue, int>> repeatedTwice = counters.FindAll(count => count.Value == 2);
            List<KeyValuePair<cardValue, int>> repeatedThrice = counters.FindAll(count => count.Value == 3);

            if (repeatedTwice.Count >= 1 && toBeDrawn == 2)
            {
                double outs = 2 * repeatedTwice.Count;
                case2 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                case3 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                probability += (case2 + case3);
                addUniqueFactors(repeatedTwice, Hand.FullHouse);
            }

            if (repeatedThrice.Count > 0)
            {
                Card[] distinctValuesMinusRepeated = myDistinct.Where(value => value.myValue != repeatedThrice[0].Key).ToArray();

                if (repeatedThrice.Count == 1 && toBeDrawn == 2)
                {
                    double outs = 2 * 3;
                    case2 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                    case3 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                    probability += (case2 + case3);
                }
                else if (repeatedThrice.Count == 1 && toBeDrawn == 1)
                {
                    double outs = 3 * 3;
                    case2 = (outs / unSeenCards);
                    probability += case2;
                }
                addUniqueFactors(distinctValuesMinusRepeated, Hand.FullHouse);
            }

            return probability;
        }

    }
}

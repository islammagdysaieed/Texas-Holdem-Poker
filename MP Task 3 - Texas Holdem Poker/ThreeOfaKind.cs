using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class ThreeOfaKind : PokerHand
    {
        public ThreeOfaKind(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            if (PokerHand.repetitionOrPairs(myHand, out handAndFactor) && handAndFactor.myHand == Hand.ThreeOfAkind)
                return true;
            else
                return false;
        }

        public override double couldExist(int toBeDrawn)
        {
            if (myDistinct.Length == myHand.Length && toBeDrawn == 1)
                return 0;

            double case2, case3, unSeenCards = Dealer.Deck.Count - myHand.Length, probability = 0;
            List<KeyValuePair<cardValue, int>> counters = repeatedValues(myHand);

            List<KeyValuePair<cardValue, int>> repeatedTwice = counters.FindAll(count => count.Value == 2);

            if (toBeDrawn == 2)
            {
                if (myDistinct.Length == myHand.Length)
                {
                    foreach (Card card in myDistinct)
                    {
                        double outs = 3;
                        case2 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                        probability += case2;
                        addUniqueFactors(card, Hand.ThreeOfAkind);
                    }
                }
                else
                {
                    double outs = 3;
                    for (int i = 0; i < myDistinct.Length - repeatedTwice.Count; i++)
                    {
                        case2 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                        probability += case2;
                    }

                    outs = 2 * (myHand.Length - myDistinct.Length);
                    case2 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                    case3 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                    probability += (case2 + case3);
                    addUniqueFactors(repeatedTwice , Hand.ThreeOfAkind);
                }
            }
            else
            {
                double outs = 2 * (myHand.Length - myDistinct.Length);
                case2 = outs / unSeenCards;
                probability += case2;
                addUniqueFactors(repeatedTwice, Hand.ThreeOfAkind);
            }

            return probability;
        }

    }
}

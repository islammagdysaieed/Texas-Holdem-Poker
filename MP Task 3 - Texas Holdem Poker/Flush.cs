using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class Flush : PokerHand
    {
        public Flush(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            int[] counters = new int[4];
            counters[(int)cardType.Spades] = myHand.Count(type => type.myType == cardType.Spades);
            counters[(int)cardType.Hearts] = myHand.Count(type => type.myType == cardType.Hearts);
            counters[(int)cardType.Diamonds] = myHand.Count(type => type.myType == cardType.Diamonds);
            counters[(int)cardType.Clubs] = myHand.Count(type => type.myType == cardType.Clubs);

            int khara = Array.FindIndex(counters, count => count >= 5);
            if (khara != -1)
            {
                Card[] allCardsWithThisSuit = myHand.Where(type => type.myType == (cardType)khara).ToArray();

                handAndFactor = new HandAndFactor(Hand.Flush, allCardsWithThisSuit[allCardsWithThisSuit.Length - 1].myValue);
                return true;
            }
            else
            {
                handAndFactor = new HandAndFactor();
                return false;
            }
        }

        public override double couldExist(int toBeDrawn)
        {
            int count, sIndex;
            sIndex = sameSuit(myHand, out count);
            if (sIndex == -1)
                return 0;
            else
            {
                double case2, case3, unSeenCards = Dealer.Deck.Count - myHand.Length, probability = 0;

                if (toBeDrawn == 2)
                {
                    double outs = 13 - count;
                    if (count == 3)
                    {
                        case2 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                        probability += case2;

                        getNeededCardsForFlush(sIndex, Hand.Flush);
                    }
                    else
                    {
                        case2 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                        case3 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                        probability += (case2 + case3);

                        getNeededCardsForFlush(sIndex,  Hand.Flush);
                    }
                }
                else
                {
                    double outs = 13 - count;
                    case2 = outs / unSeenCards;
                    probability += case2;

                    getNeededCardsForFlush(sIndex, Hand.Flush);
                }

                return probability;
            }
        }
    }
}

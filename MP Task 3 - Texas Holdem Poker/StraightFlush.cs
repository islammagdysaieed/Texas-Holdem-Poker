using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class StraightFlush : Straight
    {
        public StraightFlush(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            int suitIndex = PokerHand.sameSuit(myHand);

            //fick off salooka
            if (base.exists(out handAndFactor) && suitIndex != -1)
            {
                handAndFactor.myHand = Hand.StraightFlush;
                return true;
            }

            return false;
        }

        public override double couldExist(int lives)
        {
            double neededCards = 0, case1 = 0, case2 = 0, unSeenCards = Dealer.Deck.Count - base.myHand.Length;
            int diffCount, toBeDrawn = 7 - base.myHand.Length;
            int sIndex = sameSuit(myHand);

            if (sIndex != -1)
            {
                setSublists(sIndex);
                for (int i = 0; i < 9; i++)
                {
                    Card[] sublist = _deck.GetRange(i + (sIndex * 13), 5).ToArray();
                    if (canFormSequence(sublist, lives, ref neededCards, out diffCount, sIndex, Hand.StraightFlush))
                    {
                        if (toBeDrawn == 2 && diffCount == 2)
                        {
                            double outs = neededCards;
                            case1 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                        }
                        else if (toBeDrawn == 2 && diffCount == 1)
                        {
                            double outs = neededCards;
                            case1 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                            case2 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                        }
                        else
                            case1 = neededCards / unSeenCards;
                    }
                }
                return case1 + case2;
            }
            else
                return 0;
        }

    }
}
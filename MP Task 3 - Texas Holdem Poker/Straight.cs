using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class Straight : PokerHand
    {
        public Straight(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            if (PokerHand.repetitionOrPairs(myHand, out handAndFactor) && handAndFactor.myHand == Hand.Straight)
                return true;
            else
                return false;
        }

        public override double couldExist(int lives)
        {
            double case1 = 0, case2 = 0, unSeenCards = Dealer.Deck.Count - base.myHand.Length;
            int toBeDrawn = 7 - base.myHand.Length, diffCount;
            double neededCards = 0;

            base.setSublists(-1);

            for (int i = 0; i < 10; i++)
            {
                if(canFormSequence(base.sublists[i], lives, ref neededCards, out diffCount, -1, Hand.Straight))
                {
                    if (toBeDrawn == 2 && diffCount == 2)
                    {
                        double outs = neededCards * 4;
                        case1 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                    }
                    else if (toBeDrawn == 2 && diffCount == 1)
                    {
                        double outs = neededCards * 4;
                        case1 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                        case2 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                    }
                    else
                        case1 = (neededCards * 4) / unSeenCards;
                }
            }

            return case1 + case2;
        }

        public bool canFormSequence(Card[] cardsInSeq, int lives, ref double count, out int diffCount, int sIndex, Hand hand)
        {
            Card[] diff = cardsInSeq.ExceptBy(myHand, val => val.myValue).ToArray();

            diffCount = diff.Length;

            if (diff.Length > lives)
                return false;
            else
            {
                for (int i = 0; i < diff.Length; i++)
                {
                    if (!uniqueFactors.Keys.Contains(diff[i]))
                    {
                        if (sIndex == -1)
                            addUniqueFactors(diff[i], hand);
                        else
                            addUniqueFactors(diff[i], hand);
                        ++count;
                    }
                    else
                    {
                        if (sIndex == -1)
                            addUniqueFactors(diff[i], hand);
                        else
                        {
                            if (!uniqueFactors[diff[i]].Contains(hand))
                            {
                                List<Hand> dummy = new List<Hand>() { hand };
                                uniqueFactors[diff[i]].Add(hand);
                            }
                        }
                        ++count;
                    }
                }
            }
            return true;
        }

    }
}
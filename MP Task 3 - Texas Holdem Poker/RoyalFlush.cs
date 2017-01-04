using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class RoyalFlush : StraightFlush
    {
        public RoyalFlush(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            if(base.exists(out handAndFactor))
            {
                if (base.myHand[0].myValue == cardValue.Ten)
                {
                    handAndFactor.myHand = Hand.RoyalFlush;
                    return true;
                }
            }
            return false;
        }

        public override double couldExist(int toBeDrawn)
        {
            double case1, unSeenCards = Dealer.Deck.Count - myHand.Length;
            toBeDrawn = 7 - myHand.Length;
            int suitIndex = sameSuit(myHand);

            if (suitIndex != -1)
            {
                myHand = myHand.Where(type => type.myType == (cardType)suitIndex).ToArray();
                List<cardValue> diff = Helper.RoyalFlushHand.Except(myHand.Select(value => value.myValue)).ToList();

                if (diff.Count > toBeDrawn)
                    return 0;
                else
                {
                    if (toBeDrawn == 2 && diff.Count == 2)
                    {
                        Card first = new Card((cardType)suitIndex, diff[0]);
                        Card second = new Card((cardType)suitIndex, diff[1]);
                        List<Hand> dummy = new List<Hand>(1) { Hand.RoyalFlush };
                        if (uniqueFactors.Keys.Contains(first))
                            uniqueFactors[first].Add(Hand.RoyalFlush);
                        else
                            uniqueFactors.Add(first, dummy);

                        if (uniqueFactors.Keys.Contains(second))
                            uniqueFactors[second].Add(Hand.RoyalFlush);
                        else
                            uniqueFactors.Add(second, dummy);

                        double outs = 2;
                        return case1 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                    }
                    else if (toBeDrawn == 2 && diff.Count == 1)
                    {
                        Card first = new Card((cardType)suitIndex, diff[0]);
                        List<Hand> dummy = new List<Hand>(1) { Hand.RoyalFlush };
                        if (uniqueFactors.Keys.Contains(first))
                            uniqueFactors[first].Add(Hand.RoyalFlush);
                        else
                            uniqueFactors.Add(first, dummy);

                        double outs = 1;
                        double case2 = (outs / unSeenCards) * ((unSeenCards - outs) / (unSeenCards - 1));
                        double case3 = ((unSeenCards - outs) / unSeenCards) * (outs / (unSeenCards - 1));
                        return case2 + case3;
                    }
                    //both = 1
                    else
                    {
                        Card first = new Card((cardType)suitIndex, diff[0]);
                        List<Hand> dummy = new List<Hand>(1) { Hand.RoyalFlush };
                        if (uniqueFactors.Keys.Contains(first))
                            uniqueFactors[first].Add(Hand.RoyalFlush);
                        else
                            uniqueFactors.Add(first, dummy);

                        return 1 / unSeenCards; 
                    }
                }
            }
            else
                return 0;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class FourOfaKind : PokerHand
    {
        public FourOfaKind(Card[] playerCards)
            : base(playerCards)
        {

        }

        public override bool exists(out HandAndFactor handAndFactor)
        {
            int[] counters = new int[myHand.Length - 3];

            for (int i = 0; i < counters.Length; i++)
                counters[i] = myHand.Count(value => value.myValue == myHand[i].myValue);

            int indexOfEndValue = Array.FindIndex(counters, counter => counter == 4);

            if (indexOfEndValue != -1)
            {
                handAndFactor = new HandAndFactor(Hand.FourOfAkind, myHand[indexOfEndValue].myValue);
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
            if (myDistinct.Length == myHand.Length)
                return 0;

            double case1, case2, case3, unSeenCards = Dealer.Deck.Count - myHand.Length, probability = 0;
            List<KeyValuePair<cardValue, int>> counters = repeatedValues(myHand);

            List<KeyValuePair<cardValue, int>> repeatedTwice = counters.FindAll(count => count.Value == 2);
            List<KeyValuePair<cardValue, int>> repeatedThrice = counters.FindAll(count => count.Value == 3);

            if (repeatedTwice.Count >= 1 && toBeDrawn == 2)
            {
                double outs = 2 * repeatedTwice.Count;
                case1 = (outs / unSeenCards) * ((outs - 1) / (unSeenCards - 1));
                probability += case1;

                addUniqueFactors(repeatedTwice, Hand.FourOfAkind);
            }

            if (repeatedThrice.Count >= 1 && toBeDrawn == 2)
            {
                case2 = (1 / unSeenCards) * ((unSeenCards - 1) / (unSeenCards - 1));
                case3 = ((unSeenCards - 1) / unSeenCards) * (1 / (unSeenCards - 1));
                probability += (case2 + case3);
                addUniqueFactors(repeatedThrice, Hand.FourOfAkind);
            }
            else if (repeatedThrice.Count >= 1 && toBeDrawn == 1)
            {
                case1 = repeatedThrice.Count / unSeenCards;
                probability += case1;

                addUniqueFactors(repeatedTwice, Hand.FourOfAkind);
            }

            return probability;
        }

    }
}

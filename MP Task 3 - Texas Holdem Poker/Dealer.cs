using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class Dealer
    {
        public static List<Card> Deck = new List<Card>(52);
        public double Pot;
        public List<Card> Deck_Clone;

        public Dealer()
        {          
            Deck_Clone = Helper.DeepCopy(Deck);
            Helper.Shuffle(Deck_Clone);
        }

        public static void originalDeck()
        {
            Deck.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(cardType)).Length; i++)
                for (int j = 0; j < Enum.GetNames(typeof(cardValue)).Length; j++)
                    Deck.Add(new Card(i, j + 2));
            Helper.Shuffle(Deck);
        }

        public Card[] Deal(int num)
        {
            Card[] toBeReturned = new Card[num];

            for (int i = 0; i < num; i++)
            {
                toBeReturned[i] = Deck_Clone[0];
                Deck_Clone.Remove(Deck_Clone[0]);
            }

            return toBeReturned;
        }

        public void addToPot(double bet)
        {
            Pot += bet;
        }

        public void resetPot()
        {
            Pot = 0;
        }

    }
}
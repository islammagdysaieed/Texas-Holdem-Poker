using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public static class Poker
    {
        public static int numOfPlayers, numOfRounds;
        public static Dealer myDealer;
        public static List<Player> myPlayers;
        public static List<Card> communityCards;
        public static List<int> Winners;
        public static double minBet, originalMinBet;
        public static Decision[] decisions; 
        public static List<HandAndFactor> playersHands;
        public static void Initialize(int numPlayers, double _minBet, int numRounds)
        {
            myPlayers = new List<Player>();
            myPlayers.Add(new Player(500000, new PlayerType(true, AI_Level.Hard)));
            for (int i = 1; i < numPlayers; i++)
                 myPlayers.Add(new Player(500000, new PlayerType(false, AI_Level.Easy)));

            PokerHand.setDeck();
            Dealer.originalDeck();
            numOfPlayers = numPlayers;
            numOfRounds = numRounds;
            Winners = new List<int>();
            communityCards = new List<Card>();
            decisions = new Decision[Poker.numOfPlayers];
            playersHands = new List<HandAndFactor>();
            originalMinBet = _minBet;
            minBet = _minBet;
        }

        public static void PreFlop()
        {
            myDealer = new Dealer();
            for (int i = 0; i < numOfPlayers; i++)
            {
                myPlayers[i].setMyHoldCards(myDealer.Deal(2).ToArray());
                myPlayers[i].Active = true;
            }
        }

        public static void Flop()
        {
            communityCards = myDealer.Deal(3).ToList();
        }

        public static void Turn()
        {
            communityCards.Add(myDealer.Deal(1)[0]);
        }

        public static void River()
        {
            communityCards.Add(myDealer.Deal(1)[0]);
        }

        public static void EndOfRound()
        {
            List<HandAndFactor> playersHands = new List<HandAndFactor>();

            for (int i = 0; i < Poker.numOfPlayers; i++)
                playersHands.Add(Poker.myPlayers[i].myHandAndFactor);

            Helper.getWinners(playersHands);

            for (int i = 0; i < Winners.Count; i++)
                Poker.myPlayers[Winners[i]].MyChipsProperty += myDealer.Pot / Winners.Count;

            Winners.Clear();
            communityCards.Clear();
            myDealer.resetPot();
        }
    }
}
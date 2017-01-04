using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace MP_Task_3___Texas_Holdem_Poker
{
    public class Player
    {

        public bool Active;
        public Decision myDecision;
        double myChips;
        public double MyChipsProperty
        {
            get { return myChips; }
            set { myChips = value; }
        }
        public PlayerType myType;
        public Card[] myCards = new Card[2];
        public List<double> probability;
        public double probabilityOfWinning;
        public List<Hand> possibleHands = new List<Hand>();
        private double myPossibleHandsValue
        {
            get { return setMyPossibleHandsValue(); }
        }
        public HandAndFactor myHandAndFactor;
        private double myHandAndFactorValue
        {
            get { return setMyHandAndFactorValue(); }
        }
        private double Multiplyer;
        public Dictionary<Card, List<Hand>> neededCards;
        private RoyalFlush royalFlush;
        private StraightFlush straightFlush;
        private FourOfaKind fourOfaKind;
        private FullHouse fullHouse;
        private Flush flush;
        private Straight straight;
        private ThreeOfaKind threeOfaKind;
        private Pair pair;

        public Player(double chips, PlayerType _myType)
        {
            myChips = chips;
            Active = true;
            myType = _myType;
            possibleHands = new List<Hand>();
            myHandAndFactor = new HandAndFactor();
            probability = new List<double>(Hand.RoyalFlush - myHandAndFactor.myHand);
        }

        public void setMyHoldCards(Card[] _myCards)
        {
            _myCards.CopyTo(myCards, 0);
        }

        private void getMyBestPossibleHands(HandAndFactor myHandAndFactor)
        {
            probability = new List<double>(Hand.RoyalFlush - myHandAndFactor.myHand);
            possibleHands = new List<Hand>();

            //#fuck off salooka
            Func<int, double>[] functions = { pair.couldExist, threeOfaKind.couldExist, straight.couldExist, flush.couldExist,
                                              fullHouse.couldExist, fourOfaKind.couldExist, straightFlush.couldExist ,royalFlush.couldExist };

            for (int i = (int)myHandAndFactor.myHand + 1; i <= functions.Length; i++)
            {
                probability.Add(functions[i - 1](5 - Poker.communityCards.Count));
                if (probability[probability.Count - 1] > 0)
                    possibleHands.Add((Hand)i + 1);
            }

            probabilityOfWinning = probability.Sum();
            neededCards = PokerHand.uniqueFactors;
        }

        private Card[] playerPlusCommunity()
        {
            Card[] both = new Card[myCards.Length + Poker.communityCards.Count];
            myCards.CopyTo(both, 0);
            Poker.communityCards.CopyTo(both, myCards.Length);

            both = both.OrderBy(item => item.myValue).ToArray();

            return both;
        }

        public void getMyDecision(List<status> decisionSpace)
        {
            if (myType.human == true)
                Multiplyer = Poker.minBet * 1.3;
            else
            {
                if (myType.level == AI_Level.Easy)
                    Multiplyer = Poker.minBet * 3.1;
                else if (myType.level == AI_Level.Medium)
                    Multiplyer = Poker.minBet * 2.2;
                else
                    Multiplyer = Poker.minBet * 1.3;
            }

            if (Poker.communityCards.Count == 0)
                myDecision = RandomizeStatusAndCalcBet(decisionSpace, Multiplyer * myHandAndFactorValue);
            else
            {
                if (myHandAndFactor.myHand != Hand.RoyalFlush)
                    getMyBestPossibleHands(myHandAndFactor);
                else
                    myDecision = new Decision(status.All_In, MyChipsProperty);

                myDecision = CalculateBet(decisionSpace);
            }

            if (myDecision.myStatus == status.All_In)
            {
                myDecision.myBet = MyChipsProperty;
                Poker.minBet = myDecision.myBet;
            }
            else if (myDecision.myStatus == status.Call)
                myDecision.myBet = Poker.minBet;
            else if (myDecision.myStatus == status.Fold)
                myDecision.myBet = 0;
            else
            {
                if (myDecision.myBet < Poker.minBet)
                    if ((Poker.minBet - myDecision.myBet) > Poker.minBet * 10)
                        myDecision.myStatus = status.Fold;
                    else
                        myDecision.myBet = Poker.minBet * 2;
                else
                    myDecision.myBet = Poker.minBet * 2;
            }
        }

        private Decision CalculateBet(List<status> decisionSpace)
        {
            List<status> raise = new List<status>() { status.Raise };
            if (probabilityOfWinning <= 0.02)
                return new Decision(status.Fold, 0);

            if (myType.level == AI_Level.Easy)
                if (probabilityOfWinning > 0.02)
                    return RandomizeStatusAndCalcBet(decisionSpace, Multiplyer * myHandAndFactorValue * myPossibleHandsValue);
                else
                    return RandomizeStatusAndCalcBet(decisionSpace.Except(raise).ToList(), 0);

            else if (myType.level == AI_Level.Medium)
                if (probabilityOfWinning > 0.2)
                    return RandomizeStatusAndCalcBet(decisionSpace, Multiplyer * myHandAndFactorValue * myPossibleHandsValue);
                else
                    return RandomizeStatusAndCalcBet(decisionSpace.Except(raise).ToList(), 0);

            else
                if (probabilityOfWinning > 0.3)
                    return RandomizeStatusAndCalcBet(decisionSpace, Multiplyer * myHandAndFactorValue * myPossibleHandsValue);
                else
                    return new Decision(status.Fold, 0);
        }

        private Decision RandomizeStatusAndCalcBet(List<status> decisionSpace, double myBet)
        {
            status myStatus;

            myStatus = Helper.getaRandomStatus(decisionSpace);

            if (myStatus == status.Raise)
            {
                if (myBet >= MyChipsProperty)
                    return new Decision(status.All_In, MyChipsProperty);
                else
                    return new Decision(myStatus, myBet);
            }
            else
                return new Decision(myStatus, 0);
        }

        public void getMyHand()
        {
            if (myCards.Length == 0)
                myHandAndFactor = new HandAndFactor();
            if (Poker.communityCards.Count == 0)
                myHandAndFactor = getHoldCardsHand(myCards);
            else
                getHand();
        }

        private void getHand()
        {
            royalFlush = new RoyalFlush(myCards);
            straightFlush = new StraightFlush(myCards);
            fourOfaKind = new FourOfaKind(myCards);
            fullHouse = new FullHouse(myCards);
            flush = new Flush(myCards);
            straight = new Straight(myCards);
            threeOfaKind = new ThreeOfaKind(myCards);
            pair = new Pair(myCards);

            if (royalFlush.exists(out myHandAndFactor))
                return;

            if (straightFlush.exists(out myHandAndFactor))
                return;

            if (fourOfaKind.exists(out myHandAndFactor))
                return;

            if (fullHouse.exists(out myHandAndFactor))
                return;

            if (flush.exists(out myHandAndFactor))
                return;

            if (straight.exists(out myHandAndFactor))
                return;

            if (threeOfaKind.exists(out myHandAndFactor))
                return;

            if (pair.exists(out myHandAndFactor))
                return;

            Card[] allOfThem = playerPlusCommunity();

            myHandAndFactor = new HandAndFactor(Hand.HighCard, allOfThem[allOfThem.Length - 1].myValue);
        }

        private HandAndFactor getHoldCardsHand(Card[] playerCards)
        {
            if (playerCards[0].myValue == playerCards[1].myValue)
                return new HandAndFactor(Hand.OnePair, playerCards[0].myValue);
            else
                return new HandAndFactor(Hand.HighCard, playerCards[1].myValue);
        }

        private double setMyPossibleHandsValue()
        {
            double possibleHandsMultiplyer = 0;
            for (int i = 0; i < possibleHands.Count; i++)
                possibleHandsMultiplyer += (double)possibleHands[i];

            return possibleHandsMultiplyer / 10;
        }

        private double setMyHandAndFactorValue()
        {
            double myHandAndFactorMultiplyer = 0;

            switch (myHandAndFactor.myHand)
            {
                case Hand.RoyalFlush:
                case Hand.StraightFlush:
                case Hand.FourOfAkind:
                case Hand.FullHouse:
                    myHandAndFactorMultiplyer += (double)myHandAndFactor.myHand;
                    break;

                case Hand.Flush:
                case Hand.Straight:
                case Hand.ThreeOfAkind:
                case Hand.TwoPairs:
                case Hand.OnePair:
                case Hand.HighCard:
                    myHandAndFactorMultiplyer += (double)myHandAndFactor.myFactor;
                    break;

                default:
                    myHandAndFactorMultiplyer += 0;
                    break;
            }

            return myHandAndFactorMultiplyer / 10;
        }

    }
}
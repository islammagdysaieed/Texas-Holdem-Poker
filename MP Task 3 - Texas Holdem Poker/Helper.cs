using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using MoreLinq;
using System.Drawing;
namespace MP_Task_3___Texas_Holdem_Poker
{
    [Serializable]
    public struct Card
    {
        public cardType myType;
        public cardValue myValue;
        // public bool isFaceUp;

        public Card(int type, int value)
        {
            myType = (cardType)type;
            myValue = (cardValue)value;
            //  isFaceUp = false;
        }

        public Card(cardType type, cardValue value)
        {
            myType = type;
            myValue = value;
            // isFaceUp = false;
        }
        public Image GetCardImage(bool isFaceUp = false)
        {
            //this.isFaceUp = isFaceUp;
            string path = (System.Environment.CurrentDirectory) + @"\Assets\deck\";
            int val = (int)myValue;
            string cardname = val.ToString() + myType.ToString()[0] + ".png";
            Image cardimg = Image.FromFile(path + cardname);
            if (!isFaceUp)
                cardimg = Image.FromFile(path + "back.png");
            return cardimg;
        }
    }

    public struct PlayerType
    {
        public bool human;
        public AI_Level level;

        public PlayerType(bool _human, AI_Level lvl)
        {
            human = _human;
            level = lvl;
        }
    }

    public struct HandAndFactor
    {
        public Hand myHand;
        public cardValue myFactor;

        public HandAndFactor(Hand hand, cardValue factor)
        {
            myHand = hand;
            myFactor = factor;
        }
    }

    public enum AI_Level { Easy, Medium, Hard };
    public enum cardType { Spades, Hearts, Diamonds, Clubs };
    public enum cardValue
    {
        Two = 2, Three = 3, Four = 4, Five = 5,
        Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10,
        Jack = 11, Queen = 12, King = 13, Ace = 14
    };
    public enum Hand { HighCard, OnePair, TwoPairs, ThreeOfAkind, Straight, Flush, FullHouse, FourOfAkind, StraightFlush, RoyalFlush };

    public struct Decision
    {
        public status myStatus;
        public double myBet;

        public Decision(status stat, double bet)
        {
            myStatus = stat;
            myBet = bet;
        }
    }

    public enum status { Fold, Check, Call, Raise, All_In };

    public static class Helper
    {
        static Random r;
        public static List<cardValue> allValues = new List<cardValue>(){ 
            cardValue.Two, cardValue.Three, cardValue.Four, cardValue.Five,cardValue.Six, cardValue.Seven,cardValue.Eight,cardValue.Nine,
            cardValue.Ten,cardValue.Jack,cardValue.Queen, cardValue.King, cardValue.Ace};

        public static List<cardType> allTypes = new List<cardType>() { cardType.Spades, cardType.Hearts, cardType.Diamonds, cardType.Clubs };

        public static List<cardValue> RoyalFlushHand = new List<cardValue>(){
            cardValue.Ace, cardValue.King, cardValue.Queen, cardValue.Jack, cardValue.Ten };

        public static void getWinners(List<HandAndFactor> hands)
        {
            //max by hand
            HandAndFactor bestHand = hands.MaxBy(hand => hand.myHand);
            List<HandAndFactor> equivalentHands = hands.FindAll(hand => hand.myHand == bestHand.myHand);

            if (equivalentHands.Count == 1)
                Poker.Winners.Add(hands.IndexOf(bestHand));
            else
            {
                //max by factor
                bestHand = equivalentHands.MaxBy(factor => factor.myFactor);
                List<HandAndFactor> sigh = equivalentHands.FindAll(factor => factor.myFactor == bestHand.myFactor);

                if (sigh.Count == 1)
                    Poker.Winners.Add(hands.IndexOf(bestHand));
                else
                {
                    for (int i = 0; i < equivalentHands.Count; i++)
                    {
                        Poker.Winners.Add(hands.IndexOf(bestHand));
                        hands.RemoveAt(Poker.Winners[i]);
                    }
                }
            }
        }

        //Fisher–Yates shuffle
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        public static status getaRandomStatus(List<status> decisionSpace)
        {
            r = new Random();
            int rand = r.Next(0, ((decisionSpace.Count - 1) * 100) + 30);

            for (int i = 0; i < decisionSpace.Count; i++)
            {
                if (i == 0)
                {
                    if (rand <= 99)
                        return decisionSpace[i];
                }
                else if(i == decisionSpace.Count - 1)
                {
                    if (rand > ((i - 1) * 100) - 1 && rand <= (((i - 1) * 100) - 1) + 30)
                        return decisionSpace[i];
                }
                else
                {
                    if (rand > ((i - 1) * 100) - 1 && rand <= ((i + 1) * 100) - 1)
                        return decisionSpace[i];
                }
            }

            //if something is wrong return null
            return new status();
        }

    }

}
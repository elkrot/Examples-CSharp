using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShuffle
{
    enum CardSuit
    {
        Clubs = 0,
        Diamonds,
        Hearts,
        Spades,
    };
    class Card
    {
        public CardSuit Suit { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            string str;
            switch (Value)
            {
                case 1:
                    str = "A";
                    break;
                case 11:
                    str = "J";
                    break;
                case 12:
                    str = "Q";
                    break;
                case 13:
                    str = "K";
                    break;
                default:
                    str = Value.ToString();
                    break;
            }
            switch (Suit)
            {
                case CardSuit.Clubs:
                    str += "C";
                    break;
                case CardSuit.Diamonds:
                    str += "D";
                    break;
                case CardSuit.Hearts:
                    str += "H";
                    break;
                case CardSuit.Spades:
                    str += "S";
                    break;
            }
            return str;
        }

        public int Ordinal
        {
            get
            {
                //return a value from 0 - 51
                return ((int)Suit * 13) + (Value - 1);
            }
        }
    };

    class Program
    {
        const int NumCards = 52;

        static Random rand = new Random();

        static void Main(string[] args)
        {
            int numTests = 100000000;
            if (args.Length > 0)
            {
                if (!int.TryParse(args[0], out numTests))
                {
                    Console.WriteLine("Usage: CardShuffle [numTests]\nnumTests is optional. Default value is 100,000,000");
                    return;
                }
            }
            CheckShuffleAlgorithm(numTests);            
        }

        private static Card[] CreateNewDeck()
        {
            Card[] cards = new Card[NumCards];
            int index = 0;
            for (CardSuit s = CardSuit.Clubs; s <= CardSuit.Spades; s++)
            {
                for (int value = 1; value <= 13; ++value)
                {
                    cards[index++] = new Card() { Suit = s, Value = value };
                }
            }
            return cards;
        }

        private static void PrintDeck(Card[] cards)
        {
            for (int i = 0; i < cards.Length; ++i)
            {
                if (i > 0)
                {
                    Console.Write(",");
                }
                Console.Write(cards[i].ToString());
            }
        }

        private static void ShuffleDeck(Card[] cards)
        {
            int n = cards.Length;

            while (n > 1)
            {
                int k = rand.Next(n);
                --n;
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }
        }

        private static void BadShuffle(Card[] cards)
        {
            for (int i = cards.Length - 1; i > 0; i--)
            {
                int n = rand.Next(i+1);
                Card temp = cards[i];
                cards[i] = cards[n];
                cards[n] = temp;
            }
        }

        private static void CheckShuffleAlgorithm(int numIterations)
        {
            //count how many times each card appears in each position
            //each row represents the positions for each card
            int[,] cardCount = Generate2DArray<int>(0);
            for (int i = 0; i < numIterations; ++i)
            {
                Card[] cards = CreateNewDeck();
                //BadShuffle(cards);
                ShuffleDeck(cards);
                RecordStats(cards, cardCount);
            }

            for (int i = 0; i < NumCards; ++i)
            {
                UInt64 rowTotal = 0;
               
                for (int j = 0; j < NumCards; ++j)
                {
                    rowTotal += (UInt64)cardCount[i, j];
                }

                for (int j = 0; j < NumCards; ++j)
                {
                    double percent = (double)cardCount[i, j] * 100.0 / (double)rowTotal;
                    Console.Write("{0:N3}% ", percent);
                }
                Console.WriteLine();
            }

           
        }

        private static T[,] Generate2DArray<T>(T defaultValue) 
        {
            T[,] cardCount = new T[NumCards, NumCards];
            for (int i = 0; i < NumCards; ++i)
            {
                for (int j = 0; j < NumCards; ++j)
                {
                    cardCount[i, j] = defaultValue;
                }
            }
            return cardCount;
        }

        private static void RecordStats(Card[] cards, int[,] cardCount)
        {
            for (int i = 0; i < cards.Length; ++i)
            {
                ++cardCount[cards[i].Ordinal, i];
            }
        }

    }
}

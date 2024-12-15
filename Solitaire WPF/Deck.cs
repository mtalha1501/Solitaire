using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire_WPF
{
    public class Deck
    {
        public Card[] deck;  // Deck Array

        private SuitType[] suits = { SuitType.hearts, SuitType.diamonds, SuitType.clubs, SuitType.spades };
        private RankType[] ranks = { RankType.ace, RankType.Two, RankType.Three, RankType.Four, RankType.Five,
                                     RankType.Six, RankType.Seven, RankType.Eight, RankType.Nine, RankType.Ten,
                                     RankType.jack, RankType.queen, RankType.king };

        public Deck()
        {
            deck = new Card[52];
            LoadCards();
            Shuffle();
        }
        void LoadCards()
        {
            int idx = 0;
            foreach(SuitType suit in suits)
            {
                foreach(RankType rank in ranks)
                {
                    
                    deck[idx] = new Card(suit, rank, true);
                    idx++;
                }
            }
        }
        public void Shuffle()   //this Function shuffles randomly
        {
            Random rn = new Random();
            for(int i =0;i < deck.Length;i++)
            {
                int j = rn.Next(i,deck.Length);
                Card flag = deck[j];
                deck[j] = deck[i];
                deck[i] = flag;
            }
        }
        public void EasyShuffle()   //This shuffle places all aces in stock pile and end and shuffles remaining
        {
            List<Card> aces = new List<Card>();
            List<Card> remaining = new List<Card>();

            foreach (Card card in deck)
            {
                if (card.Rank == RankType.ace)
                {
                    aces.Add(card);
                }
                else
                {
                    remaining.Add(card);
                }
            }
            Random rn = new Random();

            for (int i = 0; i < remaining.Count; i++)
            {
                int j = rn.Next(remaining.Count);
                Card flag = remaining[i];
                remaining[i] = remaining[j];
                remaining[j] = flag;
            }
            deck = remaining.Concat(aces).ToArray();
        }

    }
}

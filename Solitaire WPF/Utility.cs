using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Solitaire_WPF

{
    public enum SuitType
    {
        hearts,
        diamonds,
        clubs,
        spades
    }

    public enum RankType
    {
        ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        jack,
        queen,
        king
    }

    

    public enum Tableaus
    {
        Tableau1, Tableau2, Tableau3, Tableau4, Tableau5, Tableau6, Tableau7
    }




    static class Utility
    {
        
        public static List<TableauPile> tableauPiles = new List<TableauPile>();
        public static List<Foundation> foundations = new List<Foundation>();
        public static Deck deck = new Deck();
        public static StockPile stockPile;
        public static WastePile wastePile;
       


        public static int GetCardRankIndexDictionary(Card card)
        {
            
            var cardRankIndex = new Dictionary<RankType, int>{
                {RankType.ace, 1 },
                {RankType.Two, 2 },
                {RankType.Three, 3 },
                {RankType.Four, 4 },
                {RankType.Five, 5 },
                {RankType.Six, 6 },
                {RankType.Seven, 7 },
                {RankType.Eight, 8 },
                {RankType.Nine, 9 },
                {RankType.Ten, 10 },
                {RankType.jack, 11 },
                {RankType.queen, 12 },
                {RankType.king, 13 }
    };
            return cardRankIndex[card.Rank];
        }

        public static bool CanPlaceCardInOtherTableau(Card crd)
        {
            foreach(TableauPile t in tableauPiles)
            {
                if (t.PeekTopCard() == null)
                {
                    if(crd.Rank == RankType.king)
                    {
                        return true;
                    }
                    
                }
                
                else if(GetCardRankIndexDictionary(t.PeekTopCard()) - GetCardRankIndexDictionary(crd) == 1)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsCardInTableau(Card crd)
        {
            foreach(TableauPile t in tableauPiles)
            {
                if (t.ContainsCardinTableau(crd))
                {
                    return true;
                }
            }
            return false;
        }
        public static TableauPile CardTableau(Card crd)
        {
            foreach(TableauPile t in tableauPiles)
            {
                if (t.ContainsCardinTableau(crd))
                {
                    return t;
                }
            }
            return null;
        }
        public static bool WinCondition()
        {
            foreach (Foundation f in foundations)
            {
                if(!f.IsComplete())
                {
                    return false;
                }
            }
            return true;
        }
        

    }
}

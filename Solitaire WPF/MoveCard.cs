using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls; 

namespace Solitaire_WPF
{
    public class MoveCard
    {

        //takes a single card if it is Face Up, 
        public static void MoveCrd(Card crd)
        {
            if (Utility.ContainsCardInTableau(crd)) // checks if the clicked card is in Tableaus
            {
                TableauPile t = Utility.CardTableau(crd);  //takes the tableau of card
                int idx = t.CardIndexInTableau(crd);       //takes the index of card in that tableau
                int count = t.CardsCount();               // total cards in tableau
                int diff = count - idx;                  // the clicked card position in the tableau
                if (diff == 1)       // if clicked card is top card
                {
                    foreach (Foundation f in Utility.foundations)   // First check in foundation piles if card move there
                    {
                        if (f.isEmpty())     // first check if foundation is nill than only king place is there
                        {
                            if (crd.Rank == RankType.ace)
                            {
                                Card c = t.RemoveTopCard();
                                if (c.Parent != null) 
                                {
                                    ((Panel)c.Parent).Children.Remove(c);  //removes card current parent
                                }
                              
                                f.AddCard(c);
                                if (t.PeekTopCard() != null)
                                {
                                    t.PeekTopCard().FaceUp = true;
                                }
                                if (Utility.WinCondition())  //check if user has won
                                {
                                    MessageBox.Show("You Won the Game");
                                }
                                return;
                            }
                        }
                        else  // Foundation Move End
                        {
                            //if any card is already present in foundation than checks valid move for card

                            if (Utility.GetCardRankIndexDictionary(f.GetTopCard()) == Utility.GetCardRankIndexDictionary(crd) - 1)
                            {
                                if (f.GetTopCard().Suit == crd.Suit)
                                {
                                    Card c = t.RemoveTopCard();
                                    if (c.Parent != null)
                                    {
                                        ((Panel)c.Parent).Children.Remove(c);
                                    }
                                    
                                    f.AddCard(c);

                                    if (t.PeekTopCard() != null)   // if tableau is not empty after removal of card
                                    {
                                        if (!t.PeekTopCard().FaceUp)
                                        {
                                            t.PeekTopCard().FaceUp = true;   // face up the last card of that tableau
                                        }
                                    }
                                    if (Utility.WinCondition())    // check Win Condition in Game
                                    {
                                        MessageBox.Show("You Won the Game");
                                        
                                    }
                                    return;
                                }
                            }
                        }
                    } 
                    foreach (TableauPile tb in Utility.tableauPiles)  // Tableau TO Tableau Move 
                    {
                        if (tb.isEmpty())
                        {
                            if (crd.Rank == RankType.king)
                            {
                                Card c = t.RemoveTopCard();
                                if(c.Parent!=null && c!= null)
                                {
                                    ((Panel)c.Parent).Children.Remove(c);
                                }
                               
                                tb.AddCard(c);
                                if (t.PeekTopCard() != null)
                                {
                                    if (!t.PeekTopCard().FaceUp)
                                    {
                                        t.PeekTopCard().FaceUp = true;
                                    }
                                }

                                return;
                            }
                        }
                        else
                        {
                            Card peekCard = tb.PeekTopCard();
                            if (Utility.GetCardRankIndexDictionary(crd) == Utility.GetCardRankIndexDictionary(tb.PeekTopCard()) - 1)
                            {
                                // In MoveCard class, modify the tableau to tableau movement section:
                                if (peekCard.GetColor() != crd.GetColor())
                                {
                                    Card c = t.RemoveTopCard();  // Remove from source tableau
                                    if (c.Parent != null && c!= null)
                                    {
                                        ((Panel)c.Parent).Children.Remove(c);  // Remove from visual tree
                                    }
                                    tb.AddCard(c);  // Add to destination tableau
                                    if (t.PeekTopCard() != null)
                                    {
                                        t.PeekTopCard().FaceUp = true;
                                    }
                                    return;
                                }
                               
                            }
                        }
                    }
                }
                else      //Multiple Card Movement
                {
                    if (Utility.CanPlaceCardInOtherTableau(crd))
                    {
                        int cardLocation = count - idx;  // card location in tableau
                        CStack temp = new CStack();
                        for (int i = 0; i < cardLocation; i++)
                        {
                            Card c = t.RemoveTopCard();
                            temp.Push(c);
                        }
                        foreach (TableauPile tb in Utility.tableauPiles)
                        {
                            if (tb.isEmpty())   // if empty stack than rank should be king
                            {
                                if (crd.Rank == RankType.king)
                                {
                                    for (int i = 0; i < cardLocation; i++)
                                    {
                                        Card c = temp.Pop();
                                        tb.AddCard(c);
                                        if (t.PeekTopCard() != null)

                                        {
                                            if (!t.PeekTopCard().FaceUp)
                                            {
                                                t.PeekTopCard().FaceUp = true;
                                            }
                                        }
                                    }
                                    return;
                                }
                            }
                            else     // if not empty than check rank index from dictionary
                            {
                                Card peekCard = tb.PeekTopCard();
                                if (Utility.GetCardRankIndexDictionary(crd) == Utility.GetCardRankIndexDictionary(tb.PeekTopCard()) - 1)
                                {
                                    if (peekCard.GetColor() != crd.GetColor())
                                    {
                                        for (int i = 0; i < cardLocation; i++)
                                        {
                                            Card c = temp.Pop();
                                            tb.AddCard(c);
                                            if (i == cardLocation - 1 && t.PeekTopCard() != null)
                                            {
                                                if (!t.PeekTopCard().FaceUp)
                                                {
                                                    t.PeekTopCard().FaceUp = true;
                                                }
                                            }
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                
            }// Foundation and Tableau Move End
            else if (Utility.wastePile.ContainsCard(crd))   // Waste pile card movement
            {
                if(crd!= null)
                {
                    foreach (Foundation f in Utility.foundations)  // first check in tableau
                    {
                        if (f.isEmpty())
                        {
                            if(crd.Rank == RankType.ace)
                            {
                                Utility.wastePile.removeTopCard();
                                f.AddCard(crd);
                                if (Utility.WinCondition())
                                {
                                    MessageBox.Show("You Won the Game");
                                }
                                
                                return;
                            }
                        }
                        else    
                        {
                            if (Utility.GetCardRankIndexDictionary(f.GetTopCard()) == Utility.GetCardRankIndexDictionary(crd) - 1)
                            {
                                if (f.GetTopCard().Suit == crd.Suit)
                                {
                                    Card c = Utility.wastePile.GetTopCard();
                                    Utility.wastePile.removeTopCard();
                                    if (c.Parent != null)
                                    {
                                        ((Panel)c.Parent).Children.Remove(c);
                                    }

                                    f.AddCard(c);
                                    if (Utility.WinCondition())
                                    {
                                        
                                        MessageBox.Show(" asd");
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    foreach(TableauPile tb in Utility.tableauPiles)  // now check in tableau if not foiund
                    {
                        if (tb.isEmpty())
                        {
                            if(crd.Rank == RankType.king)
                            {
                                Utility.wastePile.removeTopCard();
                                tb.AddCard(crd);
                                if (tb.PeekTopCard() != null)
                                {
                                    tb.PeekTopCard().FaceUp = true;
                                }
                                return;
                            }
                        }
                        if (tb.PeekTopCard() != null)
                        {
                            Card c = tb.PeekTopCard();
                            if (Utility.GetCardRankIndexDictionary(crd) == Utility.GetCardRankIndexDictionary(c) - 1)
                            {
                                if (c.GetColor() != crd.GetColor())
                                {
                                    Utility.wastePile.removeTopCard();
                                    tb.AddCard(crd);
                                    if (tb.PeekTopCard() != null)
                                    {
                                        tb.PeekTopCard().FaceUp = true;
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    MessageBox.Show("No position Found");
                }
                else
                {
                    MessageBox.Show("Card was Null");
                }
            }
            else
            {
                MessageBox.Show("Invalid Move"); // foundation card click`
            }
             
        }
        
    }
    
}

using Solitaire.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Solitaire_WPF

{
    public class TableauPile
    {
        private CStack cards;
        public Canvas canvas;
        private int zIndex = 10;
        

        public TableauPile(Canvas canvas)
        {
            
            this.canvas = canvas;
            cards = new CStack();
            
            canvas.VerticalAlignment = VerticalAlignment.Top;
        }
        public void AddCard(Card card)
        {
            if (card.Parent != null)
            {
                ((Panel)card.Parent).Children.Remove(card);
            }
            Canvas.SetZIndex(card, zIndex);
            zIndex+=4;

            cards.Push(card);
            UpdateCardPositions();
        }
        public Card PeekTopCard()
        {
            return cards.Peek();
        }

        public Card RemoveTopCard()
        {
            Card removedCard = cards.Pop();
            if(removedCard != null)
            {
                UpdateCardPositions();
                if (PeekTopCard() != null)
                {
                    PeekTopCard().FaceUp = true;
                    Canvas.SetZIndex(PeekTopCard(), zIndex);
                    zIndex--;
                }
                return removedCard;
            }
            return null;
        }
        public bool isEmpty()
        {
            return cards.IsStackEmpty();
        }
        public int CardsCount()
        {
            return cards.GetCount();
        }
        public void UpdateCardPositions()
        {
            const int VERTICAL_OFFSET = 30; // Space between cards

            List<Card> cards = GetAllCards();
            canvas.Children.Clear();

            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                Canvas.SetLeft(card, 0);

                // Use smaller offset for face-down cards
                int yPosition = i* VERTICAL_OFFSET;

                Canvas.SetTop(card, yPosition);
                Canvas.SetZIndex(card, i); 
                canvas.Children.Add(card);
            }
        }

        public List<Card> GetAllCards()
        {
            return cards.GetAllCardsList();
        }
       
        public bool ContainsCardinTableau(Card card)
        {
            return cards.ContainCard(card);
        }
        public int CardIndexInTableau(Card crd)
        {
            int idx = cards.CardIndex(crd);
            return idx;
        }

    }
    
}

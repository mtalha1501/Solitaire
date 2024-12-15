using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Solitaire_WPF
{
    public class WastePile
    {
        private CStack cards;
        private Canvas canvas;

        public WastePile(Canvas canvas)
        {
            this.canvas = canvas;
            cards = new CStack();
        }

        public void AddCard(Card card)
        {
            // Remove card from current parent if exists
            if (card.Parent != null)
            {
                ((Panel)card.Parent).Children.Remove(card);
            }
            cards.Push(card);
            UpdateDisplay();
        }
        public void removeTopCard()
        {
            if (cards != null)
            {
                cards.Pop();
                UpdateDisplay();
            }
        }
        public CStack RemoveAllCards()
        {
            CStack tempStack = new CStack();
            while (!cards.IsStackEmpty())
            {
                Card card = cards.Pop();
                if (card.Parent != null)
                {
                    ((Panel)card.Parent).Children.Remove(card);
                }
                tempStack.Push(card);
            }
            UpdateDisplay();
            return tempStack;
        }

        private void UpdateDisplay()
        {
            canvas.Children.Clear();
            if (!cards.IsStackEmpty())
            {
                Card topCard = cards.Peek();
                topCard.FaceUp = true;
                Canvas.SetLeft(topCard, 0);
                Canvas.SetTop(topCard, 0);
                canvas.Children.Add(topCard);
            }
        }

        public Card GetTopCard()
        {
            if (!cards.IsStackEmpty())
            {
                return cards.Peek();
            }
            else
            {
                return null;
            }
        }

        public bool IsEmpty()
        {
            return cards.IsStackEmpty();
        }

        public int GetCount()
        {
            return cards.GetCount();
        }
        public bool ContainsCard(Card crd)
        {
            return cards.ContainCard(crd);
        }
    }
}

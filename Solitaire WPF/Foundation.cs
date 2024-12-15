using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Solitaire_WPF
{
    // As cards are just added in Foundation , so it is a stack
    public class Foundation
    {
        private CStack foundation;
        private Canvas canva;
        int zIndex;

        public Foundation(Canvas canva)
        {
            zIndex = 0;    // for bringing card to front of stack
            foundation = new CStack();
            this.canva = canva;
        }

        //add a card to the foundation and update the canvas
        public void AddCard(Card card)
        {
            Canvas.SetZIndex(card,zIndex);
            zIndex++;        // brings card to front of other cards
            foundation.Push(card);
            UpdateCardPositions();   //change position of card

        }
        //check if foundation is empty
        public bool isEmpty()
        {
            return foundation.IsStackEmpty();
        }
        //Get the top card from the foundation without removing it
        public Card GetTopCard()
        {
            return foundation.Peek();
        }

        //check if the foundation is complete,mean it contains all 13 cards
        public bool IsComplete()
        {
            return foundation.GetCount() == 13;
        }

        //update the visual positions of the cards in the foundation canvas
        private void UpdateCardPositions()
        {
            canva.Children.Clear();        //clears all cards in foundation canvas
            CStack tempStack = new CStack();
            while (!foundation.IsStackEmpty())
            {
                tempStack.Push(foundation.Pop());
            }
            while (!tempStack.IsStackEmpty())
            {
                Card card = tempStack.Pop();
                Canvas.SetTop(card, 0);
                Canvas.SetLeft(card, 0);
                canva.Children.Add(card);
                foundation.Push(card);
               
            }
            Canvas.SetTop(canva, 0);
        }
        
    }
}

using Solitaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Solitaire.Data_Structures;

namespace Solitaire_WPF
{
    public class StockPile
    {
        private Node front;
        private Node rear;
        private int count;
        private Canvas canvas;
        private WastePile wastePile;
        private Button drawButton;

        public StockPile(Canvas canvas, WastePile wastePile, Button drawButton)
        {
            this.canvas = canvas;
            this.wastePile = wastePile;
            this.drawButton = drawButton;
            front = null;
            rear = null;
            count = 0;
            UpdateDisplay();
        }

        // Adds a card to the rear of the queue
        public void Enqueue(Card card)
        {
            Node newNode = new Node(card);
            card.FaceUp = false;

            if (rear == null) // If queue is empty
            {
                front = newNode;
                rear = newNode;
            }
            else
            {
                rear.Next = newNode;
                rear = newNode;
            }

            count++;
            UpdateDisplay();
        }

        // Removes a card from the front of the queue
        public Card Dequeue()
        {
            if (IsStockEmpty())
            {
                return null;
            }

            Card dequeuedCard = front.crd;
            front = front.Next;

            if (front == null) // If the queue is now empty
            {
                rear = null;
            }

            count--;
            return dequeuedCard;
        }

        // Returns the card at the front of the queue without removing it
        public Card PeekFront()
        {
            if (IsStockEmpty())
            {
                return null;
            }
            return front.crd;
        }

        private void UpdateDisplay()
        {
            canvas.Children.Clear();

            if (!IsStockEmpty())
            {
                Card topCard = PeekFront();
                topCard.FaceUp = false;
                Canvas.SetTop(topCard, 0);
                Canvas.SetLeft(topCard, 0);
                canvas.Children.Add(topCard);

                drawButton.Content = "Draw Card";
                drawButton.IsEnabled = true;
            }
            else
            {
                if (!wastePile.IsEmpty())
                {
                    drawButton.Content = "Reset Stack";
                    drawButton.IsEnabled = true;
                }
                else
                {
                    drawButton.Content = "Empty";
                    drawButton.IsEnabled = false;
                }
            }
        }

        public void HandleDrawButton()  
        {
            if (IsStockEmpty())
            {
                if (!wastePile.IsEmpty())
                {
                    RecycleWastePile();
                }
            }
            else
            {
                DealToWaste();
            }
        }

        private void DealToWaste()
        {
            if (!IsStockEmpty())
            {
                Card card = Dequeue();
                card.FaceUp = true;
                wastePile.AddCard(card);
                UpdateDisplay();
            }
        }

        private void RecycleWastePile()
        {
            CStack tempStack = wastePile.RemoveAllCards();

            while (!tempStack.IsStackEmpty())
            {
                Card card = tempStack.Pop();
                card.FaceUp = false;
                Enqueue(card);
            }

            UpdateDisplay();
        }

        // Check if the stock pile is empty
        public bool IsStockEmpty()
        {
            return front == null;
        }

        // Get the total count of cards in the stock pile
        public int GetCount()
        {
            return count;
        }
    }
}

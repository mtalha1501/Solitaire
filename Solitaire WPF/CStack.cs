using Solitaire.Data_Structures;
using Solitaire_WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Solitaire_WPF
{
    // Stack is Implemented in LinkedList
    public class CStack
    {
        private Node Head;
        private int Count;

        public CStack()
        {
            this.Head = null;
            this.Count = 0;
        }
        //As the cards are added and removed at the end of the Tableau  
        //So push,pop functions are insert at tail and remove at tail.

        // Push adds card at the end of stack
        public void Push(Card crd)
        {
            Node n = new Node(crd);
            if (Head == null)
            {
                Head = n;
                Count++;
                return;
            }
            Node temp = Head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            temp.Next = n;
            Count++;
        }
        //Pop returns last Card in stack, and remove it from stack 
        public Card Pop()
        {
            if (Head == null)
            {
                return null;
            }
            if (Head.Next == null)
            {
                Card t = Head.crd;
                Head = null;
                Count--;
                return t;
            }
            Node temp = Head;
            while (temp.Next.Next != null)
            {
                temp = temp.Next;
            }
            Card n = temp.Next.crd;
            temp.Next = null;
            Count--;
            return n;

        }
        // Returns last card in stack ,
        public Card Peek()
        {
            if (IsStackEmpty())
            {
                return null;
            }
            if(Head.Next == null)
            {
                return Head.crd;
            }
            Node temp = Head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            Card toPeek = temp.crd;
            return toPeek;
        }
        public bool IsStackEmpty()
        {
            return Head == null;
        }
        
        public int GetCount()
        {
            return Count;
        }
        //returns  list of the cards present in stack
        public List<Card> GetAllCardsList()
        {
            List<Card> cards = new List<Card>();
            if (Head == null)
                return cards;

            List<Card> crds = new List<Card>();
            Node temp = Head;
            while (temp != null)
            {
                crds.Add(temp.crd);
                temp = temp.Next;
            }
            
            return crds;
        }
        // checks if a specific card is present in stack
        public bool ContainCard(Card card)
        {
            Node temp = Head;
            while (temp != null)
            {
                if (temp.crd == card)
                {
                    return true;
                }
                temp = temp.Next;
            }
            return false;
        }
        // returns card index in the stack
        public int CardIndex(Card card)
        { 
            int idx = 0;
            Node temp = Head;
            while (temp != null)
            {
                if (temp.crd == card)
                {
                    return idx;
                }
                idx++;
                temp = temp.Next;
            }
            return -1;
        }
        
        
    }
}
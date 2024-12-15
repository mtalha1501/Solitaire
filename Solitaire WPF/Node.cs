using Solitaire_WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Data_Structures
{
    public class Node
    {
        public Card crd;
        public Node Next;
        public Node(Card card)
        {
            this.crd = card;
            Next = null;
        }
    }
}
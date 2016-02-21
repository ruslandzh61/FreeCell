using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class OrderedCascade
    {
        private LinkedList<Card> cards;
        private Cascade cascade;

        public OrderedCascade(Cascade cascade)
        {
            this.cascade = cascade;
            cards = new LinkedList<Card>();
        }

        public void Add(Card card)
        {
            cards.AddLast(card);
        }

        public void SetSelected(bool selected)
        {
            foreach (Card c in cards)
            {
                c.IsSelected = selected;
            }
        }

        public Card GetFirst()
        {
            return cards.First.Value;
        }

        public LinkedList<Card> Cards
        {
            get
            {
                return cards;
            }
        }

        public Cascade Cascade
        {
            get
            {
                return cascade;
            }
        }

        public int Count
        {
            get
            {
                return cards.Count;
            }
        }
    }
}

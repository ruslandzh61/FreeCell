using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class Cascade
    {
        LinkedList<Card> cards;
        int x;
        int y;
        private const int DeltaCardY = 20;

        public Cascade(int numberOfInitialCards, int x, int y, deck deck)
        {
            cards = new LinkedList<Card>();
            for (int i = 0; i < numberOfInitialCards; ++i)
            {
                Card card = deck.Top();
                card.X = x;
                card.Y = y + i * DeltaCardY;
                cards.AddLast(deck.Top());
                deck.Pop();
            }
            this.x = x;
            this.y = y;
        }

        public Cascade(int x, int y)
        {
            cards = new LinkedList<Card>();
            this.x = x;
            this.y = y;
        }

        public Card SelectCard(int x, int y)
        {
            Card selectedCard = null;
            foreach (Card card in cards)
            {
                if (x >= card.X && x <= card.X + Card.CardWidth && y >= card.Y && y <= card.Y + Card.CardHeight)
                {
                    selectedCard = card;
                }
            }
            return selectedCard;
        }

        public Cascade GetCopy()
        {
            Cascade cascade = new Cascade(X, Y);
            foreach (Card card in Cards)
            {
                Card newCard = card.GetCopy();
                cascade.Cards.AddLast(newCard);
            }
            return cascade;
        }

        public bool IsClicked(int mouseX, int mouseY)
        {
            if (cards.Count == 0)
            {
                if (mouseX >= X && mouseX <= X + Card.CardWidth && mouseY >= Y && mouseY <= Y + Card.CardHeight)
                {
                    return true;
                }
            }
            else
            {
                Card card = SelectCard(mouseX, mouseY);
                if (card != null)
                {
                    return true;
                }
            }

            return false;
        }

        public OrderedCascade SelectOrderedCascade(Card card)
        {
            if (cards.Count == 0)
            {
                return null;
            }

            OrderedCascade cascade = new OrderedCascade(this);
            cascade.Add(card);
            LinkedListNode<Card> node = cards.Find(card);
            node = node.Next;
            while (node != null)
            {
                if (!IsOrderedCards(node.Previous.Value, node.Value))
                {
                    return null;
                }
                cascade.Add(node.Value);
                node = node.Next;
            }

            return cascade;
        }

        public bool TryMoveOrderedCascade(OrderedCascade orderedCascade, int maxNumberOfSuperMoves)
        {
            if (orderedCascade.Count > maxNumberOfSuperMoves)
            {
                return false;
            }
            Card cardToMove = orderedCascade.GetFirst();
            if (cards.Count == 0 || IsOrderedCards(cards.Last.Value, cardToMove))
            {
                foreach (Card c in orderedCascade.Cards)
                {
                    c.X = x;
                    c.Y = y + cards.Count * DeltaCardY;
                    cards.AddLast(c);
                    orderedCascade.Cascade.Cards.Remove(c);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryAddCardFromCell(OpenCell cell)
        {
            Card card = cell.Top();
            if (card != null)
            {
                if (cards.Count == 0 || IsOrderedCards(cards.Last.Value, card))
                {
                    card.X = x;
                    card.Y = y + cards.Count * DeltaCardY;
                    cards.AddLast(card);

                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Method returns true if two cards are ordered.
        /// For ex., first card is black 3, second card is red 2 then method returns true.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Method returns true if two cards are ordered.</returns> 
        public bool IsOrderedCards(Card first, Card second)
        {
            bool hasDifferntColors = (first.IsRed() && !second.IsRed()) || (!first.IsRed() && second.IsRed());
            return first.Rank == (second.Rank + 1) && hasDifferntColors;
        }

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public LinkedList<Card> Cards
        {
            get
            {
                return cards;
            }
        }
    }
}

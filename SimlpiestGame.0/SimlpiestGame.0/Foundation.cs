using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class Foundation
    {
        Stack<Card> foundation;
        int suit;
        int x;
        int y;
        
        public Foundation(int suit, int x, int y)
        {
            foundation = new Stack<Card>();
            this.suit = suit;
            this.x = x;
            this.y = y;
        }

        public Card Top()
        {
            if (foundation.Count == 0)
            {
                return null;
            }
            return foundation.Peek();
        }

        public void Push(Card card)
        {
            foundation.Push(card);
        }

        public bool TryAddCard(Card card)
        {
            if (card.Suit == Suit)
            {
                if (foundation.Count == 0)
                {
                    if (card.Rank == 0)
                    {
                        card.X = x;
                        card.Y = y;
                        foundation.Push(card);
                        return true;
                    }
                }
                else if ((Top().Rank + 1) == card.Rank)
                {
                    card.X = x;
                    card.Y = y;
                    foundation.Push(card);
                    return true;
                }
            }

            return false;
        }

        public bool TryMoveOrderedCascade(OrderedCascade orderedCascade)
        {
            if (orderedCascade.Count == 1)
            {
                Card cardToMove = orderedCascade.GetFirst();
                if (TryAddCard(cardToMove))
                {
                    orderedCascade.Cascade.Cards.Remove(cardToMove);
                    return true;
                }
            }

            return false;
        }

        public int Suit
        {
            get
            {
                return suit;
            }
        }

        public int Count
        {
            get
            {
                return foundation.Count;
            }
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

        public bool IsClicked(int mouseX, int mouseY)
        {
            if (mouseX >= X && mouseX <= X + Card.CardWidth && mouseY >= Y && mouseY <= Y + Card.CardHeight)
            {
                return true;
            }

            return false;
        }
    }
}

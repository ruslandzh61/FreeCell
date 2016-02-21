using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class OpenCell
    {
        Stack<Card> cell;
        int x;
        int y;

        public OpenCell()
        {
            cell = new Stack<Card>();
        }

        public bool TryMove(OrderedCascade orderedCascade)
        {
            if (cell.Count == 0 && orderedCascade.Cards.Count == 1)
            {
                Card card = orderedCascade.Cards.Last.Value;
                card.X = x;
                card.Y = y;
                cell.Push(card);
                orderedCascade.Cascade.Cards.Remove(card);
                return true;
            }

            return false;
        }

        public Card Top()
        {
            if (cell.Count == 1)
            {
                return cell.Peek();
            }

            return null;
        }

        public void Pop()
        {
            cell.Pop();
        }

        public void Push(Card card)
        {
            cell.Push(card);
        }

        public bool IsCellClicked(int mouseX, int mouseY)
        {
            if (mouseX >= X && mouseX <= X + Card.CardWidth && mouseY >= Y && mouseY <= Y + Card.CardHeight)
            {
                return true;
            }

            return false;
        }

        public int Count
        {
            get
            {
                return cell.Count;
            }
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
    }
}

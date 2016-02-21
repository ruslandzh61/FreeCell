using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class Tableau
    {
        private LinkedList<Cascade> cascades;

        public Tableau(deck deck, int x, int y, int width)
        {
            cascades = new LinkedList<Cascade>();
            for (int i = 0; i < 4; i++)
            {
                cascades.AddLast(new Cascade(7, x + i * width / 8, y, deck));
            }
            for (int i = 4; i < 8; i++)
            {
                cascades.AddLast( new Cascade(6, x + i * width / 8, y, deck));
            }
        }

        public Tableau()
        {
            cascades = new LinkedList<Cascade>();
        }

        public LinkedList<Cascade> Cascades
        {
            get
            {
                return cascades;
            }
        }

        public Cascade GetClickedCascade(int x, int y)
        {
            foreach (Cascade cascade in cascades)
            {
                if (cascade.IsClicked(x, y))
                {
                    return cascade;
                }
            }

            return null;
        }

        public Tableau GetCopy()
        {
            Tableau t = new Tableau();
            foreach (Cascade cascade in cascades)
            {
                Cascade newCascade = cascade.GetCopy();
                t.cascades.AddLast(newCascade);
            }

            return t;
        }

        public int GetNumberOfEmptyCascades()
        {
            int number = 0;
            foreach (Cascade cascade in cascades)
            {
                if (cascade.Cards.Count == 0)
                {
                    number++;
                }
            }

            return number;
        }

        public OrderedCascade FindOrderedCascadeByCard(Card card)
        {
            foreach (Cascade cascade in cascades)
            {
                foreach (Card c in cascade.Cards)
                {
                    if (c.IsEqual(card))
                    {
                        return cascade.SelectOrderedCascade(c);
                    }
                }
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class deck
    {
        List<Card> data;

        public deck()
        {
            data = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    data.Add(new Card(j, i, 0, 0, false));
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            for (int i = data.Count - 1; i > 0; --i)
            {
                int k = rnd.Next(i + 1);
                Card t = data[k];
                data[k] = data[i];
                data[i] = t;
            }
        }

        public Card Top()
        {
            return data[data.Count - 1];
        }

        public void Pop()
        {
            data.Remove(data[data.Count - 1]);
        }

        public bool IsEmpty()
        {
            return data.Count == 0;
        }
    }
}

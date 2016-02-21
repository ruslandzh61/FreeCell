using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimlpiestGame._0
{
    class GameData
    {
        public Foundation[] Foundations;
        public OpenCell[] Cells;
        public Tableau Tableau;
        public OrderedCascade SelectedOrderedCascade = null;
        public OpenCell SelectedCellWithCard = null;

        public GameData(Tableau tableau, Foundation[] foundations, OpenCell[] cells, OrderedCascade selectedOrderedCascade, OpenCell selectedCell)
        {
            Cells = new OpenCell[cells.Length];
            for (int i = 0; i < 4; i++)
            {
                Cells[i] = new OpenCell();
                Cells[i].X = cells[i].X;
                Cells[i].Y = cells[i].Y;
                Card card = cells[i].Top();
                if(card != null)
                {
                    Cells[i].Push(card.GetCopy());
                    if(selectedCell != null && card.IsEqual(selectedCell.Top()))
                    {
                        SelectedCellWithCard = Cells[i];
                    }
                }
            }

            Foundations = new Foundation[4];
            for (int i = 0; i < 4; ++i)
            {
                Foundations[i] = new Foundation(foundations[i].Suit, foundations[i].X, foundations[i].Y);
                Card card = foundations[i].Top();
                if (card != null)
                {

                    Foundations[i].Push(card.GetCopy());
                }
            }

            Tableau = tableau.GetCopy();

            if (selectedOrderedCascade != null)
            {
                SelectedOrderedCascade = Tableau.FindOrderedCascadeByCard(selectedOrderedCascade.Cards.First.Value);
            }
        }
    }
}

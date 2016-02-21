using System;

namespace SimlpiestGame._0
{
    class Card
    {
        public string[] StrRanks = { "ace","two","three","four","five", "six","seven","eight","nine","ten","jack","gueen","king"};
        public string[] StrSuit = { "clubs","diamonds", "hearts","spades"};
        readonly int suit;
        readonly int rank;
        bool isSelected;

        public const int CardWidth = 84;
        public const int CardHeight = 122;

        public Card(int rank, int suit, int x, int y, bool isSelected)
        {
            if (rank < 0 || 12 < rank)
            {
                throw new ArgumentException("Bad rank of Card");
            }
            if (suit < 0 || 3 < suit)
            {
                throw new ArgumentException("Bad suit of Card");
            }
            X = x;
            Y = y;
            this.rank = rank;
            this.suit = suit;
            this.isSelected = isSelected;
        }

        public bool IsRed()
        {
            return suit == 2 || suit == 1;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Rank
        {
            get
            {
                return rank;
            }
        }

        public int Suit
        {
            get
            {
                return suit;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
            }
        }

        public override string ToString()
        {
            string res =  StrRanks[rank] + " of " + StrSuit[suit];
            if (isSelected)
            {
                res += "selected";
            }
            return res;
        }

        public bool IsEqual(Card card)
        {
            if (card == null)
            {
                return false;
            }

            return card.Suit == Suit && card.Rank == Rank & card.X == X && card.Y == Y;
        }

        public Card GetCopy()
        {
            return new Card(Rank, Suit, X, Y, IsSelected);
        }
    }
}

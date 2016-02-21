using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SimlpiestGame._0
{
    class Drawer
    {
        Image regCards;
        Image invCards;
        Image suits;
        float cardWidth;
        float cardHeight;
        float suitWidth;
        float suitHeight;
        Form1 form;
        public Drawer()
        {
            regCards = Image.FromFile("Cards.png");
            invCards = Image.FromFile("InvCards.png");
            suits = Image.FromFile("Suits.png");
            cardWidth = (float)regCards.Width / 13;
            cardHeight = (float)regCards.Height / 5;
            suitWidth = (float)suits.Width / 2;
            suitHeight = (float)suits.Height / 2;


            form = new Form1();
        }

        public void DrawCard(Graphics g, Card card)
        {
            Rectangle destRect = new Rectangle(card.X, card.Y, Card.CardWidth, Card.CardHeight);
            Image img = card.IsSelected ? invCards : regCards;
            g.DrawImage(img, destRect, 
                card.Rank * cardWidth, cardHeight * card.Suit,
                cardWidth, cardHeight, GraphicsUnit.Pixel);
        }

        public void DrawTableau(Graphics g, Tableau tableau)
        {
            foreach (Cascade cascade in tableau.Cascades)
            {
                DrawCascade(g, cascade);
            }
        }

        public void DrawCascade(Graphics g, Cascade cascade)
        {
            foreach (Card card in cascade.Cards)
            {
                DrawCard(g, card);
            }
        }

        public void DrawFoundaion(Graphics g, Foundation foundation)
        {
            Rectangle destRect = new Rectangle(foundation.X, foundation.Y, Card.CardWidth, Card.CardHeight);
            if (foundation.Count == 0)
            {
                switch (foundation.Suit)
                {
                    case 0:
                        g.DrawImage(suits, destRect, suitWidth * foundation.Suit, suitHeight * foundation.Suit, suitWidth, suitHeight, GraphicsUnit.Pixel);
                        break;
                    case 1:
                        g.DrawImage(suits, destRect, suitWidth, 0, suitWidth, suitHeight, GraphicsUnit.Pixel);
                        break;
                    case 2:
                        g.DrawImage(suits, destRect, 0, suitHeight, suitWidth, suitHeight, GraphicsUnit.Pixel);
                        break;
                    case 3:
                        g.DrawImage(suits, destRect, suitWidth, suitHeight, suitWidth, suitHeight, GraphicsUnit.Pixel);
                        break;
                }
            }
            else
            {
                DrawCard(g, foundation.Top());
            }
        }

        public void DrawOpenCell(Graphics g, OpenCell cell)
        {
            Rectangle destRect = new Rectangle(cell.X, cell.Y, Card.CardWidth, Card.CardHeight);
            if (cell.Count == 0)
            {
                g.DrawImage(regCards, destRect, cardWidth * 2, CardHeight * 4, cardWidth, CardHeight, GraphicsUnit.Pixel);
            }
            else
            {
                DrawCard(g, cell.Top());
            }
        }

        public int CardWidth
        {
            get
            {
                return (int)cardWidth;
            }
        }
        public int CardHeight
        {
            get
            {
                return (int)cardHeight;
            }
        }

        public int SuitWidth
        {
            get
            {
                return (int)suitWidth;
            }
        }
        public int SuitHeight
        {
            get
            {
                return (int)suitHeight;
            }
        }
    }
}

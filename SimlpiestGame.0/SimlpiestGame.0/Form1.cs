using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimlpiestGame._0
{
    public partial class Form1 : Form
    {
        Drawer drawer;
        Foundation[] foundations;
        OpenCell[] cells;
        //deck deck;
        Tableau tableau;
        OrderedCascade selectedOrderedCascade = null;
        OpenCell selectedCellWithCard = null;
        Stack<GameData> gameData;
        GameData initialGameState = null;
        const string HelpMessage = "Press Z to undo the last action; Press R to restart the game; Press N to start new a game;\n";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                drawer = new Drawer();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Error.... " + exc.Message);
                Application.Exit();
            }

            NewGame();
            //labelState.Text = deck.Top().ToString();
        }

        private void NewGame()
        {
            cells = new OpenCell[4];
            for (int i = 0; i < 4; i++)
            {
                int xc = Width / 2 - Width / 8 * (i + 1);
                cells[i] = new OpenCell();
                cells[i].X = xc;
                cells[i].Y = 0;
            }

            foundations = new Foundation[4];
            for (int i = 0; i < 4; ++i)
            {
                foundations[i] = new Foundation(i, Width / 2 + Width / 8 * i, 0);
            }
            //deck = new deck();
            tableau = new Tableau(new deck(), 0, Card.CardHeight + 20, Width);

            gameData = new Stack<GameData>();

            selectedOrderedCascade = null;
            selectedCellWithCard = null;

            // save initial game state, to be able to restart the game
            initialGameState = new GameData(tableau, foundations, cells, selectedOrderedCascade, selectedCellWithCard);
        }

        private void InitGameFromState(GameData state)
        {
            tableau = state.Tableau;
            foundations = state.Foundations;
            cells = state.Cells;
            selectedCellWithCard = state.SelectedCellWithCard;
            selectedOrderedCascade = state.SelectedOrderedCascade;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // draw foundation
            for (int i = 0; i < 4; ++i)
            {
                drawer.DrawFoundaion(e.Graphics, foundations[i]);
            }

            // draw free cells
            for (int i = 0; i < 4; ++i)
            {
                drawer.DrawOpenCell(e.Graphics, cells[i]);
            }

            //draw tableau
            drawer.DrawTableau(e.Graphics, tableau);

            labelState.Text = HelpMessage + "Number of SuperMoves: " + GetMaxNumberOfSuperMoves().ToString();
        }

        private int GetClickedCellIndex(int mouseX, int mouseY)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (cells[i].IsCellClicked(mouseX, mouseY))
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetClickedFoundationIndex(int mouseX, int mouseY)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (foundations[i].IsClicked(mouseX, mouseY))
                {
                    return i;
                }
            }

            return -1;
        }

        private void SelectOrderedCascadeFromCascade(Cascade cascade, int mouseX, int mouseY)
        {
            Card card = cascade.SelectCard(mouseX, mouseY);
            OrderedCascade orderedCascade = cascade.SelectOrderedCascade(card);
            if (orderedCascade != null)
            {
                UnselectOrderedCascade();
                UnselectCellWithCard(false);
                selectedOrderedCascade = orderedCascade;
                selectedOrderedCascade.SetSelected(true);
            }
        }

        private void UnselectOrderedCascade()
        {
            if (selectedOrderedCascade != null)
            {
                selectedOrderedCascade.SetSelected(false);
                selectedOrderedCascade = null;
            }
        }

        private void UnselectCellWithCard(bool removeCard)
        {
            if (selectedCellWithCard != null)
            {
                selectedCellWithCard.Top().IsSelected = false;
                if (removeCard)
                {
                    selectedCellWithCard.Pop();
                }
                selectedCellWithCard = null;
            }
        }

        private int GetMaxNumberOfSuperMoves()
        {
            return (GetNumberOfEmptyCells() + 1) * (int)Math.Pow(2, tableau.GetNumberOfEmptyCascades());
        }

        private int GetNumberOfEmptyCells()
        {
            int number = 0;
            for (int i = 0; i < 4; ++i)
            {
                if (cells[i].Count == 0)
                {
                    number++;
                }
            }

            return number;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            GameData currentState = new GameData(tableau, foundations, cells, selectedOrderedCascade, selectedCellWithCard);
            gameData.Push(currentState);

            Cascade clickedCascade = tableau.GetClickedCascade(e.X, e.Y);
            int clickedCellIndex = GetClickedCellIndex(e.X, e.Y);
            int clickedFoundationIndex = GetClickedFoundationIndex(e.X, e.Y);

            if (clickedCascade != null)
            {
                if (selectedCellWithCard != null)
                {
                    // move card
                    bool isMoved = clickedCascade.TryAddCardFromCell(selectedCellWithCard);
                    if (isMoved)
                    {
                        UnselectCellWithCard(true);
                    }
                    else
                    {
                        SelectOrderedCascadeFromCascade(clickedCascade, e.X, e.Y);
                    }
                }
                else if (selectedOrderedCascade != null)
                {
                    // move ordered cascade
                    bool isMoved = clickedCascade.TryMoveOrderedCascade(selectedOrderedCascade, GetMaxNumberOfSuperMoves());
                    if (isMoved)
                    {
                        UnselectOrderedCascade();
                    }
                    else
                    {
                        SelectOrderedCascadeFromCascade(clickedCascade, e.X, e.Y);
                    }
                }
                else
                {
                    SelectOrderedCascadeFromCascade(clickedCascade, e.X, e.Y);
                }
            }
            else if (clickedCellIndex != -1)
            {
                Card card = cells[clickedCellIndex].Top();
                if (card != null)
                {
                    UnselectCellWithCard(false);
                    UnselectOrderedCascade();

                    // select new cell with card
                    card.IsSelected = true;
                    selectedCellWithCard = cells[clickedCellIndex];
                }
                else if (selectedOrderedCascade != null)
                {
                    if (cells[clickedCellIndex].TryMove(selectedOrderedCascade))
                    {
                        UnselectOrderedCascade();
                    }
                }
            }
            else if (clickedFoundationIndex != -1)
            {
                if (selectedCellWithCard != null)
                {
                    bool isMoved = foundations[clickedFoundationIndex].TryAddCard(selectedCellWithCard.Top());
                    if (isMoved)
                    {
                        UnselectCellWithCard(true);
                    }
                }
                else if (selectedOrderedCascade != null)
                {
                    bool isMoved = foundations[clickedFoundationIndex].TryMoveOrderedCascade(selectedOrderedCascade);
                    if (isMoved)
                    {
                        UnselectOrderedCascade();
                    }
                }
            }

            Invalidate();

            if (foundations[0].Count == 13 && foundations[1].Count == 13 && foundations[2].Count == 13 && foundations[3].Count == 13)
            {
                MessageBox.Show("Congratulation. You win!!!");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    if (gameData.Count != 0)
                    {
                        GameData prevState = gameData.Pop();
                        InitGameFromState(prevState);
                        Invalidate();
                    }
                    break;
                case Keys.N:
                    NewGame();
                    Invalidate();
                    MessageBox.Show("New game started.");
                    break;
                case Keys.R:
                    InitGameFromState(initialGameState);
                    // save initial game state, to be able to restart the game
                    initialGameState = new GameData(tableau, foundations, cells, selectedOrderedCascade, selectedCellWithCard);
                    Invalidate();
                    MessageBox.Show("Game restarted.");
                    break;
            }
        }
    }
}

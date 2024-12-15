using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Solitaire_WPF.Card;
using System.Windows.Threading;

namespace Solitaire_WPF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Utility.wastePile = new WastePile(WastePileCanva);
            Utility.stockPile = new StockPile(StockpileCanva, Utility.wastePile, DrawButton);
            DrawButton.IsEnabled = true;  //Enabled DrawButton
            Utility.deck.EasyShuffle();

            InitializeTableaus();
            DealCardsToTableaus();
            InitializeFoundations();
            DealRemainingCardsToStockPile();
        }

        private void InitializeTableaus()  //initialize tableau piles list in Utility 
        {
            Canvas[] tableauCanvases = { Tableau1, Tableau2, Tableau3, Tableau4, Tableau5, Tableau6, Tableau7 };

            for (int i = 0; i < 7; i++)
            {
                Utility.tableauPiles.Add(new TableauPile(tableauCanvases[i]));
            }
        }
        private void DealCardsToTableaus()    // Display Cards In Tableaus
        {
            int cardIndex = 0;

            // Deal cards to each tableau, with increasing number of cards per stack
            for (int tableau = 0; tableau < 7; tableau++)
            {
                for (int cards = 0; cards <= tableau; cards++)
                {
                    Card card = Utility.deck.deck[cardIndex++];
                    if (cards == tableau)    // Face up only the last card in each pile
                    {
                        card.FaceUp = true;
                    }
                    else
                    {
                        card.FaceUp = false;
                    }

                    Utility.tableauPiles[tableau].canvas.Children.Add(card);   // i Added for bug fix when multiple card move disappear
                    Utility.tableauPiles[tableau].AddCard(card);

                }
            }
        }
        private void InitializeFoundations() //Initialize All Foundations
        {
            
            
            Foundation foundation1 = new Foundation(Foundation1);
            Utility.foundations.Add(foundation1);

            Foundation foundation2 = new Foundation(Foundation2);
            Utility.foundations.Add(foundation2);

            
            Foundation foundation3 = new Foundation(Foundation3);
            Utility.foundations.Add(foundation3);

            Foundation foundation4 = new Foundation(Foundation4);
            Utility.foundations.Add(foundation4);
        }

        private void DealRemainingCardsToStockPile()     // Add the remaining cards to the stock pile
        {
            
            for (int i = 28; i < 52; i++)
            {
                Utility.stockPile.Enqueue(Utility.deck.deck[i]);
            }
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)       //StockPile Draw Card
        {
            Utility.stockPile.HandleDrawButton();
        }

    }
}

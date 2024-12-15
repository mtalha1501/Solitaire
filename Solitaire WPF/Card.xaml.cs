using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Solitaire_WPF
{
    public partial class Card : UserControl
    {
        //Attributes are made Properties for easy Access and Use
        public SuitType Suit 
        { 
            get;
            set;
        }
        public RankType Rank 
        { 
            get;
            set; 
        }


        private bool faceUp;   
        private string color;

        //This property Automatically updates the card image based on faceup value
        public bool FaceUp
        {
            get
            {
                return faceUp;
            }
            set
            {
                faceUp = value;
                UpdateCardImage();
            }
        }

        public Card(SuitType suit, RankType rank, bool isFaceUp = false)
        {
            InitializeComponent();
            
            Suit = suit;
            Rank = rank;
            FaceUp = isFaceUp;
            if(suit == SuitType.hearts || suit == SuitType.diamonds)
            {
                this.color = "Red";
            }
            else
            {
                this.color = "Black";
            }
            //Mouse Double Click Function
            this.MouseDoubleClick += (s, e) => this.OnCardClick(s, e);
        }
        public string GetColor()
        {
            return this.color;
        }
        //Updates the Card Image
        private void UpdateCardImage()
        {
            string imagePath = "";
            if (FaceUp)
            {
                imagePath = $"G:\\My CS\\Semester 3\\DSA Lab\\Mid Project - Solitaire Game\\Solitaire WPF\\Solitaire WPF\\card-images\\{Rank}_of_{Suit}.png";
            }
            else
            {
                imagePath = "G:\\My CS\\Semester 3\\DSA Lab\\Mid Project - Solitaire Game\\Solitaire WPF\\Solitaire WPF\\card-images\\back-side.png";
            }
            CardImage.Source = new BitmapImage(new Uri(imagePath));
        }
        //On card Double click Function
        public void OnCardClick(object sender,MouseButtonEventArgs e)
        {
            if (this.FaceUp)
            {
                MoveCard.MoveCrd(this);


                //Check win condition after each card move
                if (Utility.WinCondition())
                {
                    WinPage winPage = new WinPage();
                    winPage.WindowState = WindowState.Maximized;
                    winPage.Show();
                }                
            }
        }
        
       
    }
}

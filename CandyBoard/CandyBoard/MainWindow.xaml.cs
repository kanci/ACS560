using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CandyBoard {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public ObservableCollection<CandyPiece> Pieces { get; set; }


        public MainWindow() {
            Pieces = new ObservableCollection<CandyPiece>();
            InitializeComponent();
            DataContext = Pieces;
            NewGame();
        }

        private void NewGame() {
            Pieces.Clear();



            for(int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if((i==j) || (i+4==j) || (j+4==i)) { 
                        Pieces.Add(new CandyPiece() { Row = i, Column = j, Type = CandyPieceTypes.candies1});
                    }
                    if ((i+1==j) || (i+5==j) || (j+5==i) || (j+1==i))
                    {
                        Pieces.Add(new CandyPiece() { Row = i, Column = j, Type = CandyPieceTypes.candies2});
                    }
                    if ((i+2==j) || (i+6 == j)||(j+6==i) || (j+2==i))
                    {
                        Pieces.Add(new CandyPiece() { Row = i, Column = j, Type = CandyPieceTypes.candies3});
                    }
                    if ((i+3==j) || (i+7 == j)||(j+7==i) || (j+3==i))
                    {
                        Pieces.Add(new CandyPiece() { Row = i, Column = j, Type = CandyPieceTypes.candies4});
                    }
                }
            }
            
        }

    }

    //ViewModel
    public class CandyPiece : INotifyPropertyChanged {

        public CandyPieceTypes Type { get; set; }

        private int _row;
        public int Row {
            get {
                return _row;
            }
            set {
                _row = value;
                OnPropertyChanged("Row");
            }
        }
        private int _column;
        public int Column {
            get {
                return _column;
            }
            set {
                _column = value;
                OnPropertyChanged("Column");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ImageSource {
          //  get { return "candies/" + (IsBlack ? "Black" : "White") + Type.ToString() + ".png"; }
            
            
            get { return "candies/" + Type.ToString() + ".bmp"; }
        }
    }

    public enum CandyPieceTypes {
        //Pawn,
        //Tower,
        //Knight,
        //Bishop,
        //Queen,
        //King,
        candies1,
        candies2,
        candies3,
        candies4,
    }
}



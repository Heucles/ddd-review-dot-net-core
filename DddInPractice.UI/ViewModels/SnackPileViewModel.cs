using System;
using DddInPractice.Logic;

namespace DddInPractice.UI.ViewModels
{
    public class SnackPileViewModel
    {
        private readonly SnackPile _snackPile;

        public string Price => _snackPile.Price.ToString("C2");
        public int Amount => _snackPile.Quantity;
        
        public String Image => "images/" + _snackPile.Snack.Name + ".png";

        public String QuantityLeftLabelId => _snackPile.Snack.Name + "_qtleft";

        public String SnackTitle => _snackPile.Snack.Name;

        public SnackPileViewModel(SnackPile snackPile)
        {
            _snackPile = snackPile;
        }
    }
}
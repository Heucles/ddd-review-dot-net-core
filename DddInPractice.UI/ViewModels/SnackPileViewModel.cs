using System;
using DddInPractice.Logic;

namespace DddInPractice.UI.ViewModels
{
    public class SnackPileViewModel
    {
        private readonly SnackPile _snackPile;

        public string Price => _snackPile.Price.ToString("C2");
        public int Amount => _snackPile.Quantity;
        public int ImageWidth => GetImageWidth(_snackPile.Snack);
        public String Image => "images/" + _snackPile.Snack.Name + ".png";

        public String QuantityLeftLabelId => _snackPile.Snack.Name + "_qtleft";

        public SnackPileViewModel(SnackPile snackPile)
        {
            _snackPile = snackPile;
        }

        private int GetImageWidth(Snack snack)
        {
            if (snack == Snack.Chocolate)
                return 120;

            if (snack == Snack.Soda)
                return 70;

            if (snack == Snack.Gum)
                return 70;

            throw new ArgumentException();
        }
    }
}
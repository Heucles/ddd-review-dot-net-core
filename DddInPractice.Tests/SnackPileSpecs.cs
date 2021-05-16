using Xunit;
using FluentAssertions;
using System;

using static DddInPractice.Logic.SnackMachines.Snack;
using DddInPractice.Logic.SnackMachines;

namespace DddInPractice.Tests
{
    public class SnackPileSpecs
    {

        [Fact]
        public void Cannot_create_snack_with_negative_quantity()
        {

            //When
            Action action = () => new SnackPile(snack: Chocolate, quantity: -1, price: 1m);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_create_snack_with_negative_price()
        {

            //When
            Action action = () => new SnackPile(snack: Chocolate, quantity: 1, price: -1m);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_create_snack_with_price_that_is_less_than_one_cent()
        {

            //When
            Action action = () => new SnackPile(snack: Chocolate, quantity: 1, price: 0.001m);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }
    }
}

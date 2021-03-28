using System;
using System.Linq;
using DddInPractice.Logic;
using FluentAssertions;
using Xunit;

using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void Return_money_empties_money_in_transaction()
        {
            //Given
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Dollar);

            //When
            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);

            //Then
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            //Given
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            //When
            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);

            //Then
        }

        [Fact]
        public void Cannot_insert_more_than_one_coin_at_a_time()
        {
            //Given
            var snackMachine = new SnackMachine();
            var twoCent = Cent + Cent;

            //When
            Action action = () => snackMachine.InsertMoney(twoCent);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Money_in_transaction_should_go_to_money_inside_after_purchase()
        {
            //Given
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);

            //When
            snackMachine.BuySnack(1);

            //Then
            snackMachine.MoneyInTransaction.Should().Be(Money.None);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }

        // TODO: Make sure that a new Snack Machine do not contain any money inside 

        [Fact]
        public void BuySnack_trades_inserted_money_for_a_snack()
        {
            //Given
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(
                new SnackPile(
                    snack: new Snack("Some snack"),
                    quantity: 10,
                    price: 1m),
                position: 1);
            snackMachine.InsertMoney(Dollar);

            //When

            snackMachine.BuySnack(1);

            //Then

            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(1m);

            //validate the number of snacks
            snackMachine.GetSnackPileInSlot(1).Quantity.Should().Be(9);
        }

        [Fact]
        public void Cannot_buy_snack_on_a_machine_with_no_snacks()
        {
            //Given
            var snackMachine = new SnackMachine();

            //When
            Action action = () => snackMachine.BuySnack(1);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_make_purchase_if_not_enough_money_is_inserted()
        {
            //Given
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(
                new SnackPile(
                    snack: new Snack("Some snack"),
                    quantity: 10,
                    price: 2m),
                position: 1);
            snackMachine.InsertMoney(Dollar);

            //When
            Action action = () => snackMachine.BuySnack(1);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

    }
}

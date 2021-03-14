using System;
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
            snackMachine.BuySnack();

            //Then
            snackMachine.MoneyInTransaction.Should().Be(Money.None);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }

        // TODO: Make sure that a new Snack Machine do not contain any money inside 

    }
}

using System;
using System.Linq;
using DddInPractice.Logic;
using FluentAssertions;
using Xunit;

using static DddInPractice.Logic.Snack;

using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests
{
    public class VendingMachineSpecs
    {
        [Fact]
        public void Return_money_empties_money_in_transaction()
        {
            //Given
            var VendingMachine = new VendingMachine();

            VendingMachine.InsertMoney(Dollar);

            //When
            VendingMachine.ReturnMoney();

            VendingMachine.MoneyInTransaction.Should().Be(0m);

            //Then
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            //Given
            var VendingMachine = new VendingMachine();

            VendingMachine.InsertMoney(Cent);
            VendingMachine.InsertMoney(Dollar);

            //When
            VendingMachine.MoneyInTransaction.Should().Be(1.01m);

            //Then
        }

        [Fact]
        public void Cannot_insert_more_than_one_coin_at_a_time()
        {
            //Given
            var VendingMachine = new VendingMachine();
            var twoCent = Cent + Cent;

            //When
            Action action = () => VendingMachine.InsertMoney(twoCent);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Money_in_transaction_should_go_to_money_inside_after_purchase()
        {
            //Given
            var vendingMachine = new VendingMachine();

            vendingMachine.LoadSnacks(1,
            new SnackPile(
                snack: Chocolate,
                quantity: 10,
                price: 1m));

            vendingMachine.InsertMoney(Dollar);
            vendingMachine.InsertMoney(Dollar);

            vendingMachine.MoneyInside.Amount.Should().Be(2m);

            //When
            vendingMachine.BuySnack(1);

            //Then
            vendingMachine.MoneyInTransaction.Should().Be(0m);
            vendingMachine.MoneyInside.Amount.Should().Be(1m);
        }

        // TODO: Make sure that a new Snack Machine do not contain any money inside 

        [Fact]
        public void BuySnack_trades_inserted_money_for_a_snack()
        {
            //Given
            var VendingMachine = new VendingMachine();
            VendingMachine.LoadSnacks(1,
                new SnackPile(
                    snack: Chocolate,
                    quantity: 10,
                    price: 1m));
            VendingMachine.InsertMoney(Dollar);

            //When

            VendingMachine.BuySnack(1);

            //Then

            VendingMachine.MoneyInTransaction.Should().Be(0m);
            VendingMachine.MoneyInside.Amount.Should().Be(1m);

            //validate the number of snacks
            VendingMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [Fact]
        public void Cannot_buy_snack_on_a_machine_with_no_snacks()
        {
            //Given
            var VendingMachine = new VendingMachine();

            //When
            Action action = () => VendingMachine.BuySnack(1);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_make_purchase_if_not_enough_money_is_inserted()
        {
            //Given
            var VendingMachine = new VendingMachine();
            VendingMachine.LoadSnacks(1,
                new SnackPile(
                    snack: Chocolate,
                    quantity: 10,
                    price: 2m));
            VendingMachine.InsertMoney(Dollar);

            //When
            Action action = () => VendingMachine.BuySnack(1);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Snack_machine_should_return_money_with_highest_denomination_first()
        {
            //Given
            VendingMachine VendingMachine = new VendingMachine();
            VendingMachine.LoadMoney(Dollar);

            VendingMachine.InsertMoney(Quarter);
            VendingMachine.InsertMoney(Quarter);
            VendingMachine.InsertMoney(Quarter);
            VendingMachine.InsertMoney(Quarter);

            //When
            VendingMachine.ReturnMoney();

            //Then
            VendingMachine.MoneyInside.QuarterCount.Should().Be(4);
            VendingMachine.MoneyInside.OneDollarCount.Should().Be(0);
        }

        [Fact]
        public void After_purchase_change_is_returned()
        {
            //Given
            var VendingMachine = new VendingMachine();
            VendingMachine.LoadSnacks(1,new SnackPile(Chocolate, 1, 0.5m));

            VendingMachine.LoadMoney(TenCent * 10);

            //When
            VendingMachine.InsertMoney(Dollar);
            VendingMachine.BuySnack(1);

            //Then
            VendingMachine.MoneyInside.Amount.Should().Be(1.5m);
            VendingMachine.MoneyInTransaction.Should().Be(0m);
            VendingMachine.MoneyInside.TenCentCount.Should().Be(5);
        }

        [Fact]
        public void Cannot_buy_snack_not_enough_change()
        {
            //Given
            var VendingMachine = new VendingMachine();
            VendingMachine.LoadSnacks(1,new SnackPile(Chocolate, 1, 0.5m));

            VendingMachine.LoadMoney(TenCent * 3);
            //When
            Action action = () => VendingMachine.BuySnack(1);

            //Then
            action.Should().Throw<InvalidOperationException>();
        }


    }
}

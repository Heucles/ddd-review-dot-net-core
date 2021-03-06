using Xunit;
using DddInPractice.Logic;
using FluentAssertions;
using System;
using DddInPractice.Logic.SharedKernel;

namespace DddInPractice.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void Sum_of_to_moneys_produce_correct_result()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);

        }

        [Fact]
        public void Two_money_instances_equal_if_contain_the_same_money_amount()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Two_money_instances_do_not_equal_if_contain_different_money_amounts()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 66);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().NotBe(money2);
            money1.GetHashCode().Should().NotBe(money2.GetHashCode());
        }


        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_is_calculated_correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount)
        {
            Money testedMoney = new Money(
            oneCentCount,
            tenCentCount,
            quarterCount,
            oneDollarCount,
            fiveDollarCount,
            twentyDollarCount);

            testedMoney.Amount.Should().Be(expectedAmount);

        }

        [Fact]
        public void Substraction_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);

        }

        [Fact]
        // TODO: IMPLEMENT SUBTRACTION BETWEEN  DIFERENT TYPES OF COINS
        public void Cannot_subtract_more_than_exists()
        {
            Money money1 = new Money(0, 1, 0, 0, 0, 0);
            Money money2 = new Money(1, 0, 0, 0, 0, 0);
            Action action = () =>
            {
                Money result = money1 - money2;
            };

            action.Should().Throw<InvalidOperationException>();
        }

        
        [Theory]
        [InlineData(1, 0, 0, 0, 0, 0,"??1")]
        [InlineData(0, 2, 0, 0, 0, 0,"??20")]
        [InlineData(0, 0, 3, 0, 0, 0,"??75")]
        [InlineData(0, 0, 0, 4, 0, 0,"$4.00")]
        [InlineData(0, 0, 0, 0, 5, 0,"$25.00")]
        [InlineData(0, 0, 0, 0, 0, 6,"$120.00")]
        public void To_String_should_return_the_ammount_of_money_correctly(int oneCentCount,
                                                                           int tenCentCount,
                                                                           int quarterCount,
                                                                           int oneDollarCount,
                                                                           int fiveDollarCount,
                                                                           int twentyDollarCount,
                                                                           string expectedString)
        {
            Money money = new Money(oneCentCount,
                                            tenCentCount,
                                            quarterCount,
                                            oneDollarCount,
                                            fiveDollarCount,
                                            twentyDollarCount);

            money.ToString().Should().Be(expectedString);

        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value
        (int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            Action action = () => new Money(
            oneCentCount,
            tenCentCount,
            quarterCount,
            oneDollarCount,
            fiveDollarCount,
            twentyDollarCount);

            action.Should().Throw<InvalidOperationException>();

        }
    }
}

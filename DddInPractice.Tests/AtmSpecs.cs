using System;
using DddInPractice.Logic.Atms;
using Xunit;
using FluentAssertions;

using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Tests
{
    public class AtmSpecs
    {
        [Fact]
        public void Take_money_exchanges_money_with_comission()
        {
            //Given

            var atm = new Atm();
            atm.LoadMoney(Dollar);

            //When

            atm.TakeMoney(1m);

            //Then
            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void Comission_is_at_least_one_cent()
        {
            //Given
            var atm = new Atm();
            atm.LoadMoney(Cent);

            //When
            atm.TakeMoney(0.01m);


            //Then
            atm.MoneyCharged.Should().Be(0.02m);
        }

        [Fact]
        public void Comission_is_rounded_up_to_the_next_cent()
        {
            //Given
            var atm = new Atm();
            atm.LoadMoney(TenCent);
            atm.LoadMoney(Dollar);

            //When
            atm.TakeMoney(1.1m);


            //Then
            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}

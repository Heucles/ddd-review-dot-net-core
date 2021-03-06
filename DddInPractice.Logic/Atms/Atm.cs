using System;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.SharedKernel;

using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Logic.Atms
{
    public class Atm : AggregateRoot
    {
        private const decimal ComissionRate = 0.01m;
        public virtual Money MoneyInside {get; protected set;} = None;
        public virtual decimal MoneyCharged {get; protected set;}

        public virtual void LoadMoney(Money money)
        {
            this.MoneyInside += money;
        }

        public virtual string CanTakeMoney(decimal amount){
            if (amount <= 0m)
                return "Invalid amount";

            if (MoneyInside.Amount < amount)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(amount)){
                return "Not enough change";
            }

            return string.Empty;

        }

        public virtual void TakeMoney(decimal amount)
        {
            if (CanTakeMoney(amount) != string.Empty)
                throw new InvalidOperationException();

            Money output = this.MoneyInside.Allocate(amount);
            this.MoneyInside -= output;

            decimal amountWithCommission = CalculateAmountWithCommission(amount);
            this.MoneyCharged += amountWithCommission;

            // raising the event
            // This implementation hurts the isolation principle of DDD, since it needs to 
            // Access a class which is in a outer scope from its on layer
            AddDomainEvent(new BalanceChangedEvent(amountWithCommission));
        }

        private decimal CalculateAmountWithCommission(decimal amount){
            decimal comission = amount * ComissionRate;
            decimal lessThanCent = comission % 0.01m;

            if (lessThanCent > 0){
                comission = comission - lessThanCent + 0.01m;
            }

            return amount + comission;
        }
    }
}

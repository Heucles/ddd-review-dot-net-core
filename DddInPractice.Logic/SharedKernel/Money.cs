using System;
using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.SharedKernel
{
    public class Money : ValueObject<Money>
    {

        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);


        // without the getters and setters
        // even if you provide values through a HTTP 
        // request they will not be presented
        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
                 OneCentCount * 0.01m
                       + TenCentCount * 0.10m
                       + QuarterCount * 0.25m
                       + OneDollarCount
                       + FiveDollarCount * 5
                       + TwentyDollarCount * 20;

        private Money() { }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount) : this()
        {
            if (oneCentCount < 0)
                throw new InvalidOperationException();
            if (tenCentCount < 0)
                throw new InvalidOperationException();
            if (quarterCount < 0)
                throw new InvalidOperationException();
            if (oneDollarCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        internal bool CanAllocate(decimal amount)
        {
            Money money = AllocateCore(amount);
            return money.Amount == amount;

        }

        internal Money Allocate(decimal amount)
        {
            if (!CanAllocate(amount))
            {
                throw new InvalidOperationException();
            }

            return AllocateCore(amount);
        }
        
        // Handles all of the logic of bringing up the notes or coins of the highest denomination 
        internal Money AllocateCore(decimal amount)
        {
            // we start with the most valuable notes and then try to fulfill the amount using then
            int twentyDollarCount = Math.Min((int)(amount / 20), this.TwentyDollarCount);
            amount = amount - twentyDollarCount * 20;

            int fiveDollarCount = Math.Min((int)(amount / 5), this.FiveDollarCount);
            amount = amount - fiveDollarCount * 5;

            int oneDollarCount = Math.Min((int)(amount), this.OneDollarCount);
            amount = amount - oneDollarCount;

            int quarterCount = Math.Min((int)(amount / 0.25m), this.QuarterCount);
            amount = amount - quarterCount * 0.25m;

            int tenCentCount = Math.Min((int)(amount / 0.10m), this.TenCentCount);
            amount = amount - tenCentCount * 0.10m;

            int oneCentCount = Math.Min((int)(amount / 0.01m), this.OneCentCount);

            return new Money(
                twentyDollarCount: twentyDollarCount,
                fiveDollarCount: fiveDollarCount,
                oneDollarCount: oneDollarCount,
                quarterCount: quarterCount,
                tenCentCount: tenCentCount,
                oneCentCount: oneCentCount

            );
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
             money1.OneCentCount + money2.OneCentCount,
             money1.TenCentCount + money2.TenCentCount,
             money1.QuarterCount + money2.QuarterCount,
             money1.OneDollarCount + money2.OneDollarCount,
             money1.FiveDollarCount + money2.FiveDollarCount,
             money1.TwentyDollarCount + money2.TwentyDollarCount
            );

            return sum;
        }

        public static Money operator *(Money money, int multiplier)
        {
            Money sum = new Money(
             money.OneCentCount * multiplier,
             money.TenCentCount * multiplier,
             money.QuarterCount * multiplier,
             money.OneDollarCount * multiplier,
             money.FiveDollarCount * multiplier,
             money.TwentyDollarCount * multiplier
            );

            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {
            Money subtraction = new Money(
             money1.OneCentCount - money2.OneCentCount,
             money1.TenCentCount - money2.TenCentCount,
             money1.QuarterCount - money2.QuarterCount,
             money1.OneDollarCount - money2.OneDollarCount,
             money1.FiveDollarCount - money2.FiveDollarCount,
             money1.TwentyDollarCount - money2.TwentyDollarCount
            );

            return subtraction;
        }

        public override string ToString()
        {
            if (this.Amount < 1)
            {
                return "??" + (this.Amount * 100).ToString("0");
            }

            return "$" + Amount.ToString("0.00");
        }

        protected override bool EqualsCore(Money other)
        {
            return
            this.OneCentCount == other.OneCentCount &&
            this.TenCentCount == other.TenCentCount &&
            this.QuarterCount == other.QuarterCount &&
            this.OneDollarCount == other.OneDollarCount &&
            this.FiveDollarCount == other.FiveDollarCount &&
            this.TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;

                return hashCode;

            }
        }
    }
}

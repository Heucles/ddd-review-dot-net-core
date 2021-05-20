
using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.Management
{
    public class BalanceChangedEvent : IDomainEvent
    {
        public virtual decimal Delta { get; private set;}
        
        public BalanceChangedEvent(decimal delta){
            this.Delta = delta;
        }

    }
}

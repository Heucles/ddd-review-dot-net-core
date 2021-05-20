using System;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;

namespace DddInPractice.Logic.Management
{
    public class HeadOffice : AggregateRoot
    {

        // all payments made from the users bank cards
        public virtual decimal Balance { get; protected set;}
        // all of the cash transferred from the snack machines
        public virtual Money Cash {get; protected set;}

        public virtual void ChangeBalance(decimal delta){
            Balance += delta;
        }

    }
}

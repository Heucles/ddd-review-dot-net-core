using System;
using System.Collections.Generic;
using System.Linq;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;
using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Logic.SnackMachines
{
    public class SnackMachineDto
    {
        public virtual long Id { get; protected set; }
        public virtual decimal MoneyInside { get; protected set; }

        public SnackMachineDto(long id, decimal moneyInside)
        {
            Id = id;
            MoneyInside = moneyInside;
        }

    }
}

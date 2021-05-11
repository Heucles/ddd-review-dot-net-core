// using System;
// using System.Collections.Generic;
// using System.Linq;
// using static DddInPractice.Logic.Money;

// namespace DddInPractice.Logic
// {
//     public class SnackMachine2 : AggregateRoot
//     {
//         protected virtual IList<Slot2> Slots2 { get; }

//         public virtual Money MoneyInside { get; protected set; }

//         public SnackMachine2()
//         {
//             Slots2 = new List<Slot2>
//             {
//                 new Slot2(this, 1),
//                 new Slot2(this, 2),
//                 new Slot2(this, 3),
//             };
//         }
//     }
// }

using System;
using DddInPractice.Logic;
using NHibernate;
using Xunit;
using static DddInPractice.Logic.MoneyAdded;

namespace DddInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void TestRun()
        {
            SessionFactory.Init(@"Server=localhost;Database=DddInPractice;User Id=sa;Password=reviewddd@123;");

            using (ISession session = SessionFactory.OpenSession()){
                // long id = 1;
                // var snackMachine = session.Get<SnackMachine>(id);
                // Console.Write(snackMachine.MoneyInside.OneDollarCount);

                // var repository2 = new SnackMachine2Repository(session);

                // var snackMachine2 = repository2.GetById(1);

                var repository = new SnackMachineRepository(session);
                SnackMachine snackMachine1 = repository.GetById(1);

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("*********************************");
                Console.WriteLine("");
                Console.WriteLine(snackMachine1);
                Console.WriteLine("");
                Console.WriteLine("*********************************");
                Console.WriteLine("");
                Console.WriteLine("");

                // Slot teste = repository.GetSlotById(1);

                snackMachine1.InsertMoney(Dollar.Money);
                snackMachine1.InsertMoney(Dollar.Money);
                snackMachine1.InsertMoney(Dollar.Money);

                snackMachine1.BuySnack(1);
                repository.Save(snackMachine1);

            }

        }
    }
}

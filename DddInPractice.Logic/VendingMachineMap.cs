using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace DddInPractice.Logic
{
    public class VendingMachineMap : ClassMap<VendingMachine>
    {
        public VendingMachineMap()
        {
            // Table("SnackMachine");
            Id(x => x.Id);

            Component(x => x.MoneyInside, y =>
            {
                y.Map(x => x.OneCentCount);
                y.Map(x => x.TenCentCount);
                y.Map(x => x.QuarterCount);
                y.Map(x => x.OneDollarCount);
                y.Map(x => x.FiveDollarCount);
                y.Map(x => x.TwentyDollarCount);
            });

            // HasMany<SlotOnVendingMachine>(x=>x.Slots);
            HasMany<SlotOnVendingMachine>(Reveal.Member<VendingMachine>("Slots")).KeyColumn("VendingMachineID")
                .Cascade.SaveUpdate()
                .Not.LazyLoad()
                .Inverse();
        }
    }
}

using FluentNHibernate.Mapping;

namespace DddInPractice.Logic
{
    public class SlotOnVendingMachineMap : ClassMap<SlotOnVendingMachine>
    {
        public SlotOnVendingMachineMap()
        {
            Id(x => x.Id);
            Map(x => x.Position);
            References(x => x.VendingMachine);

            Component(x => x.SnackPile, y =>
            {
                y.Map(x => x.Quantity);
                y.Map(x => x.Price);
                y.References(x => x.Snack).Not.LazyLoad();
            });
        }
    }
}

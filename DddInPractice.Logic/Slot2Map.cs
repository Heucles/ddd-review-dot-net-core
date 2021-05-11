using FluentNHibernate.Mapping;

namespace DddInPractice.Logic
{
    public class Slot2Map : ClassMap<Slot2>
    {
        public Slot2Map()
        {
            Id(x => x.Id);
            Map(x => x.Position);
            References(x => x.SnackMachine2);

            Component(x => x.SnackPile, y =>
            {
                y.Map(x => x.Quantity);
                y.Map(x => x.Price);
                y.References(x => x.Snack).Not.LazyLoad();
            });
        }
    }
}

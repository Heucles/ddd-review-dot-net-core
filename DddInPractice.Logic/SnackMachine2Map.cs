using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace DddInPractice.Logic
{
    public class SnackMachine2Map : ClassMap<SnackMachine2>
    {
        public SnackMachine2Map()
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

            // HasMany<Slot2>(x=>x.Slots);
            HasMany<Slot2>(Reveal.Member<SnackMachine2>("Slots")).KeyColumn("SnackMachine2ID")
                .Cascade.SaveUpdate()
                .Not.LazyLoad()
                .Inverse();
        }
    }
}

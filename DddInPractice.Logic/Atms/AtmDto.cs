namespace DddInPractice.Logic.Atms
{
    public class AtmDto
    {
        public virtual long Id { get; protected set; }
        public virtual decimal Cash { get; protected set; }
        
        public AtmDto(long id, decimal cash)
        {
            Id = id;
            Cash = cash;
        }

    }
}

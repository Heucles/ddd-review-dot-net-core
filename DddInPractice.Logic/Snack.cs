using System;

namespace DddInPractice.Logic
{
    public class Snack : Entity
    {
        public virtual string Name { get; protected set; }

        protected Snack() { }

        public Snack(string Name) : this()
        {
            this.Name = Name;
        }
    }
}

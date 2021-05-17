using System;
using DddInPractice.Logic.Atms;

namespace DddInPractice.UI.Models
{
    public interface IAtmContainer
    {
        public Atm Atm { get; }
    }
}

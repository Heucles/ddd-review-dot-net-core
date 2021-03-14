using System;
using DddInPractice.Logic;

namespace DddInPractice.UI.Models
{
    public interface ISnackMachineContainer
    {
        public SnackMachine SnackMachine { get; }
    }
}

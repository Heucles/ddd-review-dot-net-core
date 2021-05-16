using System;
using DddInPractice.Logic.SnackMachines;

namespace DddInPractice.UI.Models
{
    public interface ISnackMachineContainer
    {
        public SnackMachine SnackMachine { get; }
    }
}

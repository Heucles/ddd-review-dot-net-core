using System.Collections.Generic;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Logic.Management;

namespace DddInPractice.UI.ViewModels
{
    public class HeadOfficeViewModel
    {
        public HeadOffice HeadOffice { get; private set; }

        public IReadOnlyList<AtmDto> Atms { get; private set; }
        public IReadOnlyList<SnackMachineDto> SnackMachines { get; private set; }
        public static HeadOfficeViewModel Create(IReadOnlyList<AtmDto> atms, IReadOnlyList<SnackMachineDto> snackMachines)
        {
            return new HeadOfficeViewModel(HeadOfficeInstance.Instance, atms, snackMachines);
        }
        private HeadOfficeViewModel(HeadOffice headOffice, IReadOnlyList<AtmDto> atms, IReadOnlyList<SnackMachineDto> snackMachines)
        {
            this.Atms = atms;
            this.SnackMachines = snackMachines;
            this.HeadOffice = headOffice;
        }
    }
}

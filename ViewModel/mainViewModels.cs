using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO.Ports;

namespace gas_sensor.ViewModel
{
    partial class mainViewModels : ObservableObject
    {
        public ObservableCollection<string> AvailablePorts { get; } = new ObservableCollection<string>(SerialPort.GetPortNames());
    }
}

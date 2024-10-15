using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO.Ports;
using gas_sensor.Model;
using CommunityToolkit.Mvvm.Input;

namespace gas_sensor.ViewModel
{
    partial class mainViewModels : ObservableObject
    {
        [ObservableProperty]
        private GasSensorModel gas_model;

        [ObservableProperty]
        private int select_port_index = 0;

        [ObservableProperty]
        private int slave_id = 0xfe;

        [ObservableProperty]
        private SerialPort sp;

        public ObservableCollection<string> AvailablePorts { get; set; } = new ObservableCollection<string>(SerialPort.GetPortNames());
        [RelayCommand]
        void update_available_ports()
        {
            AvailablePorts.Clear();
            var portNames = SerialPort.GetPortNames(); // 获取所有端口名称
            foreach (var port in portNames)
            {
                AvailablePorts.Add(port); // 将每个端口名称添加到 ObservableCollection 中
            }
        }

        public mainViewModels()
        {
            gas_model = new GasSensorModel();
        }
    }
}

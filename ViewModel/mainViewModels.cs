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
using System.Windows;

namespace gas_sensor.ViewModel
{
    partial class mainViewModels : ObservableObject
    {
        [ObservableProperty]
        private GasSensorModel gas_model;

        [ObservableProperty]
        private int select_port_index = 0;
        public ObservableCollection<string> AvailablePorts { get; set; } = new ObservableCollection<string>(SerialPort.GetPortNames());

        [ObservableProperty]
        private int select_baud_index = 0;
        public ObservableCollection<int> AvailableBaudRates { get; set; } = new ObservableCollection<int>()
        {
            9600,
            4800,
            2400,1200,
        };

        [ObservableProperty]
        private int slave_id = 0xfe;

        [ObservableProperty]
        private SerialPort sp;

        [RelayCommand]
        void update_available_ports()
        {
            AvailablePorts.Clear();
            var portNames = SerialPort.GetPortNames(); // 获取所有端口名称
            foreach (var port in portNames)
            {
                AvailablePorts.Add(port); // 将每个端口名称添加到 ObservableCollection 中
            }
            Select_port_index = 0;
        }

        public mainViewModels()
        {
            gas_model = new GasSensorModel();
        }


        public bool IsAllredayConfig
        {
            get { return (Gas_model.Channel_status_l & (1 << 0)) == 0; }
            //get { return false; }
        }
        public float Gas_float_value
        {
            get
            {
                UInt32 tv = ((UInt32)Gas_model.Gas_concentration_float_h << 16) | ((UInt32)Gas_model.Gas_concentration_float_l);
                //byte[] b = new byte[4];
                //b[0] = (byte)(Gas_model.Gas_concentration_float_h & 0xff00);
                //b[1] = (byte)(Gas_model.Gas_concentration_float_h & 0x00ff);
                //b[2] = (byte)(Gas_model.Gas_concentration_float_l & 0xff00);
                //b[3] = (byte)(Gas_model.Gas_concentration_float_l & 0x00ff);
                return BitConverter.ToSingle(BitConverter.GetBytes(tv), 0);
            }
        }
        public int Gas_int_value
        {
            get
            {
                return Gas_model.Gas_concentration_h << 16 | Gas_model.Gas_concentration_l;
            }
        }

    }
}

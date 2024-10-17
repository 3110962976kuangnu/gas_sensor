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

        #region REG_40000 通道状态

        static bool get_bit_bool(UInt32 reg, int index)
        {
            return (reg & (1 << index)) != 0;
        }

        public bool IsAllredayConfig
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 0);
            }
        }
        public bool Is_Temperature_concentration_config
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
| (Gas_model.Channel_status_l), 1);
            }
        }
        public bool Is_bd0_config
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 2);
            }
        }
        public bool Is_bd1_config
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 3);
            }
        }
        public bool Is_sensor_ok
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 4);
            }
        }
        public bool Is_powerOn_init
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 5);
            }
        }
        public bool Is_config_buckup
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 6);
            }
        }
        public bool Is_alarm1_triggered
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 7);
            }
        }
        public bool Is_alarm2_triggered
        {
            get
            {
                return get_bit_bool(((UInt32)Gas_model.Channel_status_h << 16)
                | (Gas_model.Channel_status_l), 8);
            }
        }
        #endregion

        #region 实时参数


        public float Gas_float_value
        {
            get
            {
                UInt32 tv = ((UInt32)Gas_model.Gas_concentration_float_h << 16) | ((UInt32)Gas_model.Gas_concentration_float_l);
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
        public int Gas_adc_value
        {
            get
            {
                return (int)Gas_model.Gas_adc_value;
            }
        }
        public float Temperature_value
        {
            get { return (float)((Gas_model.Temperature_h << 16) | (Gas_model.Temperature_l)) / 1000; }
        }
        public int Temperature_adc_value
        {
            get { return (int)Gas_model.Temperature_adc_value; }

        }





        #endregion
    }
}

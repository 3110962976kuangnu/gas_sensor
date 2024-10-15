using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gas_sensor.Model
{
    class GasSensorModel
    {
        public UInt32 Sensor_board_status { get; set; } = 0;
        public float Gas_concentration_float { get; set; }
        public UInt32 Gas_concentration_int { get; set; }
        public UInt16 Gas_adc_value { get; set; }
        public UInt16 temperature_int { get; set; }
        public UInt16 temperature_adc_value { get; set; }
        public enum Enum_GasType
        {
            GasType_O2 = 2,
            GasType_CH4 = 3,
        }
        public Enum_GasType GasType { get; set; }
        public byte sensor_id { get; set; }
        public bool Is_toxic_gas { get; set; }
        public bool Enable_alarm1 { get; set; }
        public bool Enable_alarm2 { get; set; }
        public enum Enum_normal_operating_range
        {
            Operating_range_low_range = 0,
            Operating_range_middle_range = 1,
            Operating_range_high_range = 2,
        }
        public Enum_normal_operating_range Normal_Operating_Range { get; set; }
        public enum Enum_display_resolution
        {
            display_resolution_None = 0,
            display_resolution_One = 1,
            display_resolution_Two = 2,
        }
        public Enum_display_resolution Display_Resolution { get; set; }
        public UInt32 Gas_full_range { get; set; }
        public UInt32 alarm1_value { get; set; }
        public UInt32 alarm2_value { get; set; }
        public UInt16 Warm_up_time { get; set; }
        public bool Enable_zero_display { get; set; }
        public UInt32 bd0_value { get; set; }
    }
}

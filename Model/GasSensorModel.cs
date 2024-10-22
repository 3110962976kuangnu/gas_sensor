using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace gas_sensor.Model
{
    class GasSensorModel
    {
        public UInt16 Channel_status_h { get; set; }
        public UInt16 Channel_status_l { get; set; }
        public UInt16 Gas_concentration_h { get; set; }
        public UInt16 Gas_concentration_l { get; set; }
        public UInt16 Gas_concentration_float_h { get; set; }
        public UInt16 Gas_concentration_float_l { get; set; }
        public UInt16 Gas_adc_value { get; set; }
        public UInt16 Temperature_h { get; set; }
        public UInt16 Temperature_l { get; set; }
        public UInt16 Temperature_adc_value { get; set; }

        public UInt16 GasType { get; set; }
        public UInt16 Bit_config { get; set; }
        public UInt16 Concentration_unit { get; set; }
        public UInt16 Init_time { get; set; }
        public UInt16 Sensor_Brand_h { get; set; }
        public UInt16 Sensor_Brand_l { get; set; }
        public UInt16 Gas_full_range_h { get; set; }
        public UInt16 Gas_full_range_l { get; set; }

        public UInt16 Zero_display_range_h { get; set; }
        public UInt16 Zero_display_range_L { get; set; }
        public UInt16 Alarm1_value_h { get; set; }
        public UInt16 Alarm1_value_L { get; set; }
        public UInt16 Alarm2_value_h { get; set; }
        public UInt16 Alarm2_value_L { get; set; }

        public UInt16 Gas_bd0_value_h { get; set; }
        public UInt16 Gas_bd0_value_L { get; set; }
        public UInt16 Gas_bd0_adc_value { get; set; }
        public UInt16 Gas_bd1_value_h { get; set; }
        public UInt16 Gas_bd1_value_L { get; set; }
        public UInt16 Gas_bd1_adc_value { get; set; }
        public UInt16[] Temperature_compensation_data { get; set; } = new UInt16[36];

        public UInt16 Slave_ID { get; set; } = 0xfe;
        public UInt16 Communition_baud { get; set; } = 9600;


        #region get_value_use_regAddress

        public UInt16 Get_reg_use_address(UInt16 address)
        {
            if (address >= 40060 && address <= 40060 + 36)
            {
                return Temperature_compensation_data[address - 40060];
            }
            switch (address)
            {
                case 40000: return this.Channel_status_h;
                case 40001: return this.Channel_status_l;
                case 40002: return this.Gas_concentration_float_h;
                case 40003: return this.Gas_concentration_float_l;
                case 40004: return this.Gas_concentration_h;
                case 40005: return this.Gas_concentration_l;
                case 40006: return this.Gas_adc_value;
                case 40007: return this.Temperature_h;
                case 40008: return this.Temperature_l;
                case 40009: return this.Temperature_adc_value;

                case 40016: return this.GasType;
                case 40017: return this.Bit_config;
                case 40018: return this.Concentration_unit;
                case 40019: return this.Init_time;
                case 40020: return this.Sensor_Brand_h;
                case 40021: return this.Sensor_Brand_l;
                case 40022: return this.Gas_full_range_h;
                case 40023: return this.Gas_full_range_l;
                case 40024: return this.Gas_bd0_value_h;
                case 40025: return this.Gas_bd0_value_L;
                case 40026: return this.Zero_display_range_h;
                case 40027: return this.Zero_display_range_L;
                case 40028: return this.Alarm1_value_h;
                case 40029: return this.Alarm1_value_L;
                case 40030: return this.Alarm2_value_h;
                case 40031: return this.Alarm2_value_L;

                case 40048: return this.Gas_bd0_value_h;
                case 40049: return this.Gas_bd0_value_L;
                case 40050: return this.Gas_bd0_adc_value;
                case 40051: return this.Gas_bd1_value_h;
                case 40052: return this.Gas_bd1_value_L;
                case 40053: return this.Gas_bd1_adc_value;

                case 50016: return this.Slave_ID;
                case 50017: return this.Communition_baud;



                default: return 0;

            }

        }
        #endregion

        #region set_value_use_regAddress
        public UInt16 Set_reg_use_address(UInt16 address, UInt16 new_value)
        {
            if (address >= 40060 && address <= 40060 + 36)
            {
                Temperature_compensation_data[address - 40060] = new_value;
                return Temperature_compensation_data[address - 40060];
            }
            switch (address)
            {
                case 40000: { this.Channel_status_h = new_value; return this.Channel_status_h; }
                case 40001: { this.Channel_status_l = new_value; return this.Channel_status_l; }
                case 40002: { this.Gas_concentration_float_h = new_value; return this.Gas_concentration_float_h; }
                case 40003: { this.Gas_concentration_float_l = new_value; return this.Gas_concentration_float_l; }
                case 40004: { this.Gas_concentration_h = new_value; return this.Gas_concentration_h; }
                case 40005: { this.Gas_concentration_l = new_value; return this.Gas_concentration_l; }
                case 40006: { this.Gas_adc_value = new_value; return this.Gas_adc_value; }
                case 40007: { this.Temperature_h = new_value; return this.Temperature_h; }
                case 40008: { this.Temperature_l = new_value; return this.Temperature_l; }
                case 40009: { this.Temperature_adc_value = new_value; return this.Temperature_adc_value; }

                case 40016: { this.GasType = new_value; return this.GasType; }
                case 40017: { this.Bit_config = new_value; return this.Bit_config; }
                case 40018: { this.Concentration_unit = new_value; return this.Concentration_unit; }
                case 40019: { this.Init_time = new_value; return this.Init_time; }
                case 40020: { this.Sensor_Brand_h = new_value; return this.Sensor_Brand_h; }
                case 40021: { this.Sensor_Brand_l = new_value; return this.Sensor_Brand_l; }
                case 40022: { this.Gas_full_range_h = new_value; return this.Gas_full_range_h; }
                case 40023: { this.Gas_full_range_l = new_value; return this.Gas_full_range_l; }
                case 40024: { this.Gas_bd0_value_h = new_value; return this.Gas_bd0_value_h; }
                case 40025: { this.Gas_bd0_value_L = new_value; return this.Gas_bd0_value_L; }
                case 40026: { this.Zero_display_range_h = new_value; return this.Zero_display_range_h; }
                case 40027: { this.Zero_display_range_L = new_value; return this.Zero_display_range_L; }
                case 40028: { this.Alarm1_value_h = new_value; return this.Alarm1_value_h; }
                case 40029: { this.Alarm1_value_L = new_value; return this.Alarm1_value_L; }
                case 40030: { this.Alarm2_value_h = new_value; return this.Alarm2_value_h; }
                case 40031: { this.Alarm2_value_L = new_value; return this.Alarm2_value_L; }

                case 40048: { this.Gas_bd0_value_h = new_value; return this.Gas_bd0_value_h; }
                case 40049: { this.Gas_bd0_value_L = new_value; return this.Gas_bd0_value_L; }
                case 40050: { this.Gas_bd0_adc_value = new_value; return this.Gas_bd0_adc_value; }
                case 40051: { this.Gas_bd1_value_h = new_value; return this.Gas_bd1_value_h; }
                case 40052: { this.Gas_bd1_value_L = new_value; return this.Gas_bd1_value_L; }
                case 40053: { this.Gas_bd1_adc_value = new_value; return this.Gas_bd1_adc_value; }

                case 50016: { this.Slave_ID = new_value; return this.Slave_ID; }
                case 50017: { this.Communition_baud = new_value; return this.Communition_baud; }
                default: return 0;
            }
        }

        #endregion
    }
}

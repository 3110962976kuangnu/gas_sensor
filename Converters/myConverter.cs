using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace gas_sensor.Converters
{
    internal class HEXtoStrConverter : IValueConverter
    {
        private int? lastValidValue;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v)
            {
                return $"{v:X}";
            }
            return " value not int";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                if (s.StartsWith("0x") || s.StartsWith("0X"))
                {
                    s = s.Substring(2);
                }
                if (s.Length > 2 || s.Length == 0)
                {
                    return lastValidValue ?? DependencyProperty.UnsetValue;
                }

                // 尝试将十六进制字符串转换为整数
                if (int.TryParse(s, NumberStyles.HexNumber, culture, out int result))
                {
                    lastValidValue = result; // 更新上一次有效值
                    return result;
                }
            }
            // 返回上一次有效值（如果存在），否则返回未设置值
            return lastValidValue ?? DependencyProperty.UnsetValue;
        }
    }

    internal class GetBitBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (
                value is int intValue
                && parameter is string bitString
                && int.TryParse(bitString, out int bitindex))
            {
                bool bitvalue = (intValue & (1 << bitindex)) != 0;
                return bitvalue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolvalue
                && parameter is string bitString
                && int.TryParse(bitString, out int bitindex))
            {
                int res = boolvalue ? (1 << bitindex) : 0;
                return res;
            }
            return 0;
        }
    }

    //BooleanToInverseConverter
    internal class BooleanToInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool) && targetType != typeof(bool?))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }



}

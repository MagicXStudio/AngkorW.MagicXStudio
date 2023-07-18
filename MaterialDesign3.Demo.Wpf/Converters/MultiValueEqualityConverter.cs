using System.Globalization;
using System.Windows.Data;

namespace MaterialDesign3Demo.Converters
{

    /// <summary>
    /// 提供在 MultiBinding 中应用自定义逻辑的方法
    /// </summary>
    public class MultiValueEqualityConverter : IMultiValueConverter
    {
        /// <summary>
        /// 将源值转换为绑定目标的值,数据绑定引擎在将该值从源绑定传播到绑定目标时会调用此方法
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values?.All(o => o?.Equals(values[0]) == true) == true || values?.All(o => o == null) == true;
        }

        /// <summary>
        /// 将绑定目标值转换为源绑定值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace CASE.Navis2BCF.Converters
{

  /// <summary>
  /// Converter used on Navis2BCFWin.xaml to enalbe/disable a button depending if there are or not
  /// issues in the listview to be saved in the BCF file
  /// </summary>
  public class VisibConverter : IValueConverter
  {

    /// <summary>
    /// Convert
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if ((int)value != 0)
        return true;
      else return false;
    }

    /// <summary>
    /// Convert Back
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {

      throw new NotImplementedException();
    }

  }

}
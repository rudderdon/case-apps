using System;
using System.Globalization;
using System.Windows.Data;

namespace CASE.Navis2BCF.Converters
{

  /// <summary>
  /// Converter used on Navis2BCFWin.xaml to output a friendly button label
  /// depanding on the number of issues the are going to be saved in the BCF report
  /// </summary>
  public class IssueConverter : IValueConverter
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
      string c;
      if ((int)value == 1)
        c = "Export " + value.ToString() + " Issue »";
      else
        c = "Export " + value.ToString() + " Issues »";
      return c;
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
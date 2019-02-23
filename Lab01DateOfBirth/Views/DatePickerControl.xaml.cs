using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.Views
{
    /// <summary>
    /// Interaction logic for DatePickerControl.xaml
    /// </summary>
    public partial class DatePickerControl : UserControl
    {
        public DatePickerControl()
        {
            InitializeComponent();
            DataContext = new DatePickerViewModel();
        }
    }
}

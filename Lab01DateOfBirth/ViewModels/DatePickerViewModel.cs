using System;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.Tools;

namespace KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.ViewModels
{
    internal class DatePickerViewModel : BaseViewModel
    {
        #region Fields
        private DateTime? _dateOfBirth;
        private int? _age;
        private string _shownText;
        
        #region Commands
        private RelayCommand<object> _applyCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion
        #endregion

        #region Properties
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public int? Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string ShownText
        {
            get => _shownText;
            set
            {
                _shownText = value;
                OnPropertyChanged(nameof(ShownText));
            }
        }

        #region Commands

        public RelayCommand<object> ApplyCommand
        {
            get
            {
                return _applyCommand ?? (_applyCommand = new RelayCommand<object>(
                           ApplyImplementation, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => Environment.Exit(0)));
            }
        }
        #endregion
        #endregion

        public bool CanExecuteCommand()
        {
            return _dateOfBirth.HasValue;
        }

        private async void ApplyImplementation(object obj)
        {
            if (!DateOfBirth.HasValue)
            {
                MessageBox.Show("Please, enter the date.");
                return;
            }

            await Task.Run(() => Age = Years(DateOfBirth.Value, DateTime.Now));
            if (!Age.HasValue || Age.Value < 0 || Age.Value > 135)
            {
                MessageBox.Show("The entered date is not valid.");
                return;
            }

            ShownText = $"You are {Age} years old.";
            if (DateOfBirth.Value.Day == DateTime.Now.Day && DateOfBirth.Value.Month == DateTime.Now.Month)
            {
                MessageBox.Show("Happy birthday! :D");
            }

            await Task.Run(() => CalculateZodiac());
        }

        private static int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                   (((end.Month > start.Month) ||
                   ((end.Month == start.Month) && (end.Day >= start.Day)))
                       ? 1 : 0);
        }

        private void CalculateZodiac()
        {
            if (!DateOfBirth.HasValue)
            {
                return;
            }

            int leap = DateTime.IsLeapYear(DateOfBirth.Value.Year) ? 1 : 0;
            string westernZodiac;
            switch (DateOfBirth.Value.DayOfYear)
            {
                case int n when n >= 21 && n <= 50:
                    westernZodiac = "Aquarius";
                    break;
                case int n when n >= 51 && n <= 79 + leap:
                    westernZodiac = "Pisces";
                    break;
                case int n when n >= 80 + leap && n <= 110 + leap:
                    westernZodiac = "Aries";
                    break;
                case int n when n >= 111 + leap && n <= 141 + leap:
                    westernZodiac = "Taurus";
                    break;
                case int n when n >= 142 + leap && n <= 172 + leap:
                    westernZodiac = "Gemini";
                    break;
                case int n when n >= 173 + leap && n <= 203 + leap:
                    westernZodiac = "Cancer";
                    break;
                case int n when n >= 204 + leap && n <= 234 + leap:
                    westernZodiac = "Leo";
                    break;
                case int n when n >= 235 + leap && n <= 266 + leap:
                    westernZodiac = "Virgo";
                    break;
                case int n when n >= 267 + leap && n <= 296 + leap:
                    westernZodiac = "Libra";
                    break;
                case int n when n >= 297 + leap && n <= 326 + leap:
                    westernZodiac = "Scorpio";
                    break;
                case int n when n >= 327 + leap && n <= 355 + leap:
                    westernZodiac = "Sagittarius";
                    break;
                case int n when n >= 355 + leap || n <= 20:
                    westernZodiac = "Capricorn";
                    break;
                default:
                    westernZodiac = "Error";
                    break;
            }

            ShownText = string.Concat(ShownText, $"\nYour western zodiac sign is {westernZodiac}.");

            string[] chineseZodiacs =
                {"Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};
            ShownText = string.Concat(ShownText,
                $"\nYour chinese zodiac sign is {chineseZodiacs[DateOfBirth.Value.Year % 12]}.");
        }
    }
}

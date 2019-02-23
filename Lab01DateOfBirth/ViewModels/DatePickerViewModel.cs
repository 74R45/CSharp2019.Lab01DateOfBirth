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
                OnPropertyChanged();
            }
        }

        public int? Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string ShownText
        {
            get => _shownText;
            set
            {
                _shownText = value;
                OnPropertyChanged();
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
                     ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
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
                case int n when n >= 51 && n <= 79:
                    westernZodiac = "Pisces";
                    break;
                case int n when n >= 80 && n <= 110:
                    westernZodiac = "Aries";
                    break;
                case int n when n >= 111 && n <= 141:
                    westernZodiac = "Taurus";
                    break;
                case int n when n >= 142 && n <= 172:
                    westernZodiac = "Gemini";
                    break;
                case int n when n >= 173 && n <= 203:
                    westernZodiac = "Cancer";
                    break;
                case int n when n >= 204 && n <= 234:
                    westernZodiac = "Leo";
                    break;
                case int n when n >= 235 && n <= 266:
                    westernZodiac = "Virgo";
                    break;
                case int n when n >= 267 && n <= 296:
                    westernZodiac = "Libra";
                    break;
                case int n when n >= 297 && n <= 326:
                    westernZodiac = "Scorpio";
                    break;
                case int n when n >= 327 && n <= 355:
                    westernZodiac = "Sagittarius";
                    break;
                case int n when n >= 355 || n <= 20:
                    westernZodiac = "Capricorn";
                    break;
                default:
                    westernZodiac = "Error";
                    break;
            }
            ShownText = String.Concat(ShownText, $"\nYour western zodiac sign is {westernZodiac}.");

            string[] chineseZodiacs =
                {"Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};
            ShownText = String.Concat(ShownText, $"\nYour chinese zodiac sign is {chineseZodiacs[DateOfBirth.Value.Year % 12]}.");
        }
    }
}

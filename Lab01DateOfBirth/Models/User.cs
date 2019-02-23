using System;

namespace KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.Models
{
    internal class User
    {
        private DateTime? _dateOfBirth;

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
    }
}

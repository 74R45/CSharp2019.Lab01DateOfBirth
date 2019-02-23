using System;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.Tools;

namespace KMA.ProgrammingInCSharp2019.Kreshchenko.Lab01DateOfBirth.Models
{
    internal class Model : BaseViewModel
    {
        public DateTime? DateOfBirth { get; set; }
    }
}

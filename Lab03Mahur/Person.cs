using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Lab03Mahur.Exceptions;

namespace Lab03Mahur
{
    internal class Person
    {
        private static readonly string[] ChineeseZodiacs = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Sheep" };

        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _email;
        private readonly DateTime _dateOfBirth;

        private readonly bool _isAdult;
        private readonly string _westernZodiac;
        private readonly string _chineeseZodiac;
        private readonly bool _isBirthday;

        internal string FirstName { get { return _firstName; } }
        internal string LastName { get { return _lastName; } }
        internal string Email { get { return _email; } }
        internal DateTime DateOfBirth { get { return _dateOfBirth; } }

        internal bool IsAdult { get { return _isAdult; } }
        internal string WesternZodiac { get { return _westernZodiac; } }
        internal string ChineeseZodiac { get { return _chineeseZodiac; } }
        internal bool IsBirthday { get { return _isBirthday; } }


        internal Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _firstName = firstName;

            if (!new UserNameAttribute().IsValid(_firstName))
                throw new InvalidNameException("First name must contain only letters or -/' symbols");

            _lastName = lastName;

            if (!new UserNameAttribute().IsValid(_lastName))
                throw new InvalidNameException("Last name must contain only letters or -/' symbols");

            _email = email;

            if (!new EmailAddressAttribute().IsValid(_email))
                throw new InvalidNameException("Email address has invalid format");

            _dateOfBirth = dateOfBirth;

            var age = GetAge();
            if (age < 0)
                throw new NegativeAgeException("Age cannot be less than 0");
            if(age > 135)
                throw new DiedPersonException("Person's age is too big to say they are alive");

            _isAdult = age > 18;
            _westernZodiac = GetWesternZodiac();
            _chineeseZodiac = GetEasternZodiac();
            _isBirthday = IsBirthdayToday();
        }


        internal Person(string firstName, string lastName, string email) : this(firstName, lastName, email, DateTime.Today)
        {
        }

        internal Person(string firstName, string lastName, DateTime dateOfBirth) : this(firstName, lastName,
            string.Empty, dateOfBirth)
        {
        }

        private int GetAge()
        {
            int age = DateTime.Today.Year - _dateOfBirth.Year;
            if (DateTime.Today.Month < _dateOfBirth.Month)
                return age - 1;
            if (DateTime.Today.Month == _dateOfBirth.Month && DateTime.Today.Day < _dateOfBirth.Day)
                return age - 1;
            return age;
        }

        private bool IsBirthdayToday()
        {
            return DateTime.Today.Month.Equals(_dateOfBirth.Month) && DateTime.Today.Day.Equals(_dateOfBirth.Day);
        }

        private string GetEasternZodiac()
        {
            return ChineeseZodiacs[_dateOfBirth.Year % 12];
        }

        private string GetWesternZodiac()
        {
            switch (_dateOfBirth.Month)
            {
                case 1:
                    if (_dateOfBirth.Day >= 20)
                        return "Aquarius";
                    if (_dateOfBirth.Day <= 19)
                        return "Capricorn";
                    break;
                case 2:
                    if (_dateOfBirth.Day >= 19)
                        return "Pisces";
                    if (_dateOfBirth.Day <= 18)
                        return "Aquarius";
                    break;
                case 3:
                    if (_dateOfBirth.Day >= 21)
                        return "Aries";
                    if (_dateOfBirth.Day <= 20)
                        return "Pisces";
                    break;
                case 4:
                    if (_dateOfBirth.Day >= 20)
                        return "Taurus";
                    if (_dateOfBirth.Day <= 19)
                        return "Aries";
                    break;
                case 5:
                    if (_dateOfBirth.Day >= 21)
                        return "Gemini";
                    if (_dateOfBirth.Day <= 20)
                        return "Taurus";
                    break;
                case 6:
                    if (_dateOfBirth.Day >= 21)
                        return "Cancer";
                    if (_dateOfBirth.Day <= 20)
                        return "Gemini";
                    break;
                case 7:
                    if (_dateOfBirth.Day >= 23)
                        return "Leo";
                    if (_dateOfBirth.Day <= 22)
                        return "Cancer";
                    break;
                case 8:
                    if (_dateOfBirth.Day >= 23)
                        return "Virgo";
                    if (_dateOfBirth.Day <= 22)
                        return "Leo";
                    break;
                case 9:
                    if (_dateOfBirth.Day >= 23)
                        return "Libra";
                    if (_dateOfBirth.Day <= 22)
                        return "Virgo";
                    break;
                case 10:
                    if (_dateOfBirth.Day >= 23)
                        return "Scorpio";
                    if (_dateOfBirth.Day <= 22)
                        return "Libra";
                    break;
                case 11:
                    if (_dateOfBirth.Day >= 22)
                        return "Sagittarius";
                    if (_dateOfBirth.Day <= 21)
                        return "Scorpio";
                    break;
                case 12:
                    if (_dateOfBirth.Day >= 22)
                        return "Capricorn";
                    if (_dateOfBirth.Day <= 21)
                        return "Sagittarius";
                    break;
                default:
                    throw new ArgumentException();
            }
            return "";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("First name: " + _firstName);
            sb.AppendLine();
            sb.Append("Last name: " + _lastName);
            sb.AppendLine();
            sb.Append("Email: " + _email);
            sb.AppendLine();
            sb.Append("Date of birth: " + _dateOfBirth);
            sb.AppendLine();
            sb.Append("Is adult?: " + _isAdult);
            sb.AppendLine();
            sb.Append("Zodiac sign (western variant): " + _westernZodiac);
            sb.AppendLine();
            sb.Append("Zodiac sign (eastern variant): " + _chineeseZodiac);
            sb.AppendLine();
            sb.Append("Is it birthday today?: " + _isBirthday);
            return sb.ToString();
        }
    }
}

/*
 Person.cs
 a class for storing info about a Person.
 Stewart Wilson, Oct 2024.
*/

using System;

namespace MyClasses
{
    public class Person
    {
        // private backing fields for the similarly named public properties...
        private DateTime _dateOfBirth;
        private string _phoneNumber;

        // automatic properties, the compiler creates hidden backing fields...
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressTownCity { get; set; }
        public string AddressCounty { get; set; }
        public string AddressCountry { get; set; }
        public string AddressPostCode { get; set; }

        // properties...

        // a person's date of birth should not exceed that of the oldest living human on record or be in the future...
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value >= new DateTime(1900, 1, 1) && value <= DateTime.Now)
                    _dateOfBirth = value;
            }
        }

        // a phone number should only contain numeric digits and be of a reasonable length...
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = "n/a"; // set the backing field to a standard fail value that will be replaced after successful verification...

                if (value.Length > 8 && value.Length < 20)
                {
                    bool justNumbers = true;

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (!Char.IsDigit(value[i]))
                            justNumbers = false;
                    }

                    if (justNumbers)
                        _phoneNumber = value;
                }
            }
        }

        // methods...

        /*
         constructor.
         the parameters should contain the essentials, all other data is set to default values.
        */
        public Person(string firstName, string lastName, string emailAddress, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;

            AddressLine1 = "n/a";
            AddressTownCity = "n/a";
            AddressCounty = "n/a";
            AddressCountry = "n/a";
            AddressPostCode = "n/a";

            /*
             nb. we set the backing field directly as the value here would fail the property verification.
             nb. actually the verifier now sets this same (invalid) value anyway so we could also assign to the property
             without it ie. being left blank.
            */
            _phoneNumber = "n/a";
        }

        // prints the person's details to the console.
        public void PrintDetails()
        {
            Console.WriteLine("\r\nName: {0} {1}", FirstName, LastName);

            Console.WriteLine("E-mail Address: {0}", EmailAddress);

            Console.WriteLine("Address (line 1): {0}", AddressLine1);
            Console.WriteLine("Address (town/city): {0}", AddressTownCity);
            Console.WriteLine("Address (county): {0}", AddressCounty);
            Console.WriteLine("Address (country): {0}", AddressCountry);
            Console.WriteLine("Address (postcode): {0}", AddressPostCode);

            Console.WriteLine("Phone Number: {0}", PhoneNumber);

            int age = GetAgeInYears();
            Console.WriteLine("Date of Birth: {0}\\{1}\\{2} ({3} yrs)", DateOfBirth.Day, DateOfBirth.Month, DateOfBirth.Year, age);
        }

        /*
         returns the person's age (in days) calculated from their birthday and the current date.
         not especially useful but interesting nonetheless.
        */
        public int GetAgeInDays()
        {
            int age = (DateTime.Now - DateOfBirth).Days;

            return age;
        }

        // returns the person's age (in years) calculated from their birthday and the current date.
        public int GetAgeInYears()
        {
            int age = (DateTime.Now.Year - DateOfBirth.Year);

            if (DateTime.Now.Month < DateOfBirth.Month)
            {
                age--;
            }
            else if (DateTime.Now.Month == DateOfBirth.Month)
            {
                if (DateTime.Now.Day < DateOfBirth.Day)
                    age--;
            }

            return age;
        }
    }
}

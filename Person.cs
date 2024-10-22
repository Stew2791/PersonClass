/*
 Person.cs
 a C# class for storing general contact info about a Person.
 Stewart Wilson, Oct 2024.

 Todo...
 - add some basic validation for names and addresses?, ie. minimum string lengths.
 - add some basic validation for the e-mail address, ie. contains an '@' symbol and looks like a valid domain name.

*/

using System;

namespace MyClasses
{
    public class Person
    {
        // a string that can be used as a default value for any unspecified/unavailable/invalid (string) items..
        private const string defaultString = "n/a";

        // private backing fields for the similarly named public properties...
        private DateTime _dateOfBirth;
        private string _phoneNumber;

        // public 'automatic properties', the compiler will create hidden backing fields for them...
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressTownCity { get; set; }
        public string AddressCounty { get; set; }
        public string AddressCountry { get; set; }
        public string AddressPostCode { get; set; }

        // public properties...

        // a living person's date of birth should not exist in the future or make them unfeasibly old...
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value >= new DateTime(1900, 1, 1) && value <= DateTime.Now)
                    _dateOfBirth = value;
                else
                    _dateOfBirth = new DateTime(1, 1, 1);
                /*
                 nb. this date (1,1,1) also happens to be the date of an uninitialized DateTime, it's best to set it 
                 specifically though and we can use the year 1 as an indicator of an invalid date of birth elsewhere.
                */
            }
        }

        // a phone number should only contain digits (and possibly spaces) and be of a reasonable length...
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                /*
                 set the backing field to a default value that will be replaced after successful validation...
                 ie. if the value supplied is invalid then it will end up with the default value instead.
                 nb. the value in this case is obviously a string.
                */
                _phoneNumber = defaultString;

                if (value.Length > 8 && value.Length < 20)
                {
                    bool looksOk = true;

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (!Char.IsDigit(value[i]) && !Char.IsWhiteSpace(value[i]))
                            looksOk = false;
                    }

                    if (looksOk)
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

            AddressLine1 = defaultString;
            AddressTownCity = defaultString;
            AddressCounty = defaultString;
            AddressCountry = defaultString;
            AddressPostCode = defaultString;

            PhoneNumber = defaultString;
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

            if (DateOfBirth.Year != 1)
            {
                int age = GetAgeInYears();
                Console.WriteLine("Date of Birth: {0}\\{1}\\{2} ({3} yrs)", DateOfBirth.Day, DateOfBirth.Month, DateOfBirth.Year, age);
            }
            else
                Console.WriteLine("Date of Birth: {0}", defaultString);
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

        /*
         returns the person's age (in days) calculated from their birthday and the current date.
         not especially useful but interesting nonetheless.
        */
        public int GetAgeInDays()
        {
            int age = (DateTime.Now - DateOfBirth).Days;

            return age;
        }
    }
}

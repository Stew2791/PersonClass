/*
 Person.cs
 a C# class for storing general contact info about a Person.
 Stewart Wilson, Oct 2024.

 Todo...
 - add some basic validation for names and addresses?, ie. minimum string lengths.
 - improve the validation of the e-mail address, ie. contains just one '@' and at least one '.' in approximately the correct position.
*/

using System;

namespace AirBrainIndustries
{
    public class Person
    {
        /*
         a string that can be used as a default value for any unspecified/unavailable/invalid (string) items..
         nb. we can assign this rather than leaving a field's string value at null.
        */
        private const string defaultValueString = "n/a";

        // private backing fields for the similarly named public properties...
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private string _emailAddress;

        /*
         public 'automatic properties', these properties cannot use any additional logic.
         { get; set; } is really just convenient shorthand for.. get{ return fieldName; } set {fieldName = value; } 
         nb. the compiler creates hidden backing fields for them.
        */
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
                 nb. this date (1,1,1) also happens to be the date of an uninitialized DateTime object, it's best to set it 
                 specifically though and we can use the year 1 as an indicator of an invalid (living person's) date of birth 
                 elsewhere.
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
                _phoneNumber = defaultValueString;

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

        // an email address should at least contain one occurrence of the '@' and '.' characters.
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (value.Contains("@") && value.Contains("."))
                    _emailAddress = value;
                else
                    _emailAddress = defaultValueString;
            }
        }

        // methods...

        /*
         constructor.
         the parameters should contain the essentials, all other data is set to default values.
        */
        public Person(string firstName, string lastName, string emailAddress, DateTime dateOfBirth)
        {
            /*
             nb. note we are making the assignments through the properties which will perform validation in some cases.
             we want to avoid any string items being null, assigning a default string value to these instead, this allows 
             us to avoid a lot of messy null checks elsewhere...
            */
            FirstName = firstName != null ? firstName : defaultValueString;
            LastName = lastName != null ? lastName : defaultValueString;

            EmailAddress = emailAddress != null ? emailAddress : defaultValueString;

            DateOfBirth = dateOfBirth != null ? dateOfBirth : new DateTime(1, 1, 1);

            AddressLine1 = defaultValueString;
            AddressTownCity = defaultValueString;
            AddressCounty = defaultValueString;
            AddressCountry = defaultValueString;
            AddressPostCode = defaultValueString;

            PhoneNumber = defaultValueString;
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
                Console.WriteLine("Date of Birth: {0}", defaultValueString);
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


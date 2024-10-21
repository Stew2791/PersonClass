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
        public string Name { get; set; } // an automatic property.

        // nb. setting a date seems easiest using a method with multiple parameters (d/m/y), hence not made a property.
        private DateTime dateOfBirth;

        private double _heightInFeet; // a private backing field for a property. 

        public double HeightinFeet // a property.
        {
            get { return _heightInFeet; }
            set { _heightInFeet = value < 1 ? 1 : value; }
        }

        // constructor.
        public Person(string name)
        {
            Name = name;
            dateOfBirth = DateTime.Now; // using a default date of 'now' seems as reasonable as any. 
            HeightinFeet = 0;
        }

        // sets the date of birth, returns true on success.
        public bool SetDateOfBirth(int day, int month, int year)
        {
            try
            {
                dateOfBirth = new DateTime(year, month, day);
            }
            catch { return false; }

            return true;
        }

        // gets the date of birth.
        public DateTime GetDateOfBirth()
        {
            return dateOfBirth.Date;
        }

        // returns the person's age (in days) calculated from their birthday and the current date.
        public int GetAgeInDays()
        {
            int age = (DateTime.Now - dateOfBirth).Days;

            return age;
        }

        // returns the person's age (in days) calculated from their birthday and the current date.
        public int GetAgeInYears()
        {
            int age = (DateTime.Now.Year - dateOfBirth.Year);

            if (DateTime.Now.Month < dateOfBirth.Month)
            {
                age--;
            }
            else if (DateTime.Now.Month == dateOfBirth.Month)
            {
                if (DateTime.Now.Day < dateOfBirth.Day)
                    age--;
            }

            return age;
        }

        // prints the person's details to the console.
        public void PrintDetails()
        {
            Console.WriteLine("\r\nName: {0}", Name);
            Console.WriteLine("Date of Birth: {0}\\{1}\\{2}", dateOfBirth.Day, dateOfBirth.Month, dateOfBirth.Year);
            Console.WriteLine("Age (Years): {0}", GetAgeInYears());
            Console.WriteLine("Height: {0}", HeightinFeet);
        }
    }
}

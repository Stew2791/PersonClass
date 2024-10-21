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
        private int _age;
        private double _height;

        public string Name { get; set; }

        public int Age
        {
            get { return _age; }
            set { _age = value < 0 ? 0 : value; }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value < 0 ? 0 : value; }
        }

        // constructor.
        public Person(string name)
        {
            Name = name;
            Age = 0;
            Height = 0;
        }

        // constructor.
        public Person(string name, int age, double height)
        {
            Name = name;
            Age = age;
            Height = height;
        }

        // prints the person's details to the console.
        public void PrintDetails()
        {
            Console.WriteLine("Name: {0}, Age: {1}, Height: {2}", Name, Age, Height);
        }
    }
}

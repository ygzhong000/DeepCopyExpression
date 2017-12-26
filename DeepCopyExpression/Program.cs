using System;
using System.Collections.Generic;
using System.Linq;

namespace DeepCopyExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student() { Age = 20, Id = 1, Name = "郭靖" };
            Student ss = DeepCopyExp<Student, Student>.Copy(s);
            ss.Age = 23;
            ss.Id = 2;
            ss.Name = "黄蓉";
            Console.WriteLine(ss.Age + "," + ss.Id + "," + ss.Name);
            Console.WriteLine(s.Age + "," + s.Id + "," + s.Name);
            Console.ReadKey();
        }
    }
}

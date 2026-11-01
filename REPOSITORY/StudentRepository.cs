using System.Collections.Generic;
using System.IO;
using HostelSystem.MODEL;

namespace HostelSystem.REPOSITORY
{
    public class StudentRepository
    {
        public List<Student> students { get; set; }

        public StudentRepository()
        {
            students = new List<Student>();
            if (File.Exists("students.txt"))
            {
                StreamReader reader = new StreamReader("students.txt");
                int count = int.Parse(reader.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    string name = reader.ReadLine();
                    int roll = int.Parse(reader.ReadLine());
                    int sem = int.Parse(reader.ReadLine());
                    float matric = float.Parse(reader.ReadLine());
                    float fsc = float.Parse(reader.ReadLine());
                    float ecat = float.Parse(reader.ReadLine());
                    float gpa = float.Parse(reader.ReadLine());
                    float merit = float.Parse(reader.ReadLine());
                    int pref = int.Parse(reader.ReadLine());
                    string room = reader.ReadLine();
                    int mess = int.Parse(reader.ReadLine());
                    float bill = float.Parse(reader.ReadLine());
                    Student s = new Student(name, roll,  sem, matric, fsc, ecat, gpa, merit, pref, room, mess, bill);
                    students.Add(s);
                }
                reader.Close();
            }
        }

        public void addStudent(Student s)
        {
            students.Add(s);
            SaveStudents();
        }

        public void removeStudent(Student s)
        {
            students.Remove(s);
            SaveStudents();
        }

        public void SaveStudents()
        {
            StreamWriter writer = new StreamWriter("students.txt");
            writer.WriteLine(students.Count);
            foreach (Student s in students)
            {
                writer.WriteLine(s.name);
                writer.WriteLine(s.roll);
                writer.WriteLine(s.sem);
                writer.WriteLine(s.matric);
                writer.WriteLine(s.fsc);
                writer.WriteLine(s.ecat);
                writer.WriteLine(s.gpa);
                writer.WriteLine(s.merit);
                writer.WriteLine(s.pref);
                writer.WriteLine(s.room);
                writer.WriteLine(s.mess);
                writer.WriteLine(s.bill);
            }
            writer.Close();
        }

        public int execCount()
        {
            int c = 0;
            foreach (Student s in students)
            {
                if (s.room == "Executive Cubicle")
                {
                    c++;
                }
            }
            return c;
        }

        public int stdCount()
        {
            int c = 0;
            foreach (Student s in students)
            {
                if (s.room == "Standard Cubicle")
                {
                    c++;
                }
            }
            return c;
        }
    }
}

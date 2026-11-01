using System.Collections.Generic;
using HostelSystem.MODEL;
using HostelSystem.REPOSITORY;

namespace HostelSystem.Service
{
    public class StudentService
    {
        private StudentRepository repo;

        public StudentService(StudentRepository r)
        {
            repo = r;
        }

        public bool apply(Student s)
        {
            foreach (Student old in repo.students)
            {
                if (old.roll == s.roll)
                {
                    return false;
                }
            }
            s.calcMerit();
            if (s.pref == 1 && s.merit >= 80 && repo.execCount() < 2)
            {
                s.room = "Executive Cubicle";
            }
            else if ((s.pref == 1 || s.pref == 2) && s.merit >= 70 && repo.stdCount() < 5)
            {
                s.room = "Standard Cubicle";
            }
            else
            {
                s.room = "Shared Dorm";
            }
            s.calcBill();
            repo.addStudent(s);
            return true;
        }

        public Student searchByRoll(int roll)
        {
            foreach (Student s in repo.students)
            {
                if (s.roll == roll)
                {
                    return s;
                }
            }
            return null;
        }

        public List<Student> getAll()
        {
            return repo.students;
        }
    }
}

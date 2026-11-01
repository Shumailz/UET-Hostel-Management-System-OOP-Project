using System.Collections.Generic;
using HostelSystem.MODEL;
using HostelSystem.REPOSITORY;

namespace HostelSystem.Service
{
    public class AdminService
    {
        private StudentRepository repo;

        public AdminService(StudentRepository r)
        {
            repo = r;
        }

        public bool login(string user, string pass)
        {
            if (user == "Hitler" && pass == "123")
            {
                return true;
            }
            return false;
        }


        public List<Student> getAll()
        {
            return repo.students;
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

        public void meritList()
        {
            for (int i = 0; i < repo.students.Count; i++)
            {
                for (int j = i + 1; j < repo.students.Count; j++)
                {
                    if (repo.students[i].merit < repo.students[j].merit)
                    {
                        Student t = repo.students[i];
                        repo.students[i] = repo.students[j];
                        repo.students[j] = t;
                    }
                }
            }
            repo.SaveStudents();
        }

        public bool removeByRoll(int roll)
        {
            Student s = searchByRoll(roll);
            if (s == null)
            {
                return false;
            }
            repo.removeStudent(s);
            return true;
        }

        public int execLeft()
        {
            return 2 - repo.execCount();
        }

        public int stdLeft()
        {
            return 5 - repo.stdCount();
        }
    }
}

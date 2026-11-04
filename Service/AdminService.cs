using System;
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

        public int totalPages(int pageSize)
        {
            if (pageSize <= 0)
            {
                return 0;
            }
            int total = repo.students.Count;
            if (total == 0)
            {
                return 0;
            }
            int pages = total / pageSize;
            if (total % pageSize != 0)
            {
                pages++;
            }
            return pages;
        }

        public List<Student> getPage(int pageNumber, int pageSize)
        {
            List<Student> page = new List<Student>();
            if (pageSize <= 0)
            {
                return page;
            }
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int start = (pageNumber - 1) * pageSize;
            for (int i = start; i < start + pageSize && i < repo.students.Count; i++)
            {
                page.Add(repo.students[i]);
            }
            return page;
        }

        public List<Student> quickFilter(string room, int numChoice, float numValue)
        {
            List<Student> result = new List<Student>();
            foreach (Student s in repo.students)
            {
                bool roomOk = true;
                if (room != null && room != "")
                {
                    roomOk = string.Equals(s.room, room, StringComparison.OrdinalIgnoreCase);
                }
                bool numOk = true;
                if (numChoice == 1)
                {
                    numOk = s.bill >= numValue;
                }
                else if (numChoice == 2)
                {
                    numOk = s.bill <= numValue;
                }
                if (roomOk && numOk)
                {
                    result.Add(s);
                }
            }
            return result;
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

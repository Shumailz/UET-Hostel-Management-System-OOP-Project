using System;

namespace HostelSystem.MODEL
{
    public class Student
    {
        public string name {  get; set; }
        public int roll { get; set; }
        public int sem { get; set; }
        public float matric { get; set; }
        public float fsc {  get; set; }
        public float ecat { get; set; }
        public float gpa { get; set; }
        public float merit {  get; set; }
        public int pref { get; set; }
        public string room { get; set; }
        public int mess { get; set; }
        public float bill { get; set; }

        public Student()
        {
            name = "";
            roll = 0;
            sem = 0;
            matric = 0;
            fsc = 0;
            ecat = 0;
            gpa = 0;
            merit = 0;
            pref = 0;
            room = "";
            mess = 0;
            bill = 0;
        }

        public Student(string name, int roll, int sem, float matric, float fsc, float ecat, float gpa, int pref, int mess)
        {
            this.name = name;
            this.roll = roll;
            this.sem = sem;
            this.matric = matric;
            this.fsc = fsc;
            this.ecat = ecat;
            this.gpa = gpa;
            this.pref = pref;
            this.mess = mess;
            merit = 0;
            room = "";
            bill = 0;
        }

        public Student(string name, int roll, int sem, float matric, float fsc, float ecat, float gpa, float merit, int pref, string room, int mess, float bill)
        {
            this.name = name;
            this.roll = roll;
            this.sem = sem;
            this.matric = matric;
            this.fsc = fsc;
            this.ecat = ecat;
            this.gpa = gpa;
            this.merit = merit;
            this.pref = pref;
            this.room = room;
            this.mess = mess;
            this.bill = bill;
        }

        public Student(Student other)
        {
            name = other.name;
            roll = other.roll;
            sem = other.sem;
            matric = other.matric;
            fsc = other.fsc;
            ecat = other.ecat;
            gpa = other.gpa;
            merit = other.merit;
            pref = other.pref;
            room = other.room;
            mess = other.mess;
            bill = other.bill;
        }

        public void calcMerit()
        {
            if (sem == 1)
            {
                merit = (matric / 1100) * 25 + (fsc / 1100) * 45 + (ecat / 400) * 30;
            }
            else
            {
                merit = (gpa / 4) * 100;
            }
        }

        public float roomCost()
        {
            if (room == "Executive Cubicle")
            {
                return 6000;
            }
            if (room == "Standard Cubicle")
            {
                return 4500;
            }
            return 3000;
        }

        public float messCost()
        {
            if (mess == 1)
            {
                return 9000;
            }
            return 13500;
        }

        public void calcBill()
        {
            bill = roomCost() + messCost();
        }

        public override string ToString()
        {
            return roll + " " + name + " Sem " + sem + " Merit " + merit + "% " + room + " Rs " + bill;
        }
    }
}

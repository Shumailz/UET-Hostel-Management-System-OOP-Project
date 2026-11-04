using System;
using System.Collections.Generic;
using HostelSystem.MODEL;
using HostelSystem.REPOSITORY;
using HostelSystem.Service;
using HostelSystem.UI;

namespace HostelSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            StudentRepository repo = new StudentRepository();
            AdminService admin = new AdminService(repo);
            StudentService stud = new StudentService(repo);
            while (true)
            {
                Console.Clear();
                ConsoleHelper.head("--- UET HOSTEL MERIT & ALLOCATION SYSTEM ---");
                Console.WriteLine("1. Admin Portal (Warden)");
                Console.WriteLine("2. Student Portal");
                Console.WriteLine("3. Exit");
                int opt = ConsoleHelper.readNum("Choose one option: ");
                if (opt == 1)
                {
                    adminPortal(admin);
                }
                else if (opt == 2)
                {
                    studentPortal(stud);
                }
                else if (opt == 3)
                {
                    Console.WriteLine("Shutting down the system...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    ConsoleHelper.pause();
                }
            }
            Console.ReadKey();
        }

        static void adminPortal(AdminService admin)
        {
            Console.Clear();
            string user = ConsoleHelper.readText("Admin Username: ");
            string pass = ConsoleHelper.readText("Password: ");
            if (admin.login(user, pass) == false)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid login.");
                ConsoleHelper.pause();
                return;
            }
            Console.WriteLine();
            Console.WriteLine("Login successful.");
            ConsoleHelper.pause();
            while (true)
            {
                Console.Clear();
                ConsoleHelper.head("--- ADMIN DASHBOARD ---");
                Console.WriteLine("Rooms Available -> Executive: " + admin.execLeft() + " | Standard: " + admin.stdLeft());
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. View all Allocations");
                Console.WriteLine("2. Search & Print Fee Challan");
                Console.WriteLine("3. Generate Official Merit List");
                Console.WriteLine("4. Reject/Remove Applicant");
                Console.WriteLine("5. View Students Page by Page");
                Console.WriteLine("6. Quick Filter Students");
                Console.WriteLine("7. Logout");
                int opt = ConsoleHelper.readNum("Choose option: ");
                if (opt == 1)
                {
                    Console.Clear();
                    Console.WriteLine("RollNO      Name        Sem      Merit %      Room        Total Bill");
                    Console.WriteLine("------------------------------------------------------------------");
                    foreach (Student s in admin.getAll())
                    {
                        Console.WriteLine(s);
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 2)
                {
                    Console.Clear();
                    int roll = ConsoleHelper.readNum("Enter Roll Number to search: ");
                    Student s = admin.searchByRoll(roll);
                    if (s == null)
                    {
                        Console.WriteLine("Not found.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("======================================");
                        Console.WriteLine("        UET HOSTEL FEE CHALLAN        ");
                        Console.WriteLine("======================================");
                        Console.WriteLine(" Name: " + s.name + "   Roll No: " + s.roll);
                        Console.WriteLine(" Semester: " + s.sem + "        Merit: " + s.merit + "%");
                        Console.WriteLine(" Allocated: " + s.room);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine(" Room Charges:      Rs " + s.roomCost());
                        Console.WriteLine(" Mess Plan:         Rs " + s.messCost());
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine(" TOTAL PAYABLE:     Rs " + s.bill);
                        Console.WriteLine("======================================");
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 3)
                {
                    Console.Clear();
                    admin.meritList();
                    Console.WriteLine("--- OFFICIAL UET HOSTEL MERIT LIST ---");
                    List<Student> list = admin.getAll();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + list[i].name + " (Sem " + list[i].sem + ") - Merit: " + list[i].merit + "% -> " + list[i].room);
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 4)
                {
                    Console.Clear();
                    int roll = ConsoleHelper.readNum("Enter student Roll Number to reject/remove: ");
                    if (admin.removeByRoll(roll))
                    {
                        Console.WriteLine("Applicant removed. Room given back to hostel administration.");
                    }
                    else
                    {
                        Console.WriteLine("Not found.");
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 5)
                {
                    Console.Clear();
                    int total = admin.getAll().Count;
                    if (total == 0)
                    {
                        Console.WriteLine("No students found.");
                        ConsoleHelper.pause();
                    }
                    else
                    {
                        int pageSize = ConsoleHelper.readNum("Enter page size (how many students on one page): ");
                        if (pageSize <= 0)
                        {
                            Console.WriteLine("Invalid page size.");
                            ConsoleHelper.pause();
                        }
                        else
                        {
                            int pages = admin.totalPages(pageSize);
                            int currentPage = 1;
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("RollNO      Name        Sem      Merit %      Room        Total Bill");
                                Console.WriteLine("------------------------------------------------------------------");
                                foreach (Student s in admin.getPage(currentPage, pageSize))
                                {
                                    Console.WriteLine(s);
                                }
                                Console.WriteLine("Page " + currentPage + " of " + pages + " (Total records: " + total + ")");
                                Console.WriteLine("N. Next Page | P. Previous Page | F. First Page | L. Last Page | X. Exit");
                                string nav = ConsoleHelper.readText("Choose: ");
                                if (nav == "N" || nav == "n")
                                {
                                    if (currentPage < pages)
                                    {
                                        currentPage++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("You are already on last page.");
                                        ConsoleHelper.pause();
                                    }
                                }
                                else if (nav == "P" || nav == "p")
                                {
                                    if (currentPage > 1)
                                    {
                                        currentPage--;
                                    }
                                    else
                                    {
                                        Console.WriteLine("You are already on first page.");
                                        ConsoleHelper.pause();
                                    }
                                }
                                else if (nav == "F" || nav == "f")
                                {
                                    currentPage = 1;
                                }
                                else if (nav == "L" || nav == "l")
                                {
                                    currentPage = pages;
                                }
                                else if (nav == "X" || nav == "x")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                    ConsoleHelper.pause();
                                }
                            }
                        }
                    }
                }
                else if (opt == 6)
                {
                    Console.Clear();
                    Console.WriteLine("Room types: Executive Cubicle, Standard Cubicle, Shared Dorm");
                    string room = ConsoleHelper.readText("Enter Room (press Enter to skip): ");
                    Console.WriteLine("1. Bill above value");
                    Console.WriteLine("2. Bill below value");
                    Console.WriteLine("3. Skip bill filter");
                    int numChoice = ConsoleHelper.readNum("Choose bill option: ");
                    float numValue = 0;
                    if (numChoice == 1 || numChoice == 2)
                    {
                        numValue = ConsoleHelper.readFloat("Enter bill value: ");
                    }
                    else
                    {
                        numChoice = 0;
                    }
                    List<Student> result = admin.quickFilter(room, numChoice, numValue);
                    if (result.Count == 0)
                    {
                        Console.WriteLine("No records found.");
                    }
                    else
                    {
                        Console.WriteLine("RollNO      Name        Sem      Merit %      Room        Total Bill");
                        Console.WriteLine("------------------------------------------------------------------");
                        foreach (Student s in result)
                        {
                            Console.WriteLine(s);
                        }
                        float totalBill = 0;
                        float highest = result[0].bill;
                        float lowest = result[0].bill;
                        foreach (Student s in result)
                        {
                            totalBill = totalBill + s.bill;
                            if (s.bill > highest)
                            {
                                highest = s.bill;
                            }
                            if (s.bill < lowest)
                            {
                                lowest = s.bill;
                            }
                        }
                        Console.WriteLine("Count: " + result.Count);
                        Console.WriteLine("Total Bill: Rs " + totalBill);
                        Console.WriteLine("Highest Bill: Rs " + highest);
                        Console.WriteLine("Lowest Bill: Rs " + lowest);
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 7)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    ConsoleHelper.pause();
                }
            }
        }


        static void studentPortal(StudentService stud)
        {
            while (true)
            {
                Console.Clear();
                ConsoleHelper.head("--- STUDENT PORTAL ---");
                Console.WriteLine("1. Apply for Hostel Allotment");
                Console.WriteLine("2. Check Merit Status");
                Console.WriteLine("3. Go Back");
                int opt = ConsoleHelper.readNum("Choose option: ");
                if (opt == 1)
                {
                    Console.Clear();
                    ConsoleHelper.head("--- REGISTRATION FORM ---");
                    string name = ConsoleHelper.readText("Enter your First Name: ");
                    int roll = ConsoleHelper.readNum("Enter your Roll Number: ");
                    int sem = ConsoleHelper.readNum("Enter Current Semester (1 to 8): ");
                    float matric = 0;
                    float fsc = 0;
                    float ecat = 0;
                    float gpa = 0;
                    if (sem == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("--- FIRST YEAR DATA ---");
                        matric = ConsoleHelper.readFloat("Enter Matric Marks (out of 1100): ");
                        fsc = ConsoleHelper.readFloat("Enter FSc Marks (out of 1100): ");
                        ecat = ConsoleHelper.readFloat("Enter ECAT Marks (out of 400): ");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("--- SENIOR Students ---");
                        gpa = ConsoleHelper.readFloat("Enter your latest CGPA (out of 4.0): ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("--- ROOM PREFERENCE ---");
                    Console.WriteLine("1. Executive Cubicle (Requires  atleast 80% Merit)");
                    Console.WriteLine("2. Standard Cubicle  (Requires atleast  70% Merit)");
                    Console.WriteLine("3. Shared Dorm ");
                    int pref = ConsoleHelper.readNum("Select your preference (1-3): ");
                    Console.WriteLine();
                    Console.WriteLine("--- MESS PREFERENCE ---");
                    Console.WriteLine("1. Basic Plan (Rs 9000)");
                    Console.WriteLine("2. Full Plan (Rs 13500)");
                    int mess = ConsoleHelper.readNum("Choose mess plan (1 or 2): ");
                    Console.WriteLine();
                    Console.WriteLine("System calculating your merit and checking allocation...");
                    Student s = new Student(name, roll, sem, matric, fsc, ecat, gpa, pref, mess);
                    if (stud.apply(s))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Application submitted! Check 'Merit Status' to see your allotment.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Roll Number already exists. Application not submitted.");
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 2)
                {
                    Console.Clear();
                    ConsoleHelper.head("--- CHECK STATUS ---");
                    int roll = ConsoleHelper.readNum("Enter your Roll Number to view your allocation: ");
                    Student s = stud.searchByRoll(roll);
                    if (s == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Roll Number not found.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Hello, " + s.name + "!");
                        Console.WriteLine("Your Merit is : " + s.merit + "%");
                        if (s.pref == 1 && s.room != "Executive Cubicle")
                        {
                            Console.WriteLine("Note: Executive Cubicle denied. (Low Merit or No Space Left)");
                        }
                        else if (s.pref == 2 && s.room != "Standard Cubicle")
                        {
                            Console.WriteLine("Note: Standard Cubicle denied. (Low Merit or No Space Left)");
                        }
                        Console.WriteLine("Allocated Room: " + s.room);
                        Console.WriteLine("Total Monthly Dues: Rs " + s.bill);
                    }
                    ConsoleHelper.pause();
                }
                else if (opt == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    ConsoleHelper.pause();
                }
            }
        }
    }
}

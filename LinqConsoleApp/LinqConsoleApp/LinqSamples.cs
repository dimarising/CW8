using Microsoft.Ajax.Utilities;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };

            foreach (var re in res)
            {
                Console.WriteLine(re);
            }


            //2. Lambda and Extension methods

            var res2 = Emps.Where(x => x.Job == "Backend programmer");

            foreach (var emp in res)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            var res = Emps.Where(x => x.Job == "Frontend programmer" && x.Salary > 1000).OrderBy(x => x.Ename).Select(x => new 
            { 
            Nazwisko = x.Ename,
            Zawod = x.Job
            
            });

            Console.WriteLine("");
            foreach (var x in res)
            {
                Console.WriteLine(x);
            }


        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {

            var res = Emps.Select(x => new
            {
                Pensja = x.Salary

            }).Max(x => x.Pensja);

                Console.WriteLine("\nMax Salary: " + res);

        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {

            var p4 = from emp in Emps
                     where emp.Salary == Emps.Max(x => x.Salary)
                     select new
                     {
                         Nazwisko = emp.Ename,
                         Praca = emp.Job,
                         Pensja = emp.Salary
                     };

            //var p4 = Emps.Where(x => x.Salary == Emps.Max(x => x.Salary));   // Władysław Torzewski (8)
            
            Console.WriteLine("\n");
            foreach (var x in p4)
            {
                Console.WriteLine(x);
            }

        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            var p5 = Emps.Select(x => new
            {
                Nazwisko = x.Ename,
                Zawod = x.Job

            });

            Console.WriteLine("\n");
            foreach (var x in p5)
            {
                Console.WriteLine(x);
            }

        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {

            var p6 = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
            {
                Nazwisko = emp.Ename,
                Praca = emp.Job,
                Departament = dept.Dname
            });
            
            Console.WriteLine("\n");
            foreach (var x in p6)
            {
                Console.WriteLine(x);
            }


        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {

            var p7 = Emps.GroupBy(x => x.Job).Select(x => new
            {
                Praca = x.Key,
                LiczbaPracownikow = x.Count()
            });
           
            Console.WriteLine("\n");
            foreach (var x in p7)
            {
                Console.WriteLine(x);
            }

        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            var p8 = Emps.Any(x => x.Job.Equals("Backend programmer"));
            
                Console.WriteLine("\n"+ p8);

        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {

            var p9 = Emps.Where(x => x.Job.Equals("Backend programmer")).OrderBy(x => x.HireDate).Select(x => new
            {
                Nazwisko = x.Ename,
                Zawod = x.Job

            }).Take(1);
            
            Console.WriteLine("\n");
            foreach (var x in p9)
            {
                Console.WriteLine(x);
            }

        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {
            var p10 = Emps.Select(x => new
            {
                Nazwisko = x.Ename,
                Zawod = x.Job,
                Date = x.HireDate

            }).Union(Emps.Select(x => new
            {
                Nazwisko = "Brak wartości",
                Zawod = (String) null,
                Date = (DateTime?) null

            }));

            Console.WriteLine("\n");
            foreach (var x in p10)
            {
                Console.WriteLine(x);
            }

        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            var p11 = Emps.Aggregate((x, y) => y.Salary > x.Salary ? y : x);
            Console.WriteLine("\n"+p11);

        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {

            var p12 = Emps.SelectMany(emp => Depts, (emp, dept) => new
            {
                Nazwisko = emp.Ename,
                
                });

            Console.WriteLine("\n");
            foreach (var x in p12)
            {
                Console.WriteLine(x);
            }
        }
    }
}

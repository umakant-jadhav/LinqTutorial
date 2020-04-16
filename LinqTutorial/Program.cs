using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTutorial
{

    // Developer one file
    public class Sample
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int Salary { get; set; }
        public int Dept { get; set; }


        public Sample(int id,string name,int salary,int dept)
        {
            this.Id = id;
            this.Name = name;
            this.Salary = salary;
            this.Dept = dept;
        }
    
    }

    public class ClassForGit
    { 
    
    }
    public class DemoClass
    {
        public int MyProperty { get; set; }
        public decimal salary { get; set; }

        public DemoClass(int a,decimal s)
        {
            MyProperty = a;
            salary = s*3;
        }

        public void displayDemoClassProp()
        {
            Console.WriteLine("MyProperty ="+MyProperty+"  salary= "+salary);
        }
    }

    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
       

        public Department(int id,string Name)
        {
            this.Id = id;
            this.DeptName = Name;
           
        }
    
    }

    //Contains method works well with premitive type
    // IEqualityComparer class - Generally used when we are using Contains method with class object
    

    public class SampleComparer : IEqualityComparer<Sample>
    {


        public bool Equals(Sample x, Sample y)
        {

            return (x.Id == y.Id && x.Name == y.Name && x.Salary == y.Salary && x.Dept == y.Dept);
          
        }

        public int GetHashCode(Sample obj)
        {
           // throw new NotImplementedException();
            return 1;
        }
    }



    class Program
    {


        static void Main(string[] args)
        {

            List<Sample> list = new List<Sample>(){
               new Sample(1,"Ram",10000,1),
               new Sample(2,"Shyam",15000,2),
               new Sample(3,"John",10000,2),
               new Sample(4,"Mike",40000,3),
               new Sample(5,"Tom",50322,1),
               new Sample(6,"Jack",15000,4),
          };

          list.ForEach(s=>Console.WriteLine("Id - "+s.Id+"  Name - "+s.Name+"  Salary - "+s.Salary));

           
            // Using Query syntax
          var ss = from s in list where s.Name.Contains("ya") select s;

          Console.WriteLine("Filtered List - Using Query syntax");

          ss.ToList<Sample>().ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name));



            // Using method syntax


          var sList = list.Where((l) =>  l.Name.Contains("ya") ).ToList<Sample>();

            Console.WriteLine("Filtered List - Using method syntax");

            sList.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name));



            // Lambda expression can be assigned to Func Delegate

            Func<List<Sample>, string, List<Sample> >  f = (l, t) =>  l.Where(l1 => l1.Name.Contains(t)).ToList<Sample>();
            List<Sample> rList = f.Invoke(list, "am");
            Console.WriteLine("Filtered List - Lambda expression can be assigned to Func Delegate");

            rList.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name));

            // Using Standard query operators
            // Where extension method  - second parameter 'i' indicates index of each element

            var indexed = list.Where((l, i) => 
            {
                Console.WriteLine(l.Name+" - index "+i);
                return (i % 2) == 0;
            }).ToList<Sample>();
            Console.WriteLine("Filtered List - indexing in Where extension method");
            indexed.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name));


            // using multiple Where extension method
            var a = list.Where((l, i) =>
            {
               // Console.WriteLine(l.Name + " - index " + i);
                return (i % 2) == 0;
            }).Where(s=>s.Salary>15000).ToList<Sample>();
            Console.WriteLine("Filtered List - using multiple Where extension method");
            a.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name));

            // OfType extension method
            

            //The Where operator filters the collection based on a predicate function.
            //The OfType operator filters the collection based on a given type
            //Where and OfType extension methods can be called multiple times in a single LINQ query.


            IList mixedList = new ArrayList();
            mixedList.Add(10);
            mixedList.Add(12);
            mixedList.Add("Hello");
            mixedList.Add("Good Morning");
            mixedList.Add(new Sample(20,"Nick",20000,2));
            mixedList.Add(new Sample(21, "Scott", 25000,1));
            Console.WriteLine("Midex list before filtering");
            foreach (var s in mixedList)
                Console.WriteLine("Value - "+s);

            List<int> iList = mixedList.OfType<int>().ToList<int>();
            Console.WriteLine("Integer element in Midex list after filtering");
            iList.ForEach(i=>Console.WriteLine("i= "+i));

            List<string> stringList = mixedList.OfType<string>().ToList<string>();
            Console.WriteLine("string element in Midex list after filtering");
            stringList.ForEach(s => Console.WriteLine("s= " + s));

            List<Sample> sampleList = mixedList.OfType<Sample>().ToList<Sample>();
            Console.WriteLine("Sample object element in Midex list after filtering");
            sampleList.ForEach(s => Console.WriteLine("Id= " + s.Id +"  Name="+s.Name+"  Salary="+s.Salary ));

            // OrderBy, OrderByDescending 

            Console.WriteLine("Before sorting - ");
            list.ForEach(s=>Console.WriteLine("Id - "+s.Id+"  Name - "+s.Name+"  Salary - "+s.Salary));

            var orderbyASC=list.Where(s=>s.Salary<60000).OrderBy(s=>s.Salary).ToList<Sample>();
             Console.WriteLine("After sorting OrderBy ASC- ");
            orderbyASC.ForEach(s=>Console.WriteLine("Id - "+s.Id+"  Name - "+s.Name+"  Salary - "+s.Salary));

            var orderbyDESC = list.Where(s => s.Salary < 60000).OrderByDescending(s => s.Salary).ToList<Sample>();
            Console.WriteLine("After sorting OrderByDescending DESC- ");
            orderbyDESC.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name + "  Salary - " + s.Salary));

            //  ThenBy, ThenByDescending

            var thenbyASC = list.Where(s => s.Salary < 60000).OrderBy(s => s.Salary).ThenBy(s=>s.Name).ToList<Sample>();
            Console.WriteLine("After sorting ThenBy ASC- ");
            thenbyASC.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name + "  Salary - " + s.Salary));

            var thenbyDESC = list.Where(s => s.Salary < 60000).OrderByDescending(s => s.Salary).ThenByDescending(s => s.Name).ToList<Sample>();
            Console.WriteLine("After sorting ThenByDescending DESC- ");
            thenbyDESC.ForEach(s => Console.WriteLine("Id - " + s.Id + "  Name - " + s.Name + "  Salary - " + s.Salary));



            // GroupBy

            var gList = list.GroupBy(s => s.Salary);
            Console.WriteLine("Using GroupBy Clause ");
            foreach(var group in gList)
            {
                Console.WriteLine("Key "+group.Key);
                foreach(var item in group)
                {
                    Console.WriteLine("Id - " + item.Id + "  Name - " + item.Name + "  Salary - " + item.Salary);
                }
            
            }


            // ToLookup  -- ToLookup is the same as GroupBy; the only difference is GroupBy execution is deferred,
            //whereas ToLookup execution is immediate. Also, ToLookup is only applicable in Method syntax. 
            //ToLookup is not supported in the query syntax.
            
             gList = list.ToLookup(s => s.Salary);
             Console.WriteLine("Using ToLookup Clause ");
                foreach (var group in gList)
                {
                    Console.WriteLine("Key " + group.Key);
                    foreach (var item in group)
                    {
                        Console.WriteLine("Id - " + item.Id + "  Name - " + item.Name + "  Salary - " + item.Salary);
                    }

                }


            // Using Joins

                Console.WriteLine("Using Join - \n\n");

                List<Sample> list1 = new List<Sample>(){
               new Sample(1,"Ram",10000,1),
               new Sample(2,"Shyam",15000,2),
               new Sample(3,"John",10000,2),
               new Sample(4,"Mike",40000,3),
               new Sample(5,"Tom",50322,1),
               new Sample(6,"Jack",15000,4),
                new Sample(6,"Jack",15000,4),
                 new Sample(6,"Jack",15000,4),
          };


                List<Department> list2 = new List<Department>(){
               new Department(1,"HR"),
               new Department(2,"IT"),
               new Department(3,"Sales"),
               new Department(4,"Accounting"),
              
          };


                var joinedList = list1.Join(list2, l1 => l1.Dept, l2 => l2.Id,
                    (l1, l2) => new
                    {

                        Id=l1.Id,
                        Name=l1.Name,
                        Salary=l1.Salary,
                        Dept=l2.DeptName

                    });

            foreach(var item in joinedList)
            {
                Console.WriteLine("ID - "+item.Id+" Name - "+item.Name+" Salary - "+item.Salary+" Department - "+item.Dept);
            }

            // Select Clause
            Console.WriteLine("\nUsing Select Clause\n");
            var selectList = list1.Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Salary = s.Salary,
                Dept = s.Dept
            })
            .Where(s => s.Salary > 20000).OrderByDescending(s => s.Salary)
            .Join(list2, l1 => l1.Dept, l2 => l2.Id, (l1, l2) => new{ 
                Id = l1.Id,
                Name = l1.Name,
                Salary = l1.Salary,
                Dept = l2.DeptName

            });

            foreach (var item in selectList)
            {
                Console.WriteLine("ID - " + item.Id + " Name - " + item.Name + " Salary - " + item.Salary + " Department - " + item.Dept);
            }

            // Quantifier operators - All
            Console.WriteLine("\n Quantifier operators - All\n");
            var opAll = list1.All(s=>s.Salary>20000);

            Console.WriteLine("Result - "+opAll);

            // Quantifier operators - Any
            Console.WriteLine("\n Quantifier operators - Any\n");
            var opAny = list1.Any(s => s.Salary > 20000);

            Console.WriteLine("Result - " + opAny);


            // Using Contains

            Console.WriteLine("\n Using Contains\n");

            Sample oSample = new Sample(6, "Jack", 15000, 4);
            // it always returns false even if object present in list, To work well we must write IEqualityComparer class
            var isPresent = list1.Contains(oSample);
            
            //Console.WriteLine("\n object is - "+(isPresent==true?"Present":"Not Present")+" in list"); 

            var isPresentUsingIEqualityComparer = list1.Contains(oSample,new SampleComparer());
            Console.WriteLine("\n Using IEqualityComparer object is - " + (isPresentUsingIEqualityComparer == true ? "Present" : "Not Present") + " in list"); 


            // Count 
            Console.WriteLine("\n Using Count ");
            Console.WriteLine("\n Total elements - " + list1.Count());
            Console.WriteLine("\n Total elements having salary > 20000 => " + list1.Count(s=>s.Salary>20000)); 


            // Distinct 
            Console.WriteLine("\n Elements before distinct");
            list1.ForEach(s => {
                Console.WriteLine("Id - "+s.Id+" Name - "+s.Name+" Salary - "+s.Salary);
            });

            // it will not work without IEqualityComparer with Complex Type
            Console.WriteLine("\n Elements After distinct (but it will not work without IEqualityComparer with Complex Type)");
            list1.Distinct().ToList<Sample>().ForEach(s =>
            {
                Console.WriteLine("Id - " + s.Id + " Name - " + s.Name + " Salary - " + s.Salary);
            });

            // it will  work only with IEqualityComparer with Complex Type
            Console.WriteLine("\n Elements After distinct (will work with IEqualityComparer with Complex Type)");
            list1.Distinct(new SampleComparer()).ToList<Sample>().ForEach(s =>
            {
                Console.WriteLine("Id - " + s.Id + " Name - " + s.Name + " Salary - " + s.Salary);
            });


            List<Sample> sList2 = new List<Sample>(){
               new Sample(1,"Ram",10000,1),
               new Sample(2,"Shyam",15000,2),
               new Sample(3,"John",10000,2),
               
          };

            // Except 
            // it will not work without IEqualityComparer with Complex Type
            Console.WriteLine("\n Elements After Except (but it will not work without IEqualityComparer with Complex Type)");
            list1.Except(sList2,new SampleComparer()).ToList<Sample>().ForEach(s =>
            {
                Console.WriteLine("Id - " + s.Id + " Name - " + s.Name + " Salary - " + s.Salary);
            });

            // it will not work without IEqualityComparer with Complex Type - Without Id
            Console.WriteLine("\n Elements After Except (but it will not work without IEqualityComparer with Complex Type)");
            list1.Except(sList2, new SampleComparer()).ToList<Sample>().ForEach(s =>
            {
                Console.WriteLine(" Name - " + s.Name + " Salary - " + s.Salary);
            });

            // salary total
            Console.WriteLine("\n Elements After Except (but it will not work without IEqualityComparer with Complex Type)");
            int sal = 0;
            list1.Except(sList2, new SampleComparer()).ToList<Sample>().ForEach(s =>
            {
                sal = sal + s.Salary;

            });

            Console.WriteLine(" Salary total "+ sal);


            DemoClass democlass = new DemoClass(2,1000.20m);
            democlass.displayDemoClassProp();

            Console.ReadLine();

        }
    }
}

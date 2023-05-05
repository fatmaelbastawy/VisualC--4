using System;
using System.Collections.Generic;
using static Lab4.Animal;

namespace Lab4
{


    public  delegate void deadanimal(Animal animal);
    public class Animal
    {
        public string Name { get; set; }

        public event deadanimal deadanimal;    

        public void Die()
        {
            if (deadanimal != null)
            {
                deadanimal.Invoke(this);
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }



    }
    public class Zoo
    {
        public List<Animal> Animallist { get; set; } = new List<Animal>();

        public void AddAnimal(Animal addAnimal)
        {
            Animallist.Add(addAnimal);
        }

        public void RemoveAnimal(Animal animal)   
        {
            Animallist.Remove(animal);
        }


        public void displayAnimals()
        {

            foreach (var item in Animallist)
            {
                Console.WriteLine($"{item}");
            }
           
        }


        

    }

               // // (2) // //

    public delegate void newSalary(float salary);     


    public delegate bool filter_employee(float salary);        

    public class Employee  //publisher//                  
    {
        public string Name { get; set; }

        public float Salary { get; set; }

        public event newSalary newSalary;                               

        public void increase_salary(float increase)        
        {
            Salary += increase;

            newSalary.Invoke(increase);



        }

        public override string ToString()
        {
            return $"Employee:{Salary}";
        }

        
    }

    public class Company   //supscriper//
    {
        public string Name { get; set; }
        public float Budget { get; set; }

        public List<Employee> FilterEmpLIST { set; get; } = new List<Employee>();       /* 1*/

        public List<Employee> employess { set; get; } = new List<Employee>();               /*2*/
        

        public List<Employee> filterEmployees(List<Employee> empList, filter_employee filter_employee)      
        {
            List<Employee> results = new List<Employee>();

            foreach (Employee item in empList)
            {
                if (filter_employee.Invoke(item.Salary))
                    results.Add(item);
            }

            return results;
        }


        public bool filterEmpSalary(float salary)                  
        {
            return salary > 7000;
        }

        public void decreese_budget(float n)        
        {
            Console.WriteLine(Budget - n);
        }



    }

    class Program
        {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            Animal elephant = new Animal();
            Animal lion = new Animal();
            Animal sparow = new Animal();
            Animal tiger = new Animal();

            elephant.Name = "Elephant";
            lion.Name = "Lion";
            sparow.Name = "Sparow";
            tiger.Name = "Tiger";

            zoo.AddAnimal(elephant);
            zoo.AddAnimal(lion);
            zoo.AddAnimal(sparow);
            zoo.AddAnimal(tiger);

            elephant.deadanimal += zoo.RemoveAnimal;
            lion.deadanimal += zoo.RemoveAnimal;
            sparow.deadanimal += zoo.RemoveAnimal;
            tiger.deadanimal += zoo.RemoveAnimal;

            elephant.Die();
            tiger.Die();

            zoo.displayAnimals();


            ///////////////////////////////////////////
            Company com = new Company();
            com.Name = "company1";
            com.Budget = 50000;


            Employee emp1 = new Employee();
            Employee emp2 = new Employee();
            Employee emp3 = new Employee();

            emp1.Salary = 10000;
            emp2.Salary = 7000;
            emp3.Salary = 4000;


            com.FilterEmpLIST.Add(emp1);
            com.FilterEmpLIST.Add(emp2);
            com.FilterEmpLIST.Add(emp3);


            List<Employee> empList = com.filterEmployees(com.FilterEmpLIST, com.filterEmpSalary);  // هنبعت الليست والفانكشن


            foreach (Employee item in empList)
            {
                Console.WriteLine(item);
            }
                
                       ////////*2*////////

            com.employess.Add(emp1);
            com.employess.Add(emp2);
            com.employess.Add(emp3);

            emp1.newSalary += com.decreese_budget;
            emp2.newSalary += com.decreese_budget;
            emp3.newSalary += com.decreese_budget;

            emp1.increase_salary(500);
            emp2.increase_salary(600);
            emp3.increase_salary(1000);


            foreach (Employee item in com.employess)
            {
                Console.WriteLine(item);
            }













        }    
       
    }
}       

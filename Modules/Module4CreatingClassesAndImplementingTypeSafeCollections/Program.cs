using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module03BasicTypesAndConstructs;

namespace Module4CreatingClassesAndImplementingTypeSafeCollections
{
    //Unit test method phases
    //Arrange: Create conditions for the test. Instatiate class and configure any input values that the test requires
    //Act: perform the action you want to test
    //Assert: verify the results of the action. If not as expected, the test fails

    class Program
    {
        //Classes and Delegates are always reference types
        //Structs are value types
        static void Main(string[] args)
        {
            Module03BasicTypesAndConstructs.Coffee firstBean = new Module03BasicTypesAndConstructs.Coffee(5, "Good Beans", "My Place");
            firstBean.OutOfBeans += new Inventory().HandleOutOfBeans;
            DrinksMachine dm = new DrinksMachine
            {
                Age = 2,
                Model = "BeanSmasher 3000",
                Make = "Fourth Coffee"
            };
            for (int x = 0; x < 4; x++)
            {
                
                dm.MakeCappuccino(ref firstBean);
            }
            DrinksMachine dm2 = new DrinksMachine(2);
            DrinksMachine dm3 = new DrinksMachine("Fourth Coffee", "BeanDestroyer 3000");
            DrinksMachine dm4 = new DrinksMachine(3, "Fourth Coffee", "BeanToaster Turbo");

            //Boxing is converting a value type to a reference type by wrapping it in an object
            int i = 100;
            object obj = i;
            // Unboxing is converting a reference type to a value type and you must explicitly cast the variable back to its original type
            int j;
            j = (int)obj;

            double weightInKilos = 80;
            double weightInPounds = Conversions.KilosToPounds(weightInKilos);
            //Using IComparer implentation to sort coffee by rating rather than Variety (default set by coffee class IComparable implentation)
            //Coffee Class (rather than coffee struct above)
            Coffee coffeenew1 = new Coffee();
            coffeenew1.AverageRating = 4.5;
            Coffee coffeenew2 = new Coffee();
            coffeenew2.AverageRating = 8.1;
            Coffee coffeenew3 = new Coffee();
            coffeenew3.AverageRating = 7.1;
            //Add the instances to arraylist
            ArrayList coffeList = new ArrayList();
            coffeList.Add(coffeenew1);
            coffeList.Add(coffeenew2);
            coffeList.Add(coffeenew3);
            //Sort arraylist by average rating(passed custom Icomparer implentation)
            coffeList.Sort(new CoffeeRatingComparer());
            foreach(Coffee drink in coffeList)
            {
                Console.WriteLine(drink.AverageRating);
            }
            Console.ReadLine();
        }

        #region nonstatic class
        public class DrinksMachine
        {
            public DrinksMachine():this(0, "", ""){}
            public DrinksMachine(int age):this(age,"",""){}
            public DrinksMachine(string make, string model) : this(0, make, model){}
            public DrinksMachine(int age, string make, string model)
            {
                this.Age = age;
                this.Make = make;
                this.Model = model;
            }

            // A property with a private field
            private int _age;
            public int Age
            {
                get => _age;
                set
                {
                    if(value > 0)
                    {
                        _age = value;
                    }
                }
            }
            //Properties
            public string Make;
            public string Model;
            //Methods
            public void MakeCappuccino(ref Module03BasicTypesAndConstructs.Coffee coffee)
            {
                Console.WriteLine("Making Cappuccino");
                coffee.MakeCoffee();
            }
            public void MakeEspresso( ref Module03BasicTypesAndConstructs.Coffee coffee)
            {
                Console.WriteLine("Making Expresso");
                coffee.MakeCoffee();
            }
            //The following defines an event. The delegate definition is not shown
            public event Module03BasicTypesAndConstructs.Coffee.OutOfBeansHandler OutOfBeans;
        }
        #endregion

        #region Static Class
        //Static class cannot be instantiated. Use the static keyword when creating class and any members within the class must use static as well.
        public static class Conversions
        {
            public static double PoundsToKilos(double pounds)
            {
                //Convert from pounds to kilos
                double kilos = pounds * 0.4536;
                return kilos;
            }
            public static double KilosToPounds(double Kilos)
            {
                //Convert from kilos to pounds
                double pounds = Kilos * 2.205;
                return pounds;
            }
        }

        #endregion

        #region Interfaces
        //Interfaces define a set of characteristics and behaviors
        //Member signatures only
        //No implementation details
        //Cannot be instantiated

        //Implemented by classes or structs
        //The class or struct must implement every member
        //implementation details do not matter to consumers
        //Member signatures must match definitions in interface


            //interface(s)
        public interface ILoyaltyCardHolder
        {
            int TotalPoints { get; }
            int AddPoints(decimal transactionValue);
            void ResetPoints();
        }

        public interface IBeverage
        {
            int GetServingTemperature(bool includesMilk);
            bool IsFairTrade { get; set; }
            //event EventHandler OnSoldOut;
            //string this[int index] { get;set; }
        }

        //Class(es) implementing above interface(s)
        public class Customer: ILoyaltyCardHolder
        {
            private int totalPoints;
            public int TotalPoints
            {
                get => totalPoints;
            }
            public int AddPoints(decimal transactionValue)
            {
                int points = decimal.ToInt32(transactionValue);
                totalPoints += points;
                return points;
            }
            public void ResetPoints()
            {
                totalPoints = 0;
            }
        }

        public class Coffee: IBeverage, IComparable
        {
            private int servingTempWithoutMilk { get; set; }
            private int servingTempWithMilk { get; set; }
            public int GetServingTemperature(bool includesMilk)
            {
                return includesMilk ? servingTempWithMilk : servingTempWithoutMilk;
            }

            public double AverageRating { get; set; }
            public string Variety { get; set; }
            int IComparable.CompareTo(object obj)
            {
                Coffee coffee2 = obj as Coffee;
                return String.Compare(this.Variety, coffee2.Variety);
            }

            public bool IsFairTrade { get; set; }
        }
        public class CoffeeRatingComparer: IComparer
        {
            public int Compare(Object x, Object y)
            {
                Coffee coffee1 = x as Coffee;
                Coffee coffee2 = y as Coffee;
                double rating1 = coffee1.AverageRating;
                double rating2 = coffee2.AverageRating;
                return rating1.CompareTo(rating2);
            }
        }

        #endregion
    }
}

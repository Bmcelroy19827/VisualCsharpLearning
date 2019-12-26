using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module03BasicTypesAndConstructs;//Added module 3 to references so I could write using statement

namespace Module03BasicTypesAndConstructs
{
    public class Program
    {
        // Create variables with a fixed set of possible values outside of main for enums/structs
        // numeric values autoincrement and start at 0 by default but can be changed by assigning
        enum Day { Sunday=1, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        static void Main(string[] args)
        {

            #region Enums
            //Set enum variable by name or value
            Day favoriteDay = Day.Friday;
            // is equivalent to
            Day favoriteDay1 = (Day)6;

            Console.WriteLine($"{favoriteDay} via Day.Friday and {favoriteDay1} via (Day)6.");

            #endregion

            #region structs
            //instantiating a struct without constructor
            Coffee coffee1 = new Coffee
            {
                Strength = 3,
                Bean = "Arabica",
                CountryOfOrigin = "Columbia"
            };
            Coffee coffee2 = new Coffee(5, "Antigua Volcanic", "Guetemala");

            //Accessing a public array in a struct
            PublicMenu myMenu = new PublicMenu("cocoa");
            string firstDrink = myMenu.beverages[0];
            Console.WriteLine(firstDrink);

            //Accessing a value in a private array via indexer
            Menu myPrivateMenu = new Menu("YooHoo", "Screwdriver");
            string firstPrivateDrink = myPrivateMenu[0];
            int numberOfChoices = myPrivateMenu.Length;
            Console.WriteLine($"The first private drink is {firstPrivateDrink} which I accessed via an indexer and the number of beverages to choose is {numberOfChoices}.");
            #endregion

            #region Collections
            //Collections are multiple items of the same time stored in a variable
            //List store linear collections of items
            //Dictionary store collections of key/value pairs
            //Queue store items in a FIFO collection
            //Stack store items in a LIFO collection
            //Core Operations of collections: Add items, remove items, retrieve specific items, count items, iterate through collection(one at a time)

            #region List collection
            ArrayList beverages = new ArrayList();
            //Items are implicitly cast to the object type when you add them
            beverages.Add(coffee1);
            beverages.Add(coffee2);
            //retrieve items by index
            //Items must be explicitly cast back to their original type
            Coffee firstCoffee = (Coffee)beverages[0];
            Coffee secondCoffee = (Coffee)beverages[1];
            Console.WriteLine(firstCoffee + " " + secondCoffee);
            //foreach loop to iterate over collection
            foreach(Coffee c in beverages)
            {
                Console.WriteLine(c.CountryOfOrigin);
            }
            #endregion

            #region Dictionary Collection
            //Create a new hashtable collection
            Hashtable ingredients = new Hashtable();
            // Add some key/value pairs to the collection
            ingredients.Add("Cafe au Lait", "Coffee, Milk");
            ingredients.Add("Cafe Mocha", "Coffee, Milk, Chocolate");
            ingredients.Add("Cappuccino", "Coffee, Milk, Foam");
            ingredients.Add("Irish Coffee", "Coffee, Whiskey, Cream, Sugar");
            ingredients.Add("Macchiato", "Coffee, Milk, Foam");
            //Check whether a key exists.
            if(ingredients.ContainsKey("Cafe Mocha"))
            {
                // Retrieve the value associated with a key.
                Console.WriteLine("the ingredients of a cafe mocha are: {0}", ingredients["Cafe Mocha"]);
            }

            // dictionary classes actually contain two enumerable collections so you can iterate over either of these collections
            // although it is usually the keys you iterate over to retrieve the values
            foreach (string key in ingredients.Keys)
            {
                // For Each key, retrieve the value associated with the key.
                Console.WriteLine("The ingredients of a {0} are {1}", key, ingredients[key]);
            }

            #endregion

            #region Querying a Collection
            //LINQ expressions
            // from <variable names> in <data source>
            // where <selection criteria>
            // orderby <result ordering criteria>
            // select <variable names>
            // Can also use: FirstOrDefault, Last, Max, and Min after the query above (use dot notation on the variable name(s)

            // Create a new hashtable with drinks and prices
            Hashtable prices = new Hashtable();
            prices.Add("Cafe au Lait", 1.99M);
            prices.Add("Caffe Americano", 1.89M);
            prices.Add("Cafe Mocha", 2.99M);
            prices.Add("Cappuccino", 2.49M);
            prices.Add("Espresso", 1.49M);
            prices.Add("Espresso Romana", 1.59M);
            prices.Add("English Tea", 1.69M);
            prices.Add("Juice", 2.89M);
            // Select all the drinks that cost les than $2.00 and order them by cost.
            var bargains =
                from string drink in prices.Keys
                where (Decimal)prices[drink] < 2.00M
                orderby prices[drink] ascending
                select drink;
            // Display the results
            foreach(string bargain in bargains)
            {
                Console.WriteLine("{0} costs {1}", bargain, prices[bargain]);
            }

            #endregion

            #endregion

            #region Handling Events
            // Delegates are reference to Methods
            // They can be saved as field and passed as parameter
            //They allow one to execute code that is provided from external sources
            //Delegates can be executed exactly like methods
            //Delegates first parameter is the object that raised the event
            //Second parameter is the event arguments that much be an instance of EventArgs or an instance of a class that derives from EventArgs


            // events wrap delegates like a property wraps a field
            // Allow the class to notify other consumers of actions performed within it.
            // An event may only be raised by it's containing class

            Inventory myInventory = new Inventory();
            coffee2.OutOfBeans += myInventory.HandleOutOfBeans;
            for (int i = 0; i < 9; i++)
            {
                coffee2.MakeCoffee();
            }
            

            #endregion


            Console.ReadLine();
        }
    }

    #region Basic Struct construction
    //The Struct keyword is preceded by an access modifier - "public" below. options are public, internal, and private.
    public struct Coffee
    {
        //Custom Constructor
        public Coffee(int strength, string bean, string origin)
        {
            if (strength < 1)
            {
                this.strength = 1;
            }
            else if(strength > 5)
            {
                this.strength = 5;
            }
            else
            {
                this.strength = strength;

            }
            this.Bean = bean;
            this.CountryOfOrigin = origin;
            this.e = null;
            this.OutOfBeans = null;
            this.currentStockLevel = 5;
            this.minimumStockLevel = 3;
        }

        public EventArgs e;
        public delegate void OutOfBeansHandler(Coffee coffee, EventArgs args);
        public event OutOfBeansHandler OutOfBeans;

        int currentStockLevel;
        int minimumStockLevel;
        public void MakeCoffee()
        {
            //Decrement the stock level.
            currentStockLevel--;
            Console.WriteLine("Stock Level: {0} Minimum Stock: {1} of {2}", currentStockLevel, minimumStockLevel, this.Bean);
            // If the stock level drops below the minimum, raise the event
            if (currentStockLevel < minimumStockLevel)
            {
                // Check whether the event is null
                if (OutOfBeans != null)
                {
                    // Raise the event.
                    OutOfBeans(this, e);
                }
            }
        }

        private int strength;
        //Below is Property that can be used to control access to fields (in this case 'strength') 
        public int Strength
        {
            get => strength;
            set
            {
                if(value < 1)
                {
                    strength = 1;
                }
                else if(value > 5)
                {
                    strength = 5;
                }
                else
                {
                    strength = value;
                }
            }
        }
        //Abbreviated syntax that reads and writes to a private field without performing any additional logic
        public string Bean { get; set; }
        public string CountryOfOrigin;

        //const property returns a literal value
        public string Hello { get => "World"; }

        //publicly read but set only by its containing class
        //public int Strength {get; private set;}

        //readonly property where only the constructor can set the value to this property
        //public int Strength{get;}

        //Create a property that writes to a private field implicitly created by the compiler. known as auto-implemented properties
        //public int Strength(private get; set;}

    }

    #endregion

    #region Structs that include an array and possibly an indexer
    public struct PublicMenu
    {
        public string[] beverages;
        public PublicMenu(string bev1)
        {
            beverages = new string[] { "Americano", "Cafe au Lait", "Cafe Machiato", "Cappuccino", "Espresso", bev1 };
        }
    }

    public struct Menu
    {
        public Menu(string bev1, string bev2)
        {
            beverages = new string[] { bev1, bev2 };
        }

        private string[] beverages;
        //This is the indexer
        public string this[int index]
        {
            get => this.beverages[index];
            set
            {
                this.beverages[index] = value;
            }
        }
        //Enable client code to determine the size of the collection
        public int Length
        {
            get => beverages.Length;
        }
    }

    #endregion

    public class Inventory
    {

        public void HandleOutOfBeans(Coffee sender, EventArgs args)
        {
            string coffeeBean = sender.Bean;
            Console.WriteLine("ReOrder more {0} beans. We are out!", coffeeBean);
            UnsubscribeFromEvent(sender);
        }
        public void SubscribeToEvent(Coffee sender)
        {
            sender.OutOfBeans += HandleOutOfBeans;
        }
        //Very important to unsubscribe from events when you are done with them to prevent memory leaks
        public void UnsubscribeFromEvent(Coffee sender)
        {
            sender.OutOfBeans -= HandleOutOfBeans;
        }
    }

}


using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace GR44_W2_SodaCrate
{
    //internal record Product2(int ID, string Name, decimal Price, decimal DepositFee = 0m, string Type = "Product")
    //{
    //    public decimal TotalPrice => Price + DepositFee;
    //};

    internal abstract class Product
    {
        public int ID { get; }
        public string Name { get; }
        public decimal Price { get; }
        public decimal DepositFee { get; }

        protected Product(int id, string name, decimal price, decimal depositFee)
        {
            ID = id;
            Name = name;
            Price = price;
            DepositFee = depositFee;
        }
    }


    internal class ProductCollection : KeyedCollection<int, Product>
    {
        public ProductCollection()
        {
            LoadData();
            SaveData();
        }
        protected override int GetKeyForItem(Product item)
        {
            return item.ID;
        }

        private string DataFilePath => AppDomain.CurrentDomain.BaseDirectory + "ProductData.txt";

        private void LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                // Can't make it handle sub-types
                //string json = File.ReadAllText(DataFilePath);
                //if (!string.IsNullOrWhiteSpace(json))
                //{
                //    var listOfProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Product>>(json);
                //    if (listOfProducts is not null)
                //        foreach (Product p2 in listOfProducts)
                //            Add(p2);
                //}
            }

            if (Count == 0)
            {
                // Add some default products
                Add(new SodaCrate(2, "Soda Crate 4x6", 0, 20, 4, 6));
                Add(new SodaCrate(1, "Soda Crate 5x5", 0, 150, 5, 5));
                Add(new SodaBottle(4, "Fanta", 3, 0.5m));
                Add(new SodaBottle(5, "Päronsoda", 2, 0.5m));
                Add(new SodaBottle(3, "Coke", 3, 0.5m));
                Add(new SodaBottle(6, "Sockerhicka", 2, 0.5m));
                Add(new SodaBottle(7, "Pripps Blå klass 1", 2.8m, 0.5m));
                Add(new SodaBottle(8, "7UP", 3, 0.5m));
                Add(new SodaBottle(9, "Hallonsoda", 2, 0.5m));
                Add(new SodaBottle(10, "Julmust", 2.25m, 0.5m));
                Add(new SodaBottle(11, "Wichyvatten", 1.5m, 0.5m));
                Add(new SodaBottle(12, "Loka Citron", 1.8m, 0.5m));
            }
        }
        private void SaveData()
        {
            //List<string> list = new();
            //foreach (Product item in Items)
            //{
            //    string temp; 
            //    switch (item.Type)
            //    {
            //        case "Product":
            //            break;
            //        case "SodaCrate":
            //            temp = JsonConvert.SerializeObject(item as SodaCrate);
            //            list.Add(temp);
            //            break;
            //        case "SodaBottle":
            //            temp = JsonConvert.SerializeObject(item as SodaBottle);
            //            list.Add(temp);
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //File.WriteAllLines(DataFilePath, list);


            // Can't make it handle sub-types this way
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(Items,  Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText(DataFilePath, json);
        }


    }
}

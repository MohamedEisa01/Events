using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock("Neugrx");
            stock.Price = 100m;

            stock.OnStockPriceChanged += Stock_OnStockPriceChanged;//subscrib
            stock.ChangeStockPriceBy(0.02m);
            stock.ChangeStockPriceBy(-0.01m);
            stock.ChangeStockPriceBy(0.15m);

            stock.OnStockPriceChanged -= Stock_OnStockPriceChanged;//unsubscrib
            stock.ChangeStockPriceBy(0.02m);
            stock.ChangeStockPriceBy(0.01m);
            stock.ChangeStockPriceBy(0.15m);
        }

        private static void Stock_OnStockPriceChanged(Stock stock, decimal oldPrice)
        {
            if (stock.Price > oldPrice)
            {
                Console.BackgroundColor = ConsoleColor.Green;

            }else if(stock.Price < oldPrice)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($"{stock.Name} : {stock.Price}");
        }
    }
    public delegate void StockPriceHandler(Stock stock, decimal oldPrice);
    public class Stock
    {
        private string name;
        private decimal price;

        public string Name => this.name;
        public decimal Price { get => this.price; set => this.price = value; }

        public event StockPriceHandler OnStockPriceChanged;
        public Stock(string name)
        {
            this.name = name;
        }
        public void ChangeStockPriceBy(decimal percent)
        {
            decimal oldPrice = this.price;
            this.price += Math.Round(this.price * percent, 2);
            if (OnStockPriceChanged != null) //Check if there is a subscriber 
            {
                OnStockPriceChanged(this, oldPrice);
            }
        }
    }
}

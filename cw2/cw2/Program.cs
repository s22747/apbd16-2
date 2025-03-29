using cw2.Containers;
using cw2.Exceptions;
using cw2.Models;
using cw2.Transport;

namespace cw2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //tworzenie kontenerow
            var bananas = new ProductType("Banany", 10);
            var milk = new ProductType("Mleko", 4);
            var helium = new ProductType("Hel", 5);

            var fridge = new RefrigeratedContainer(1300, 220, 270, 5000, temperatureC: 12);
            fridge.LoadProduct(bananas, 2000);

            var liquid = new LiquidContainer(1000, 200, 250, 8000, isHazardous: false);
            liquid.Load(3000);

            var gas = new GasContainer(1100, 210, 260, 6000, pressureAtm: 10);
            gas.Load(5000);

            var ship1 = new Ship("kontenerowiec 1", 25, 5, 50);
            var ship2 = new Ship("kontenerowiec 2", 20, 3, 30);

            ship1.AddContainer(fridge);
            ship1.AddContainer(liquid);
            ship1.AddContainer(gas);

            ship1.PrintShipInfo();

            ship1.RemoveContainer(gas.SerialNumber);

            var newGas = new GasContainer(1100, 210, 260, 6000, pressureAtm: 12);
            newGas.Load(4500);
            ship1.ReplaceContainer(liquid.SerialNumber, newGas);

            Console.WriteLine("\n *****po wymianie kontenera*****");
            ship1.PrintShipInfo();

            ship1.RemoveContainer(fridge.SerialNumber);
            ship2.AddContainer(fridge);

            Console.WriteLine("\n *****statek 1 po przeniesieniu chlodni*****");
            ship1.PrintShipInfo();

            Console.WriteLine("\n*****Statek 2 po przyjeciu chlodni*****");
            ship2.PrintShipInfo();
        }
    }
}

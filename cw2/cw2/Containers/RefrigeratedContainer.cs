using cw2.Exceptions;
using cw2.Interfaces;
using cw2.Models;

namespace cw2.Containers
{
    public class RefrigeratedContainer : Container, IHazardNotifier
    {
        public ProductType? StoredProduct { get; private set; }
        public double TemperatureC { get; }

        public RefrigeratedContainer(
            double tareWeightKg,
            double heightCm,
            double depthCm,
            double maxLoadKg,
            double temperatureC)
            : base(tareWeightKg, heightCm, depthCm, maxLoadKg, "C")
        {
            TemperatureC = temperatureC;
        }

        public void LoadProduct(ProductType product, double weight)
        {
            if (StoredProduct != null && StoredProduct.Name != product.Name)
            {
                NotifyHazard("mie mozna ladowac roznych typow produktow do jednego kontenera");
                throw new OverfillException("typ produktu niezgodny z juz przechowywanym");
            }

            if (TemperatureC < product.RequiredTemperatureC)
            {
                NotifyHazard($"temperatura kontenera ({TemperatureC}°C) jest za niska dla produktu {product.Name}");
                throw new OverfillException("temperatura zbyt niska dla danego produktu");
            }

            if (CurrentLoadKg + weight > MaxLoadKg)
            {
                NotifyHazard("proba przeladowania kontenera chlodniczego");
                throw new OverfillException("zbyt duza masa produktu");
            }

            StoredProduct = product;
            CurrentLoadKg += weight;
        }

        public override void Unload()
        {
            CurrentLoadKg = 0;
            StoredProduct = null;
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"[ALERT - {SerialNumber}] {message}");
        }

        public override string ToString()
        {
            string productInfo = StoredProduct != null ? StoredProduct.ToString() : "Brak";
            return base.ToString() + $", produkt: {productInfo}, temp: {TemperatureC}°c";
        }
    }
}

namespace cw2.Models
{
    public class ProductType
    {
        public string Name { get; }
        public double RequiredTemperatureC { get; }

        public ProductType(string name, double requiredTemperatureC)
        {
            Name = name;
            RequiredTemperatureC = requiredTemperatureC;
        }

        public override string ToString()
        {
            return $"{Name} (min {RequiredTemperatureC}°C)";
        }
    }
}

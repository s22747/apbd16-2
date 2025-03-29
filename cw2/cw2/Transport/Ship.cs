using cw2.Containers;

namespace cw2.Transport
{
    public class Ship
    {
        public string Name { get; }
        public double MaxSpeedKnots { get; }
        public int MaxContainerCount { get; }
        public double MaxTotalWeightTons { get; }

        private List<Container> containers = new();

        public Ship(string name, double maxSpeedKnots, int maxContainerCount, double maxTotalWeightTons)
        {
            Name = name;
            MaxSpeedKnots = maxSpeedKnots;
            MaxContainerCount = maxContainerCount;
            MaxTotalWeightTons = maxTotalWeightTons;
        }

        public bool AddContainer(Container container)
        {
            if (containers.Count >= MaxContainerCount)
            {
                Console.WriteLine("przekroczono maksymalna liczbe kontenerow");
                return false;
            }

            double currentWeight = containers.Sum(c => c.CurrentLoadKg + c.TareWeightKg);
            double newTotalWeight = currentWeight + container.CurrentLoadKg + container.TareWeightKg;

            if (newTotalWeight > MaxTotalWeightTons * 1000) // konwersja ton na kg
            {
                Console.WriteLine("przekroczono maksymalna wage statku");
                return false;
            }

            containers.Add(container);
            return true;
        }

        public bool RemoveContainer(string serialNumber)
        {
            var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                containers.Remove(container);
                return true;
            }

            Console.WriteLine($"nie znaleziono kontenera {serialNumber}");
            return false;
        }

        public void ReplaceContainer(string serialNumber, Container newContainer)
        {
            int index = containers.FindIndex(c => c.SerialNumber == serialNumber);
            if (index == -1)
            {
                Console.WriteLine($"nie znaleziono kontenera {serialNumber}");
                return;
            }

            containers[index] = newContainer;
            Console.WriteLine($"kontener {serialNumber} zostal zastapiony");
        }

        public void UnloadAll()
        {
            foreach (var container in containers)
            {
                container.Unload();
            }
        }

        public void PrintShipInfo()
        {
            Console.WriteLine($"\n=== STATEK: {Name} ===");
            Console.WriteLine($"predkosc maksymalna: {MaxSpeedKnots} wezlow");
            Console.WriteLine($"limit kontenerow: {MaxContainerCount}");
            Console.WriteLine($"limit masy: {MaxTotalWeightTons} ton");
            Console.WriteLine($"liczba kontenerow na pokladzie: {containers.Count}");
            Console.WriteLine("kontenery:");
            foreach (var container in containers)
            {
                Console.WriteLine(container);
            }
        }
    }
}

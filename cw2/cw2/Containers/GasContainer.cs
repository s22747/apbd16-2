using cw2.Exceptions;
using cw2.Interfaces;

namespace cw2.Containers
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double PressureAtm { get; }

        public GasContainer(
            double tareWeightKg,
            double heightCm,
            double depthCm,
            double maxLoadKg,
            double pressureAtm)
            : base(tareWeightKg, heightCm, depthCm, maxLoadKg, "G")
        {
            PressureAtm = pressureAtm;
        }

        public override void Load(double weight)
        {
            if (CurrentLoadKg + weight > MaxLoadKg)
            {
                NotifyHazard($"przekroczono ladownosc kontenera z gazem ({SerialNumber})");
                throw new OverfillException($"zaladunek przekracza pojemnosc kontenera gazowego {SerialNumber}");
            }

            CurrentLoadKg += weight;
        }

        public override void Unload()
        {
            // Gaz – 5% zostaje w środku
            CurrentLoadKg *= 0.05;
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"[ALERT - {SerialNumber}] {message}");
        }

        public override string ToString()
        {
            return base.ToString() + $", cisnienie: {PressureAtm} atm";
        }
    }
}

using cw2.Exceptions;
using cw2.Interfaces;

namespace cw2.Containers
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; }

        public LiquidContainer(
            double tareWeightKg,
            double heightCm,
            double depthCm,
            double maxLoadKg,
            bool isHazardous)
            : base(tareWeightKg, heightCm, depthCm, maxLoadKg, "L")
        {
            IsHazardous = isHazardous;
        }

        public override void Load(double weight)
        {
            double limit = IsHazardous ? MaxLoadKg * 0.5 : MaxLoadKg * 0.9;

            if (CurrentLoadKg + weight > limit)
            {
                NotifyHazard($"proba przeladowania kontenera z ciecza! limit: {limit}kg, proba: {weight + CurrentLoadKg}kg");
                throw new OverfillException($"przekroczono bezpieczny limit dla kontenera {SerialNumber}");
            }

            CurrentLoadKg += weight;
        }

        public override void Unload()
        {
            CurrentLoadKg = 0;
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"[ALERT - {SerialNumber}] {message}");
        }
    }
}

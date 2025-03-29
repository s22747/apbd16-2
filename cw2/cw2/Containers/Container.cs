using cw2.Exceptions;

namespace cw2.Containers
{
    public abstract class Container
    {
        private static int serialCounter = 0;

        public string SerialNumber { get; }
        public double TareWeightKg { get; }
        public double HeightCm { get; }
        public double DepthCm { get; }
        public double MaxLoadKg { get; }
        public double CurrentLoadKg { get; protected set; }
        public string ContainerType { get; }

        protected Container(double tareWeightKg, double heightCm, double depthCm, double maxLoadKg, string containerType)
        {
            TareWeightKg = tareWeightKg;
            HeightCm = heightCm;
            DepthCm = depthCm;
            MaxLoadKg = maxLoadKg;
            ContainerType = containerType;

            serialCounter++;
            SerialNumber = $"KON-{containerType}-{serialCounter}";
        }

        public virtual void Load(double weight)
        {
            if (CurrentLoadKg + weight > MaxLoadKg)
            {
                throw new OverfillException($"proba przeladowania kontenera {SerialNumber} - maksymalna ladownosc: {MaxLoadKg}kg");
            }

            CurrentLoadKg += weight;
        }

        public abstract void Unload();

        public override string ToString()
        {
            return $"[{SerialNumber}] typ: {ContainerType}, masa ladunku: {CurrentLoadKg}kg / {MaxLoadKg}kg";
        }
    }
}

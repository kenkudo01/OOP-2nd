using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace sumilation_prigram
{
    using System;

    // Interface for Conditions
    public interface IConditions
    {
        Gases Transform(Gases p);
    }

    // Singleton Thunderstorm class
    public class Thunderstorm : IConditions
    {
        private static readonly Lazy<Thunderstorm> _instance = new Lazy<Thunderstorm>(() => new Thunderstorm());
        public static Thunderstorm Instance => _instance.Value;

        private Thunderstorm() { }

        public Gases Transform(Gases p)
        {
            return p.Transform(this);
        }
    }

    // Singleton Sunshine class
    public class Sunshine : IConditions
    {
        private static readonly Lazy<Sunshine> _instance = new Lazy<Sunshine>(() => new Sunshine());
        public static Sunshine Instance => _instance.Value;

        private Sunshine() { }

        public Gases Transform(Gases p)
        {
            return p.Transform(this);
        }
    }

    // Singleton Other class
    public class Other : IConditions
    {
        private static readonly Lazy<Other> _instance = new Lazy<Other>(() => new Other());
        public static Other Instance => _instance.Value;

        private Other() { }

        public Gases Transform(Gases p)
        {
            return p.Transform(this);
        }
    }

}

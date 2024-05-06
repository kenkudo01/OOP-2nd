using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace sumilation_prigram
{
    using System;

    public abstract class Gases
    {
        protected string Name;
        protected double Thickness;

        protected Gases(string name, double thickness = 0)
        {
            Name = name;
            Thickness = thickness;
        }

        public string GetName() => Name;

        public bool Exists() => Thickness >= 0.5;

        public double GetThickness() => Thickness;

        public void AddThickness(double amount)
        {
            Thickness += amount;
        }

        public void ChangeThickness(double percent, out double amountChanged)
        {
            amountChanged = (percent / 100) * Thickness;
            Thickness -= amountChanged;
        }

        public abstract Gases Transform(Thunderstorm p);
        public abstract Gases Transform(Sunshine p);
        public abstract Gases Transform(Other p);
    }

    public class Ozone : Gases
    {
        public Ozone(string name, double thickness = 0) : base(name, thickness) { }

        public override Gases Transform(Thunderstorm p)
        {
            double amountChanged;
            ChangeThickness(0, out amountChanged);
            return this;
        }

        public override Gases Transform(Sunshine p)
        {
            double amountChanged;
            ChangeThickness(0, out amountChanged);
            return this;
        }

        public override Gases Transform(Other p)
        {
            double amountChanged;
            ChangeThickness(5, out amountChanged);
            return new Oxygen("Oxygen", amountChanged);
        }
    }

    public class Oxygen : Gases
    {
        public Oxygen(string name, double thickness = 0) : base(name, thickness) { }

        public override Gases Transform(Thunderstorm p)
        {
            double amountChanged;
            ChangeThickness(50, out amountChanged);
            return new Ozone("Ozone", amountChanged);
        }

        public override Gases Transform(Sunshine p)
        {
            double amountChanged;
            ChangeThickness(5, out amountChanged);
            return new Ozone("Ozone", amountChanged);
        }

        public override Gases Transform(Other p)
        {
            double amountChanged;
            ChangeThickness(10, out amountChanged);
            return new CarbonDioxide("Carbondioxide", amountChanged);
        }
    }

    public class CarbonDioxide : Gases
    {
        public CarbonDioxide(string name, double thickness = 0) : base(name, thickness) { }

        public override Gases Transform(Thunderstorm p)
        {
            double amountChanged;
            ChangeThickness(0, out amountChanged);
            return this;
        }

        public override Gases Transform(Sunshine p)
        {
            double amountChanged;
            ChangeThickness(5, out amountChanged);
            return new Oxygen("Oxygen", amountChanged);
        }

        public override Gases Transform(Other p)
        {
            double amountChanged;
            ChangeThickness(0, out amountChanged);
            return this;
        }
    }

}

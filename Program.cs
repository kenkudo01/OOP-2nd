using System;
using TextFile;


namespace sumilation_prigram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read input from file
            string[] lines = File.ReadAllLines("input.txt");
            int num = int.Parse(lines[0]);  // Number of gases
            List<Gases> gases = new List<Gases>();

            // Parse gases from lines
            for (int i = 1; i <= num; i++)
            {
                string[] parts = lines[i].Split(' ');
                string type = parts[0];
                double thickness = double.Parse(parts[1]);
                switch (type)
                {
                    case "Z":
                        gases.Add(new Ozone("Ozone", thickness));
                        break;
                    case "X":
                        gases.Add(new Oxygen("Oxygen", thickness));
                        break;
                    case "C":
                        gases.Add(new CarbonDioxide("Carbon", thickness));
                        break;
                }
            }

            // Parse weather sequence from the last line
            string weatherPattern = lines[num + 1];
            int cycle = 0;
            bool allAlive;

            do
            {
                allAlive = true;
                Console.WriteLine($"Cycle {cycle + 1}:");

                foreach (char condition in weatherPattern)
                {
                    for (int j = 0; j < gases.Count; j++)
                    {
                        Gases currentGas = gases[j];
                        double oldThickness = currentGas.GetThickness();

                        switch (condition)
                        {
                            case 'O':
                                gases[j] = currentGas.Transform(Sunshine.Instance);
                                break;
                            case 'S':
                                gases[j] = currentGas.Transform(Thunderstorm.Instance);
                                break;
                            case 'T':
                                gases[j] = currentGas.Transform(Other.Instance);
                                break;
                        }

                        // Check if any gas has perished
                        if (gases[j].GetThickness() <= 0)
                        {
                            allAlive = false;
                        }
                    }

                    // Print the status of gases after each weather condition
                    PrintGases(gases);
                }

                cycle++;
            } while (allAlive);

            // Print final states of all gases
            Console.WriteLine("Final states of all gases:");
            PrintGases(gases);
        }

        static void PrintGases(List<Gases> gases)
        {
            foreach (Gases gas in gases)
            {
                Console.WriteLine($"Gas: {gas.GetName()}, Thickness: {gas.GetThickness()}");
            }
            Console.WriteLine(); // Add a blank line for better readability
        }


    }

    
    
}
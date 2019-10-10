using RocketSimulator.Domain;
using System;
using System.Threading;
using static System.Math;
using static System.Console;

namespace RocketSimulator
{
    class Program
    {
        static Rocket[] rocketList = new Rocket[10];

        static void Main(string[] args)
        {
            bool shouldNotExit = true;

            uint rocketListCurrentIndexPosition = 0;

            while (shouldNotExit)
            {
                WriteLine("1. Add rocket");
                WriteLine("2. List rockets");
                WriteLine("3. Run simulation");
                WriteLine("4. Display velocity over time");
                WriteLine("5. Exit");

                ConsoleKeyInfo keyPressed = ReadKey(true);

                Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        {
                            WriteLine("Choose rocket to add: ");
                            WriteLine("");
                            WriteLine("1 - Archytas' Flying Pigeon");           // Able to fly about 200 meters before it ran out of steam.
                            WriteLine("2 - Goddard's Auburn rocket");           // It attained a height of 41 feet in 2.5 seconds, and it came to rest 184 feet from the launch pad.
                            WriteLine("3 - Goddard's Rosswell rocket");         // Goddard fired an 11 foot liquid fueled rocket, to a height of 2000 feet at a speed of 500 miles per hour.
                            WriteLine("4 - Bell X-1");                          // Achieved a speed of nearly 1,000 miles per hour (1,600 km/h; 870 kn)
                            WriteLine("5 - Navy Viking 7");                     // Set the new altitude record for single stage rockets by reaching 136 miles and a speed of 4,100 mph.
                            WriteLine("6 - Jupiter-C");                         // To an altitude of 680 mi (1,100 km), a speed of 16,000 mph (7 km/s), and a range of 3,300 mi (5,300 km).
                            WriteLine("7 - Saturn V");                          // The first stage burned for about 2 minutes and 41 seconds, lifting the rocket to an altitude of 42 miles (68 km) and a speed of 6,164 miles per hour (2,756 m/s) and burning 4,700,000 pounds (2,100,000 kg) of propellant.  S-II second stage burned for 6 minutes and propelled the craft to 109 miles (175 km) and 15,647 mph (6,995 m/s), close to orbital velocity. During Apollo 11, a typical lunar mission, the third stage burned for about 2.5 minutes until first cutoff at 11 minutes 40 seconds. At this point it was 1,430 nautical miles (2,650 km)  downrange and in a parking orbit at an altitude of 103.2 nautical miles (191.1 km)  and velocity of 17,432 mph (7,793 m/s).
                            
                            //Genererar ett hexadecimalt slumptal på exakt sju tecken som används som registry
                            var r = new Random();
                            int A = r.Next(16777216, 268435455);
                            string registry = A.ToString("X");

                            keyPressed = ReadKey(true);

                            Rocket newRocket = null;

                            if (keyPressed.Key == ConsoleKey.D1)
                            {
                                newRocket = new FlyingPigeon(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D2)
                            {
                                newRocket = new AuburnRocket(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D3)
                            {
                                newRocket = new RosswellRocket(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D4)
                            {
                                newRocket = new BellX(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D5)
                            {
                                newRocket = new NavyViking(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D6)
                            {
                                newRocket = new JupiterC(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D7)
                            {
                                newRocket = new SaturnV(registry);
                            }

                            Clear();

                            string name = newRocket.Brand;

                            Rocket theRocket = SearchRocketByName(name);

                                if (theRocket != null)
                                {
                                    WriteLine("Rocket already added");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    rocketList[rocketListCurrentIndexPosition++] = newRocket;
                                    WriteLine("Rocket added");
                                    Thread.Sleep(2000);
                                }

                        }

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            string nameHeader = "Name".PadRight(30, ' ');
                            string yearHeader = "Year".PadRight(10, ' ');
                            string registryHeader = "Regstry ";

                            Write(nameHeader);
                            Write(yearHeader);
                            WriteLine(registryHeader);

                            WriteLine("---------------------------------------------------");

                            foreach (Rocket car in rocketList)
                            {
                                if (car == null) continue;

                                string brand = car.Brand.PadRight(30, ' ');
                                string model = car.Model.PadRight(10, ' ');
                                string registry = car.Registry.PadRight(10, ' ');

                                Write(brand);
                                Write(model);
                                WriteLine(registry);
                            }

                            WriteLine("");
                            WriteLine("<Press any key to continue>");
                            ReadKey(true);
                        }

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        Write("Engine burn period (sec): ");

                        int seconds = int.Parse(ReadLine());

                        Clear();

                        string nameHeaderSimulation = "Name".PadRight(30, ' ');
                        string yearHeaderSimulation = "Year".PadRight(10, ' ');
                        string registryHeaderSimulation = "Velocity";

                        Write(nameHeaderSimulation);
                        Write(yearHeaderSimulation);
                        WriteLine(registryHeaderSimulation);

                        WriteLine("---------------------------------------------------");

                        foreach (Rocket rocket in rocketList)
                        {
                            if (rocket == null) continue;

                            rocket.Accelerate(seconds);

                            string brand = rocket.Brand.PadRight(30, ' ');
                            string model = rocket.Model.PadRight(10, ' ');
                            int velocity = rocket.Velocity;

                            Write(brand);
                            Write(model);
                            WriteLine(velocity);
                        }

                        WriteLine("");
                        WriteLine("<Press any key to continue>");
                        ReadKey(true);

                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:



                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:

                        shouldNotExit = false;

                        break;
                }

                Clear();
            }
        }

        private static Rocket SearchRocketByName(string name)
        {
            Rocket rocketReferenceToReturn = null;

            foreach (Rocket rocketName in rocketList)
            {
                if (rocketName == null) continue;

                if (rocketName.Brand == name)
                {
                    rocketReferenceToReturn = rocketName;
                    break;
                }
            }
            return rocketReferenceToReturn;
        }
    }
}

namespace RocketSimulator.Domain
{
    class FlyingPigeon : Rocket
    {
        public FlyingPigeon(string registry)
            : base("Archytas' Flying Pigeon", " 350 BC", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            if (seconds <= 15)
            {
                Velocity = (int)((15 * seconds - (seconds * seconds)) * 0.01 * 3.6);
            }
            else
            {
                Velocity = 0;
            }            
        }
    }
}
namespace RocketSimulator.Domain
{
    class AuburnRocket : Rocket
    {
        public AuburnRocket(string registry)
            : base("Goddard's Auburn rocket", "1926 AD", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            if (seconds < 3.5)
            {
                Velocity = (int)((14 * seconds - (6 * seconds * seconds)) * 3.6);
            }
            else
            {
                Velocity = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    class RosswellRocket : Rocket
    {
        public RosswellRocket(string registry)
            : base("Goddard's Rosswell rocket", "1937 AD", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            if (seconds < 75)
            {
                Velocity = (int)(-1 * ((3 * seconds * (seconds - 50))/100) * 3.6);
            }
            else
            {
                Velocity = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    class BellX : Rocket
    {
        public BellX(string registry)
            : base("Bell X-1", "1947 AD", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            Velocity = (int)(seconds * 1 * 3.6);
        }
    }
}

namespace RocketSimulator.Domain
{
    class NavyViking : Rocket
    {
        public NavyViking(string registry)
            : base("Navy Viking 7", "1951 AD", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            Velocity = (int)(seconds * 2.5 * 3.6);
        }
    }
}

namespace RocketSimulator.Domain
{
    class JupiterC : Rocket
    {
        public JupiterC(string registry)
            : base("Jupiter-C", "1957 AD", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            Velocity = (int)(seconds * 75 * 3.6);
        }
    }
}

namespace RocketSimulator.Domain
{
    class SaturnV : Rocket
    {
        public SaturnV(string registry)
            : base("Saturn V", "1969 AD", registry)
        {

        }

        public override void Accelerate(int seconds)
        {
            Velocity = (int)((Math.Pow(Math.E, (seconds / 14)) -1) * 3.6);
        }
    }
}


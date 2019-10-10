using System;
using System.Collections.Generic;
using System.Text;

namespace RocketSimulator.Domain
{
    abstract class Rocket
    {
        public Rocket(string brand, string model, string registry)
        {
            Brand = brand;
            Model = model;
            Registry = registry;
        }

        public string Brand { get; }

        public string Model { get; }

        private string registry;

        public string Registry
        {
            get
            {
                return registry;
            }
            set
            {
                if (value.Length > 7)
                {
                    registry = value.Substring(0, 7);
                }
                else
                {
                    registry = value;
                }
            }
        }

        public int Velocity { get; protected set; }
        public int FuelLeft { get; protected set; }

        public abstract void Accelerate(int seconds);
    }
}

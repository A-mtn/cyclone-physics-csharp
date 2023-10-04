using System;
using Cyclone.Math;

namespace Cyclone.Particles
{
public class Firework : Particle
    {
        public uint type;
        public double age;

        public bool Update(double duration)
        {
            // Update our physical state
            Integrate(duration);

            // We work backwards from our age to zero.
            age -= duration;
            return (age < 0);
        }
    }

    public struct FireworkRule
    {
        public uint type;
        public double minAge;
        public double maxAge;
        public Vector3 minVelocity;
        public Vector3 maxVelocity;
        public double damping;

        public struct Payload
        {
            public uint type;
            public uint count;

            public void Set(uint type, uint count)
            {
                this.type = type;
                this.count = count;
            }
        }

        public uint payloadCount;
        public Payload[] payloads;

        public void Init(uint payloadCount)
        {
            this.payloadCount = payloadCount;
            payloads = new Payload[payloadCount];
        }

        public void SetParameters(uint type, double minAge, double maxAge,
            Vector3 minVelocity, Vector3 maxVelocity, double damping)
        {
            this.type = type;
            this.minAge = minAge;
            this.maxAge = maxAge;
            this.minVelocity = minVelocity;
            this.maxVelocity = maxVelocity;
            this.damping = damping;
        }

        public double GetRandomDouble(double minimum, double maximum)
        { 
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        public static Vector3 RandomVector(Vector3 min, Vector3 max)
        {
            Random random = new Random();
            return new Vector3(
                random.NextDouble() * (max.x - min.x) + min.x,
                random.NextDouble() * (max.y - min.y) + min.y,
                random.NextDouble() * (max.z - min.z) + min.z
            );
        }
        
        public void Create(Firework firework, Firework parent = null)
        {
            firework.type = type;
            firework.age = GetRandomDouble(minAge, maxAge);
            Random random = new Random();

            Vector3 vel = new Vector3(0, 0, 0);
            if (parent != null)
            {
                // The position and velocity are based on the parent.
                firework.Position = new Vector3(parent.Position.x, parent.Position.y, parent.Position.z);
                vel += parent.Velocity;
            }
            else
            {
                Vector3 start = new Vector3(0, 0, 0);
                int x = random.Next(1, 3) - 1;
                start.x = 5.0f * x;
                firework.SetPosition(start.x, start.y, start.z);
            }

            vel += RandomVector(minVelocity, maxVelocity);
            firework.SetVelocity(vel.x, vel.y, vel.z);

            // We use a mass of one in all cases (no point having fireworks
            // with different masses, since they are only under the influence
            // of gravity).
            firework.Mass = 1;

            firework.Damping = damping;

            firework.SetAcceleration(0, -9.8, 0);

            firework.ClearAccumulator();
        }
    }
}
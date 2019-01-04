using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnowConeTycoon.Shared.Particles
{
    public class ParticleEmitter
    {
        List<IParticle> Particles;
        int MaxParticleCount = 0;
        int Radius;
        public Vector2 Velocity = new Vector2(50, 50);
        public float AngularVelocity = 0.1f;
        public Vector2 Position;
        public bool FlowOn = false;
        public float Gravity = 4f;
        public int TTL = 3000;

        public ParticleEmitter(int maxParticles, int x, int y, int radius, int ttl)
        {
            Particles = new List<IParticle>();
            MaxParticleCount = maxParticles;
            Position = new Vector2(x, y);
            Radius = radius;
            TTL = ttl;
        }

        private IParticle GenerateNewParticle()
        {
            Vector2 position = Position;

            position.X += Utilities.GetRandomInt(-Radius, Radius);
            position.Y += Utilities.GetRandomInt(-Radius, Radius);

            float vX = Velocity.X * (float)(Utilities.rand.NextDouble() * 2 - 1);
            float vY = Velocity.Y * (float)(Utilities.rand.NextDouble() * 2 - 1);

            Vector2 velocity = new Vector2(vX, vY);
            float angle = 0;
            float angularVelocity = AngularVelocity * (float)(Utilities.rand.NextDouble() * 2 - 1);

            float size = (float)Utilities.rand.NextDouble() * 3;

            return new Particle("particle", position, velocity, angle, angularVelocity, Color.White, TTL, size, Gravity);
        }

        public void Reset()
        {
            FlowOn = true;
        }

        public void Update(GameTime gameTime)
        {
            if (FlowOn)
            {
                if (Particles.Count < MaxParticleCount)
                {
                    Particles.Add(GenerateNewParticle());
                }
            }

            List<int> particlesToRemove = new List<int>();
            var index = 0;
            foreach (var particle in Particles)
            {
                particle.Update(gameTime);

                if (!particle.IsAlive)
                {
                    particlesToRemove.Add(index);
                }

                index++;
            }

            foreach (int i in particlesToRemove)
            {
                try
                {
                    Particles.RemoveAt(i);
                }
                catch { }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in Particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}

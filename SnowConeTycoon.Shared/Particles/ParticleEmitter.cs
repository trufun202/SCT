using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;

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
        private Vector2 Origin;
        private float CircleTime = 0;
        private float CircleRadius;
        public ParticleMovementPath Path = ParticleMovementPath.None;
        private string ParticleImage;
        private float MaxScale;

        public ParticleEmitter(int maxParticles, int x, int y, int radius, int ttl, string particleImage = "particle", float maxScale = 3)
        {
            Particles = new List<IParticle>();
            MaxParticleCount = maxParticles;
            Position = new Vector2(x, y);
            Origin = Position;
            Radius = radius;
            TTL = ttl;
            ParticleImage = particleImage;
            MaxScale = maxScale;
        }

        public void SetCircularPath(float radius)
        {
            Path = ParticleMovementPath.Circle;
            CircleRadius = radius;
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

            float size = (float)Utilities.rand.NextDouble() * MaxScale;

            return new Particle(ParticleImage, position, velocity, angle, angularVelocity, Color.White, TTL, size, Gravity);
        }

        public void Reset()
        {
            FlowOn = true;
            CircleTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (Path == ParticleMovementPath.Circle)
            {
                CircleTime += gameTime.ElapsedGameTime.Milliseconds * 0.01f;
                Position.X = (float)(Origin.X + Math.Cos(CircleTime) * CircleRadius);
                Position.Y = (float)(Origin.Y + Math.Sin(CircleTime) * CircleRadius);
            }

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

            if (FlowOn)
            {
                spriteBatch.Draw(ContentHandler.Images["particle"], new Rectangle((int)Position.X, (int)Position.Y, 200, 200), null, Color.White, 1f, new Vector2(ContentHandler.Images["particle"].Width / 2, ContentHandler.Images["particle"].Height / 2), SpriteEffects.None, 0);
            }
        }
    }
}

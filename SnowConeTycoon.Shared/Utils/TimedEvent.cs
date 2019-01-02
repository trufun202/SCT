using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnowConeTycoon.Shared
{
    public class TimedEvent
    {
        public delegate void EventMethod();

        public int Time = 0;
        public int TimeTotal = 0;
        private bool IsLooping = false;
        private bool IsComplete = false;
        private EventMethod Method;

        public TimedEvent(int timeoutMilliseconds, EventMethod method, bool looping)
        {
            TimeTotal = timeoutMilliseconds;
            Method = method;
            IsLooping = looping;
        }

        public void Reset()
        {
            Time = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsComplete)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;

                if (Time >= TimeTotal)
                {
                    if (IsLooping)
                    {
                        Time = 0;
                    }
                    else
                    {
                        IsComplete = true;
                    }

                    Method.Invoke();
                }
            }
        }
    }
}

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
        private bool IsComplete = false;
        private EventMethod Method;
        private int InvokeCount = 0;
        private int InvokeCountTotal = 1;

        public TimedEvent(int timeoutMilliseconds, EventMethod method, int invokeCountTotal)
        {
            TimeTotal = timeoutMilliseconds;
            Method = method;
            InvokeCountTotal = invokeCountTotal;
        }

        public void Reset()
        {
            Time = 0;
            InvokeCount = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsComplete)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;

                if (Time >= TimeTotal)
                {
                    if (InvokeCount < InvokeCountTotal || InvokeCountTotal == -1)
                    {
                        Time = 0;
                        InvokeCount++;
                    }

                    if (InvokeCount >= InvokeCountTotal && InvokeCountTotal > 0)
                    {
                        IsComplete = true;
                    }

                    Method.Invoke();
                }
            }
        }
    }
}

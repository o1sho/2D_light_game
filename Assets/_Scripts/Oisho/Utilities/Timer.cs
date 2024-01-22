using System;
using UnityEngine;

namespace Oisho.Utilities
{
    public class Timer
    {
        public event Action OnTimerDone;

        private float startTimer;
        private float duration;
        private float targetTime;

        private bool isActive;

        public Timer(float duration)
        {
            this.duration = duration;
        }

        public void StartTimer()
        {
            startTimer = Time.time;
            targetTime = startTimer + duration;
            isActive = true;
        }

        public void StopTimer()
        {
            isActive= false;
        }

        public void Tick()
        {
            if (!isActive) return;

            if (Time.time >= targetTime)
            {
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}

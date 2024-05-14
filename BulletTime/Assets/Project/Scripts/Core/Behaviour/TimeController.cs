using Systems.Intefaces;
using UnityEngine;

namespace Core.Behaviour
{
    public class TimeController : ITimeController
    {
        private const float DEFAULT_FIXED_TIME = 0.02f;
        private const float SLOW_TIME_MODIFIER = 0.3f;
        
        public void SetDefaultTime()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = DEFAULT_FIXED_TIME;
        }

        public void SetSlowTime()
        {
            Time.timeScale *= SLOW_TIME_MODIFIER;
            Time.fixedDeltaTime *= SLOW_TIME_MODIFIER;
        }

        public void FreezeTime()
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }
    }
}
using UnityEngine;

namespace GDTools
{
    class Timer
    {
        public static bool CoolDown(float timer, float maxTime)
        {
            bool isReady = false;
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                timer = maxTime; 
                isReady = true;
            }
            return isReady;
        }
    }
}

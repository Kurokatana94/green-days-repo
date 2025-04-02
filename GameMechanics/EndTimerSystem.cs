using UnityEngine;

public class EndTimerSystem : MonoBehaviour
{
    private CountDownSystem CountDown;

    private void Awake()
    {
        CountDown = GameObject.FindGameObjectWithTag("CD").GetComponent<CountDownSystem>();
    }

    private void Update()
    {
        if(CountDown.timeLeft <= 5)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}

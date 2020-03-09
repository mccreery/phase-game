using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TestVolume : MonoBehaviour
{
    public AudioSource testSound;
    public float timeDelay = 0.5f;

    private float nextTestTime = float.MaxValue;

    private void Update()
    {
        if (Time.timeSinceLevelLoad >= nextTestTime)
        {
            testSound.Play();
            nextTestTime = Time.timeSinceLevelLoad + timeDelay;
        }
    }

    public void StartTesting()
    {
        nextTestTime = Time.timeSinceLevelLoad;
    }

    public void StopTesting()
    {
        testSound.Play();
        nextTestTime = float.MaxValue;
    }
}

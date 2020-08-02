using UnityEngine;

public class Device : MonoBehaviour
{
    [SerializeField]
    private SharedBool hasDevice = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasDevice.Value = true;
    }
}

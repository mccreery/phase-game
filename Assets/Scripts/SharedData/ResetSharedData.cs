using UnityEngine;

public class ResetSharedData : MonoBehaviour
{
    [SerializeField]
    private Shared[] sharedData = default;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        foreach (Shared shared in sharedData)
        {
            shared.Reset();
        }
    }
}

using UnityEngine;

public abstract class Shared : ScriptableObject
{
    public abstract void Reset();
}

public class Shared<T> : Shared
{
    [SerializeField]
    private T value;

    public T Value
    {
        get => value;
        set => this.value = value;
    }

    public static implicit operator T(Shared<T> shared)
    {
        return shared.value;
    }

    [SerializeField]
    private T resetValue;

    public override void Reset()
    {
        value = resetValue;
    }
}

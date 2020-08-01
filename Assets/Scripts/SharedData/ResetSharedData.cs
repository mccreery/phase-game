using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetSharedData : MonoBehaviour
{
    [SerializeField]
    private FilterType filterType = FilterType.Blacklist;

    public enum FilterType
    {
        Whitelist,
        Blacklist
    }

    [SerializeField]
    private Shared[] filterList;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        IEnumerable<Shared> toReset;
        if (filterType == FilterType.Whitelist)
        {
            toReset = filterList;
        }
        else
        {
            toReset = Object.FindObjectsOfType<Shared>().Except(filterList);
        }

        foreach (Shared shared in toReset)
        {
            shared.Reset();
        }
    }
}

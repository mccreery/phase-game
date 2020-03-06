using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MultiRuleTile : RuleTile
{
    [SerializeField]
    private List<TileBase> equivalentTiles = new List<TileBase>();

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
        {
            other = (other as RuleOverrideTile).m_InstanceTile;
        }
        switch (neighbor)
        {
            case 1:
                return other == this || equivalentTiles.Contains(other);
            case 2:
                return other != this && !equivalentTiles.Contains(other);
            default:
                return true;
        }
    }
}

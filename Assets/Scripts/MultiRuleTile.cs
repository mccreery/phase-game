using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MultiRuleTile : RuleTile
{
    public List<TileBase> equivalentTiles = new List<TileBase>();
    private ISet<TileBase> equivalentSet;

    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject instantiatedGameObject)
    {
        equivalentSet = new HashSet<TileBase>(equivalentTiles);
        return base.StartUp(location, tilemap, instantiatedGameObject);
    }

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
        {
            other = (other as RuleOverrideTile).m_InstanceTile;
        }
        switch (neighbor)
        {
            case 1:
                return other == this || equivalentSet.Contains(other);
            case 2:
                return other != this && !equivalentSet.Contains(other);
            default:
                return true;
        }
    }
}

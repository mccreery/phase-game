using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallTest
{
    public static WallFlags GetFlags(Collider2D collider)
    {
        WallFlags flags = 0;
        List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
        collider.GetContacts(contactPoints);

        foreach (ContactPoint2D contactPoint in contactPoints)
        {
            flags |= GetFlag(contactPoint);
        }
        return flags;
    }

    public static WallFlags GetFlag(ContactPoint2D contactPoint) => GetFlag(contactPoint.normal);

    public static WallFlags GetFlag(Vector2 contactNormal)
    {
        float angle = Mathf.Atan2(contactNormal.y, contactNormal.x);
        float modAngle = Mathf.Repeat(angle + Mathf.PI / 4, Mathf.PI * 2);

        int quadrant = Mathf.FloorToInt(modAngle / (Mathf.PI / 2));
        return (WallFlags)(1 << quadrant);
    }
}

[Flags]
public enum WallFlags
{
    LeftWall = 1,
    Floor = 2,
    RightWall = 4,
    Ceiling = 8,

    Horizontal = LeftWall | RightWall,
    Vertical = Floor | Ceiling
}

public static class WallFlagsExtensions
{
    public static bool Any(this WallFlags flags, WallFlags other)
    {
        return (flags & other) != 0;
    }

    public static bool All(this WallFlags flags, WallFlags other)
    {
        return (flags & other) == other;
    }

    /**
     * <summary>Nullifies vector components facing towards walls.
     * For example, when touching a left wall any negative X component is ignored.</summary>
     */
    public static Vector2 Clamp(this WallFlags flags, Vector2 delta)
    {
        if (flags.Any(WallFlags.LeftWall) && delta.x < 0 || flags.Any(WallFlags.RightWall) && delta.x > 0)
        {
            delta.x = 0;
        }
        if (flags.Any(WallFlags.Floor) && delta.y < 0 || flags.Any(WallFlags.Ceiling) && delta.y > 0)
        {
            delta.y = 0;
        }
        return delta;
    }
}

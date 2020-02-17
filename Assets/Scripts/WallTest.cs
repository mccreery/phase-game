using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTest : MonoBehaviour
{
    private Collider2D col2D;

    // Start is called before the first frame update
    void Start()
    {
        col2D = GetComponent<Collider2D>();
    }

    public WallFlags GetFlags()
    {
        WallFlags flags = 0;
        List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
        col2D.GetContacts(contactPoints);

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
}

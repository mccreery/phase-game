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

    private WallType GetFlags()
    {
        WallType flags = 0;
        List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
        col2D.GetContacts(contactPoints);

        foreach (ContactPoint2D contactPoint in contactPoints)
        {
            flags |= GetType(contactPoint);
        }
        return flags;
    }

    public bool Test(WallType typeFlags)
    {
        return (GetFlags() & typeFlags) != 0;
    }

    public static WallType GetType(ContactPoint2D contactPoint) => GetType(contactPoint.normal);

    public static WallType GetType(Vector2 contactNormal)
    {
        float angle = Mathf.Atan2(contactNormal.y, contactNormal.x);
        float modAngle = Mathf.Repeat(angle + Mathf.PI / 4, Mathf.PI * 2);

        int quadrant = Mathf.FloorToInt(modAngle / (Mathf.PI / 2));
        return (WallType)(1 << quadrant);
    }
}

[Flags]
public enum WallType
{
    LeftWall = 1,
    Floor = 2,
    RightWall = 4,
    Ceiling = 8,

    Horizontal = LeftWall | RightWall,
    Vertical = Floor | Ceiling
}

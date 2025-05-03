using UnityEngine;

public class Method
{
    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    // Used by mail or bubble after a successful color merge:
    // When two objects are merged, only one remains, the other one is destroyed.
    // The remainder will be moved to the midpoint between the two objects,
    // and its rigidbody will be given the average velocity of the two objects.
    public static void TransformMergeRemainder(Transform trs1, Rigidbody2D rb1, Transform trs2, Rigidbody2D rb2)
    {
        // Move trs1 to the midpoint between trs1 and trs2
        trs1.position += (trs2.position - trs1.position) / 2;

        // Set velocity for rb1
        rb1.velocity = (rb1.velocity + rb2.velocity) / 2;
    }

    public static Color HexColor(string hex) => ColorUtility.TryParseHtmlString(hex, out Color color) ? color : Color.white;
}

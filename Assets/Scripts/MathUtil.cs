using System;
public static class MathUtil
{
    public const float EPSILON = 0.0001f;

    public static bool IsApproxZero(this float value) => Math.Abs(value) < EPSILON;
}

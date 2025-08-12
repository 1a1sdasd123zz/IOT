using System;

namespace Vision.BaseClass.MyMath;

public static class AngleConverter
{
    /// <summary>
    /// 弧度转角度
    /// </summary>
    /// <param name="radians"></param>
    /// <returns></returns>
    public static double RadiansToDegrees(double radians)
    {
        return radians * (180.0 / Math.PI);
    }

    /// <summary>
    /// 角度转弧度
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180.0);
    }
}
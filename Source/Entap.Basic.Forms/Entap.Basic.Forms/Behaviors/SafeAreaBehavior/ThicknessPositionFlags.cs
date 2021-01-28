using System;
namespace Entap.Basic.Forms
{
    /// <summary>
    /// Thicknessのポジションを指定するフラグ
    /// </summary>
    [System.Flags]
    public enum ThicknessPositionFlags
    {
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8
    }
}

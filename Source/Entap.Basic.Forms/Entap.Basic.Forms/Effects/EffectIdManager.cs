using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms.Effects
{
    /// <summary>
    /// EffectIdを管理する
    /// </summary>
    public sealed class EffectIdManager
    {
        public static readonly string EffectResolutionGroupName = typeof(EffectIdManager).Namespace;

        public static string GetEffectId(string effectName) => $"{EffectResolutionGroupName}.{effectName}";
    }
}

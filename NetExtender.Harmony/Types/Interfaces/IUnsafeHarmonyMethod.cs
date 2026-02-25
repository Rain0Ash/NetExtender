namespace NetExtender.Harmony.Types.Interfaces
{
    public interface IUnsafeHarmonyMethod : IHarmonyMethod
    {
        public HarmonyLib.HarmonyMethod Method { get; }
    }
}
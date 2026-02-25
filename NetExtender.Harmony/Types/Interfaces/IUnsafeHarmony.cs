namespace NetExtender.Harmony.Types.Interfaces
{
    public interface IUnsafeHarmony : IHarmony
    {
        public HarmonyLib.Harmony Harmony { get; }
    }
}
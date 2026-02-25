namespace NetExtender.Harmony.Types.Interfaces
{
    public interface IUnsafeHarmonyInstruction : IHarmonyInstruction
    {
        public HarmonyLib.CodeInstruction Instruction { get; }
    }
}
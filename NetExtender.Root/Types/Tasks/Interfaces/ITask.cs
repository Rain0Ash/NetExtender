// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Tasks.Interfaces
{
    public interface ITask : IStartable, IPausable
    {
        public void Cancel();
    }
}
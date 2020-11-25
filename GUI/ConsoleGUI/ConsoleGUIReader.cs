// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Comparers.KeyInfo;
using NetExtender.Consoles;

namespace NetExtender.GUI.ConsoleGUI
{
    public class ConsoleGUIReader : ConsoleKeyReader
    {
        public ConsoleGUIReader()
        {
            Rule = new GUIConsolePage(default, new KeyInfoComparer());
        }
    }
}
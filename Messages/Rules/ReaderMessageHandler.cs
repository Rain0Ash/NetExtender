// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules.Interfaces;

namespace NetExtender.Messages.Rules
{
    public delegate Task<Boolean> ReaderHandler<T>(IConsoleRule<T> rule, IReaderMessage<T> message);
}
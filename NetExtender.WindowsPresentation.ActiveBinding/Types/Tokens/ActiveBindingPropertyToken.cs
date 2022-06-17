// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingPropertyToken : ActiveBindingToken
    {
        public IReadOnlyCollection<String> Properties { get; }
        public override ActiveBindingPathTokenId Id { get; }

        public ActiveBindingPropertyToken(Int32 start, Int32 end, IEnumerable<String> properties)
            : base(start, end)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Properties = new ReadOnlyCollection<String>(properties.ToList());
            Id = new ActiveBindingPathTokenId(ActiveBindingPathTokenType.Property, String.Join(".", Properties));
        }
    }
}

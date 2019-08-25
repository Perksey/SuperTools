using System;

namespace Ultz.SuperInvoke.Attributes
{
    public class NativeSignaturesAttribute : Attribute
    {
        public string Prefix { get; set; } = string.Empty;
    }
}
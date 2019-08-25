using System;

namespace Ultz.SuperInvoke.Loader
{
    public interface ILoader
    {
        IntPtr LoadLibrary(string name);
        IntPtr GetProcAddress(IntPtr library, string proc);
        void UnloadLibrary(IntPtr library);
    }
}
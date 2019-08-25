using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Ultz.SuperInvoke.Marshalling
{
    public interface IParameterMarshaller
    {
        bool IsApplicable();
        ParameterDefinition Marshal(ParameterDefinition parameter, ILProcessor il);
    }
}
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Ultz.SuperInvoke.Marshalling
{
    public class SpanMarshaller : IParameterMarshaller
    {
        public bool IsApplicable()
        {
            throw new System.NotImplementedException();
        }

        public void Marshal(ParameterDefinition parameter, ILProcessor il)
        {
            throw new System.NotImplementedException();
        }
    }
}
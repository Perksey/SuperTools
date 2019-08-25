using System;
using System.IO;
using System.Reflection;
using Mono.Cecil;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using ParameterAttributes = Mono.Cecil.ParameterAttributes;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace Ultz.SuperInvoke.Builder
{
    public class ImplementationBuilder
    {
        private const string Prefix = "Ultz.SuperInvoke.Generated.";
        private static AppDomain ImplementationDomain { get; set; } = AppDomain.CreateDomain("SuperInvoke Classes");

        public static void ReloadImplementationDomain()
        {
            AppDomain.Unload(ImplementationDomain);
            ImplementationDomain = AppDomain.CreateDomain("SuperInvoke Classes");
        }

        public T Load<T>(string name)
            where T:new()
        {
            return (T) Activator.CreateInstance(GetImplementation(typeof(T)));
        }

        private Type GetImplementation(Type type)
        {
            if (Type.GetType(Prefix + type.Name) is null)
            {
                CreateAndLoadAssembly(new AssemblyNameDefinition(Prefix, GetType().Assembly.GetName().Version));
            }

            return Type.GetType(Prefix + type.Name);
        }

        private Assembly CreateAndLoadAssembly(AssemblyNameDefinition name, params Type[] types)
        {
            using (var ms = new MemoryStream())
            {
                CreateAssembly(name, types).Write(ms);
                return ImplementationDomain.Load(ms.ToArray());
            }
        }
        
        private AssemblyDefinition CreateAssembly(AssemblyNameDefinition name, params Type[] types)
        {
            var asm = AssemblyDefinition.CreateAssembly(name, name.Name, ModuleKind.Dll);
            foreach (var type in types)
            {
                asm.MainModule.Types.Add(CreateTypeDefinition(type));
            }

            return asm;
        }

        private TypeDefinition CreateTypeDefinition(Type type)
        {
        }

        private MethodDefinition CreateMethod(MethodInfo declaration)
        {
            var method = new MethodDefinition(declaration.Name, MethodAttributes.Public,
                new TypeDefinition(declaration.ReturnType.Namespace, declaration.ReturnType.Name,
                    (TypeAttributes) declaration.ReturnType.Attributes));
            foreach (var parameterInfo in declaration.GetParameters())
            {
                method.Parameters.Add(new ParameterDefinition(parameterInfo.Name, (ParameterAttributes)parameterInfo.Attributes, new TypeReference(parameterInfo.ParameterType.Namespace, parameterInfo.ParameterType.Name, )));
            }
        }
    }
}
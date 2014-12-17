using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using WIDA.Storage;

namespace WIDA.Utes
{
    public class Compiler
    {
        /* From http://www.codeproject.com/Articles/9019/Compiling-and-Executing-Code-at-Runtime */
        public Assembly BuildAssembly(string[] Code, string[] ReferencedAssemblies)
        {     
            Microsoft.CSharp.CSharpCodeProvider Compiler = new CSharpCodeProvider();
            CompilerParameters CompilerParams = new CompilerParameters();
            CompilerParams.GenerateExecutable = false;
            CompilerParams.GenerateInMemory = true;
            CompilerParams.IncludeDebugInformation = false;
            CompilerParams.ReferencedAssemblies.AddRange(ReferencedAssemblies);
            CompilerResults Results = Compiler.CompileAssemblyFromSource(CompilerParams, Code);
            Compiler.Dispose();
            if (Results.Errors.HasErrors)
            {
                StringBuilder Errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError Error in Results.Errors)
                {
                    Errors.AppendFormat("Line {0},{1}\t: {2}\n", Error.Line, Error.Column, Error.ErrorText);
                }
                throw new Exception(Errors.ToString());
            }
            else
            {
                return Results.CompiledAssembly;
            }
        }

        public Delegate SourceToDelegate(string[] Code, string[] ReferencedAssemblies, Type DelType, string Namespace, string Class, string Method, object[] Args = null)
        {
            Assembly Asm = BuildAssembly(Code, ReferencedAssemblies);
            object Instance = null;
            Type Type = null;
            Instance = Asm.CreateInstance(Namespace + "." + Class);
            Type = Instance.GetType();
            MethodInfo MethodInfo = Type.GetMethod(Method);
            return Delegate.CreateDelegate(DelType, Instance, MethodInfo);
        }

        public object SourceToInstance(string[] Code, string[] ReferencedAssemblies, string Namespace, string Class, object[] Constructors)
        {
            Assembly Asm = BuildAssembly(Code, ReferencedAssemblies);
            return Asm.CreateInstance(Namespace + "." + Class, false, BindingFlags.CreateInstance, null, Constructors, null, null);
        }
    }
}

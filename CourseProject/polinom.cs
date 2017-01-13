using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Reflection;


namespace CourseProject {
    public class polinom
    {
        private Assembly assembly { set; get; }
        public string eqation { set; get; }
        // Здійснює компіляцію програми, яка обчислює задану функцію
        public polinom()
        {
            assembly = null;
            eqation = "";
        }
        public bool Compile(string str)
        {
            str = str.Replace(",", ".");
            // Клас, який надає можливості компіляції:
            CodeDomProvider icc = CodeDomProvider.CreateProvider("CSharp");

            // Параметри компілятора:
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("system.dll"); // підключаємо складання
            cp.CompilerOptions = "/t:library"; // створюємо бібліотеку
            cp.GenerateInMemory = true; // створюємо складання у пам'яті       

            // Створюємо рядок, який містить вихідний код класу Func
            StringBuilder sb = new StringBuilder("");
            sb.Append("using System;\n");
            sb.Append("namespace Func{ \n");
            sb.Append("public class Func{ \n");
            sb.Append("public double MyFunc(double x){\n");
            // З функції MyFunc повертаємо вираз, отриманий у вигляді рядку:
            sb.Append("return " + str + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            // Здійснюємо компіляцію:
            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                return false;
            }
            eqation = str;
            assembly = cr.CompiledAssembly;
            return true;
        }
        public override string ToString()
        {
            return eqation;
        }
        public double? Find(decimal x)
        {
            if (assembly == null)
                return null; // Складання не було створене
            // Створюємо екземпляр (об'єкт) класу:
            object o = assembly.CreateInstance("Func.Func");
            // Отримуємо інформацію про тип:
            Type t = o.GetType();
            // Отримуємо інформацію про метод з указаним ім'ям: 
            MethodInfo mi = t.GetMethod("MyFunc");
            // Викликаємо метод з параметром x:
            object result = mi.Invoke(o, new object[] { (double)x });
            return (double)result;
        }
    }
}

using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Lab7
{
    internal class stringTypeVariableDictionary
    {
        Dictionary<string, (Type Type, string value)> internalDictionary = new Dictionary<string, (Type Type, string Value)>();

        public (Type Type, string Value) Add(Type type, string nameV)
        {
            var name = nameV.Trim();
            if (internalDictionary.ContainsKey(name))
            {
                return (Type.Error, "Variable with this name was already declared");
            }
            else
            {
                internalDictionary.Add(name, (type,""));
               
            }
            return (Type.Null, null);
        }
        public (Type Type, string Value) this[string variable]
        {
            get
            {
                var name = variable.Trim();
                if (internalDictionary.ContainsKey(name))
                {
                    return internalDictionary[name];
                }
                else
                {
                    return (Type.Error, "Variable with this name was not declared");
                }
            }
            set
            {
                var name = variable.Trim();
                internalDictionary[name] = value;
            }
        }

    }
}

using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Lab7
{
    internal class VariableDictionary
    {
        Dictionary<string, (Type Type, object Value)> internalDictionary = new Dictionary<string, (Type Type, object Value)>();

        public (Type Type, object Value) Add(Type type, IToken variable)
        {
            var name = variable.Text.Trim();
            if (internalDictionary.ContainsKey(name))
            {
                return (Type.Error, "Variable with this name was already declared");
            }
            else
            {
                if (type == Type.Int)
                    internalDictionary.Add(name, (type, 0));
                else if (type == Type.Float)
                    internalDictionary.Add(name, (type, (float)0));
                else if (type == Type.String)
                    internalDictionary.Add(name, (type, ""));
                else
                    internalDictionary.Add(name, (type, false));
            }
            return (Type.Null, null);
        }
        public (Type Type, object Value) this[IToken variable]
        {
            get
            {
                var name = variable.Text.Trim();
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
                var name = variable.Text.Trim();
                internalDictionary[name] = value;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Lab7
{
    internal class VM
    {
 

        Stack<(Type type, object value)> stack = new Stack<(Type type, object value)>();
        private Dictionary<int, int> labels = new Dictionary<int, int>();
        Dictionary<string, (Type type, object value)> memory = new Dictionary<string, (Type type, object value)>();

        

        public void run(string code)
        {
            using (StringReader reader = new StringReader(code)) // preprocces labels
            {
                int i = 0;
                string line;
                while ((line = reader.ReadLine()) != null) 
                {
                    if (line.StartsWith("label"))
                    {
                        var split = line.Split(' ');
                        var index = int.Parse(split[1]);

                        labels.Add(index, i);
                    }
                    i++;
                }
            }

            using (StringReader reader = new StringReader(code)) // preprocces labels
            {
                int i = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var commandFull = line.Split(" ");
                    string command = commandFull[0];
                    command = command.Trim();
                    switch (command) 
                    {
                        case "label":
                            i++;
                            continue;                            
                        case "jmp":
                            i = labels[int.Parse(commandFull[1])];
                            break;
                        case "fjmp":
                            var shouldJump = stack.Pop();
                            if ((bool)shouldJump.value)
                            {
                                i++;
                                continue;
                            }
                            else
                            {
                                i = labels[int.Parse(commandFull[1])];
                            }                         
                            break;
                        case "load":
                            load(commandFull);                         
                            i++;
                            continue;                            
                        case "save":
                            safe(commandFull);
                            i++;
                            continue;
                        case "print":                          
                            print(commandFull);
                            i++;
                            continue;                            
                        case "read":                        
                            read(commandFull);
                            i++;
                            continue;
                        case "pop":                     
                            stack.Pop();
                            i++;
                            continue;                            
                        case "push":                      
                            push(commandFull);
                            i++;
                            continue;
                        case "itof":
                            itOf();
                            i++;
                            continue;
                        case "not":
                            not();
                            i++;
                            continue;
                        case "eq":
                            equal();
                            i++;
                            continue;                          
                        case "lt":
                            lessThen();
                            i++;
                            continue;
                        case "gt":
                            greaterThen();      
                            i++;
                            continue;                            
                        case "or":
                            or();
                            i++;
                            continue;                            
                        case "and":
                            and();
                            i++;
                            continue;
                        case "concat":
                            concat();
                            i++;
                            continue;
                        case "uminus":
                            uminus();
                            i++;
                            continue;                            
                        case "mod":
                            mod();
                            i++;
                            continue;                            
                        case "div":
                            div();
                            i++;
                            continue;                            
                        case "mul":
                            mul();
                            i++;
                            continue;                            
                        case "sub":
                            sub();
                            i++;
                            continue;
                        case "add":
                            add();
                            i++;
                            continue;
                        default:
                            throw new Exception("nonImplemented code");                            
                    }
                }
            }
        }

        private void safe(string[] command)
        {
            string name = command[1];
            var value = stack.Pop();
            memory[name] = value;
        }

        private void load(string[] command)
        {
            string name = command[1];
            if (memory.ContainsKey(name))
            {
                stack.Push(memory[name]);
            }
            else
                throw new Exception($"Variable {name} was not initialized");
        }

        private void print(string[] command)
        {
            var n = int.Parse(command[1]);

            List<object> items = new List<object>();
            for (int i = 0; i < n; i++)
            {
                items.Add(stack.Pop().value);
            }
            items.Reverse();
            foreach (var item in items)
            {
                Console.Write(item);
            }
            Console.Write("\n");
        }

        private void read(string[] command)
        {
            string type = command[1];         

            string value = Console.ReadLine();
            switch (type)
            {
                case "I":
                    {
                        int output;
                        if (int.TryParse(value, out output))
                            stack.Push((Type.Int, output));
                        else
                            throw new Exception($"Read value isnt of expected type");
                    }
                    break;
                case "F":
                    {
                        float output;
                        if (float.TryParse(value, out output))
                            stack.Push((Type.Float, output));
                        else
                            throw new Exception($"Read value isnt of expected type");
                    }
                    break;
                case "B":
                    {
                        if (value.Equals("true"))
                            stack.Push((Type.Bool, true));
                        else if (value.Equals("false"))
                            stack.Push((Type.Bool, false));
                        else
                            throw new Exception($"Read value isnt of expected type");

                    }
                    break;
                case "S":
                    {
                        stack.Push((Type.String, value.Replace("\"", String.Empty)));
                    }
                    break;
            }
        }
        private void push(string[] command)
        {
            string type = command[1];
            var value = command[2];
            if (type == "S")
                for (int i = 3; i < command.Length; i++)
                    value += " " + command[i];

            switch (type)
            {
                case "I":
                    {
                        int output;
                        if (int.TryParse(value, out output))
                            stack.Push((Type.Int, output));
                        else
                            throw new Exception($"Pushed value isnt of expected type");
                    }
                    break;
                case "F":
                    {
                        float output;
                        if (float.TryParse(value, out output))
                            stack.Push((Type.Float, output));
                        else
                            throw new Exception($"Pushed value isnt of expected type");
                    }
                    break;
                case "B":
                    {
                        if (value.Equals("true"))
                            stack.Push((Type.Bool, true));
                        else if (value.Equals("false"))
                            stack.Push((Type.Bool, false));
                        else
                            throw new Exception($"Pushed value isnt of expected type");

                    }
                    break;
                case "S":
                    {
                        stack.Push((Type.String, value.Replace("\"", String.Empty)));
                    }
                    break;
            }
        }
        private void itOf()
        {
            var value = stack.Pop();
            if (value.type == Type.Int)
                stack.Push((Type.Float, (float)(Convert.ToInt32(value.value))));
            else
                stack.Push((Type.Float, value.value));
        }
        private void not()
        {
            (Type type, object value) value = stack.Pop();
            stack.Push((Type.Bool, !(bool)value.value));
        }
        private void equal()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();

            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Bool, (float)leftValue.value == (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Bool, (int)leftValue.value == (int)rightValue.value));
            }
            else if (leftValue.type == Type.String)
            {
                stack.Push((Type.Bool, (string)leftValue.value == (string)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }

        private void lessThen()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Bool, (float)leftValue.value < (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Bool, (int)leftValue.value < (int)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }
        private void greaterThen()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Bool, (float)leftValue.value > (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Bool, (int)leftValue.value > (int)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }

        private void or()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            stack.Push((Type.Bool, (bool)leftValue.value || (bool)rightValue.value));
        }
        private void and()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            stack.Push((Type.Bool, (bool)leftValue.value && (bool)rightValue.value));
        }

        private void concat()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            stack.Push((Type.String, (string)leftValue.value + (string)rightValue.value));
        }

        private void uminus()
        {
            (Type type, object value) value = stack.Pop();
            if (value.type == Type.Float)
            {
                stack.Push((Type.Float, -(float)value.value));             
            }
            else if (value.type == Type.Int)
            {
                stack.Push((Type.Int, -(int)value.value));
            }
            else
            {
                throw new Exception("how");
            }
        }

        private void mod()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            stack.Push((Type.Int, (int)leftValue.value % (int)rightValue.value));
        }

        private void div()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Float, (float)leftValue.value / (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Int, (int)leftValue.value / (int)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }

        private void mul()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Float, (float)leftValue.value * (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Int, (int)leftValue.value * (int)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }

        private void sub()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();

            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Float, (float)leftValue.value - (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Int, (int)leftValue.value - (int)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }
        private void add()
        {
            (Type type, object value) rightValue = stack.Pop();
            (Type type, object value) leftValue = stack.Pop();
            if (leftValue.type == Type.Float)
            {
                stack.Push((Type.Float, (float)leftValue.value + (float)rightValue.value));
            }
            else if (leftValue.type == Type.Int)
            {
                stack.Push((Type.Int, (int)leftValue.value + (int)rightValue.value));
            }
            else
            {
                throw new Exception("how");
            }
        }
    }
}

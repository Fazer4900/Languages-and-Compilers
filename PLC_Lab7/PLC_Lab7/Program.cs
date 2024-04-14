
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace PLC_Lab7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var fileName = "input.txt";
            Console.WriteLine("Parsing: " + fileName);
            var inputFile = new StreamReader(fileName);
            AntlrInputStream input = new AntlrInputStream(inputFile);
    
            PLC_Lab7_exprLexer lexer = new PLC_Lab7_exprLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            PLC_Lab7_exprParser parser = new PLC_Lab7_exprParser(tokens);
            

            parser.AddErrorListener(new VerboseListener());
            IParseTree tree = parser.program();
            
            if (parser.NumberOfSyntaxErrors == 0)
            {
                Console.WriteLine("input Succesfully Parsed");          
                
                var typeCheck = new EvalVisitor().Visit(tree);
                if (typeCheck.Type != Type.Error)
                {
                    Console.WriteLine("input Sucesfully ErrorChecked");




                }
                else
                {
                    Console.WriteLine("Errors Found \n " + typeCheck.Value);
                }
                
            }
            else
            {
                Console.WriteLine("Input doesnt match the g4 grammar");
            }

            Console.ReadKey();
            
        }
    }
}


using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
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
            

           // parser.AddErrorListener(new VerboseListener());
            IParseTree tree = parser.program();
            
            if (parser.NumberOfSyntaxErrors == 0)
            {
                Console.WriteLine("input Succesfully Parsed");          
                
                EvalVisitor visitor = new EvalVisitor();

                var typeCheck = visitor.Visit(tree);
                if (typeCheck.Type != Type.Error)
                {
                  
                   
                    var stackBaseCode = new StackBasedCodeGeneratorVisitor(visitor).Visit(tree);
                    //Console.WriteLine(stackBaseCode);

                    string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string filePath = Path.Combine(directory, "gayresuklt.txt");
                    try
                    {
                        // Write generated string to the file, overwriting existing content
                        File.WriteAllText(filePath, stackBaseCode);
                        //Console.WriteLine("Content of gayresuklt.txt has been created in the main project folder.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
                    }
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

           
        }
    }
}

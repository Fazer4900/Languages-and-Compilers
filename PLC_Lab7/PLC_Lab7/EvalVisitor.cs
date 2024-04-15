using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using PLC_Lab7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PLC_Lab7
{

    public class EvalVisitor : PLC_Lab7_exprBaseVisitor<(Type Type, object Value)>
    {
        VariableDictionary variableDictionary = new VariableDictionary();
            
        // takže pro každý statement visitni
        public override (Type Type, object Value) VisitProgram([NotNull] PLC_Lab7_exprParser.ProgramContext context)
        {
            int errorCount = 0;
            string allErrors = "";

            foreach (var statement in context.statement())
            {
                var result = Visit(statement);
                if (result.Type == Type.Error)
                {
                    allErrors += result.Value + "\n";
                    errorCount++;
                }
            }
            if (errorCount > 0)
            {
                return (Type.Error, allErrors);
            }
            return (Type.Null, null);
        }
        public override (Type Type, object Value) VisitEmptyStatement([NotNull] PLC_Lab7_exprParser.EmptyStatementContext context) // pokud je prazdný we chilling
        {
            return (Type.Null, null);
        }

        public override (Type Type, object Value) VisitDeclarationStatement([NotNull] PLC_Lab7_exprParser.DeclarationStatementContext context) // pokud jde o deklaraci
        {
            var type = Visit(context.type());
            if(type.Type == Type.Error)
            {
                return type;
            }
            foreach (var id in context.ID())
            {
                var errorCheck = variableDictionary.Add(type.Type,id.Symbol);

                if(errorCheck.Type == Type.Error) 
                {
                    return errorCheck;
                }
            }
            return (Type.Null, null);
        }

        public override (Type Type, object Value) VisitWriteStatement([NotNull] PLC_Lab7_exprParser.WriteStatementContext context)
        {
            var result = VisitExprList(context.exprList());

            if (result.Type == Type.Error) { return result; }

    //            Console.WriteLine(result.Value);
            return (Type.Null, null);
        }

        public override (Type Type, object Value) VisitExprList([NotNull] PLC_Lab7_exprParser.ExprListContext context)
        {
            string finalString = "";
            foreach (var exp in context.exp())
            {
                var result = Visit(exp);
                if (result.Type == Type.Error)
                    return result;
                finalString += result.Value + ", ";
            }         
            finalString = finalString.TrimEnd(' ', ',');
            return (Type.String, finalString);
        }

        public override (Type Type, object Value) VisitAssignmentStatement([NotNull] PLC_Lab7_exprParser.AssignmentStatementContext context)
        {          
            foreach (var id in context.ID())
            {
                var foundVariable = variableDictionary[id.Symbol];
                Type foundType = foundVariable.Type;
                switch (foundType)
                {
                    case Type.Error:
                        return ((foundVariable.Type, foundVariable.Value));
                    case Type.Int:
                    case Type.Float:
                    case Type.Bool:
                    case Type.String:
                    
                        var exp = context.exp(0);
                        var result = Visit(exp);

                        if (result.Type == foundType)
                        {
                            var errorCheck = variableDictionary[id.Symbol] = result;

                            if(errorCheck.Type == Type.Error)
                            {
                                return errorCheck;
                            }

                        }
                        else if(result.Type == Type.Int && foundType == Type.Float)
                        {
                            float floatValue = (int)result.Value;

                            var errorCheck = variableDictionary[id.Symbol] = (Type.Float, floatValue);

                            if (errorCheck.Type == Type.Error)
                            {
                                return errorCheck;
                            }

                           
                        }                    
                        else
                        {
                            return (Type.Error, $"Type mismatch: Cannot assign {result.Type} to {foundType} variable named {id.GetText()}");
                        }                      
                        break;
                    default:
                        return (Type.Error, "Unimplemented type in VisitAssignmentStatement ");
                }
            }  
            return (Type.Null, null);
        }

        

        public override (Type Type, object Value) VisitReadStatement([NotNull] PLC_Lab7_exprParser.ReadStatementContext context)
        {
            foreach(var id in context.ID())
            {
                var variable = variableDictionary[id.Symbol];
                              

                string input = Console.ReadLine();

                switch (variable.Type)                 
                { 
                
                    case Type.Int:
                       int number = 0;
                       if(int.TryParse(input,out number))
                       {
                            variableDictionary[id.Symbol] = (Type.Int, number);                            
                       }
                       else
                       {
                            return (Type.Error, $"Entering non int value to int, variable named {id.GetText()}");
                       }
                    break;
                    case Type.Float:
                        float floatNumber;
                        if (float.TryParse(input, out floatNumber))
                        {
                            variableDictionary[id.Symbol] = (Type.Float, floatNumber);
                        }
                        else
                        {
                            return (Type.Error, $"Entering nonfloat value ito float, variable named {id.GetText()}");
                        }
                    break;
                    case Type.Bool:
                        bool boolState;
                        if (bool.TryParse(input, out boolState))
                        {
                            variableDictionary[id.Symbol] = (Type.Bool, boolState);
                        }
                        else
                        {
                            return (Type.Error, $"Entering nonbool value to bool, variable named {id.GetText()}");
                        }
                    break;
                        case Type.String:
                        variableDictionary[id.Symbol] = (Type.String, input);
                        break;
                    default:
                        return (Type.Error, "Unsupported format in VisitReadStatement");

                }

            }
            return (Type.Null, 0);
        }


        public override (Type Type, object Value) VisitExp([NotNull] PLC_Lab7_exprParser.ExpContext context)
        {
          
            Type resultType = Type.Null;   
            object resultValue = null;
            if (context.ChildCount == 1)
            {
                var child = context.GetChild(0);
                if (child is TerminalNodeImpl terminalNode) 
                {
                    if (terminalNode.Symbol.Type == PLC_Lab7_exprLexer.ID) 
                    {                        
                       var  result = variableDictionary[terminalNode.Symbol];

                        resultType = result.Type;
                        resultValue = result.Value;
                        return (resultType, resultValue);
                    }
                    else 
                    {
                        var result = Visit(terminalNode);
                        resultType = result.Type;
                        resultValue = result.Value;
                        return (resultType, resultValue);                     
                    }
                }
            }
            else 
            {
                var left = Visit(context.GetChild(0));
                var op = context.GetChild(1).GetText(); 
                var right = Visit(context.GetChild(2));

            }
            return (resultType, resultValue);
        
        }

        public override (Type Type, object Value) VisitParenthesis([NotNull] PLC_Lab7_exprParser.ParenthesisContext context)
        {
            return Visit(context.exp());
        }

        public override (Type Type, object Value) VisitMultiplication([NotNull] PLC_Lab7_exprParser.MultiplicationContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }
            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                int result = (int)left.Value * (int)right.Value;
                return (Type.Int, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);
                float result = leftValue * rightValue;
                return (Type.Float, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for multiplication");
            }
        }

        public override (Type Type, object Value) VisitBlockStatement([NotNull] PLC_Lab7_exprParser.BlockStatementContext context)
        {
            foreach (var statement in context.statement())
            {
                var result = Visit(statement);
                if (result.Type == Type.Error)
                    return result;
            }
            return (Type.Null, null);
        }

        public override (Type Type, object Value) VisitIfStatement([NotNull] PLC_Lab7_exprParser.IfStatementContext context)
        {
            var conditionResult = Visit(context.exp());
            if (conditionResult.Type != Type.Bool)
                return (Type.Error, "Condition must be a boolean expression");

            if ((bool)conditionResult.Value)
            {
                var ifStatementResult = Visit(context.statement(0));
                if (ifStatementResult.Type == Type.Error)
                    return ifStatementResult;
            }
            else if (context.ELSE() != null)
            {
                var elseStatementResult = Visit(context.statement(1));
                if (elseStatementResult.Type == Type.Error)
                    return elseStatementResult;
            }

            return (Type.Null, null);
        }

        public override (Type Type, object Value) VisitWhileStatement([NotNull] PLC_Lab7_exprParser.WhileStatementContext context)
        {
            var conditionResult = Visit(context.exp());
            if (conditionResult.Type != Type.Bool)
                return (Type.Error, "Condition must be a boolean expression");

            while ((bool)conditionResult.Value)
            {
                var statementResult = Visit(context.statement());
                if (statementResult.Type == Type.Error)
                    return statementResult;

                conditionResult = Visit(context.exp());
                if (conditionResult.Type != Type.Bool)
                    return (Type.Error, "Condition must be a boolean expression");
            }

            return (Type.Null, null);
        }

        public override (Type Type, object Value) VisitDivision([NotNull] PLC_Lab7_exprParser.DivisionContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                int leftValue = (int)left.Value;
                int rightValue = (int)right.Value;

                if (rightValue == 0)
                {
                    return (Type.Error, "Division by zero");
                }

                int result = leftValue / rightValue;
                return (Type.Int, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);

                if (rightValue == 0)
                {
                    return (Type.Error, "Division by zero");
                }

                float result = leftValue / rightValue;
                return (Type.Float, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for division");
            }

        }
        public override (Type Type, object Value) VisitModulo([NotNull] PLC_Lab7_exprParser.ModuloContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                int leftValue = (int)left.Value;
                int rightValue = (int)right.Value;

                if (rightValue == 0)
                {
                    return (Type.Error, "Modulo by zero");
                }

                int result = leftValue % rightValue;
                return (Type.Int, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for modulo");
            }
        }

        public override (Type Type, object Value) VisitAddition([NotNull] PLC_Lab7_exprParser.AdditionContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                int result = (int)left.Value + (int)right.Value;
                return (Type.Int, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);
                float result = leftValue + rightValue;
                return (Type.Float, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for addition");
            }
        }

        public override (Type Type, object Value) VisitSubtraction([NotNull] PLC_Lab7_exprParser.SubtractionContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                int result = (int)left.Value - (int)right.Value;
                return (Type.Int, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);
                float result = leftValue - rightValue;
                return (Type.Float, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for subtraction");
            }
        }
        public override (Type Type, object Value) VisitEqual([NotNull] PLC_Lab7_exprParser.EqualContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            // Compare values based on their types
            if (left.Type == right.Type)
            {
                bool result;
                if (left.Type == Type.Int)
                {
                    result = (int)left.Value == (int)right.Value;
                }
                else if (left.Type == Type.Float)
                {
                    result = Math.Abs((float)left.Value - (float)right.Value) < float.Epsilon;
                }
                else if (left.Type == Type.Bool)
                {
                    result = (bool)left.Value == (bool)right.Value;
                }
                else if (left.Type == Type.String)
                {
                    result = (string)left.Value == (string)right.Value;
                }
                else
                {
                    return (Type.Error, "Unsupported type for equality comparison");
                }

                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for equality comparison");
            }
        }

        public override (Type Type, object Value) VisitGreater([NotNull] PLC_Lab7_exprParser.GreaterContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                bool result = (int)left.Value > (int)right.Value;
                return (Type.Bool, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);
                bool result = leftValue > rightValue;
                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for comparison");
            }
        }

        public override (Type Type, object Value) VisitSmaller([NotNull] PLC_Lab7_exprParser.SmallerContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                bool result = (int)left.Value < (int)right.Value;
                return (Type.Bool, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);
                bool result = leftValue < rightValue;
                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for comparison");
            }
        }
        public override (Type Type, object Value) VisitNotequal([NotNull] PLC_Lab7_exprParser.NotequalContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }
            else if(left.Type == right.Type) 
            {
                bool result = left.Value != right.Value;
                return (left.Type, result);
            }
            if (left.Type == Type.Int && right.Type == Type.Int)
            {
                bool result = (int)left.Value != (int)right.Value;
                return (Type.Bool, result);
            }
            else if ((left.Type == Type.Int && right.Type == Type.Float) || (left.Type == Type.Float && right.Type == Type.Int) || (left.Type == Type.Float && right.Type == Type.Float))
            {
                float leftValue = Convert.ToSingle(left.Value);
                float rightValue = Convert.ToSingle(right.Value);
                bool result = Math.Abs(leftValue - rightValue) >= float.Epsilon;
                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Incompatible types for comparison");
            }
        }
        public override (Type Type, object Value) VisitBinaryAnd([NotNull] PLC_Lab7_exprParser.BinaryAndContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Bool && right.Type == Type.Bool)
            {
                bool result = (bool)left.Value && (bool)right.Value;
                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Binary AND operation requires boolean operands");
            }
        }
        public override (Type Type, object Value) VisitBinaryOr([NotNull] PLC_Lab7_exprParser.BinaryOrContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.Bool && right.Type == Type.Bool)
            {
                bool result = (bool)left.Value || (bool)right.Value;
                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Binary OR operation requires boolean operands");
            }
        }
        public override (Type Type, object Value) VisitConcatenate([NotNull] PLC_Lab7_exprParser.ConcatenateContext context)
        {
            var left = Visit(context.exp(0));
            var right = Visit(context.exp(1));

            if (left.Type == Type.Error || right.Type == Type.Error)
            {
                return (Type.Error, "Error in operand types");
            }

            if (left.Type == Type.String && right.Type == Type.String)
            {
                string result = (string)left.Value + (string)right.Value;
                return (Type.String, result);
            }
            else
            {
                return (Type.Error, "String concatenation requires string operands");
            }
        }

        public override (Type Type, object Value) VisitUnaryMinus([NotNull] PLC_Lab7_exprParser.UnaryMinusContext context)
        {
            var operand = Visit(context.exp());

            if (operand.Type == Type.Error)
            {
                return (Type.Error, operand.Value);
            }

            if (operand.Type == Type.Int)
            {
                int result = -(int)operand.Value;
                return (Type.Int, result);
            }
            else if (operand.Type == Type.Float)
            {
                float result = -(float)operand.Value;
                return (Type.Float, result);
            }
            else
            {
                return (Type.Error, "Unary minus operation requires numeric operand");
            }
        }

        public override (Type Type, object Value) VisitLogicNot([NotNull] PLC_Lab7_exprParser.LogicNotContext context)
        {
            var operand = Visit(context.exp());

            if (operand.Type == Type.Error)
            {
                return (Type.Error, operand.Value);
            }

            if (operand.Type == Type.Bool)
            {
                bool result = !(bool)operand.Value;
                return (Type.Bool, result);
            }
            else
            {
                return (Type.Error, "Logical NOT operation requires boolean operand");
            }
        }

        public override (Type Type, object Value) VisitLiteralExpr([NotNull] PLC_Lab7_exprParser.LiteralExprContext context)
        {
            return Visit(context.literal());
        }

        public override (Type Type, object Value) VisitID([NotNull] PLC_Lab7_exprParser.IDContext context)
        {
            var symbol = variableDictionary[context.ID().Symbol];
            return (symbol.Type, symbol.Value);
        }

        public override (Type Type, object Value) VisitLiteral([NotNull] PLC_Lab7_exprParser.LiteralContext context)
        {
            string literalString = context.GetText();     

            if (context.BOOL() != null)
            {
                return (Type.Bool, bool.Parse(literalString));
            }
            else if (context.INT() != null) 
            {
                return (Type.Int, int.Parse(literalString));

            }
            else if (context.FLOAT() != null) 
            {
                return (Type.Float, float.Parse(literalString));
            }
            else if (context.STRING() != null)
            {
            if (literalString.StartsWith("\"") && literalString.EndsWith("\""))
            {
                literalString = literalString.Substring(1, literalString.Length - 2);
            }

                return ((Type.String, literalString));
            }
            else
            {
                return (Type.Error, "Unsupported literal type");
            }         

        }


        public override (Type Type, object Value) VisitType([NotNull] PLC_Lab7_exprParser.TypeContext context)
        {
            string contextText = context.GetText();
            switch(contextText)
            {
                case "int":
                    return (Type.Int,0);
                case "float":
                    return (Type.Float,0.0f);
                case "string":
                    return (Type.String,"");
                case "bool":
                    return (Type.Bool,false);
                default:
                    return (Type.Error,"Unknown Type");         
            }                     
        }
        // OPERATION FOR EACH DATA TYPE
       
        
    }
        
}

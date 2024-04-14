using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Lab7
{
    internal class StackBasedCodeGeneratorVisitor : PLC_Lab7_exprBaseVisitor<string>
    {

        VariableDictionary variableDictionary = new VariableDictionary();

        public override string VisitLiteral(PLC_Lab7_exprParser.LiteralContext context)
        {
            if (context.BOOL() != null)
            {
                bool value = Convert.ToBoolean(context.BOOL().GetText());
                return $"push B {value}\n";
            }
            else if (context.INT() != null)
            {
                int value = Convert.ToInt32(context.INT().GetText(), 10);
                return $"push I {value}\n";
            }
            else if (context.FLOAT() != null)
            {
                double value = Convert.ToDouble(context.FLOAT().GetText());
                return $"push F {value}\n";
            }
            else if (context.STRING() != null)
            {
                string value = context.STRING().GetText();
                return $"push S {value}\n";
            }
            return "";
        }

        public override string VisitProgram(PLC_Lab7_exprParser.ProgramContext context)
        {
            string instructions = "";
            foreach (var statement in context.statement())
            {
                instructions += Visit(statement);
            }
            return instructions;
        }
        public override string VisitEmptyStatement(PLC_Lab7_exprParser.EmptyStatementContext context) // pokud je prazdný we chilling
        {
            return "";
        }

        public override string VisitDeclarationStatement(PLC_Lab7_exprParser.DeclarationStatementContext context)
        {
            string declarationCode = "";
            var type = Visit(context.type());
            foreach (var idNode in context.ID())
            {
                string variableName = idNode.GetText().Trim();
                declarationCode += $"push {type} {variableName}\n";
                declarationCode += $"save {variableName}\n";
            }
            return declarationCode;
        }


        public override string VisitAssignmentStatement(PLC_Lab7_exprParser.AssignmentStatementContext context)
        {
            string assignmentCode = "";

            foreach (var id in context.ID())
            {
                string variableName = id.GetText();
                var exp = context.exp(0);
                string expressionCode = Visit(exp);
                assignmentCode += expressionCode;
                assignmentCode += $"save {variableName}\n";
            }

            return assignmentCode;
        }

        public override string VisitWriteStatement(PLC_Lab7_exprParser.WriteStatementContext context)
        {
            string writeCode = "";
            foreach (var exp in context.exprList().exp())
            {
                string expressionCode = Visit(exp);
                writeCode += expressionCode;
                writeCode += "print 1\n";
            }
            return writeCode;
        }

        public override string VisitExprList(PLC_Lab7_exprParser.ExprListContext context)
        {
            string exprListCode = "";
            foreach (var exp in context.exp())
            {
                string expressionCode = Visit(exp);
                exprListCode += expressionCode;
            }
            return exprListCode;
        }


        public override string VisitReadStatement(PLC_Lab7_exprParser.ReadStatementContext context)
        {
            string readCode = "";
            foreach (var id in context.ID())
            {
                string variableName = id.GetText();
                readCode += $"read {variableName}\n";
                readCode += $"save {variableName}\n";
            }
            return readCode;
        }


        public override string VisitExp(PLC_Lab7_exprParser.ExpContext context)
        {
            if (context.ChildCount == 1)
            {
                var child = context.GetChild(0);
                if (child is TerminalNodeImpl terminalNode)
                {
                    if (terminalNode.Symbol.Type == PLC_Lab7_exprLexer.ID)
                    {
                        var variableName = terminalNode.GetText();
                        return $"load {variableName}\n";
                    }
                    else
                    {
                        return Visit(terminalNode);
                    }
                }
            }
            else
            {
                var left = Visit(context.GetChild(0));
                var op = context.GetChild(1).GetText();
                var right = Visit(context.GetChild(2));
                return $"{left}{right}{op}\n";
            }
            return "";
        }

        public override string VisitParenthesis(PLC_Lab7_exprParser.ParenthesisContext context)
        {
            return Visit(context.exp());
        }
        public override string VisitMultiplication(PLC_Lab7_exprParser.MultiplicationContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "mul\n";
        }
        public override string VisitBlockStatement(PLC_Lab7_exprParser.BlockStatementContext context)
        {
            string blockCode = "";
            foreach (var statement in context.statement())
            {
                blockCode += Visit(statement);
            }
            return blockCode;
        }

        public override string VisitIfStatement(PLC_Lab7_exprParser.IfStatementContext context)
        {
            string ifCode = "";
            string conditionCode = Visit(context.exp());
            string thenCode = Visit(context.statement(0));
            ifCode += conditionCode;
            ifCode += "if_eq else_" + context.GetHashCode() + "\n";
            ifCode += thenCode;
            ifCode += "goto endif_" + context.GetHashCode() + "\n";
            ifCode += "else_" + context.GetHashCode() + ":\n";
            if (context.statement().Length > 1)
            {
                string elseCode = Visit(context.statement(1));
                ifCode += elseCode;
            }
            ifCode += "endif_" + context.GetHashCode() + ":\n";
            return ifCode;
        }

        public override string VisitWhileStatement(PLC_Lab7_exprParser.WhileStatementContext context)
        {
            string whileCode = "";
            string conditionCode = Visit(context.exp());
            string loopCode = Visit(context.statement());

            whileCode += "while_start_" + context.GetHashCode() + ":\n";
            whileCode += conditionCode;
            whileCode += "if_eq while_end_" + context.GetHashCode() + "\n";
            whileCode += loopCode;
            whileCode += "goto while_start_" + context.GetHashCode() + "\n";
            whileCode += "while_end_" + context.GetHashCode() + ":\n";

            return whileCode;
        }

        public override string VisitDivision(PLC_Lab7_exprParser.DivisionContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "div\n";
        }
        public override string VisitModulo(PLC_Lab7_exprParser.ModuloContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "mod\n";
        }

        public override string VisitAddition(PLC_Lab7_exprParser.AdditionContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "add\n";
        }

        public override string VisitSubtraction(PLC_Lab7_exprParser.SubtractionContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "sub\n";
        }
        public override string VisitEqual(PLC_Lab7_exprParser.EqualContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "eq\n";
        }
        public override string VisitSmaller(PLC_Lab7_exprParser.SmallerContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "lt\n";
        }
        public override string VisitNotequal(PLC_Lab7_exprParser.NotequalContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "not eq\n"; // idk whit this one
        }
        public override string VisitBinaryAnd(PLC_Lab7_exprParser.BinaryAndContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "and\n";
        }
        public override string VisitBinaryOr(PLC_Lab7_exprParser.BinaryOrContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "or\n";
        }
        public override string VisitConcatenate(PLC_Lab7_exprParser.ConcatenateContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "concat\n";
        }

        public override string VisitUnaryMinus(PLC_Lab7_exprParser.UnaryMinusContext context)
        {
            return  "uminus\n";
        }

        public override string VisitLogicNot(PLC_Lab7_exprParser.LogicNotContext context)
        {
           
            return "!\n";
        }

        public override string VisitLiteralExpr(PLC_Lab7_exprParser.LiteralExprContext context)
        {
            return Visit(context.literal());
        }

        public override string VisitID(PLC_Lab7_exprParser.IDContext context)
        {
            string variableName = context.GetText();                    
            return $"load {variableName}\n";
        }


        public override string VisitType(PLC_Lab7_exprParser.TypeContext context)
        {
            string type = context.GetText().ToLower();
            switch (type)
            {
                case "int":
                    return "I";
                case "float":
                    return "F";
                case "bool":
                    return "B";
                case "string":
                    return "S";
                default:
                    return "";
            }
        }






    }
}

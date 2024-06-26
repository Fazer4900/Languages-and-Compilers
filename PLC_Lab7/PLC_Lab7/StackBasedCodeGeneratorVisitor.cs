﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
    using System.Threading.Tasks;

    namespace PLC_Lab7
    {
        internal class StackBasedCodeGeneratorVisitor : PLC_Lab7_exprBaseVisitor<string>
        {


            public StackBasedCodeGeneratorVisitor(EvalVisitor visitor)
            {
                this.evalVisitor = visitor;
            }

            EvalVisitor evalVisitor;
            VariableDictionary variableDictionary = new VariableDictionary();
            
            public override string VisitLiteral(PLC_Lab7_exprParser.LiteralContext context)
            {
                if (context.BOOL() != null)
                {
                    bool value = Convert.ToBoolean(context.BOOL().GetText());   
                    if(value)
                    {
                        return $"push B true\n";
                    }
                    else
                    {
                        return $"push B false\n";
                    }
                   
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

            string initV = "\"\""; // Default initialization value for string type

            // Determine initialization value based on variable type


            if (type == "I")
            {
                initV = "0";
            }
            else if (type == "F")
            {
                initV = "0.0";
            }
            else if (type == "B")
            {
                initV = "true";
            }

            foreach (var idNode in context.ID())
            {
                string variableName = idNode.GetText().Trim();

                if (type == "I")
                {
                    variableDictionary.Add(Type.Int,idNode.Symbol);
                }
                else if (type == "F")
                {
                    variableDictionary.Add(Type.Float,idNode.Symbol);
                }
                else if (type == "B")
                {
                    variableDictionary.Add(Type.Bool, idNode.Symbol);
                }
                else
                {
                    variableDictionary.Add(Type.String, idNode.Symbol);
                }

                // type muže byt int,float,bool,string
                declarationCode += $"push {type} {initV}\n"; // Initialize variables with appropriate default values
                declarationCode += $"save {variableName}\n";
            }
            return declarationCode;
        }


        bool oncePased =  false;
        public override string VisitAssignmentStatement(PLC_Lab7_exprParser.AssignmentStatementContext context)
        {
            string assignmentCode = "";

            if (context.ID().Length == 1)
            {

                foreach (var id in context.ID())
                {
                    var foundVariable = variableDictionary[id.Symbol];
                    Console.WriteLine(foundVariable.Type.ToString());

                    string variableName = id.GetText();
                    var exp = context.exp(0);


                    var assigningtype = evalVisitor.Visit(exp);

                    assignmentCode += Visit(exp);
                    if (foundVariable.Type == Type.Float && assigningtype.Type == Type.Int)
                    {
                        assignmentCode += "itof\n";
                    }

                    assignmentCode += $"save {variableName}\n";
                    assignmentCode += $"load {variableName} \n";
                    assignmentCode += $"pop\n";
                }
            }
            else
            {
                foreach (var id in context.ID().Reverse())
                {
                    var foundVariable = variableDictionary[id.Symbol];
                    Console.WriteLine(foundVariable.Type.ToString());
                    string variableName = id.GetText();
                    var exp = context.exp(0);
                    var assigningtype = evalVisitor.Visit(exp);
                    if (!oncePased)
                    {
                        assignmentCode += Visit(exp);
                        oncePased = true;
                    }
                    if (foundVariable.Type == Type.Float && assigningtype.Type == Type.Int)
                    {
                        assignmentCode += "itof\n";
                    }
                    assignmentCode += $"save {variableName}\n";
                    assignmentCode += $"load {variableName} \n";
                }
                assignmentCode += $"pop\n";
                oncePased = true;
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
            }
            writeCode += "print " + context.exprList().exp().Length + "\n";
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

                var returnValue = this.evalVisitor.variableDictionary[id.Symbol];

                string type = "";

                if (returnValue.Type == Type.Int)
                {
                    type = "I";
                }
                if (returnValue.Type == Type.String)
                {
                    type = "S";
                }
                if (returnValue.Type == Type.Float)
                {
                    type = "F";
                }
                if (returnValue.Type == Type.Bool)
                {
                    type = "B";
                }

                readCode += $"read {type}\n";
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
                    return Visit(terminalNode);
                }
            }
            else
            {
                if (context.GetChild(0) is PLC_Lab7_exprParser.UnaryMinusContext)
                {
                    var right = Visit(context.GetChild(1));
                    return right + "uminus\n";
                }
                else
                {
                    var leftType = evalVisitor.Visit(context.GetChild(0));
                    var rightType = evalVisitor.Visit(context.GetChild(1));
                    
                    var left = Visit(context.GetChild(0));
                    var op = context.GetChild(1).GetText();
                    var right = Visit(context.GetChild(2));

                    return $"{left}{right}{op}\n";
                }
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

            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if(leftEpi.Type == Type.Float && rightEpi.Type == Type.Int) 
            {
                leftAddOn = "itof\n";
            }
            if(leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right +leftAddOn+ "mul\n";
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

        private int labelCounter = 0;

        public override string VisitIfStatement(PLC_Lab7_exprParser.IfStatementContext context)
        {
            string ifCode = "";
            string conditionCode = Visit(context.exp());
            string thenCode = Visit(context.statement(0));

            int label1 = labelCounter;
            labelCounter++;
            int label2 = labelCounter;
            labelCounter++;

            ifCode += conditionCode;
            ifCode += "fjmp " + label1 + "\n";
            ifCode += thenCode;
            ifCode += "jmp " + label2 + "\n";
            ifCode += "label " + label1 + "\n";
            if (context.statement().Length > 1)
            {
                string elseCode = Visit(context.statement(1));
                ifCode += elseCode;
            }
            ifCode += "label " + label2 + "\n";
            return ifCode;
        }

        public override string VisitWhileStatement(PLC_Lab7_exprParser.WhileStatementContext context)
        {
            string whileCode = "";
            string conditionCode = Visit(context.exp());
            string loopCode = Visit(context.statement());

            int label1 = labelCounter;
            labelCounter++;
            int label2 = labelCounter;
            labelCounter++;
                       
            string condition = conditionCode;


            string body = "";
            body += loopCode;
            body += "jmp " + label1 + "\n";
            return "label " + label1 + "\n" + condition + "fjmp " + label2 + "\n" + body + "label " + label2 + "\n" ;

        }

        public override string VisitDivision(PLC_Lab7_exprParser.DivisionContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if (leftEpi.Type == Type.Float && rightEpi.Type == Type.Int)
            {
                leftAddOn = "itof\n";
            }
            if (leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right + leftAddOn + "div\n";
        }
        public override string VisitModulo(PLC_Lab7_exprParser.ModuloContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if (leftEpi.Type == Type.Float && rightEpi.Type == Type.Int)
            {
                leftAddOn = "itof\n";
            }
            if (leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right + leftAddOn + "mod\n";
        }

        public override string VisitAddition(PLC_Lab7_exprParser.AdditionContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if (leftEpi.Type == Type.Float && rightEpi.Type == Type.Int)
            {
                leftAddOn = "itof\n";
            }
            if (leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right + leftAddOn + "add\n";
        }

        public override string VisitSubtraction(PLC_Lab7_exprParser.SubtractionContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if (leftEpi.Type == Type.Float && rightEpi.Type == Type.Int)
            {
                leftAddOn = "itof\n";
            }
            if (leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right + leftAddOn + "sub\n";
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
            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if (leftEpi.Type == Type.Float && rightEpi.Type == Type.Int)
            {
                leftAddOn = "itof\n";
            }
            if (leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right + leftAddOn + "lt\n";
        }

        public override string VisitGreater(PLC_Lab7_exprParser.GreaterContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            string leftAddOn = "";
            string rightAddOn = "";
            var leftEpi = evalVisitor.Visit(context.exp()[0]);
            var rightEpi = evalVisitor.Visit(context.exp()[1]);
            if (leftEpi.Type == Type.Float && rightEpi.Type == Type.Int)
            {
                leftAddOn = "itof\n";
            }
            if (leftEpi.Type == Type.Int && rightEpi.Type == Type.Float)
            {
                rightAddOn = "itof\n";
            }

            return left + rightAddOn + right + leftAddOn + "gt\n";
        }
        public override string VisitNotequal(PLC_Lab7_exprParser.NotequalContext context)
        {
            var left = Visit(context.exp()[0]);
            var right = Visit(context.exp()[1]);
            return left + right + "eq\nnot\n"; // idk whit this one
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
            var value = Visit(context.exp());
            return value + "uminus\n";
        }

        public override string VisitLogicNot(PLC_Lab7_exprParser.LogicNotContext context)
        {
            var exp = Visit(context.exp());
            return exp + "not\n";
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

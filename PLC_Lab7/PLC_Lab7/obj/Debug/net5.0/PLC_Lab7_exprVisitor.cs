﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Fazer4900\Desktop\New folder (2)\PLC_Lab7\PLC_Lab7\PLC_Lab7_expr.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace PLC_Lab7 {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="PLC_Lab7_exprParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IPLC_Lab7_exprVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>parenthesis</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesis([NotNull] PLC_Lab7_exprParser.ParenthesisContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>multiplication</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplication([NotNull] PLC_Lab7_exprParser.MultiplicationContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>division</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDivision([NotNull] PLC_Lab7_exprParser.DivisionContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>modulo</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitModulo([NotNull] PLC_Lab7_exprParser.ModuloContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>addition</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddition([NotNull] PLC_Lab7_exprParser.AdditionContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>subtraction</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubtraction([NotNull] PLC_Lab7_exprParser.SubtractionContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>equal</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqual([NotNull] PLC_Lab7_exprParser.EqualContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>greater</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGreater([NotNull] PLC_Lab7_exprParser.GreaterContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>smaller</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSmaller([NotNull] PLC_Lab7_exprParser.SmallerContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>notequal</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotequal([NotNull] PLC_Lab7_exprParser.NotequalContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>binaryAnd</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryAnd([NotNull] PLC_Lab7_exprParser.BinaryAndContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>binaryOr</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryOr([NotNull] PLC_Lab7_exprParser.BinaryOrContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>concatenate</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConcatenate([NotNull] PLC_Lab7_exprParser.ConcatenateContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>unaryMinus</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryMinus([NotNull] PLC_Lab7_exprParser.UnaryMinusContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>logicNot</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicNot([NotNull] PLC_Lab7_exprParser.LogicNotContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>literalExpr</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralExpr([NotNull] PLC_Lab7_exprParser.LiteralExprContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>ID</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitID([NotNull] PLC_Lab7_exprParser.IDContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] PLC_Lab7_exprParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] PLC_Lab7_exprParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.emptyStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyStatement([NotNull] PLC_Lab7_exprParser.EmptyStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.declarationStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationStatement([NotNull] PLC_Lab7_exprParser.DeclarationStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.assignmentStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignmentStatement([NotNull] PLC_Lab7_exprParser.AssignmentStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.expStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpStatement([NotNull] PLC_Lab7_exprParser.ExpStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.readStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReadStatement([NotNull] PLC_Lab7_exprParser.ReadStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.writeStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWriteStatement([NotNull] PLC_Lab7_exprParser.WriteStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.exprList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprList([NotNull] PLC_Lab7_exprParser.ExprListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.blockStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockStatement([NotNull] PLC_Lab7_exprParser.BlockStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] PLC_Lab7_exprParser.IfStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] PLC_Lab7_exprParser.WhileStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExp([NotNull] PLC_Lab7_exprParser.ExpContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] PLC_Lab7_exprParser.TypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PLC_Lab7_exprParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] PLC_Lab7_exprParser.LiteralContext context);
}
} // namespace PLC_Lab7
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
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="PLC_Lab7_exprParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IPLC_Lab7_exprListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>parenthesis</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenthesis([NotNull] PLC_Lab7_exprParser.ParenthesisContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>parenthesis</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenthesis([NotNull] PLC_Lab7_exprParser.ParenthesisContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>multiplication</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplication([NotNull] PLC_Lab7_exprParser.MultiplicationContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>multiplication</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplication([NotNull] PLC_Lab7_exprParser.MultiplicationContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>division</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDivision([NotNull] PLC_Lab7_exprParser.DivisionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>division</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDivision([NotNull] PLC_Lab7_exprParser.DivisionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>modulo</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModulo([NotNull] PLC_Lab7_exprParser.ModuloContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>modulo</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModulo([NotNull] PLC_Lab7_exprParser.ModuloContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>addition</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddition([NotNull] PLC_Lab7_exprParser.AdditionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>addition</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddition([NotNull] PLC_Lab7_exprParser.AdditionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>subtraction</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSubtraction([NotNull] PLC_Lab7_exprParser.SubtractionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>subtraction</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSubtraction([NotNull] PLC_Lab7_exprParser.SubtractionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>equal</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEqual([NotNull] PLC_Lab7_exprParser.EqualContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>equal</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEqual([NotNull] PLC_Lab7_exprParser.EqualContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>greater</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGreater([NotNull] PLC_Lab7_exprParser.GreaterContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>greater</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGreater([NotNull] PLC_Lab7_exprParser.GreaterContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>smaller</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSmaller([NotNull] PLC_Lab7_exprParser.SmallerContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>smaller</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSmaller([NotNull] PLC_Lab7_exprParser.SmallerContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>notequal</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotequal([NotNull] PLC_Lab7_exprParser.NotequalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>notequal</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotequal([NotNull] PLC_Lab7_exprParser.NotequalContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>binaryAnd</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryAnd([NotNull] PLC_Lab7_exprParser.BinaryAndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>binaryAnd</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryAnd([NotNull] PLC_Lab7_exprParser.BinaryAndContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>binaryOr</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryOr([NotNull] PLC_Lab7_exprParser.BinaryOrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>binaryOr</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryOr([NotNull] PLC_Lab7_exprParser.BinaryOrContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>concatenate</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConcatenate([NotNull] PLC_Lab7_exprParser.ConcatenateContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>concatenate</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConcatenate([NotNull] PLC_Lab7_exprParser.ConcatenateContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>unaryMinus</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnaryMinus([NotNull] PLC_Lab7_exprParser.UnaryMinusContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>unaryMinus</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnaryMinus([NotNull] PLC_Lab7_exprParser.UnaryMinusContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>logicNot</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogicNot([NotNull] PLC_Lab7_exprParser.LogicNotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>logicNot</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogicNot([NotNull] PLC_Lab7_exprParser.LogicNotContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>literalExpr</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralExpr([NotNull] PLC_Lab7_exprParser.LiteralExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>literalExpr</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralExpr([NotNull] PLC_Lab7_exprParser.LiteralExprContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>ID</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterID([NotNull] PLC_Lab7_exprParser.IDContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ID</c>
	/// labeled alternative in <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitID([NotNull] PLC_Lab7_exprParser.IDContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] PLC_Lab7_exprParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] PLC_Lab7_exprParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] PLC_Lab7_exprParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] PLC_Lab7_exprParser.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.emptyStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEmptyStatement([NotNull] PLC_Lab7_exprParser.EmptyStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.emptyStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEmptyStatement([NotNull] PLC_Lab7_exprParser.EmptyStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.declarationStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclarationStatement([NotNull] PLC_Lab7_exprParser.DeclarationStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.declarationStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclarationStatement([NotNull] PLC_Lab7_exprParser.DeclarationStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.assignmentStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignmentStatement([NotNull] PLC_Lab7_exprParser.AssignmentStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.assignmentStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignmentStatement([NotNull] PLC_Lab7_exprParser.AssignmentStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.expStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpStatement([NotNull] PLC_Lab7_exprParser.ExpStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.expStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpStatement([NotNull] PLC_Lab7_exprParser.ExpStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.readStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReadStatement([NotNull] PLC_Lab7_exprParser.ReadStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.readStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReadStatement([NotNull] PLC_Lab7_exprParser.ReadStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.writeStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWriteStatement([NotNull] PLC_Lab7_exprParser.WriteStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.writeStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWriteStatement([NotNull] PLC_Lab7_exprParser.WriteStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.exprList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExprList([NotNull] PLC_Lab7_exprParser.ExprListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.exprList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExprList([NotNull] PLC_Lab7_exprParser.ExprListContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.blockStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockStatement([NotNull] PLC_Lab7_exprParser.BlockStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.blockStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockStatement([NotNull] PLC_Lab7_exprParser.BlockStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatement([NotNull] PLC_Lab7_exprParser.IfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatement([NotNull] PLC_Lab7_exprParser.IfStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileStatement([NotNull] PLC_Lab7_exprParser.WhileStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileStatement([NotNull] PLC_Lab7_exprParser.WhileStatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExp([NotNull] PLC_Lab7_exprParser.ExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExp([NotNull] PLC_Lab7_exprParser.ExpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] PLC_Lab7_exprParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] PLC_Lab7_exprParser.TypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PLC_Lab7_exprParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] PLC_Lab7_exprParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLC_Lab7_exprParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] PLC_Lab7_exprParser.LiteralContext context);
}
} // namespace PLC_Lab7
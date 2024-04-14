grammar PLC_Lab7_expr;
program: statement*;

// so vstup muže byt jedno z tohoto
statement
         : emptyStatement           //prazdny prvek ";"
         | declarationStatement     //typ ID -> muže byt , a další id a ;
         | assignmentStatement      //přiazení promena = promena, nebo promena = expresion ;
         | expStatement             //expresion;
         | readStatement            //read promena (nebo vice promenych odělených:);
         | writeStatement          
         | blockStatement           // tělo bloku plněné libovolným počtem příkazů
         | ifStatement              // if leva zavorka expresion prava zavorka, dobrovolný else;
         | whileStatement
         ;

// definice statementů

emptyStatement: SEMICOLON;
declarationStatement: type ID ( COLON  ID)* SEMICOLON;
assignmentStatement:  ID (ASSIGNMENT ID |ASSIGNMENT exp)+ SEMICOLON;
expStatement: exp SEMICOLON;
readStatement: READ ID ( COLON  ID)* SEMICOLON;
writeStatement: WRITE exprList SEMICOLON;
exprList: exp ( COLON  (exp | ID))*;
blockStatement: '{' statement+ '}';
ifStatement: IF L_PAR exp R_PAR statement (ELSE statement)?;
whileStatement: WHILE L_PAR exp R_PAR statement;

//expresion
exp
    : L_PAR exp R_PAR                        # parenthesis
    | exp MUL exp                            # multiplication
    | exp DIV exp                            # division
    | exp MOD exp                            # modulo
    | exp ADD exp                            # addition
    | exp SUB exp                            # subtraction
    | exp EQ exp                             # equal
    | exp GT exp                             # greater
    | exp LT exp                             # smaller
    | exp NOTEQ exp                          # notequal
    | exp AND exp                            # binaryAnd
    | exp OR exp                             # binaryOr
    | exp CONCAT exp                         # concatenate
    | SUB exp                                # unaryMinus
    | NOT exp                                # logicNot
    | literal                                # literalExpr
    | ID                                     # ID
    ;

ADD: '+';
SUB: '-';
MUL: '*';
DIV: '/';
MOD: '%';
CONCAT: '.';
AND: '&&';
OR: '||';
GT: '>';
LT: '<';
EQ: '==';
NOTEQ: '!=';
NOT: '!';
ASSIGNMENT: '=';

// definice keyWords
IF: 'if';
ELSE: 'else';
WHILE: 'while';
READ: 'read';
WRITE: 'write';
SEMICOLON: ';';
L_PAR: '(';
R_PAR: ')';
COLON: ',';

//definice typů
type
    : 'int'
    | 'string'
    | 'float'
    | 'bool' 
    ;

//definice hodnot, které mohou být v typech
literal: FLOAT | INT | BOOL | STRING | OCT | HEXA;


//regexx popistující co může být hodnota
FLOAT: [0-9]+ '.' [0-9]+;
INT : [0-9]+;
BOOL: 'true' | 'false';
STRING: '"' (~["\r\n\\"])* '"';
OCT : '0'[0-7]+;
HEXA : '0x'[0-9a-fA-F]+;
COMMENT: '//' ~[\r\n]* -> skip;
WS : [ \t\r\n]+ -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
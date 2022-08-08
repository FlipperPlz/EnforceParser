lexer grammar EnforceLexer;

channels { COMMENTS_CHANNEL, PREPROC }

@header {namespace EnforceParser.Core.Generated;}

SINGLE_LINE_COMMENT: '//' ~[\r\n]*           -> channel(COMMENTS_CHANNEL);
EMPTY_DELIMITED_COMMENT: ('/*/' | '/**/')    -> channel(COMMENTS_CHANNEL);
DELIMITED_COMMENT: '/*' .*? '*/'             -> channel(COMMENTS_CHANNEL);
PREPROCESSOR_DIRECTIVE: '#'                  -> mode(PREPROC_MODE);
WHITESPACES: [\r\n \t]                       -> channel(HIDDEN);


CLASS:         'class';
ENUM:          'enum';
SWITCH:        'switch';
EXTENDS:       'extends';
CONST:         'const';
BREAK:         'break';
CASE:          'case';
ELSE:          'else';
FOR:           'for';
CONTINUE:      'contine';
FOREACH:       'foreach';
IF:            'if';
NEW:           'new';
RETURN:        'return';
THIS:          'this';
THREAD:        'thread';
VOID:          'void';
WHILE:         'while';
AUTOPTR:       'autoptr';
AUTO:          'auto';
REF:           'ref';
NULL:          'null';
NOTNULL:       'notnull';
FUNC:          'func';
NATIVE:        'native';
VOLATILE:      'volatile';
PROTO:         'proto';
STATIC:        'static';
OWNED:         'owned';
REFERENCE:     'reference';
OUT:           'out';
PROTECTED:     'protected';
EVENT:         'event';
TYPEDEF:       'typedef';
MODDED:        'modded';
OVERRIDE:      'override';
SEALED:        'sealed';
INOUT:         'inout';
SUPER:         'super';
TYPENAME:      'typename';
POINTER:       'pointer';
GOTO:          'goto';
PRIVATE:       'private';
EXTERNAL:      'external';
DELETE:        'delete';
LOCAL:         'local';
TYPE_INT:      'int';
TYPE_FLOAT:    'float';
TYPE_BOOL:     'bool';
TYPE_STRING:   'string';
TYPE_VECTOR:   'vector';
DEFAULT:       'default';
Increment:          '++';
Decrement:          '--';
LShift:             '<<';
RShift:             '>>';
LShift_Assign:      '<<=';
RShift_Assign:      '>>=';
Subtract_Assign:    '-=';
Add_Assign:         '+=';
Multiply_Assign:    '*=';
Divide_Assign:      '/=';
Or_Assign:          '|=';
Xor_Assign:         '^=';
LessEqual:          '<=';
GreaterEqual:       '>=';
And_Assign:         '&=';
Inequal:            '!=';
Equal:              '==';
LogicalOr:          '||';
LogicalAnd:         '&&';
BitwiseOr:          '|';
BitwiseXor:         '^';
BitwiseAnd:         '&';
BitwiseNot:         '~';
Greater:            '>';
Less:               '<';
Assign:             '=';
Subtract:           '-';
Add:                '+';
Multiply:           '*';
Divide:             '/';
LParenthesis:       '(';
RParenthesis:       ')';
LCurly:             '{';
RCurly:             '}';
Comma:              ',';
Colon:              ':';
Semicolon:          ';';
LSBracket:          '[';
RSBracket:          ']';
Dot:                '.';
Bang:               '!';
DoubleQuote:        '"';
SingleQuote:        '\'';
Modulo:             '%';
IDENTIFIER: [a-zA-Z0-9_]+;

PREPROC_LINE: '__LINE__';
PREPROC_FILE: '__FILE__';
LiteralString: '"' (EnforceEscapeSequence | .)*? '"';
LiteralInteger: Number;
LiteralFloat: DecimalNumber;
LiteralBoolean: 'true' | 'false';

mode PREPROC_MODE;
PREPROC_Whitespaces:  WHITESPACES+                        -> channel(HIDDEN);
PREPROC_digits:       Diget+                              -> channel(PREPROC);
PREPROC_define:       'define'                            -> channel(PREPROC);
PREPROC_include:      'include'                           -> channel(PREPROC);
PREPROC_undef:        'undef'                             -> channel(PREPROC);
PREPROC_if:           'if'                                -> channel(PREPROC);
PREPROC_ifdef:        'ifdef'                             -> channel(PREPROC);
PREPROC_ifndef:       'ifndef'                            -> channel(PREPROC);
PREPROC_else:         'else'                              -> channel(PREPROC);
PREPROC_endif:        'endif'                             -> channel(PREPROC);
PREPROC_LParenthesis: '('                                 -> channel(PREPROC);
PREPROC_RParenthesis: ')'                                 -> channel(PREPROC);
PREPROC_LSBracket:    '['                                 -> channel(PREPROC);
PREPROC_RSBracket:    ']'                                 -> channel(PREPROC);
PREPROC_Comma:        ','                                 -> channel(PREPROC);
PREPROC_Add:          '+'                                 -> channel(PREPROC);
PREPROC_Subtract:     '-'                                 -> channel(PREPROC);
PREPROC_Semicolon:    ';'                                 -> channel(PREPROC);
PREPROC_Assign:       '='                                 -> channel(PREPROC);

fragment EnforceEscapeSequence: '\\\\' | '\\"' | '\\\'';
fragment Diget: [0-9];
fragment Number: '-'? Diget+;
fragment DecimalNumber:  Number '.' Diget+;
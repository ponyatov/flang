module skelex

open project

let lex (app: project) =
    System.IO.File.WriteAllText(
        config.meta + $"/src/{app}.lex",
        $$$"""%{
    #include "{{{app}}}.hpp"
%}

%option noyywrap yylineno

s [+\-]
n [0-9]+

%%

#.*             {}      // line comment
[ \t\r\n]+      {}      // drop spaces

{s}{n}      { yylval.n = atoi(yytext);            return INT;  }
[^ \t\r\n]+ { yylval.s = new std::string(yytext); return SYM;  }
.           { yylval.c = yytext[0];               return CHAR; }
"""
    )

    app

let yacc (app: project) =
    System.IO.File.WriteAllText(
        config.meta + $"/src/{app}.yacc",
        $$$"""%{
    #include "{{{app}}}.hpp"
%}

%defines %union { int n; char c; std::string *s; }

%token <n> INT
%token <s> SYM
%token <c> CHAR

%%
grammar: | grammar ex

ex : INT    { std::cout << " int:" <<  $1 << std::endl; }
   | SYM    { std::cout << " sym:" << *$1 << std::endl; }
   | CHAR   { std::cout << "char:" <<  $1 << std::endl; }
"""
    )

    app


let gen (app: project) = app |> lex |> yacc

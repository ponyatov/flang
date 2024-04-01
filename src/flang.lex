%{
    #include "flang.hpp"
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

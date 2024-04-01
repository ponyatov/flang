%{
    #include "flang.hpp"
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

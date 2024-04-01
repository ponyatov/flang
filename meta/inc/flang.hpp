#pragma once
/// @file

#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

#include <iostream>

/// @defgroup main main
/// @{
extern int main(int argc, char* argv[]);
extern void arg(int argc, char argv[]);
/// @}

/// @defgroup skelex skelex
/// @{
extern int yylex();                    ///< lexer: get next token
extern FILE* yyin;                     ///< file input
extern char* yytext;                   ///< parsed text (literal)
extern char* yyfile;                   ///< current file name
extern int yylineno;                   ///< current line number
extern int yyparse(void);              ///< syntax parser
extern void yyerror(std::string msg);  ///< error callback
#include "flang.parser.hpp"
/// @}

module Cpp

let cf app =
    System.IO.File.WriteAllText(
        config.meta + "/.clang-format",
        "BasedOnStyle: Google
IndentWidth:  4
TabWidth:     4
UseTab:       Never
ColumnLimit:  80
UseCRLF:      false

SortIncludes: false

AllowShortBlocksOnASingleLine: Always
AllowShortFunctionsOnASingleLine: All"
    )

    app

let doxy app =
    System.IO.File.WriteAllText(
        config.meta + "/.doxygen",
        $"""PROJECT_NAME           = "%s{app}"
PROJECT_BRIEF          = "%s{config.brief}"
PROJECT_LOGO           = doc/logo.png
HTML_OUTPUT            = docs
OUTPUT_DIRECTORY       =
INPUT                  = README.md doc src inc
EXCLUDE                = ref/*
INCLUDE_PATH           = inc src tmp
WARN_IF_UNDOCUMENTED   = NO
RECURSIVE              = YES
USE_MDFILE_AS_MAINPAGE = README.md
GENERATE_LATEX         = NO
FILE_PATTERNS         += *.lex *.yacc
EXTENSION_MAPPING      = lex=C++ yacc=C++ ino=C++
EXTRACT_ALL            = YES
EXTRACT_PRIVATE        = YES
SORT_GROUP_NAMES       = NO"""
    // LAYOUT_FILE            = doc/DoxygenLayout.xml
    )

    app

let hpp app =
    System.IO.File.WriteAllLines(
        config.meta + $"/inc/{app}.hpp",
        [
            """#pragma once"""
            "/// @file"
            ""
            "#include <stdio.h>"
            "#include <stdlib.h>"
            "#include <assert.h>"
            ""
            "#include <iostream>"
            ""
            "/// @defgroup main main"
            "/// @{"
            "extern int main(int argc, char* argv[]);"
            "extern void arg(int argc, char argv[]);"
            "/// @}"
            ""
            "/// @defgroup skelex skelex"
            "/// @{"
            "extern int yylex();                    ///< lexer: get next token"
            "extern FILE* yyin;                     ///< file input"
            "extern char* yytext;                   ///< parsed text (literal)"
            "extern char* yyfile;                   ///< current file name"
            "extern int yylineno;                   ///< current line number"
            "extern int yyparse(void);              ///< syntax parser"
            "extern void yyerror(std::string msg);  ///< error callback"
            $"#include \"{app}.parser.hpp\""
            "/// @}"
        ]
    )

    app

let cpp app =
    System.IO.File.WriteAllLines(
        config.meta + $"/src/{app}.cpp",
        [
            $"#include \"{app}.hpp\""
            ""
            "int main(int argc, char* argv[]) {"
            "    arg(0, argv[0]);"
            "    for (int i = 1; i < argc; i++) {  //"
            "        arg(i, argv[i]);"
            "    }"
            "}"
            ""
            "void arg(int argc, char argv[]) {  //"
            "    printf(\"argv[%i] = <%s>\\n\", argc, argv);"
            "}"
        ]
    )

    app

let gen app = app |> cf |> hpp |> cpp |> doxy

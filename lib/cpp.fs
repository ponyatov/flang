module Cpp

open project

let cf (app: project) =
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
AllowShortFunctionsOnASingleLine: All
"
    )

    app

let doxy (app: project) =
    System.IO.File.WriteAllText(
        config.meta + "/.doxygen",
        $"""PROJECT_NAME           = "{app}"
PROJECT_BRIEF          = "{config.brief}"
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
SORT_GROUP_NAMES       = NO
"""
    // LAYOUT_FILE            = doc/DoxygenLayout.xml
    )

    app

let hpp (app: project) =
    System.IO.File.WriteAllText(
        config.meta + $"/inc/{app}.hpp",
        $$"""#pragma once
/// @file

#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

#include <iostream>

#include <SDL2/SDL.h>

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
#include "{{app}}.parser.hpp"
/// @}
"""
    )

    app

let cpp (app: project) =
    System.IO.File.WriteAllLines(
        config.meta + $"/src/{app}.cpp",
        [
            $"#include \"{app}.hpp\""

            if app.sdl then
                """
SDL_Window* window = nullptr;
SDL_Surface* surface = nullptr;

enum class Screen : int {
    PosX = SDL_WINDOWPOS_UNDEFINED,
    PosY = SDL_WINDOWPOS_UNDEFINED,
    Width = 240,
    Height = 320,
    Status = SDL_WINDOW_SHOWN,
};"""
            else
                ""

            """
int main(int argc, char* argv[]) {
    arg(0, argv[0]);"""

            if app.sdl then
                """
    assert(!SDL_Init(SDL_INIT_VIDEO || SDL_INIT_AUDIO));
    assert(window = SDL_CreateWindow(argv[0],  //
                                     (int)Screen::PosX, (int)Screen::PosY,
                                     (int)Screen::Width, (int)Screen::Height,
                                     (int)Screen::Status));
    assert(surface = SDL_GetWindowSurface(window));
    SDL_FillRect(surface, NULL, SDL_MapRGB(surface->format, 0x22, 0x22, 0x22));
    SDL_UpdateWindowSurface(window);
    SDL_Delay(2000);
    SDL_DestroyWindow(window);
    SDL_Quit();"""
            else
                ""

            """
    for (int i = 1; i < argc; i++) {  //
        arg(i, argv[i]);"""

            if app.skelex then
                """        yyin = fopen(argv[i], "r");
        assert(yyin);
        yyparse();
        fclose(yyin);"""
            else
                ""

            """    }
}"""
            """
void arg(int argc, char argv[]) {  //
    printf("argv[%i] = <%s>\n", argc, argv);
}"""

            if app.skelex then
                """
void yyerror(std::string msg) {
    std::cerr << std::endl
              << yylineno << ':' << msg << " [" << yytext << ']' << std::endl;
    exit(-1);
}"""
            else
                ""
        ]
    )

    app

let gen (app: project) = app |> cf |> hpp |> cpp |> doxy

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

let gen app = app |> cf |> doxy

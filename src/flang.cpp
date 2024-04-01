#include "flang.hpp"

SDL_Window* window = nullptr;

enum class Screen : int {
    PosX = SDL_WINDOWPOS_UNDEFINED,
    PosY = SDL_WINDOWPOS_UNDEFINED,
    Width = 240,
    Height = 320,
    Status = SDL_WINDOW_SHOWN,
};

int main(int argc, char* argv[]) {
    arg(0, argv[0]);
    assert(!SDL_Init(SDL_INIT_VIDEO || SDL_INIT_AUDIO));
    assert(window = SDL_CreateWindow(argv[0],  //
                                     (int)Screen::PosX, (int)Screen::PosY,
                                     (int)Screen::Width, (int)Screen::Height,
                                     (int)Screen::Status));
    for (int i = 1; i < argc; i++) {  //
        arg(i, argv[i]);
        yyin = fopen(argv[i], "r");
        assert(yyin);
        yyparse();
        fclose(yyin);
    }
}

void arg(int argc, char argv[]) {  //
    printf("argv[%i] = <%s>\n", argc, argv);
}

void yyerror(std::string msg) {
    std::cerr << std::endl
              << yylineno << ':' << msg << " [" << yytext << ']' << std::endl;
    exit(-1);
}

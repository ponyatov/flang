#include "flang.hpp"

SDL_Window* window = nullptr;
SDL_Surface* surface = nullptr;

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
    assert(surface = SDL_GetWindowSurface(window));
    SDL_FillRect(surface, NULL, SDL_MapRGB(surface->format, 0x22, 0x22, 0x22));
    SDL_UpdateWindowSurface(window);
    SDL_Delay(2000);
    SDL_DestroyWindow(window);
    SDL_Quit();

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

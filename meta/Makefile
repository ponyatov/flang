# var
MODULE  = $(notdir $(CURDIR))

# dir
CWD   = $(CURDIR)
BIN   = $(CWD)/bin
TMP   = $(CWD)/tmp
DISTR = $(HOME)/distr

# tool
CURL   = curl -L -o
CF     = clang-format -style=file

# src
C += $(wildcard src/*.c*)
H += $(wildcard inc/*.h*)
S += lib/$(MODULE).ini $(wildcard lib/*.f)
Y += $(wildcard src/*.lex) $(wildcard src/*.yacc)

# all
.PHONY: all
all: cpp

.PHONY: cpp
cpp: $(BIN)/$(MODULE) $(S)
	$^

# format
.PHONY: format
format: tmp/format_cpp
tmp/format_cpp: $(C) $(H)
	$(CF) -i $? && touch $@

# rule
$(BIN)/$(MODULE): $(C) $(H) $(Y) $(TMP)/$(MODULE)/Makefile
	$(MAKE) -C $(TMP)/$(MODULE)

$(TMP)/$(MODULE)/Makefile: CMakeLists.txt
	cmake -S $(CWD) -B $(TMP)/$(MODULE) -DAPP=$(MODULE)

# doc
.PHONY: doc
doc:

.PHONY: doxy
doxy: .doxygen
	rm -rf docs ; doxygen $< 1>/dev/null

# install
.PHONY: install update gz
install: $(NET_APT) doc gz ref
	$(MAKE) update
update:
	sudo apt update
	sudo apt install -uy `cat apt.txt`
gz:
ref:

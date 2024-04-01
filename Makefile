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

# pkg
NET_MS  = packages-microsoft-prod.deb
NET_DEB = $(DISTR)/SDK/$(NET_MS)
NET_URL = https://packages.microsoft.com/config/debian/12
NET_APT = /etc/apt/sources.list.d/microsoft-prod.list

# src
C += $(wildcard src/*.c*)
H += $(wildcard inc/*.h*)
S += lib/$(MODULE).ini $(wildcard lib/*.f)
Y += $(wildcard src/*.lex) $(wildcard src/*.yacc)
F += $(wildcard lib/*.fs*)

# all
.PHONY: all
all: build

.PHONY: cpp
cpp: $(BIN)/$(MODULE) $(S)
	$^

.PHONY: build run
run:
	dotnet $@ -- 1 2 3
build:
	dotnet $@

# format
.PHONY: format
format: tmp/format_fs tmp/format_cpp
tmp/format_fs: $(F)
	dotnet fantomas --force $? && touch $@
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
# sudo dotnet workload update
# dotnet tool install fantomas
update:
	sudo apt update
	sudo apt install -uy `cat apt.txt`
gz:
ref: \
	ref/mini-c/README.md

ref/mini-c/README.md:
	git clone https://github.com/tgjones/mini-c.git ref/mini-c

.PHONY: dotnet
dotnet: $(NET_APT)
	ls -la $<
$(NET_APT): $(NET_DEB)
	sudo dpkg -i $< && sudo touch $@
$(NET_DEB):
	$(CURL) $@ $(NET_URL)/$(NET_MS)

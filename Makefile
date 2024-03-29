# var
MODULE  = $(notdir $(CURDIR))

# dir
CWD   = $(CURDIR)

# tool
CURL   = curl -L -o

# src
F += $(wildcard lib/*.fs*)
C += $(wildcard src/*.c*)
H += $(wildcard inc/*.h*)

.PHONY: all
all:
	dotnet build

# format
.PHONY: format
format: tmp/format_f
tmp/format_f: $(F)
	dotnet fantomas --force $? && touch $@

# doc
.PHONY: doc
doc:

# install
.PHONY: install update gz
install: doc gz
	$(MAKE) update
update:
	sudo apt update
	sudo apt install -uy `cat apt.txt`
gz:

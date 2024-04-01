# var
MODULE  = $(notdir $(CURDIR))

# dir
CWD   = $(CURDIR)
DISTR = $(HOME)/distr

# tool
CURL   = curl -L -o

# pkg
NET_MS  = packages-microsoft-prod.deb
NET_DEB = $(DISTR)/SDK/$(NET_MS)
NET_URL = https://packages.microsoft.com/config/debian/12
NET_APT = /etc/apt/sources.list.d/microsoft-prod.list

# src
F += $(wildcard lib/*.fs*)
C += $(wildcard src/*.c*)
H += $(wildcard inc/*.h*)

# all
.PHONY: all
all: build

.PHONY: build run
run:
	dotnet $@ -- 1 2 3
build:
	dotnet $@

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

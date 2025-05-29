# EngineX Filelist Utilities

A C# library, command-line tool and GUI tool for reading, inspecting, extracting from and creating EngineX filelists for Eurocom games. Filelists of version 4, 5, 6 and 7 are supported.

## Usage

### Command-line Interface (flutil.exe)

The option `-?`,  `-h` or `--help` after any command will display a help screen for said command.

#### Extracting Files

The contents of a filelist can be extracted with the `extract` command. `BIN_FILE` is the input .bin file of the filelist. `OUTPUT` is the output directory to extract the files to (will default to `./` if omitted).

```
flutil extract <BIN_FILE> [<OUTPUT>] [options]
```

The `-s` or `--create-scr` option will create a .scr descriptor file at the specified location.

```
--create-scr path/to/Filelist.scr
```

Example of a full command:

```
flutil extract path/to/Filelist.bin path/to/output/dir --create-scr path/to/Filelist.scr
```

#### Creating Filelist

A new filelist can be created from a set of files using the `create` command. `INPUT_FOLDER` is the folder containing the files to be included in the created filelist. `OUTPUT_FILE` is the path to the created filelist (without the filename extension, e.g. "Filelist").

```
flutil create <INPUT_FOLDER> [<OUTPUT_FILE>] [options]
```

The only *required* parameter is the platform, set with the `-p` or `--platform` option.

```
--platform PlatformName
```

The possible values are Xbox (`xb` or `xbox`), PlayStation 2 (`ps2`), and GameCube (`gc` or `gamecube`).

The drive letter of the virtual filesystem can be set with the `-l` or `--drive-letter` option. The default is 'x'.

```
--drive-letter x
```

The version of the created filelist can be set with the `-v` or `--version` option. The default is 7 (latest supported).

```
--version 7
```

The maximum size of the archive before it's split into another archive can be set with the `-z` or `--split-size` option. This is ignored for filelists below version 5, as they exclusively use a single .dat archive. The default is -1 (no limit), except for Xbox, where it is 268,435,456 bytes.

```
--split-size 100000000
```

A .scr descriptor file can be chosen as input, to list the exact files and order of files to include, with the `-s` or `--scr-file` option.

```
--scr-file path/to/Filelist.scr
```

Example of a full command:

```
flutil create path/to/input/dir path/to/created/Filelist --platform xbox --drive-letter d --version 7 --split-size 268435456 --scr-file path/to/Filelist.scr
```

### GUI Interface (flutil_gui.exe)

TODO
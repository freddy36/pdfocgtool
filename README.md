# PDFOCGtool

PDFOCGtool is a quick and dirty CLI tool to enable/disable the default visibility of Optional Content Groups (layers) in PDF files.

It uses .NET Core and the [itext7-dotnet](https://github.com/itext/itext7-dotnet) library to archive this.

## Usage

```
Usage: pdfocgtool [options]

Options:
  -?|-h|--help        Show help information
  -v|--version        Show version information
  -i|--input <file>   Path to the input file
  -o|--output <file>  Path to the output file
  --on <layer>        Set visibility to on for layer
  --off <layer>       Set visibility to off for layer
```

## Building from source

### Linux
```bash
apt install dotnet-sdk-2.2
git clone https://github.com/freddy36/pdfocgtool.git
cd pdfocgtool
dotnet publish PDFOCGtool -f netcoreapp2.2 --self-contained -r linux-x64 -c Debug

./PDFOCGtool/bin/Debug/netcoreapp2.2/linux-x64/pdfocgtool --help
```

### Windows
Use Visual Studio with .NET Core SDK 2.2 to open and build the project.

## License 
AGPL due to itext7

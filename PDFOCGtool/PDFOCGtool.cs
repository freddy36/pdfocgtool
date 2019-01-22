using iText.Kernel.Pdf;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Reflection;

namespace PDFOCGtool
{
    class PDFOCGtool
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.Name = "pdfocgtool";
            app.Description = "PDF OCG manipulation tool";
            app.ExtendedHelpText = "This is a simple tool to modify Optional Content Group (OCG) realted options of PDF files.";

            app.HelpOption("-?|-h|--help");

            app.VersionOption("-v|--version", () => {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });

            var input = app.Option("-i|--input <file>", "Path to the input file", CommandOptionType.SingleValue);
            var output = app.Option("-o|--output <file>", "Path to the output file", CommandOptionType.SingleValue);

            var onOptions = app.Option("--on <layer>", "Set visibility to on for layer", CommandOptionType.MultipleValue);
            var offOptions = app.Option("--off <layer>", "Set visibility to off for layer", CommandOptionType.MultipleValue);

            app.OnExecute(() =>
            {
                if (!input.HasValue())
                {
                    Console.Error.WriteLine("Input file not specified");
                    app.ShowHint();
                    return 1;
                }

                if (!output.HasValue())
                {
                    Console.Error.WriteLine("Output file not specified");
                    app.ShowHint();
                    return 1;
                }

                var src = input.Value();
                var dest = output.Value();
                PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(dest));

                var catalog = pdf.GetCatalog();
                var oCProperties = catalog.GetOCProperties(true);
                var layers = oCProperties.GetLayers();
                foreach (var layer in layers)
                {
                    var name = layer.GetPdfObject().GetAsString(PdfName.Name)?.ToUnicodeString();
                    if (onOptions.Values.Contains(name))
                    {
                        layer.SetOn(true);
                    }
                    else if (offOptions.Values.Contains(name))
                    {
                        layer.SetOn(false);
                    }
                    Console.WriteLine(name);
                }

                pdf.Close();

                return 0;
            });


            try
            {
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while executing the application: {0}", ex.Message);
            }
        }
    }
}

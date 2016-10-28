using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOCsvImport
{
    [Verb("import", HelpText = "Import records from a csv file into a SharePoint list")]
    public class ImportOptions
    {
        [Option('f', "file", Required = true, HelpText = "Data file containing data to import.")]
        public string FilePath { get; set; }

        [Option('r', "url", Required = true, HelpText = "Url to the site or tenant")]
        public string Url { get; set; }

        [Option('u', "username", Required = true, HelpText = "Username to authenticate")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to authenticate")]
        public string Password { get; set; }

        [Option('l', "list", Required = true, HelpText = "List to import to.")]
        public string List { get; set; }

        [Option('m', "map", Required = false, HelpText = "File containing CSV to List column mapping. If not provided assume 1:1 match")]
        public string MappingFilePath { get; set; }
    }

    [Verb("clear", HelpText = "Remove all items from a SharePoint list.")]
    public class ClearOptions
    {
        [Option('r', "url", Required = true, HelpText = "Url to the site or tenant")]
        public string Url { get; set; }

        [Option('u', "username", Required = true, HelpText = "Username to authenticate")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to authenticate")]
        public string Password { get; set; }

        [Option('l', "list", Required = false, HelpText = "List to remove items from")]
        public string List { get; set; }
    }
}

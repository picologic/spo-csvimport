using CommandLine;
using DataAccess;
using Microsoft.SharePoint.Client;
using SPOCsvImport.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace SPOCsvImport
{
    class Program
    {
        static int Main(string[] args) {
            return CommandLine.Parser.Default.ParseArguments<ImportOptions, ClearOptions>(args)
                .MapResult(
                    (ImportOptions opts) => importData(opts),
                    (ClearOptions opts) => clearItems(opts),
                    errs => 1);
        }

        static int importData(ImportOptions opts) {
            try {
                // parse csv
                var data = DataTable.New.ReadCsv(opts.FilePath);

                // load mapping
                IDictionary<string, string> mapping = null;
                if (string.IsNullOrWhiteSpace(opts.MappingFilePath) == false){
                    mapping = new Dictionary<string, string>();
                    // load it
                }

                // initialize context
                using (var ctx = new ClientContext(opts.Url)) {
                    ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                    ctx.Credentials = getCredentials(opts.Username, opts.Password);

                    var mgr = new ListManager(ctx, opts.List);
                    mgr.ImportItems(data, mapping);
                }
            } catch (Exception ex) {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                return 1;
            }
            return 0;
        }

        static int clearItems(ClearOptions opts) {
            try {
                // initialize context
                using (var ctx = new ClientContext(opts.Url)) {
                    ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                    ctx.Credentials = getCredentials(opts.Username, opts.Password);

                    var mgr = new ListManager(ctx, opts.List);
                    mgr.DeleteAllItems();
                }
            } catch (Exception ex) {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }

        static ICredentials getCredentials(string username, string password) {
            return new SharePointOnlineCredentials(username, password.ToSecureString());
        }
    }
}

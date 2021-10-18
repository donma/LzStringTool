using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace LzStringTool.Pages
{
    public class DecompressUrlModel : PageModel
    {
        public string SourceUrl { get; set; }

        public string ErrorJS { get; set; }
        public string Result { get; set; }


        public void OnGet()
        {
            Result = "";
        }

        public IActionResult OnPostConvertLzStringDecompress(string url)
        {
            try
            {
                SourceUrl = url;
                if (url.StartsWith("http"))
                {

                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.GET);

                    var bytes = client.DownloadData(request);
                    Result = LZStringCSharp.LZString.DecompressFromUint8Array(bytes);
                }
                else
                {
                    Result = "Error Decompress, the file should be written by Uint8Aarray File.";
                }
            }
            catch
            {
                Result = "Error Decompress, I cannot fetch it.";

            }
            return Page();
        }
    }
}

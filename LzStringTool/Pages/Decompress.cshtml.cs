using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace LzStringTool.Pages
{
    public class DecompressModel : PageModel
    {
        public string SourceContent { get; set; }

        public string ErrorJS { get; set; }
        public string LzStringContentDecompress { get; set; }

        public string LzStringContentDecompressAndUri { get; set; }

        public string LzStringContentDecompressUTF16 { get; set; }

        public string LzStringContentDecompressFromBase64 { get; set; }


        public string LzStringContentDecompressFromToUint8Array { get; set; }

        public void OnGet()
        {

        }



        public IActionResult OnPostConvertLzStringCompress(string contentSource)
        {
            //var c = System.IO.File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "lz1.txt");
            //var s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "lz1.txt");
            //c =   Encoding.ASCII.GetBytes(s);
            //contentSource = s;


            if (!string.IsNullOrEmpty(contentSource))
            {
                SourceContent = contentSource;

                try
                {
                    LzStringContentDecompress = LZStringCSharp.LZString.Decompress(contentSource);
                }
                catch
                {
                    LzStringContentDecompress = "Error Decompress";
                }


                try
                {
                    LzStringContentDecompressAndUri = LZStringCSharp.LZString.DecompressFromEncodedURIComponent(contentSource);
                }
                catch
                {
                    LzStringContentDecompressAndUri = "Error Decompress";
                }

                try
                {
                    LzStringContentDecompressUTF16 = LZStringCSharp.LZString.DecompressFromUTF16(contentSource);
                }
                catch
                {
                    LzStringContentDecompressUTF16 = "Error Decompress";

                }

                try
                {
                    LzStringContentDecompressFromBase64 = LZStringCSharp.LZString.DecompressFromBase64(contentSource);
                }
                catch
                {
                    LzStringContentDecompressFromBase64 = "Error Decompress";
                }



                if (contentSource.Contains(","))
                {
                    try
                    {
                        var _tmp = new List<int>();
                        foreach (var c in contentSource.Split(','))
                        {

                            _tmp.Add(int.Parse(c));

                        }

                        var bytes = _tmp.Select(x => (byte)x).ToArray();
                        LzStringContentDecompressFromToUint8Array = LZStringCSharp.LZString.DecompressFromUint8Array(bytes);
                    }
                    catch {

                        LzStringContentDecompressFromToUint8Array = "Error Decompress";
                    }

                }


            }
            else
            {

                ErrorJS = "<script>toastr.error('Source cannot be empty.');</script>";
            }
            return Page();
        }




    }
}
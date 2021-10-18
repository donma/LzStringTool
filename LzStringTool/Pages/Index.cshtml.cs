using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LzStringTool.Pages
{
    public class IndexModel : PageModel
    {
        public string SourceContent { get; set; }

        public string ErrorJS { get; set; }
        public string LzStringContentCompress { get; set; }

        public string LzStringContentCompressAndUri { get; set; }

        public string LzStringContentCompressUTF16 { get; set; }

        public string LzStringContentCompressToBase64 { get; set; }

        public string LzStringContentCompressIntArray{ get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPostConvertLzStringCompress(string contentSource)
        {
            if (!string.IsNullOrWhiteSpace(contentSource))
            {
                SourceContent = contentSource;
                LzStringContentCompress = LZStringCSharp.LZString.Compress(contentSource);

                LzStringContentCompressAndUri = LZStringCSharp.LZString.CompressToEncodedURIComponent(contentSource);

                LzStringContentCompressUTF16= LZStringCSharp.LZString.CompressToUTF16(contentSource);

                LzStringContentCompressToBase64 = LZStringCSharp.LZString.CompressToBase64(contentSource);


                var bytes = LZStringCSharp.LZString.CompressToUint8Array(contentSource);

                var intArray = bytes.Select(x => (int)x).ToArray();

                LzStringContentCompressIntArray = string.Join(",", intArray);
                System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "xxx.txt", bytes);
            }
            else {

                ErrorJS = "<script>toastr.error('Source cannot be empty.');</script>";
            }
            return Page();
        }


        
    }
}

#pragma checksum "Z:\TASKS\School.WEB\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "22245696fdc9d73c2ded322943db7753888a1962"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "Z:\TASKS\School.WEB\Views\_ViewImports.cshtml"
using School.WEB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "Z:\TASKS\School.WEB\Views\_ViewImports.cshtml"
using School.WEB.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
using School.WEB.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"22245696fdc9d73c2ded322943db7753888a1962", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"394712e72fa5b6a8a34e321d63c3c488f1f608c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
   var operation = (OperationResult<string>)ViewBag.Result;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 5 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
 if (operation != null)
{
    var statusMessageClass = operation.Success ? "success" : "danger";
    

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 210, "\"", 275, 4);
            WriteAttributeValue("", 218, "alert", 218, 5, true);
            WriteAttributeValue(" ", 223, "alert-", 224, 7, true);
#nullable restore
#line 9 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
WriteAttributeValue("", 230, statusMessageClass, 230, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 249, "js-alert-operation-result", 250, 26, true);
            EndWriteAttribute();
            WriteLiteral(" role=\"alert\">\r\n        ");
#nullable restore
#line 10 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
   Write(operation.Result);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 12 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
    
    if (operation.Success)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <script type=\"text/javascript\">\r\n            setTimeout(function () {\r\n                document.getElementsByClassName(\'js-alert-operation-result\')[0].remove()\r\n            }, 8000);\r\n        </script>\r\n");
#nullable restore
#line 20 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
    }
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 23 "Z:\TASKS\School.WEB\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Welcome</h1>\r\n    <img alt=\"some\" src=\"1.jpg\"/>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

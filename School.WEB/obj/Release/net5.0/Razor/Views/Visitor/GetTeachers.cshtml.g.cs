#pragma checksum "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a1ffe7d70a1f9bb3e1c1aad1e9032c4aed665080"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visitor_GetTeachers), @"mvc.1.0.view", @"/Views/Visitor/GetTeachers.cshtml")]
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
#line 1 "Z:\Work\School.WEB\Views\_ViewImports.cshtml"
using School.WEB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "Z:\Work\School.WEB\Views\_ViewImports.cshtml"
using School.WEB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1ffe7d70a1f9bb3e1c1aad1e9032c4aed665080", @"/Views/Visitor/GetTeachers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"394712e72fa5b6a8a34e321d63c3c488f1f608c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Visitor_GetTeachers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<System.Collections.Generic.IEnumerable<School.BLL.Dto.TeacherDto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml"
  
    ViewBag.Title = "title";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>All teachers</h2>\r\n<div>\r\n    <table class=\"table\">\r\n        <tr>\r\n            <th>Full Name</th>\r\n            <th>Age\r\n            <th>Gender</th>\r\n        </tr>\r\n");
#nullable restore
#line 16 "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml"
         foreach (var teacher in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 20 "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml"
               Write(Html.DisplayFor(modelItem=>teacher.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 23 "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml"
               Write(Html.DisplayFor(modelItem => teacher.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 26 "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml"
               Write(Html.DisplayFor(modelItem => teacher.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    \r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 32 "Z:\Work\School.WEB\Views\Visitor\GetTeachers.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<System.Collections.Generic.IEnumerable<School.BLL.Dto.TeacherDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "62561c6b3c68f2f16b60278ac8540aa3791e91e4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_ShowClassTeacher), @"mvc.1.0.view", @"/Views/Student/ShowClassTeacher.cshtml")]
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
#line 2 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
using School.WEB.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62561c6b3c68f2f16b60278ac8540aa3791e91e4", @"/Views/Student/ShowClassTeacher.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"394712e72fa5b6a8a34e321d63c3c488f1f608c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_ShowClassTeacher : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<School.WEB.ViewModels.Student.ShowClassTeacher.ShowClassTeacherViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
  
    ViewData["Title"] = "StudentDetails";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>My class teacher</h1>\r\n\r\n<img height=\"250\"");
            BeginWriteAttribute("src", " src=\"", 259, "\"", 277, 1);
#nullable restore
#line 11 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
WriteAttributeValue("", 265, Model.Image, 265, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"avatar\"/>\r\n\r\n<div>\r\n    <h4>Teacher</h4>\r\n    <hr/>\r\n");
#nullable restore
#line 16 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
     if (Model != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <dl class=\"row\">\r\n            <dt class=\"col-sm-2\">\r\n                First Name\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 23 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
           Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                Last Name\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 29 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
           Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                Age\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 35 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
           Write(Html.DisplayFor(model => model.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                Gender\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 41 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
           Write(Html.DisplayFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                Active\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                <input type=\"checkbox\" onclick=\"return false;\"");
            BeginWriteAttribute("checked", " checked = \"", 1337, "\"", 1362, 1);
#nullable restore
#line 47 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
WriteAttributeValue("", 1349, Model.Active, 1349, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            </dd>\r\n        </dl>\r\n");
#nullable restore
#line 50 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>This student doesn\'t have class and class teacher!!!</p>\r\n");
#nullable restore
#line 54 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62561c6b3c68f2f16b60278ac8540aa3791e91e47216", async() => {
                WriteLiteral("\r\n        Back to categories\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 57 "Z:\TASKS\School.WEB\Views\Student\ShowClassTeacher.cshtml"
       WriteLiteral(nameof(StudentController.Index));

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<School.WEB.ViewModels.Student.ShowClassTeacher.ShowClassTeacherViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

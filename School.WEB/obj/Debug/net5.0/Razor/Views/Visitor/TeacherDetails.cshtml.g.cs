#pragma checksum "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95f363d2c37821a53927258f92ead516685cc307"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visitor_TeacherDetails), @"mvc.1.0.view", @"/Views/Visitor/TeacherDetails.cshtml")]
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
#line 2 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
using School.WEB.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95f363d2c37821a53927258f92ead516685cc307", @"/Views/Visitor/TeacherDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"394712e72fa5b6a8a34e321d63c3c488f1f608c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Visitor_TeacherDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<School.WEB.ViewModels.Visitor.TeacherDetails.TeacherDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
  
    ViewData["Title"] = "TeacherDetails";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<img height=\"250\"");
            BeginWriteAttribute("src", " src=\"", 246, "\"", 264, 1);
#nullable restore
#line 11 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
WriteAttributeValue("", 252, Model.Image, 252, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"avatar\"/>\r\n\r\n<div>\r\n    <h4>Class</h4>\r\n    <hr/>\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            First name\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 21 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
       Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Last name\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 27 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
       Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Age\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 33 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
       Write(Html.DisplayFor(model => model.Age));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Gender\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 39 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
       Write(Html.DisplayFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            <p>My class</p>\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n");
#nullable restore
#line 45 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
             if (Model.Class != null)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
           Write(Html.ActionLink(Model.Class.Name,"ClassDetails","Visitor",new {id = Model.Class.Id}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
                                                                                                     
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>no class</p>\r\n");
#nullable restore
#line 52 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            <p>Subject</p>\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n");
#nullable restore
#line 58 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
             if (Model.Subject != null)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
           Write(Html.ActionLink(Model.Subject.Name,"SubjectDetails","Visitor", new {id = Model.Subject.Id}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
                                                                                                            
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>no subject</p>\r\n");
#nullable restore
#line 65 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\r\n        \r\n        <dt class=\"col-sm-2\">\r\n            <p>Classes</p>\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n");
#nullable restore
#line 72 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
             if (Model.Classes.Any())
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""table table-hover"">
                    <thead>
                    <tr>
                        <th scope=""col"">
                            Classes where he teach
                        </th>
                    </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 83 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
                     foreach (var form in Model.Classes)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
#nullable restore
#line 87 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
                           Write(Html.ActionLink(form.Name,"ClassDetails","Visitor",new {id = form.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 90 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n");
#nullable restore
#line 93 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>No classes</p>\r\n");
#nullable restore
#line 97 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "95f363d2c37821a53927258f92ead516685cc30710542", async() => {
                WriteLiteral("\r\n        Back to List\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 102 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
       WriteLiteral(nameof(VisitorController.GetTeachers));

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
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "95f363d2c37821a53927258f92ead516685cc30712257", async() => {
                WriteLiteral("\r\n        Back to categories\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 105 "Z:\TASKS\School.WEB\Views\Visitor\TeacherDetails.cshtml"
       WriteLiteral(nameof(VisitorController.Index));

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<School.WEB.ViewModels.Visitor.TeacherDetails.TeacherDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1504ead35c82c5a97328584ff176bfb7fe6c6b25"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visitor_ClassDetails), @"mvc.1.0.view", @"/Views/Visitor/ClassDetails.cshtml")]
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
#line 2 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
using School.WEB.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1504ead35c82c5a97328584ff176bfb7fe6c6b25", @"/Views/Visitor/ClassDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"394712e72fa5b6a8a34e321d63c3c488f1f608c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Visitor_ClassDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<School.WEB.ViewModels.Visitor.ClassDetails.ClassDetailsViewModel>
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
#line 4 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
  
    ViewBag.Title = "Class details";
    Layout = "_Layout";
    var i = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>Class</h4>\r\n    <hr/>\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            Name\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 20 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            <p>Class teacher</p>\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n");
#nullable restore
#line 26 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
             if (Model.ClassTeacher != null)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
           Write(Html.ActionLink(Model.ClassTeacher.FullName,"TeacherDetails","Visitor", new {id = Model.ClassTeacher.Id}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                                                                                                                          
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <label>no teacher</label>\r\n");
#nullable restore
#line 33 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\r\n        <dd class=\"col-sm-6\">\r\n");
#nullable restore
#line 36 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
             if (Model.StudentNames.Any())
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""table table-hover"">
                    <thead>
                    <tr>
                        <th scope=""col"">
                            #
                        </th>
                        <th scope=""col"">
                            Students
                        </th>
                    </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 50 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                     foreach(var student in Model.StudentNames)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <th scope=\"row\">\r\n                                ");
#nullable restore
#line 54 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                           Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 55 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                                  
                                    i++;
                                

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </th>\r\n                            <td>\r\n                                <p>");
#nullable restore
#line 60 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                              Write(student);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 63 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n");
#nullable restore
#line 66 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>No students</p>\r\n");
#nullable restore
#line 70 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\r\n");
#nullable restore
#line 72 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
          
            i = 1;
        

#line default
#line hidden
#nullable disable
            WriteLiteral("        <dd class=\"col-sm-6\">\r\n");
#nullable restore
#line 76 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
             if (Model.Teachers.Any())
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""table table-hover"">
                    <thead>
                    <tr>
                        <th scope=""col"">
                            #
                        </th>
                        <th scope=""col"">
                            Teachers who teach in the classroom
                        </th>
                    </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 90 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                     foreach (var teacher in Model.Teachers)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <th scope=\"row\">\r\n                                ");
#nullable restore
#line 94 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                           Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 95 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                                  
                                    i++;
                                

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </th>\r\n                            <td>\r\n                                ");
#nullable restore
#line 100 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                           Write(Html.ActionLink(teacher.FullName,"TeacherDetails","Visitor",new {id = teacher.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 103 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n");
#nullable restore
#line 106 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>No teachers</p>\r\n");
#nullable restore
#line 110 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1504ead35c82c5a97328584ff176bfb7fe6c6b2511414", async() => {
                WriteLiteral("\r\n        Back to List\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 115 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
       WriteLiteral(nameof(VisitorController.GetClasses));

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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1504ead35c82c5a97328584ff176bfb7fe6c6b2513126", async() => {
                WriteLiteral("\r\n        Back to categories\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 118 "Z:\TASKS\School.WEB\Views\Visitor\ClassDetails.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<School.WEB.ViewModels.Visitor.ClassDetails.ClassDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "891402f84d01d3609ef47a8c7ec5ed3e57ceff7a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_UserManagementTable), @"mvc.1.0.view", @"/Views/Admin/UserManagementTable.cshtml")]
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
#line 1 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
using Helperland.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"891402f84d01d3609ef47a8c7ec5ed3e57ceff7a", @"/Views/Admin/UserManagementTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1c8a01fc3c9a7d3bce698e4248940376f213b60", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_UserManagementTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IQueryable<User>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/calander.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<table id=""upcomingHistoryTable"">
                        <thead>
                            <tr>
                                <th class=""th1"">User Name</th>
                                <th class=""th3"">Date of Registration</th>
                                <th class=""th4"">User Type</th>
                                <th class=""th5"">Phone</th>
                                <th class=""th6"" >Postal Code</th>
                                <th class=""th8"">Status</th>
                                <th class=""th10"">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 19 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                             foreach(var Data in Model){

#line default
#line hidden
#nullable disable
            WriteLiteral("                             <tr>\r\n                                 <td>\r\n                                     <span>");
#nullable restore
#line 22 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                      Write(Data.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 22 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                                      Write(Data.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                 </td>\r\n                                 \r\n                                 <td data-order=\"");
#nullable restore
#line 25 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                            Write(Data.CreatedDate.ToString("MMddyyyyHHmmss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                     <div class=\"d-flex align-items-center\">\r\n                                         <div>\r\n                                             <span>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "891402f84d01d3609ef47a8c7ec5ed3e57ceff7a5872", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</span>\r\n                                         </div>\r\n                                         <div class=\"ms-2\">\r\n                                             <span class=\"service-detail-date\">");
#nullable restore
#line 31 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                                                          Write(Data.CreatedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                         </div>\r\n                                     </div>\r\n                                    \r\n                                 </td>\r\n                                 <td>\r\n");
#nullable restore
#line 37 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                       if(Data.UserTypeId==ValuesData.CUSTOMER){

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span>\r\n                                            Customer\r\n                                        </span>\r\n");
#nullable restore
#line 41 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                     }
                                     else if(Data.UserTypeId==ValuesData.SERVICE_PROVIDER){

#line default
#line hidden
#nullable disable
            WriteLiteral("                                         <span>Service Provider</span>\r\n");
#nullable restore
#line 44 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                     }
                                     else{

#line default
#line hidden
#nullable disable
            WriteLiteral("                                         <span>Admin</span>\r\n");
#nullable restore
#line 47 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                     }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                 </td>\r\n                                 <td>\r\n                                     <span>");
#nullable restore
#line 50 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                      Write(Data.Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                 </td>\r\n                                 <td>\r\n                                     <span>");
#nullable restore
#line 53 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                      Write(Data.ZipCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n                                 </td>\r\n                                 <td>\r\n");
#nullable restore
#line 57 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                    
                                    if(@Data.IsDeleted){

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span class=\"Deleted-User\">Deleted</span>\r\n");
#nullable restore
#line 60 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                    }
                                    else{
                                        if(@Data.IsActive ){

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span class=\"Active-User\">Active</span>\r\n");
#nullable restore
#line 64 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                    }
                                    else if(!@Data.IsApproved){

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <span class=\"Inactive-User\">Inactive</span>\r\n");
#nullable restore
#line 67 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                                    }
                                    }
                                 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    
                                 </td>
                                 <td>
                                     <div   role=""button"" data-bs-toggle=""dropdown"" aria-expanded=""false"" class=""d-flex justify-content-center align-items-center navbardropdownClass navbardropdown"" >
                                        <div>
                                             <i class=""fa fa-ellipsis-v""></i>
                                        </div>

                                       
                                    </div>
                                   
                                   
                                    <ul class=""dropdown-menu"" aria-labelledby=""navbardropdown"">
                                       
                                            <li><a class=""dropdown-item""");
            BeginWriteAttribute("onclick", " onclick=\"", 4417, "\"", 4453, 3);
            WriteAttributeValue("", 4427, "ActiveUser(\'", 4427, 12, true);
#nullable restore
#line 84 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
WriteAttributeValue("", 4439, Data.UserId, 4439, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4451, "\')", 4451, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Active</a></li>\r\n                                      \r\n                                              <li><a class=\"dropdown-item\"");
            BeginWriteAttribute("onclick", " onclick=\"", 4586, "\"", 4624, 3);
            WriteAttributeValue("", 4596, "InactiveUser(\'", 4596, 14, true);
#nullable restore
#line 86 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
WriteAttributeValue("", 4610, Data.UserId, 4610, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4622, "\')", 4622, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Inactive</a></li>\r\n                                       \r\n                                            <li><a class=\"dropdown-item\"");
            BeginWriteAttribute("onclick", " onclick=\"", 4758, "\"", 4795, 3);
            WriteAttributeValue("", 4768, "DeleteUsers(\'", 4768, 13, true);
#nullable restore
#line 88 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
WriteAttributeValue("", 4781, Data.UserId, 4781, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4793, "\')", 4793, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Delete</a></li>\r\n                                        \r\n\r\n                                    </ul>\r\n                                 </td>\r\n                              \r\n                               \r\n                            </tr>\r\n");
#nullable restore
#line 96 "D:\New version Heplerland\Helperland\Views\Admin\UserManagementTable.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                           
                            
                            
                            
                           
                        </tbody>
        
                    </table>


        <script>
         $(document).ready(function () {
        $('#upcomingHistoryTable').DataTable({
            ""dom"": '<""top""i>rt<""bottom""flp><""clear"">',
            ""columnDefs"": [
                { ""orderable"": false, ""targets"": 6 }
            ],
            'responsive': true,

            ""bFilter"": false, //hide Search bar
            ""pagingType"": ""full_numbers"",
            paging: true,
            ""pagingType"": ""full_numbers"",
            // bFilter: false,
            ordering: true,
            searching: false,
            info: true,

            language: {
                paginate: {
                    first: ""<img src='/images/firstPage.png' alt='first' />"",
                    previous: ""<img src='/images/previous.png' alt='previous' />"",
  ");
            WriteLiteral(@"                  next: '<img src=""/images/previous.png"" alt=""next"" style=""transform: rotate(180deg)"" />',
                    last: ""<img src='/images/firstPage.png' alt='first' style='transform: rotate(180deg)' />"",
                },
            },
            ""buttons"": [""excel""],
            ""oLanguage"": {
                ""sInfo"": ""Total Records: _TOTAL_""
            },
            ""order"": []
        });

       
    });

                    </script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IQueryable<User>> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "D:\New version Heplerland\Helperland\Views\CustomerService\CancelServiceRequest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c56c9dc7d23606e8e0d2eee31a701f39c4013ae4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CustomerService_CancelServiceRequest), @"mvc.1.0.view", @"/Views/CustomerService/CancelServiceRequest.cshtml")]
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
#line 2 "D:\New version Heplerland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c56c9dc7d23606e8e0d2eee31a701f39c4013ae4", @"/Views/CustomerService/CancelServiceRequest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1c8a01fc3c9a7d3bce698e4248940376f213b60", @"/Views/_ViewImports.cshtml")]
    public class Views_CustomerService_CancelServiceRequest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal-dialog modal-dialog-centered cancel-modal-dialog"">
  <div class=""modal-content"">
    <div class=""modal-header"">
      <h4 class=""modal-title"">Cancel Service Request</h4>
      <button
      id=""cancleClose""
        type=""button""
        class=""btn-close""
        data-bs-dismiss=""modal""
        aria-label=""Close""
      ></button>
    </div>
    <div class=""modal-body cancel-request"">
        <span>Why you want to cancel the service request?</span>
        <textarea rows=""4"" ></textarea>
        ");
#nullable restore
#line 16 "D:\New version Heplerland\Helperland\Views\CustomerService\CancelServiceRequest.cshtml"
   Write(Html.Raw(@ViewBag.Alert));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <button id=\"cancelRequestBtn\">Cancel Now</button>\r\n    </div>\r\n  </div>\r\n</div>");
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

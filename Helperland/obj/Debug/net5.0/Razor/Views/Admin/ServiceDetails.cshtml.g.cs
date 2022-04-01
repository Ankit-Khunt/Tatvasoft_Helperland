#pragma checksum "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bea5ae2fa9751de58d8901a29b1fccfbd52253be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ServiceDetails), @"mvc.1.0.view", @"/Views/Admin/ServiceDetails.cshtml")]
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
#line 1 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bea5ae2fa9751de58d8901a29b1fccfbd52253be", @"/Views/Admin/ServiceDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1c8a01fc3c9a7d3bce698e4248940376f213b60", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ServiceDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ServiceRequest>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
  
    string comment;
    string commentImgUrl;
    Layout = null;

    if (Model.HasPets)
    {
        comment = "I have pets at home";
        commentImgUrl = "/images/right.png";
    }
    else
    {
        comment = "I don't have pets at home";
        commentImgUrl = "/images/not-included.png";
    }

    string extras="";
    foreach (var extra in Model.ServiceRequestExtra)
    {
        switch (extra.ServiceExtraId)
        {
            case 1:extras += "Inside cabinets,";
                break;
            case 2:extras += " Inside fridge,";
                break;
            case 3:extras += " Inside oven,";
                break;
            case 4:extras += " Laundry wash & dry,";
                break;
            case 5:extras += " Interior windows";
                break;
        }
    }

    var startTime = Model.ServiceStartDate;
    var endTime = startTime.AddHours(Model.ServiceHours);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""modal-dialog modal-dialog-centered srd-modal-dialog"">
  <div class=""modal-content"">
    <div class=""modal-header"">
      <h6 class=""modal-title"">Service Details</h6>
      <button
        type=""button""
        class=""btn-close""
        data-bs-dismiss=""modal""
        aria-label=""Close""
      ></button>
    </div>
    <sdiv class=""modal-body service-request-detail"">
      <h5>");
#nullable restore
#line 53 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
     Write(Model.ServiceStartDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 53 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                                    Write(startTime.ToString("HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 53 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                                                                 Write(endTime.ToString("HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n      \r\n      <span><b>Duration: </b>");
#nullable restore
#line 55 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                        Write(Model.ServiceHours);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Hrs</span><br />\r\n\r\n      <div class=\"d-flex justify-content-center my-2\">\r\n        <hr class=\"divider-line\" />\r\n      </div>\r\n\r\n      <span><b>Service Id: </b><span id=\"serviceId\">");
#nullable restore
#line 61 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                               Write(Model.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></span><br />\r\n      <span><b>Extras: </b>");
#nullable restore
#line 62 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                      Write(extras);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n      <div class=\"d-flex align-items-center\"><span><b>Net Amount: </b></span> <span class=\"payment-text ms-2\">");
#nullable restore
#line 63 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                                                                                         Write(Model.TotalCost);

#line default
#line hidden
#nullable disable
            WriteLiteral(" &euro;</span></div>\r\n\r\n      <div class=\"d-flex justify-content-center my-2\">\r\n        <hr class=\"divider-line\" />\r\n      </div>\r\n\r\n      <span><b>Service Address: </b> ");
#nullable restore
#line 69 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                Write(Model.ServiceRequestAddress.ElementAt(0).AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 69 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                                                                       Write(Model.ServiceRequestAddress.ElementAt(0).AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n      <span><b>Billing Address: </b>Same as cleaning address</span><br />\r\n      <span><b>Phone: </b>+91  ");
#nullable restore
#line 71 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                          Write(Model.ServiceRequestAddress.ElementAt(0).Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n      <span><b>Email: </b> ");
#nullable restore
#line 72 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                      Write(Model.ServiceRequestAddress.ElementAt(0).Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n\r\n      <div class=\"d-flex justify-content-center my-2\">\r\n        <hr class=\"divider-line\" />\r\n      </div>\r\n\r\n      <span><b>Comments:</b><br /><img");
            BeginWriteAttribute("src", " src=", 2680, "", 2699, 1);
#nullable restore
#line 78 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
WriteAttributeValue("", 2685, commentImgUrl, 2685, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 2699, "\"", 2705, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"me-2\">");
#nullable restore
#line 78 "D:\New version Heplerland\Helperland\Views\Admin\ServiceDetails.cshtml"
                                                                         Write(comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n    </sdiv>\r\n  </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ServiceRequest> Html { get; private set; }
    }
}
#pragma warning restore 1591
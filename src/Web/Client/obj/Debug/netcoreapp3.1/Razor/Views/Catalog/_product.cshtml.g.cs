#pragma checksum "C:\Users\3frames-61\Desktop\eShopOnContainersReplica\src\Web\Client\Views\Catalog\_product.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03fb9ea864bdce6bc754d21eb2230409a542aac1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalog__product), @"mvc.1.0.view", @"/Views/Catalog/_product.cshtml")]
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
#line 1 "C:\Users\3frames-61\Desktop\eShopOnContainersReplica\src\Web\Client\Views\_ViewImports.cshtml"
using WebMvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\3frames-61\Desktop\eShopOnContainersReplica\src\Web\Client\Views\_ViewImports.cshtml"
using WebMvc.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03fb9ea864bdce6bc754d21eb2230409a542aac1", @"/Views/Catalog/_product.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82dfb9e8d9c1e15d2e9f7b4d3cf193b2b540299a", @"/Views/_ViewImports.cshtml")]
    public class Views_Catalog__product : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebMvc.Models.CatalogItem>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n\r\n");
            WriteLiteral("    <img class=\"esh-catalog-thumbnail\"");
            BeginWriteAttribute("src", " src=\"", 137, "\"", 160, 1);
#nullable restore
#line 7 "C:\Users\3frames-61\Desktop\eShopOnContainersReplica\src\Web\Client\Views\Catalog\_product.cshtml"
WriteAttributeValue("", 143, Model.PictureUrl, 143, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    \r\n    <div class=\"esh-catalog-name\">\r\n        <span>");
#nullable restore
#line 10 "C:\Users\3frames-61\Desktop\eShopOnContainersReplica\src\Web\Client\Views\Catalog\_product.cshtml"
         Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </div>\r\n    <div class=\"esh-catalog-price\">\r\n        <span>");
#nullable restore
#line 13 "C:\Users\3frames-61\Desktop\eShopOnContainersReplica\src\Web\Client\Views\Catalog\_product.cshtml"
         Write(Model.Price.ToString("N2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebMvc.Models.CatalogItem> Html { get; private set; }
    }
}
#pragma warning restore 1591

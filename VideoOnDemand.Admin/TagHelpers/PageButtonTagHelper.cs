using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VideoOnDemand.Admin.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("page-button")]
    public class PageButtonTagHelper : TagHelper
    {
        #region Properties
        public string Path { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Glyph { get; set; } = string.Empty;
        public string BootstrapStyle { get; set; } = "btn-default";
        public string BootstrapSize { get; set; } = "btn-sm";
        #endregion

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            if (context == null) throw new
                ArgumentNullException(nameof(context));
            if (output == null) throw new
                ArgumentNullException(nameof(output));

            base.Process(context, output);

            var href = "";

            if (Path.Trim().Length > 0)
            {
                // Assemble the value for the href parameter
                if (Path.StartsWith('/'))
                    href = $@"href='{Path.Trim()}'";
                else
                    href = $@"href='/{Path.Trim()}'";

                var ids = context.AllAttributes.Where(c => c.Name.StartsWith("id"));

                // Generate Id parameters
                var param = "";
                foreach (var id in ids)
                {
                    var name = id.Name;
                    if (name.Contains("-"))
                        name = name.Substring(name.IndexOf('-') + 1);
                    param += $"&{name}={id.Value}";
                }
                if (param.StartsWith("&")) param = param.Substring(1);
                if (param.Length > 0)
                    href = href.Insert(href.Length - 1, $"?{param}");


                // Display Glyph icons
                var glyphClasses = string.Empty;
                Glyph = Glyph.Trim();
                if (Glyph.StartsWith("glyphicon-"))
                    Glyph = Glyph.Substring(Glyph.IndexOf('-') + 1);
                if (Glyph.Length > 0)
                {
                    glyphClasses = $"class='glyphicon glyphicon-{Glyph}'";
                    if (Description.Length > 0)
                        Description = $" {Description}";
                }

                // Bootstrap Buttons
                BootstrapStyle = BootstrapStyle.Trim();
                if (!BootstrapStyle.StartsWith("btn-"))
                    BootstrapStyle = $"btn-{BootstrapStyle}";

                BootstrapSize = BootstrapSize.Trim();
                if (!BootstrapSize.StartsWith("btn-"))
                    BootstrapSize = $"btn-{BootstrapSize}";

                var BootstrapClass = string.Empty;

                if (BootstrapStyle.Length > 4 && BootstrapSize.Length > 4)
                    BootstrapClass = $"class='{BootstrapSize} {BootstrapStyle}'";

                output.Content.AppendHtml($"<a style='min-width:30px;display:inline-block;' {BootstrapClass} {href}><span { glyphClasses}></span>{ Description}</a>");

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VideoOnDemand.Admin.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("status-message")]
    public class StatusMessageTagHelper : TagHelper
    {
        public string Message { get; set; }
        // Possible Bootstrap classes: success, danger, warning, ...
        public string MessageType { get; set; } = "success";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(Message)) return;
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            base.Process(context, output);

            output.TagName = "div";

            var content = $"<div class='alert alert-{MessageType} " +
               "alert-dismissible' role='alert'><button type='button' " +
               "class='close' data-dismiss='alert' aria-label='Close'>" +
               "<span aria-hidden='true'>&times;</span></button>";

            if (!string.IsNullOrEmpty(Message)) content += Message;

            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</div>");
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthWeird.WebUI.Helpers.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "northweird-id")]
    public class NorthWeirdImageTagHelper : TagHelper
    {
        public int NorthweirdId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("href", $"/image/{NorthweirdId}");
        }
    }
}

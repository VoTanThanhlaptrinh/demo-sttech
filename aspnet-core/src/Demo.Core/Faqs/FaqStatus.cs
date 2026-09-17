using System.ComponentModel;

namespace Demo.Faqs;

public enum FaqStatus
{
    [Description("Công khai")]
    Published = 1,

    [Description("Ẩn")]
    Hidden = 2
}

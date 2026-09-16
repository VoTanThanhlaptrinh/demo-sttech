using System.ComponentModel;

namespace Demo.Faqs;

public enum FaqStatus
{
    [Description("Công khai")]
    Public = 1,

    [Description("Ẩn")]
    Private = 2
}

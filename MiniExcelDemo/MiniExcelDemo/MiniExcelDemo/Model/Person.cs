using MiniExcelLibs.Attributes;

namespace MiniExcelDemo.Model
{
    public class Person
    {
        [ExcelColumnName("姓名")]
        public string? Name { get; set; }
        [ExcelColumnName("性别")]
        public string? Sex { get; set; }
        [ExcelColumnName("年龄")]
        public string? Age { get; set; }
    }
}

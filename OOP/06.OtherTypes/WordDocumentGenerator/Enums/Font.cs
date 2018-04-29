namespace WordDocumentGenerator.Enums
{
    using System.Runtime.Serialization;

    public enum Font
    {
        Arial = 0,
        [EnumMember(Value = "Arial Black")]
        ArialBlack = 1,
        Calibri = 3,
        [EnumMember(Value = "Times New Roman")]
        TimesNewRoman = 4,
        Linds = 5
    }
}

using System.ComponentModel;

namespace MotoStore.Enums
{
    /// <summary>
    /// Тип двигателя
    /// </summary>
    public enum NumberOfCycles
    {
        [Description("2-тактный")]
        TwoStroke = 2,
        [Description("4-тактный")]
        FourStroke = 4
    }
}
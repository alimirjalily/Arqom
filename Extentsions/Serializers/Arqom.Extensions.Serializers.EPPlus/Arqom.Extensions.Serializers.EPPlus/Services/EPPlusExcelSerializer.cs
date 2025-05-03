using System.Data;
using Arqom.Extensions.Serializers.EPPlus.Extensions;
using Arqom.Extensions.Serializers.Abstractions;
using Arqom.Extensions.Translations.Abstractions;

namespace Arqom.Extensions.Serializers.EPPlus.Services;

public class EPPlusExcelSerializer : IExcelSerializer
{
    private readonly ITranslator _translator;

    public EPPlusExcelSerializer(ITranslator translator)
    {
        _translator = translator;
    }

    public byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result") => list.ToExcelByteArray(_translator, sheetName);

    public DataTable ExcelToDataTable(byte[] bytes) => bytes.ToDataTableFromExcel();

    public List<T> ExcelToList<T>(byte[] bytes) => bytes.ToDataTableFromExcel().ToList<T>(_translator);
}

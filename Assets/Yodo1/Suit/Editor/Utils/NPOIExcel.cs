using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using NPOI.SS.UserModel;

/// <summary>
/// 使用NOPI读取Excel数据
/// </summary>
public class NPOIExcel
{
    private IWorkbook _workbook;
    private string _filePath;

    public List<string> SheetNames { get; set; }

    public NPOIExcel()
    {
        SheetNames = new List<string>();
    }

    /// <summary>
    /// 获取Excel信息
    /// </summary>
    /// <param name="filePath"></param>
    public void LoadFile(string filePath)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        _filePath = filePath;
        SheetNames = new List<string>();
        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            _workbook = WorkbookFactory.Create(fs, true);
        }

        var count = _workbook.NumberOfSheets;
        for (int i = 0; i < count; i++)
        {
            SheetNames.Add(_workbook.GetSheetName(i));
        }

        UnityEngine.Debug.Log("Yodo1Suit LoadFile:" + _filePath);
    }

    /// <summary>
    /// 获取第<paramref name="idx"/>的sheet的数据
    /// </summary>
    /// <param name="idx">Excel文件的第几个sheet表</param>
    /// <param name="isFirstRowCoumn">是否将第一行作为列标题</param>
    /// <returns></returns>
    public DataTable GetTable(int idx, bool isFirstRowCoumn)
    {
        if (idx >= SheetNames.Count || idx < 0)
            throw new Exception("Do not Get This Sheet");
        return ExcelToDataTable(SheetNames[idx], isFirstRowCoumn);
    }

    /// <summary>
    /// 将excel中的数据导入到DataTable中
    /// </summary>
    /// <param name="sheetName">excel工作薄sheet的名称</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns>返回的DataTable</returns>
    public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
    {
        ISheet sheet = null;
        var data = new DataTable();
        data.TableName = sheetName;
        int startRow = 0;
        try
        {
            sheet = sheetName != null ? _workbook.GetSheet(sheetName) : _workbook.GetSheetAt(0);
            if (sheet != null)
            {
                var firstRow = sheet.GetRow(0);
                if (firstRow == null)
                    return data;
                int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                startRow = isFirstRowColumn ? sheet.FirstRowNum + 1 : sheet.FirstRowNum;

                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                {
                    //.StringCellValue;
                    var column = new DataColumn(Convert.ToChar(((int) 'A') + i).ToString());
                    if (isFirstRowColumn)
                    {
                        var columnName = firstRow.GetCell(i).StringCellValue;
                        column = new DataColumn(columnName);
                    }

                    data.Columns.Add(column);
                }


                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            dataRow[j] = row.GetCell(j, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString();
                    }

                    data.Rows.Add(dataRow);
                }
            }
            else throw new Exception("Don not have This Sheet");

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return null;
        }
    }
}
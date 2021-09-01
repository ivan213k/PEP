using PerformanceEvaluationPlatform.Application.Model.Excel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using System;

namespace PerformanceEvaluationPlatform.Application.Services.Excel
{
    public class ExcelService : IExcelService
    {
        private int _lastRow;
        private XSSFWorkbook _workbook;
        public MemoryStream Convert(ReportDto report)
        {
            _workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)_workbook.CreateSheet("PE form");

            AddTitleToHeader(sheet);
            AddPropertiesToHeader(sheet, report.Header.Properties);
            AddAssessmentsToHeader(sheet, report.Header.Assessments);

            if (report.Header.Summary != null) //залишив перевірку на null, щоб якщо Summary не запонено не було ексепшена
            {
                AddSummaryToHeader(sheet, report.Header.Summary);
            }
            AddRowDataToForm(sheet, report.Rows);

            var memoryStream = new NpoiMemoryStream();
            memoryStream.AllowClose = false;
            _workbook.Write(memoryStream);
            memoryStream.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.AllowClose = true;

            return memoryStream;
        }

        private void AddTitleToHeader(XSSFSheet sheet)
        {   //перша строка заголовок
            var styleForTitle = new StyleFactory(_workbook).SetHorizontalAlignmentCenter().SetVerticalAlignmentCenter().SetBoldFont().SetIndexedColor(IndexedColors.Coral).CreateStyle();

            var currentRowForTitle = sheet.CreateRow(0);
            var currentCellForTitle = currentRowForTitle.CreateCell(0);
            SetCellValueAndMerge(sheet, currentCellForTitle, "Performance evaluation", 0, 0, 0, 4, styleForTitle);
        }

        private void AddPropertiesToHeader(XSSFSheet sheet, IEnumerable<ReportHeaderPropertiesItemDto> properties)
        {
            var styleForPropertieBold = new StyleFactory(_workbook).SetBoldFont().SetIndexedColor(IndexedColors.Grey25Percent).CreateStyle();
            var styleForPropertie = new StyleFactory(_workbook).SetIndexedColor(IndexedColors.Grey25Percent).CreateStyle();

            int propertyRowIndex = 1;
            foreach (ReportHeaderPropertiesItemDto PropertyItem in properties)
            {
                var currentRow = sheet.CreateRow(propertyRowIndex);
                var propertyNameCell = currentRow.CreateCell(1);

                propertyNameCell.SetCellValue(PropertyItem.PropertyName);
                propertyNameCell.CellStyle = styleForPropertie;

                sheet.AutoSizeColumn(1);

                var propertyValueCell = currentRow.CreateCell(2);
                propertyValueCell.SetCellValue(PropertyItem.PropertyValue);
                if (PropertyItem.IsBold)
                    propertyValueCell.CellStyle = styleForPropertieBold;
                else
                    propertyValueCell.CellStyle = styleForPropertie;

                sheet.AutoSizeColumn(2);

                propertyRowIndex ++;
            }
            _lastRow = propertyRowIndex;
        }

        private void AddAssessmentsToHeader(XSSFSheet sheet, IEnumerable<ReportHeaderAssessmentItemDto> assessments)
        {
            var styleForAssessmentBold = new StyleFactory(_workbook).SetHorizontalAlignmentCenter().SetBoldFont().SetIndexedColor(IndexedColors.Grey25Percent).CreateStyle();
            var styleForAssessment = new StyleFactory(_workbook).SetHorizontalAlignmentCenter().SetIndexedColor(IndexedColors.Grey25Percent).CreateStyle();

            var lastPropertyRow = _lastRow;

            int assessmentRowIndex = 1;
            var currentRow = sheet.GetRow(assessmentRowIndex); //отримуємо першу строку

            var assessmentNameTitleCell = currentRow.CreateCell(3); //асесменти
            assessmentNameTitleCell.SetCellValue("Бали");
            assessmentNameTitleCell.CellStyle = styleForAssessmentBold;

            var assessmentCommentRequiredTitleCell = currentRow.CreateCell(4);//чи наявні коменти
            assessmentCommentRequiredTitleCell.SetCellValue("Comment");
            assessmentCommentRequiredTitleCell.CellStyle = styleForAssessmentBold;

            assessmentRowIndex ++;

            foreach (ReportHeaderAssessmentItemDto assessmentItem in assessments)
            {
                if (assessmentRowIndex > lastPropertyRow) //відсутній метод GetOrCreate
                    { currentRow = sheet.CreateRow(assessmentRowIndex); }
                else
                    { currentRow = sheet.GetRow(assessmentRowIndex); }

                var assessmentNameCell = currentRow.CreateCell(3); //асесменти
  
                assessmentNameCell.SetCellValue(assessmentItem.Name);
                assessmentNameCell.CellStyle = styleForAssessment;

                sheet.AutoSizeColumn(3);

                var assessmentCommentRequiredCell = currentRow.CreateCell(4);//чи наявні коменти

                assessmentCommentRequiredCell.SetCellValue(assessmentItem.IsCommentRequired ? "Required" : "Not required");

                assessmentCommentRequiredCell.CellStyle = styleForAssessment;
                sheet.AutoSizeColumn(4);

                assessmentRowIndex ++;
            }
            if (assessmentRowIndex > _lastRow) //кількість асесментів може бути меншою за властивості
            { _lastRow = assessmentRowIndex; }
        }

        private void AddSummaryToHeader(XSSFSheet sheet, string summary)
        {
            var styleForSummary = new StyleFactory(_workbook).SetVerticalAlignmentTop().SetIndexedColor(IndexedColors.White).SetWrapText().CreateStyle();
            var columnForSummary = 5;

            var firstRowForSummary = sheet.GetRow(0);
            var summaryCell = firstRowForSummary.CreateCell(columnForSummary);
            SetCellValueAndMerge(sheet, summaryCell, summary, 0, _lastRow - 1, columnForSummary, columnForSummary + 1,
                styleForSummary);

            sheet.SetColumnWidth(columnForSummary, 5500); //задаемо ширину кожної з 2 колонок вручну, при автоширині колонка неадекватно розтягується 

            //ініціалізуємо пусту колону, ексель не дає мержити без цього 
            var summaryCellForMerging = firstRowForSummary.CreateCell(columnForSummary + 1);
            summaryCellForMerging.SetCellValue("");
            sheet.SetColumnWidth(columnForSummary + 1, 5500);
        }

        private void AddRowDataToForm(XSSFSheet sheet, IEnumerable<ReportFormDataRowDto> rowData)
        {
            var styleForRowTitle = new StyleFactory(_workbook).SetBoldFont().SetIndexedColor(IndexedColors.Grey25Percent).CreateStyle();
            var styleForRow = new StyleFactory(_workbook).SetIndexedColor(IndexedColors.Tan).CreateStyle();

            rowData = rowData.OrderBy(o => o.Order);

            //заповняємо першу строку в таблиці
            CreateRowDataTitle(sheet, rowData);
            bool firstHeaderAfterTitle = true;

            foreach (ReportFormDataRowDto row in rowData)
            {
                var columnIndex = 2;
                var currentStyle = styleForRowTitle;

                if (IsRowTitle(row.FieldTypeId)) //перевірка на тип поля
                {
                    if (firstHeaderAfterTitle) firstHeaderAfterTitle = false; //перевірка чи перший після заголовка
                    else _lastRow ++;
                }
                else currentStyle = styleForRow;

                var currentRow = sheet.CreateRow(_lastRow);
                var currentRowTitleCell = currentRow.CreateCell(0);
                SetCellValueAndMerge(sheet, currentRowTitleCell, row.FieldName, _lastRow, _lastRow, 0, 1,
                    currentStyle);

                sheet.AutoSizeColumn(0);
                foreach (ReportFieldDataAssessmentDto RowAssessmentData in row.Assessments)
                {
                    AddAssessmentData(sheet, currentRow, RowAssessmentData,
                        currentStyle, ref columnIndex); //використовуємо ref для обрахунку індекса в циклі
                }
                
                _lastRow ++;
            }
        }

        private void AddAssessmentData(XSSFSheet sheet, IRow currentRow, ReportFieldDataAssessmentDto rowAssessmentData, ICellStyle cellStyleAlignment, ref int columnIndex)
        {   //додаємо оцінки та коменти в таблицю
            var currentCell = currentRow.CreateCell(columnIndex); //Assessment
            currentCell.SetCellValue(rowAssessmentData.AssessmentName);
            currentCell.CellStyle = cellStyleAlignment;
            sheet.AutoSizeColumn(columnIndex);

            columnIndex ++;

            currentCell = currentRow.CreateCell(columnIndex); //Comment
            currentCell.SetCellValue(rowAssessmentData.Comment);
            currentCell.CellStyle = cellStyleAlignment;
            sheet.AutoSizeColumn(columnIndex);

            columnIndex ++;
        }

        private void CreateRowDataTitle(XSSFSheet sheet, IEnumerable<ReportFormDataRowDto> rowData)
        {
            var styleForRowTitle = new StyleFactory(_workbook).SetHorizontalAlignmentCenter().SetVerticalAlignmentCenter().SetBoldFont().SetIndexedColor(IndexedColors.Grey40Percent).CreateStyle(); ;

            var columnIndex  = 2;

            var currentTitleRow = sheet.CreateRow(_lastRow + 1); //другий рядок для заголовка

            var currentRow = sheet.CreateRow(_lastRow);
            var dataRowTitleCell = currentRow.CreateCell(0);

            var firstDataTableRow = rowData.First(); //заголовок Skills
            SetCellValueAndMerge(sheet, dataRowTitleCell, "Skills", _lastRow, _lastRow + 1, 0, 1,
                styleForRowTitle);
            sheet.AutoSizeColumn(0);

            foreach (ReportFieldDataAssessmentDto firstRowAssessmentData in firstDataTableRow.Assessments)
            {
                var currentTitleAssessmentCell = currentRow.CreateCell(columnIndex);

                SetCellValueAndMerge(sheet, currentTitleAssessmentCell, firstRowAssessmentData.ReporterName, _lastRow, _lastRow, columnIndex, columnIndex + 1,
                    styleForRowTitle);

                var assessmentTitleCell = currentTitleRow.CreateCell(columnIndex); //заголовок Assessment
                assessmentTitleCell.SetCellValue("Assessment");
                assessmentTitleCell.CellStyle = styleForRowTitle;
                sheet.AutoSizeColumn(columnIndex);

                columnIndex ++;

                var commentTitleCell = currentTitleRow.CreateCell(columnIndex); //заголовок Comment
                commentTitleCell.SetCellValue("Comment");
                commentTitleCell.CellStyle = styleForRowTitle;
                sheet.AutoSizeColumn(columnIndex);

                columnIndex ++;
            }

            _lastRow += 2;
        }

        private void SetCellValueAndMerge(XSSFSheet sheet, ICell currentCell, string cellValue, int firstRow, int lastRow, int firstCell, int lastCell, ICellStyle cellStyleAlignment = null)
        {   //створити і об'єднати комірки по переданим координатам
            currentCell.SetCellValue(cellValue);
            if (cellStyleAlignment != null)
                { currentCell.CellStyle = cellStyleAlignment; }

            var cellRange = new NPOI.SS.Util.CellRangeAddress(firstRow, lastRow, firstCell, lastCell);
            sheet.AddMergedRegion(cellRange);
        }

        private bool IsRowTitle(int rowNumber)
        {
            return rowNumber == 1;
        }

        public class NpoiMemoryStream : MemoryStream
        {   //перевизначаємо наш стрім для корректної роботи
            public NpoiMemoryStream()
            {
                AllowClose = true;
            }

            public bool AllowClose { get; set; }

            public override void Close()
            {
                if (AllowClose)
                    base.Close();
            }
        }

    }

}

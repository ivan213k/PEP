using PerformanceEvaluationPlatform.Application.Model.Excel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

namespace PerformanceEvaluationPlatform.Application.Services.Excel
{
    public class StyleFactory
    {
        private static XSSFWorkbook _workbookForStyle;
        private HorizontalAlignment _horizontalAlignment = HorizontalAlignment.Left;
        private VerticalAlignment _verticalAlignment = VerticalAlignment.Top;
        private bool _isBold;
        private bool _wrapText;
        private IndexedColors _indexedColor = IndexedColors.White;
        public StyleFactory(XSSFWorkbook workbook)
        {
            _workbookForStyle = workbook;
        }
        public ICellStyle CreateStyle()
        {
            var style = _workbookForStyle.CreateCellStyle();
            style.Alignment = this._horizontalAlignment;
            style.VerticalAlignment = this._verticalAlignment;
            style.WrapText = this._wrapText;
            style.FillBackgroundColor = this._indexedColor.Index;

            var Font = _workbookForStyle.CreateFont();
            Font.IsBold = this._isBold;
            style.SetFont(Font);

            return style;
        }

        public StyleFactory SetHorizontalAlignmentCenter()
        {
            this._horizontalAlignment = HorizontalAlignment.Center;
            return this;
        }
        public StyleFactory SetVerticalAlignmentCenter()
        {
            this._verticalAlignment = VerticalAlignment.Center;
            return this;
        }
        public StyleFactory SetVerticalAlignmentTop()
        {
            this._verticalAlignment = VerticalAlignment.Top;
            return this;
        }
        public StyleFactory SetBoldFont()
        {
            this._isBold = true;
            return this;
        }
        public StyleFactory SetWrapText()
        {
            this._wrapText = true;
            return this;
        }
        public StyleFactory SetIndexedColor(IndexedColors BackgroundColor)
        {
            this._indexedColor = BackgroundColor;
            return this;
        }
    }
}

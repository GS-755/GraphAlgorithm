namespace ConsoleApp1.Models.Utils
{
    /// <summary>
    /// Class lưu trữ thuộc tính của hàm Helper.OverrideMatrixData()
    /// </summary>
    public class OverrideMatrixParams
    {
        public int RowIndex { get; set; }
        public int SourceData { get; set; }
        public int ColIndex { get; set; }
        public int DataToReplace { get; set; }
        public bool FlgPrintMatrix { get; set; }
        public bool RemoveAllLines { get; set; }

        /// <summary>
        /// Constructor khởi tạo đối tượng params có tham số mặc định và flag printMatrix = true
        /// </summary>
        public OverrideMatrixParams()
        {
            this.FlgPrintMatrix = true; 
        }
        /// <summary>
        /// Constructor khởi tạo đối tượng params có tham số in ma trận tùy chỉnh 
        /// </summary>
        /// <param name="flgPrintMatrix"></param>
        public OverrideMatrixParams(bool flgPrintMatrix)
        {
            this.FlgPrintMatrix = flgPrintMatrix;
        }
    }
}

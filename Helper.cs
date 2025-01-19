using System;
using System.IO;

namespace ConsoleApp1
{
    public class Helper
    {
        /// <summary>
        /// Số đỉnh của đồ thị
        /// </summary>
        public static int NumOfVerticles { get; set; }
        /// <summary>
        ///  Số cạnh của đồ thị 
        /// </summary>
        public static int NumOfEdges { get; set; }
        /// <summary>
        /// Mảng 2 chiều biểu diễn đồ thị vô hướng
        /// </summary>
        public static int[,] ArrayMatrix { get; set; }
        /// <summary>
        /// Chiều dài của ma trận 
        /// </summary>
        public static int Row { get; set; }
        /// <summary>
        /// Chiều rộng của ma trận 
        /// </summary>
        public static int Col { get; set; }

        public static bool ReadMatrix(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Invalid file PATH!"); 
                return false; 
            }
            if(!File.Exists(path))
            {
                Console.WriteLine($"File {Path.GetFullPath(path)} not found!");
                return false;
            }
            string[] lines = File.ReadAllLines(path); 
            if(lines == null || lines.Length == 0)
            {
                Console.WriteLine("Invalid data!");
                return false;
            }
            /* Đọc & lấy số bậc */
            // Out reference number
            int number = 0;
            string[] numVerticesData = lines[0].Trim().Split(' ');
            /* Parse số đỉnh ma trận */
            bool getNumVerticesStatus = int.TryParse(numVerticesData[0], out number);
            if(getNumVerticesStatus == false)
            {
                Console.WriteLine("Parse number of vertices failed!"); 
                return false;
            }
            // Cast số đỉnh ma trận
            NumOfVerticles = number;
            /* Parse & cast số cạnh ma trận */
            // Check độ dài mảng số đỉnh | số cạnh 
            if(numVerticesData.Length == 2)
            {
                if (!string.IsNullOrEmpty(numVerticesData[1]))
                {
                    bool getNumEdgeStatus = int.TryParse(numVerticesData[1], out number);
                    if (getNumEdgeStatus == false)
                    {
                        Console.WriteLine("Parse number of edges failed!");
                    }
                    else
                    {
                        NumOfEdges = number;
                    }
                }
            }
            /* Khởi tạo ma trận & cast chiều dài x chiều rộng của ma trận */
            Row = (NumOfEdges > 0 ? NumOfEdges : NumOfVerticles);
            Col = (NumOfEdges > 0 ? NumOfEdges : NumOfVerticles);
            ArrayMatrix = new int[Row, Col];
            /* Loop & insert data vào ma trận */
            int len = lines.Length;
            for(int i = 1; i < len; i++)
            {
                if(string.IsNullOrEmpty(lines[i]))
                {
                    Console.WriteLine($"Text Data line #{i} Invalid");
                    continue;
                }
                string[] line = lines[i].Trim().Split(' ');
                // Sub-line length 
                int subLineLength = line.Length;
                for (int j = 0; j < subLineLength; j++)
                {
                    string data = line[j];
                    if (string.IsNullOrEmpty(data))
                    {
                        Console.WriteLine($"Row: {i}, Col: {j} Invalid data!");
                        continue;
                    }
                    bool isIntConvertSuccess = int.TryParse(line[j], out number);
                    if (isIntConvertSuccess)
                    {
                        ArrayMatrix[i - 1, j] = number;
                    }
                }
            }
            /* In thử ma trận */
            PrintMatrix(); 

            return true; 
        }
        /// <summary>
        /// In ma trận ra console 
        /// </summary>
        /// <returns></returns>
        static bool PrintMatrix()
        {
            if(ArrayMatrix == null)
            {
                Console.WriteLine("PrintMatrix invalid params!");
                return false; 
            }
            for(int i = 0; i < Row; i++)
            {
                for(int j = 0; j < Col; j++)
                {
                    Console.Write($"{ArrayMatrix[i, j]} "); 
                }
                Console.WriteLine();    
            }
            Console.WriteLine();

            return true; 
        }
    }
}

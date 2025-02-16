using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp1.Models;

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
        /// <summary>
        /// Danh sách các dòng input của ma trận (kiểu dữ liệu: int) 
        /// </summary>
        public static List<int> GetMatrixRow(int rowIndex)
        {
            if (ArrayMatrix == null)
            {
                Console.WriteLine("Helper.GetMatrixRow() invalid matrix!");
                return null;
            }
            if (Row == 0 && Col == 0)
            {
                Console.WriteLine("Helper.GetMatrixRow() invalid size!");
                return null;
            }
            if(rowIndex >= NumOfVerticles)
            {
                Console.WriteLine("Helper.GetMatrixRow() invalid row index!");
                return null;
            }
            try
            {
                List<int> matrixRowData = new List<int>();
                for (int i = 0; i < Row; i++)
                {
                    matrixRowData.Add(ArrayMatrix[rowIndex, i]);
                }

                return matrixRowData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Helper.GetMatrixRow() unhandled exception!");
                Console.WriteLine(ex);
                return null;
            }
        }
        /// <summary>
        /// Đọc dữ liệu ma trận từ file văn bản và lưu dữ liệu vào Helper.ArrayMatrix
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Bool: Kết quả đọc ma trận</returns>
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
        /// <summary>
        /// Convert Danh sách cạnh => Danh sách kề
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfEdges"></param>
        /// <param name="numOfVertices"></param>
        /// <returns>Danh sách kề theo định dạng Mảng danh sách số nguyên</returns>
        public static List<int>[] ConvertEdgeListToAdjacency(int[,] matrix, int numOfVertices, int numOfEdges)
        {
            if(matrix == null)
            {
                Console.WriteLine("Helper.ConvertEdgeListToAdjacency() Invalid params!");
                return null;
            }
            int rowSize = matrix.GetLength(0);
            if(rowSize != numOfEdges)
            {
                Console.WriteLine("Helper.ConvertEdgeListToAdjacency() Invalid data format!");
                return null;
            }
            // Khai báo List lưu trữ ds cạnh 
            List<int>[] adjList = null;
            try
            {
                adjList = new List<int>[numOfEdges + 1];
                // Loop & init các cạnh theo danh sách số nguyên 
                // Loop từ 1 để?
                for (int i = 1; i <= numOfEdges; i++)
                {
                    adjList[i] = new List<int>();
                }
                // Loop các cạnh & add các cạnh vào danh sách đã init 
                // Loop theo số đỉnh 
                for (int i = 0; i <= NumOfVerticles; i++)
                {
                    int startVertice = matrix[i, 0];
                    int endVertice = matrix[i, 1];
                    adjList[startVertice].Add(endVertice);
                    adjList[endVertice].Add(startVertice);
                }
                // Loop các danh sách cạnh đã init & sort lại giá trị bên trong danh sách 
                for (int i = 1; i <= NumOfEdges; i++)
                {
                    adjList[i].Sort();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Helper.ConvertEdgeListToAdjacency() uncaught exception: "); 
                Console.WriteLine(ex);
            }

            return adjList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfVertice"></param>
        /// <returns></returns>
        public static List<Edge> ConvertAdjacencyToEdgeList(int[,] matrix, int numOfVertice)
        {
            if (matrix == null)
            {
                Console.WriteLine("Helper.ConvertAdjacencyToEdgeList() Invalid params!");
            }
            /* Append từng dòng của input danh sách kề => List */
            int rowSize = matrix.GetLength(0);
            int colSize = matrix.GetLength(1);  
            // Khởi tạo List lưu trữ đỉnh kề 
            List<List<int>> storedAdjLst = new List<List<int>>();
            // Loop & insert đỉnh kề => List
            for(int i = 0; i < rowSize; i++)
            {
                List<int> adjLst = new List<int>();
                for(int j = 0; j < colSize; j++)
                {
                    // Trường hợp đỉnh cô lập 
                    if(matrix[i, j] == 0)
                    {
                        continue;
                    }
                    // Insert đỉnh kề
                    adjLst.Add(matrix[i, j]);
                }
                storedAdjLst.Add(adjLst);
            }
            // Tạo danh sách cạnh để lưu kết quả
            List<Edge> edgeLst = null;
            try
            {
                // Tạo danh sách cạnh
                edgeLst = new List<Edge>();
                // Khởi tạo biến cục bộ danh sách cạnh
                HashSet<Edge> seenEdges = new HashSet<Edge>();
                // Loop & tìm cạnh
                for (int j = 1; j <= numOfVertice; j++)
                {
                    // Handle trường hợp danh sách parse từ input không hợp lệ
                    if (storedAdjLst[j - 1] == null || storedAdjLst[j - 1].Count == 0)
                    {
                        continue;
                    }
                    foreach (int item in storedAdjLst[j - 1])
                    {
                        // Trường hợp Đỉnh cô lập
                        if (item == 0)
                        {
                            continue;
                        }
                        // Tìm start-point & end-point 
                        int startVertice = 0;
                        int endVertice = 0;
                        // Trường hợp loop start = end
                        if (j == item)
                        {
                            continue;
                        }
                        // Xử lý logic tìm start - end
                        if (j < item)
                        {
                            startVertice = j;
                            endVertice = item;
                        }
                        if (j > item)
                        {
                            startVertice = item;
                            endVertice = j;
                        }
                        // Build đối tượng cạnh
                        Edge builtEdge = BuildEdge(startVertice, endVertice);
                        if (builtEdge == null)
                        {
                            Console.WriteLine("Helper.ConvertAdjacencyToEdgeList build Edge failed!");
                            continue;
                        }
                        // Check nếu Edge đã build có tồn tại trong hashset 
                        Edge foundEdge = seenEdges.FirstOrDefault(x => (x != null && x.Equals(builtEdge)));
                        // Thêm cạnh đã build vào HashSet nếu chưa tồn tại
                        // Để đảm bảo danh sách cạnh không bị trùng lặp
                        if (foundEdge == null)
                        {
                            seenEdges.Add(builtEdge);
                            // Thêm cạnh vào danh sách return kết quả 
                            edgeLst.Add(builtEdge);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Helper.ConvertAdjacencyToEdgeList() uncaught exception: ");
                Console.WriteLine(ex);
            }

            return edgeLst;
        }
        static Edge BuildEdge(int startVertice, int endVertice)
        {
            if(startVertice <= 0 || endVertice <= 0)
            {
                return null;
            }
            int gap = startVertice + endVertice; 
            if(gap <= 0)
            {
                return null;
            }
            Edge edge = new Edge(startVertice, endVertice);

            return edge;
        }
        static Edge BuildEdge(int startVertice, int endVertice, int size)
        {
            if(size <= 0)
            {
                Console.WriteLine("Please use Helper.BuildEdge(int startVertice, int endVertice) instead!");
                return null;
            }
            Edge edgeWithSize = BuildEdge(startVertice, endVertice);
            edgeWithSize.Size = size;

            return edgeWithSize;
        }
    }
}

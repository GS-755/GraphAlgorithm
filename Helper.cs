using System;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    public class Helper
    {
        /// <summary>
        /// Relative asset path, ex. when running on IDE: 
        /// <br/>
        /// .NET Core: ..\\..\\..\\
        /// <br/>
        /// .NET Framework: ..\\..\\
        /// <br/>
        /// Change it on App.config => configuration => appSettings => RELATIVE_PATH_CFG
        /// </summary>
        public static string RELATIVE_ASSET_PATH = ReadConfigByKey("RELATIVE_PATH_CFG");
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
        /// Danh sách các tham số input  
        /// </summary>
        static Dictionary<int, List<int>> Args { get; set; } = new Dictionary<int, List<int>>();
        /// <summary>
        /// Tập Dictionary các đỉnh đã viếng thăm (lưu trữ toàn cục trong class Helper)
        /// </summary>
        static Dictionary<int, bool> VisitedVertice { get; set; }

        /// <summary>
        /// Kiểm tra nếu các đỉnh trong đồ thị đã được viếng thăm TOÀN BỘ hay chưa
        /// </summary>
        /// <returns>bool: Cho biết các value trong VisitedVertice: Dictionary = true hết hay chưa.</returns>
        public static bool IsAllVerticesVisited()
        {
            if(VisitedVertice == null)
            {
                Console.WriteLine("");
                return false; 
            }
            /* 
             * Loop Dictionary & tìm ĐỈNH CHƯA VIẾNG THĂM 
             * (Nếu có >= 1 false: return false => dừng kiểm tra)
             */
            if(VisitedVertice.ContainsValue(false))
            {
                return false; 
            }

            return true;
        }
        /// <summary>
        /// Handle program exit with status code & log displayed
        /// </summary>
        /// <param name="exitCode"></param>
        static void HandleCrashProgram(int exitCode = -1) 
        {
            Console.WriteLine($"Program crashed with code {exitCode}!");
            Environment.Exit(0);

            return;
        }
        /// <summary>
        /// Hàm lấy dữ liệu của 1 dòng trong ma trận 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns>Danh sách int các dữ liệu của ma trận</returns>
        public static List<int> GetMatrixRow(int rowIndex)
        {
            if (ArrayMatrix == null)
            {
                Console.WriteLine("Helper.GetMatrixRow() invalid internal matrix!");
                return null;
            }
            if (Row == 0 && Col == 0)
            {
                Console.WriteLine("Helper.GetMatrixRow() invalid internal matrix size!");
                return null;
            }
            if(rowIndex > NumOfVerticles)
            {
                Console.WriteLine("Helper.GetMatrixRow() invalid internal matrix row index!");
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
        /// Parse các tham số của bài làm theo số dòng chỉ định (tính từ dòng 0) sang Helper.Args: Dictionary với key = dòng i
        /// </summary>
        /// <param name="path"></param>
        /// <param name="inputLineSize"></param>
        public static void ParseParams(string path, int inputLineSize = 1)
        {
            // Khởi tạo lại Dictionary params 
            Args = new Dictionary<int, List<int>>(); 
            // Validate dữ liệu đầu vào 
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Helper.ParseParams() Invalid file PATH!");
                HandleCrashProgram();
                return;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine($"Helper.ParseParams() File {Path.GetFullPath(path)} not found!");
                HandleCrashProgram();
                return;
            }
            if(inputLineSize < 1)
            {
                Console.WriteLine("Helper.ParseParams() Invalid inputLineSize!");
                HandleCrashProgram();
                return; 
            }
            // Out reference number
            int number = 0;
            // Đọc stream file văn bản 
            string[] lines = File.ReadAllLines(path);
            // Loop từ dòng đầu tiên đến dòng chỉ định (-1) 
            for (int i = 0; i < inputLineSize; i++)
            {
                try
                {
                    // Tách params theo dấu ' ' 
                    string[] arrParams = lines[i].Trim().Split(' ');
                    if (arrParams == null || arrParams.Length <= 0)
                    {
                        Console.WriteLine($"Helper.ReadMatrix() Read param(s) line #{i + 1} failed!");
                        continue;
                    }
                    int arrParamsSize = arrParams.Length;
                    // Init danh sách tạm để chứa params 
                    List<int> paramsLst = new List<int>();
                    for (int j = 0; j < arrParamsSize; j++)
                    {
                        bool tryParseParam = int.TryParse(arrParams[j], out number);
                        if (tryParseParam == false)
                        {
                            Console.WriteLine($"Helper.ReadMatrix() Params index [i = {i + 1}, j = {j + 1}] failed!");
                            continue;
                        }
                        // Thêm params vào danh sách tạm 
                        paramsLst.Add(number);
                    }
                    // Thêm params vào Dictionary với key = số dòng đang duyệt 
                    if (paramsLst.Count > 0)
                    {
                        Args.Add(i, paramsLst);
                    }
                    else
                    {
                        Console.WriteLine($"Helper.ReadMatrix() Params line #{i + 1} no data!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Helper.ReadMatrix() Read params line #{i} unhandled exception: ");
                    Console.WriteLine(ex);
                }
            }
        }
        /// <summary>
        /// Lấy tham số từ Helper.Args: Dictionary 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="argsOrder"></param>
        /// <returns>Tham số đã được parsed ra số nguyên</returns>
        public static int GetParamsValue(int rowIndex, int argsOrder)
        {
            if(Args == null)
            {
                Console.WriteLine($"Helper.GetParamsValue() Invalid args data!");
                return -1; 
            }
            try
            {
                int data = Args[rowIndex][argsOrder];

                return data; 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Helper.GetParamsValue() unhandled exception: ");
                Console.WriteLine(ex);

                return -1;
            }
        }
        /// <summary>
        /// Đọc dữ liệu ma trận từ file văn bản và lưu dữ liệu vào Helper.ArrayMatrix
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startMatrixRow"></param>
        /// <returns>Bool: Kết quả đọc ma trận</returns>
        public static bool ReadMatrix(string path, int startMatrixRow = 1)
        {
            if(string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Helper.ReadMatrix() Invalid file PATH!"); 
                HandleCrashProgram();
                return false; 
            }
            if(!File.Exists(path))
            {
                Console.WriteLine($"Helper.ReadMatrix() File {Path.GetFullPath(path)} not found!");
                HandleCrashProgram();
                return false;
            }
            string[] lines = File.ReadAllLines(path); 
            if(lines == null || lines.Length == 0)
            {
                Console.WriteLine("Helper.ReadMatrix() Invalid data!");
                return false;
            }
            // Số dòng trong input file 
            int numOfLines = lines.Length;
            // Out reference number
            int number = 0;
            /* Cast chiều dài x chiều rộng của ma trận */
            Row = (NumOfEdges > NumOfVerticles ? NumOfEdges : NumOfVerticles);
            Col = (NumOfEdges > NumOfVerticles ? NumOfEdges : NumOfVerticles);
            // Khởi tạo (lại) ma trận) 
            ArrayMatrix = new int[Row, Col];
            /* Loop & insert data vào ma trận */
            for(int i = startMatrixRow; i < numOfLines; i++)
            {
                string[] line = null; 
                try
                {
                    if (string.IsNullOrEmpty(lines[i]))
                    {
                        Console.WriteLine($"Helper.ReadMatrix() Text Data - File: {Path.GetFileName(path)} - line #{i} Invalid");
                        continue;
                    }
                    line = lines[i].Trim().Split(' ');
                }
                catch(IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Helper.ReadMatrix() IndexOutOfRangeException: ");
                    Console.WriteLine(ex);
                    continue;
                }
                // Sub-line length 
                int subLineLength = line.Length;
                for (int j = 0; j < subLineLength; j++)
                {
                    string data = line[j];
                    if (string.IsNullOrEmpty(data))
                    {
                        Console.WriteLine($"Helper.ReadMatrix() Row: {i}, Col: {j} Invalid data!");
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
            Console.WriteLine($"Matrix file: {Path.GetFileName(path)}");
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
                Console.Write($"Line {i:00}: ");
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
                for (int i = 0; i <= numOfEdges; i++)
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
        /// Chuyển đổi Danh sách kề thành Danh sách cạnh
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfVertice"></param>
        /// <returns>List Danh sách cạnh</returns>
        public static List<Edge> ConvertAdjacencyListToEdgeList(int[,] matrix, int numOfVertice)
        {
            if (matrix == null)
            {
                Console.WriteLine("Helper.ConvertAdjacencyListToEdgeList() Invalid params!");
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
                            Console.WriteLine("Helper.ConvertAdjacencyListToEdgeList build Edge failed!");
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
                Console.WriteLine("Helper.ConvertAdjacencyListToEdgeList() uncaught exception: ");
                Console.WriteLine(ex);
            }

            return edgeLst;
        }
        public static Edge BuildEdge(int startVertice, int endVertice)
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
        public static Edge BuildEdge(int startVertice, int endVertice, int size)
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
        /// <summary>
        /// Khởi tạo tập danh sách các đỉnh đã viếng thăm (visited) 
        /// </summary>
        /// <param name="numOfVertice"></param>
        /// <returns>Tập danh sách các đỉnh đã viếng thăm (visited) </returns>
        static Dictionary<int, bool> InitVisitedDict(int numOfVertice)
        {
            if(numOfVertice == 0)
            {
                return null;
            }
            Dictionary<int, bool> visitedDict = new Dictionary<int, bool>();
            for (int i = 1; i <= numOfVertice; i++)
            {
                visitedDict.Add(i, false);
            }

            return visitedDict;
        }
        /// <summary>
        /// Kiểm tra đỉnh đang xét đã được viếng thăm hay chưa
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns>Trạng thái viếng thăm của đỉnh đang xét</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static bool IsVerticeVisited(int vertice)
        {
            if(vertice <= 0)
            {
                Console.WriteLine("Helper.IsVerticeVisited() Invalid params!");
                return false;
            }
            if(VisitedVertice == null)
            {
                throw new NullReferenceException("Helper.VisitedVertice is not initialized!");
            }
            // Init out boolean reference: VisitedVertice try get key status 
            bool tryGetDictValueResult = false;
            // Try to get value from dictionary 
            VisitedVertice.TryGetValue(vertice, out tryGetDictValueResult);
            if(tryGetDictValueResult == false)
            {
                return false; 
            }

            // Return value result of searched key 
            return VisitedVertice[vertice];
        } 
        /// <summary>
        /// Duyệt đồ thị theo chiều ngang (Breadth First Search a.k.a BFS)
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfVertice"></param>
        /// <param name="startVertice"></param>
        /// <param name="endVertice"></param>
        /// <param name="insertStartVertice"></param>
        /// <returns>Danh sách các miền liên thông đã duyệt từ đỉnh startVertice</returns>
        public static List<int> BFS(int[,] matrix, int numOfVertice, int startVertice, int endVertice = 0, bool insertStartVertice = false)
        {
            if(matrix == null || numOfVertice == 0 || startVertice == 0)
            {
                Console.WriteLine("Helper.BFS() Invalid params!");
                return null;
            }
            // Danh sách các đỉnh liên thông
            List<int> bfsResults = new List<int>();
            /* 
             * Danh sách các đỉnh đã viếng thăm 
             * (Khởi tạo nếu chưa có) 
             */
            if (VisitedVertice == null)
            {
                VisitedVertice = InitVisitedDict(numOfVertice);
            }
            // Hàng đợi BFS
            Queue<int> bfsQueue = new Queue<int>();
            // Khởi tạo Dictionary để lưu đường đi
            Dictionary<int, int> parent = new Dictionary<int, int>();
            try
            {
                // Enqueue đỉnh xuất phát & đánh dấu trạng thái visited = true
                bfsQueue.Enqueue(startVertice);
                VisitedVertice[startVertice] = true;
                // Nếu flag insertStartVertice = true: insert đỉnh start vào kết quả BFS 
                if(insertStartVertice == true)
                {
                    bfsResults.Add(startVertice);
                }
                // Đỉnh start => start parent = -1
                parent[startVertice] = -1;
                // Duyệt & enqueue các đỉnh kề
                while (bfsQueue.Count > 0)
                {
                    // Dequeue đỉnh trong queue
                    int dequeuedItem = bfsQueue.Dequeue();
                    // Tìm đường đi: Kiểm tra nếu đỉnh hiện tại là đỉnh kết thúc
                    if(endVertice > 0)
                    {
                        if (dequeuedItem == endVertice)
                        {
                            // Tạo đường đi từ đỉnh start đến đỉnh end
                            List<int> path = new List<int>();
                            int currentFindPathVertice = endVertice;
                            /* 
                             * Ngoại trừ startVertice, 
                             * loop các đỉnh kề trước các đỉnh đã xét và thêm vào danh sách kết quả 
                             */
                            while (currentFindPathVertice != -1)
                            {
                                path.Add(currentFindPathVertice);
                                currentFindPathVertice = parent[currentFindPathVertice];
                            }
                            path.Reverse();

                            return path;
                        }
                    }
                    List<int> adjLst = GetMatrixRow(dequeuedItem - 1);
                    if(adjLst == null || adjLst.Count == 0)
                    {
                        Console.WriteLine($"Helper.BFS() Vertice #{dequeuedItem} no adjacency vertice found!");
                        continue;
                    }
                    // Loop các đỉnh kề của đỉnh đang xét 
                    foreach (int adjVertice in adjLst)
                    {
                        // Trường hợp đỉnh cô lập
                        if(adjVertice == 0)
                        {
                            continue;
                        }
                        // Trường hợp đỉnh đã được viếng thăm
                        if (VisitedVertice[adjVertice] == true)
                        {
                            continue;
                        }
                        // Enqueue & đánh dấu visited đỉnh kề đang xét
                        bfsQueue.Enqueue(adjVertice);
                        VisitedVertice[adjVertice] = true;
                        // Thêm đỉnh kề liên thông vào danh sách kết quả
                        bfsResults.Add(adjVertice);
                        // Lưu lại đỉnh kề trước đỉnh đang xét 
                        parent[adjVertice] = dequeuedItem;
                    }
                }

                return bfsResults;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Helper.BFS() uncaught exception: ");
                Console.WriteLine(ex);  
                return null;
            }
        }
        #region External function(s), not relate to main objectives
        /// <summary>
        /// Function to parse configuration(s) from App.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Parsed value by string</returns>
        public static string ReadConfigByKey(string key)
        {
            if(key == null)
            {
                Console.WriteLine("Helper.ReadConfigByKey() Invalid params!");
                return null;
            }
            string value = null; 
            try
            {
                value = ConfigurationManager.AppSettings[key];
            }
            catch(Exception ex)
            {
                Console.WriteLine("Helper.ReadConfigByKey() uncaught exception: ");
                Console.WriteLine(ex);
                HandleCrashProgram();
            }

            return value;
        }
        #endregion
    }
}

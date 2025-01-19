using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi2
    {
        static void Bai1()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyMatrix.inp";
            string outFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyMatrix.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath); 
            if(handleInputStatus == false)
            {
                Console.WriteLine("Buoi2.Bai1() Invalid input data!");
                return; 
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Tính số bậc ma trận 
            int[] degreeOfVertices = DegreeAdjecencyMatrix(numOfVerticles, matrix);
            if(degreeOfVertices == null)
            {
                Console.WriteLine("Buoi2.Bai1() Invalid output data!");
                return;
            }
            // Ghi output ra file 
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(numOfVerticles);
                for (int i = 0; i < numOfVerticles; i++)
                {
                    sw.Write($"{degreeOfVertices[i]} ");
                }
            }
        }
        static void Bai2()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi2\\BacVaoRa.inp";
            string outFilePath = "..\\..\\Assets\\Buoi2\\BacVaoRa.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi2.Bai2() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVertices = Helper.NumOfVerticles;
            // Biến handle status output 
            bool isInvalidOutput = false; 
            try
            {
                // Khai báo mảng bậc ra (1 chiều, size n) 
                int[] outDegrees = new int[numOfVertices];
                // Khai báo mảng bậc vào (1 chiều, size n)
                int[] inDegrees = new int[numOfVertices];
                // Loop lồng i, j (row x col) 
                for (int i = 0; i < numOfVertices; i++)
                {
                    for (int j = 0; j < numOfVertices; j++)
                    {
                        // Nếu phần tử đang xét = 1 
                        if (matrix[i, j] == 1)
                        {
                            // Bậc ra [i] ++ 
                            outDegrees[i] += 1;
                            // Bậc vào [j] ++ 
                            inDegrees[j] += 1;
                        }
                    }
                }
                // Xuất output bài tập 
                using (StreamWriter sw = new StreamWriter(outFilePath))
                {
                    sw.WriteLine(numOfVertices);
                    for (int i = 0; i < numOfVertices; i++)
                    {
                        sw.Write($"{inDegrees[i]} {outDegrees[i]}\n");
                    }
                }
            }
            catch
            {
                isInvalidOutput = true;
            }
            if(isInvalidOutput)
            {
                Console.WriteLine("Buoi2.Bai2() Invalid output data!");
            }
            
        }
        static void Bai3()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyList.inp";
            string outFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyList.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi2.Bai3() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int row = Helper.Row; 
            int col = Helper.Col;   
            int numOfVerticles = Helper.NumOfVerticles;
            // Tính bậc các đỉnh của đồ thị 
            List<int> degreeOfVertices = DegreeAdjecencyList(matrix);
            if (degreeOfVertices == null)
            {
                Console.WriteLine("Buoi2.Bai3() Invalid output data!");
                return;
            }
            // In kết quả bài làm 
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(numOfVerticles);
                foreach(int degreeVal in degreeOfVertices)
                {
                    sw.Write($"{degreeVal} ");
                }
                sw.WriteLine();
            }
        }
        static void Bai4()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi2\\EdgeList.inp";
            string outFilePath = "..\\..\\Assets\\Buoi2\\EdgeList.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi2.Bai4() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Tính bậc của các đỉnh của đồ thị danh sách kề
            Dictionary<int, int> edgeListDegrees = DegreeEdgeList(matrix);
            if(edgeListDegrees == null)
            {
                Console.WriteLine("Buoi2.Bai4() Invalid output data!"); 
                return;
            }
            // Sort kết quả tính bậc theo key của dictionary (thứ tự: tăng dần) 
            var sortedEdgeListDegrees = edgeListDegrees.OrderBy(x => x.Key);
            // Ghi kết quả bài 4 
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(numOfVerticles);
                foreach(var item in sortedEdgeListDegrees)
                {
                    sw.Write($"{item.Value} "); 
                }
            }
        }
        public static void Run()
        {
            Bai1();
            Bai2();
            Bai3();
            Bai4();
        }
        /// <summary>
        /// Tính bậc của các đỉnh của đồ thị vô hướng
        /// </summary>
        /// <param name="numOfVerticles"></param>
        /// <param name="matrix"></param>
        /// <returns>Mảng 1 chiều: bậc của các đỉnh của đồ thị vô hướng</returns>
        static int[] DegreeAdjecencyMatrix(int numOfVerticles, int[,] matrix)
        {
            if(matrix == null || numOfVerticles == 0)
            {
                Console.WriteLine("DegreeAdjecencyMatrix Invalid params!");
                return null; 
            }
            int[] result = new int[numOfVerticles];
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1); 
            for (int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    result[i] += matrix[i, j];  
                }
            }

            return result;
        }
        /// <summary>
        /// Tính bậc của các đỉnh của đồ thị danh sách kề
        /// </summary>
        /// <param name="numOfVerticles"></param>
        /// <param name="matrix"></param>
        /// <returns>List<int>: bậc của các đỉnh của đồ thị danh sách kề</returns>
        static List<int> DegreeAdjecencyList(int[,] matrix)
        {
            if (matrix == null)
            {
                Console.WriteLine("DegreeAdjecencyList Invalid params!");
                return null;
            }
            int row = matrix.GetLength(0); 
            int col = matrix.GetLength(1);  
            List<int> degreeOfVertices = new List<int>();
            for (int i = 0; i < row; i++)
            {
                List<int> degreeData = new List<int>();
                for (int j = 0; j < col; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        degreeData.Add(matrix[i, j]);
                    }
                }
                int degreeVal = degreeData.Count;
                degreeOfVertices.Add(degreeVal);
            }

            return degreeOfVertices;
        }
        /// <summary>
        /// Tính bậc của đồ thị danh sách cạnh 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Dictionary bậc của đồ thị danh sách cạnh (Edge list)</returns>
        static Dictionary<int, int> DegreeEdgeList(int[,] matrix) 
        {
            if(matrix == null)
            {
                Console.WriteLine("DegreeEdgeList Invalid params!");
                return null;
            }
            int row = matrix.GetLength(0); 
            int col = matrix.GetLength(1);
            Dictionary<int, int> edgeListDegree = new Dictionary<int, int>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int data = matrix[i, j];
                    if (data == 0)
                    {
                        continue;
                    }
                    /* Đỉnh chưa thêm vào ds đếm bậc:
                    Init data với key unique - value = 1 */
                    if (!edgeListDegree.ContainsKey(data))
                    {
                        edgeListDegree.Add(data, 1);
                        continue;
                    }
                    /* Đỉnh đã có trong ds đếm bậc: 
                     +1 lần đếm cho key_unique.value
                     */
                    edgeListDegree[data] += 1; 
                }
            }

            return edgeListDegree;
        }
    }
}

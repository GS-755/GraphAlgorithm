using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp1.Models;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi3
    {
        static void Bai1()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi3\\Canh2Ke.inp";
            string outFilePath = "..\\..\\Assets\\Buoi3\\Canh2Ke.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi3.Bai1() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            int numOfEdges = Helper.NumOfEdges;
            // Convert Danh sách cận sang Danh sách kề 
            List<int>[] adjacencyLst = Helper.ConvertEdgeListToAdjacency(matrix, numOfVerticles, numOfEdges);
            if(adjacencyLst == null)
            {
                Console.WriteLine("Buoi3.Bai1() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 1 
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                // Xuất số đỉnh 
                sw.WriteLine(numOfVerticles);
                int adjLstLength = adjacencyLst.Length;
                for (int i = 0; i < adjLstLength; i++)
                {
                    List<int> adjacencyLstItems = adjacencyLst[i];
                    if(adjacencyLstItems == null)
                    {
                        continue;
                    }
                    // Loop & Xuất danh sách kề 
                    int lstItemCount = adjacencyLstItems.Count;
                    for(int j = 0; j < lstItemCount; j++)
                    {
                        sw.Write($"{adjacencyLstItems[j]} ");
                    }
                    // Kết thúc xuống dòng nếu vòng lặp đã đi đến cuối (loại bỏ \n bị dư)
                    if(i != adjLstLength - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
        static void Bai2()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi3\\Ke2Canh.inp";
            string outFilePath = "..\\..\\Assets\\Buoi3\\Ke2Canh.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi3.Bai2() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Convert Danh sách kề sang Danh sách cạnh
            List<Edge> edgeLst = Helper.ConvertAdjacencyToEdgeList(matrix, numOfVerticles);
            if(edgeLst == null)
            {
                Console.WriteLine("Buoi3.Bai2() Invalid output data!");
                return; 
            }
            // Xuất kết quả bài 2 ra output
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                // In số đỉnh & số cạnh 
                int edgeLstCnt = edgeLst.Count;
                sw.WriteLine($"{numOfVerticles} {edgeLstCnt}");
                // In danh sách cạnh 
                foreach (Edge edge in edgeLst) 
                { 
                    sw.WriteLine(edge.ToString());
                }
            }
        }
        static void Bai3()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi3\\BonChua.inp";
            string outFilePath = "..\\..\\Assets\\Buoi3\\BonChua.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi3.Bai3() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Tìm bồn chứa của ma trận kề 
            int[] tank = FindSinks(matrix, numOfVerticles);  
            if(tank == null)
            {
                Console.WriteLine("Buoi3.Bai3() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 3 ra output
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(numOfVerticles);
            }
        }
        static void Bai4()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi3\\DSKe2Canh.inp";
            string outFilePath = "..\\..\\Assets\\Buoi3\\DSKe2Canh.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi3.Bai4() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Build đồ thị chuyển vị 
            List<int>[] transposedLst = BuildTransposedGraph(matrix, numOfVerticles);
            if(transposedLst == null)
            {
                Console.WriteLine("Buoi3.Bai4() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 4
            int size = transposedLst.Length;
            using(StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(numOfVerticles);
                for (int i = 1; i < size; i++)
                {
                    sw.WriteLine(string.Join(" ", transposedLst[i]));
                }
            }
        }
        static void Bai5()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi3\\TrungBinhCanh.inp";
            string outFilePath = "..\\..\\Assets\\Buoi3\\TrungBinhCanh.out";
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi3.Bai5() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            int numOfEdges = Helper.NumOfEdges;
            int rowSize = Helper.Row;
            /* Định nghĩa các biến cần sử dụng */
            // Giá trị max size của các cạnh trong đồ thị 
            int maxSize = 0;
            // Độ dài trung bình các cạnh trong đồ thị 
            double avgSize = 0;
            // Danh sách cạnh đã build 
            List<Edge> edgeLst = null;
            // Danh sách cạnh dài nhất của đồ thị 
            List<Edge> longestEdges = null;
            try
            {
                edgeLst = new List<Edge>();
                // Loop & build danh sách cạnh 
                for (int i = 0; i < numOfEdges; i++)
                {
                    List<int> matrixRowData = Helper.GetMatrixRow(i);
                    if (matrixRowData == null)
                    {
                        continue;
                    }
                    if (matrixRowData.Count == 0)
                    {
                        continue;
                    }
                    int startVertice = matrixRowData[0];
                    int endVertice = matrixRowData[1];
                    int edgeSize = matrixRowData[2];
                    Edge builtEdge = Helper.BuildEdge(startVertice, endVertice, edgeSize);
                    if (builtEdge == null)
                    {
                        Console.WriteLine("Buoi3.Bai5() Build Edge object failed!");
                        continue;
                    }
                    edgeLst.Add(builtEdge);
                }
                /* Tính toán các dữ liệu cần output */
                // Tìm độ dài max của các cạnh trong đồ thị
                maxSize = edgeLst.Max(x => x.Size);
                // Tìm các cạnh có độ dài lớn nhất 
                longestEdges = edgeLst.Where(x => x != null && x.Size == maxSize).ToList();
                // Tính độ dài trung bình các cạnh của đồ thị 
                avgSize = edgeLst.Average(x => x.Size);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Buoi3.Bai5() unhandled exception: ");
                Console.WriteLine(ex);
                // Reset edgeLst về null
                edgeLst = null;

                return;
            }
            if (edgeLst == null)
            {
                Console.WriteLine("Buoi3.Bai5() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 5
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine($"{avgSize:0.00}");
                sw.WriteLine(longestEdges.Count);
                foreach (Edge item in longestEdges)
                {
                    sw.WriteLine(item);
                }
            }
        }
        public static void Run()
        {
            Bai1();
            Bai2();
            Bai3();
        }
        /// <summary>
        /// Tìm bồn chứa từ ma trận kề 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfVertice"></param>
        /// <returns>Mảng 1 chiều: Bồn chứa</returns>
        static int[] FindSinks(int[,] matrix, int numOfVertice)
        {
            if(matrix == null)
            {
                return null; 
            }
            if(matrix.Length == 0)
            {
                return null;
            }
            if (numOfVertice == 0) 
            {
                return null; 
            }
            int rowSize = matrix.GetLength(0);
            int colSize = matrix.GetLength(1);
            int[] arrSink = new int[numOfVertice];
            try
            {
                for (int i = 0; i < rowSize; i++)
                {
                    bool hasOutEdge = false;
                    for (int j = 0; j < colSize; j++)
                    {
                        if (matrix[i, j] == 1)
                        {
                            hasOutEdge = true;
                            break;
                        }
                    }
                    if (hasOutEdge == false)
                    {
                        arrSink[i++] = i + 1;
                    }
                }

                return arrSink;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Buoi3.FindSinks() unhandled exception: ");
                Console.WriteLine(ex);

                return null;
            }
        }
        /// <summary>
        /// Xây dựng Đồ thị chuyển vị (GT) từ Đồ thị có hướng (G)
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfVertice"></param>
        /// <returns>Đồ thị chuyển vị (GT) (Transposed graph)</returns>
        static List<int>[] BuildTransposedGraph(int[,] matrix, int numOfVertice)
        {
            if (matrix == null || matrix.Length == 0)
            {
                return null;
            }
            if (numOfVertice == 0)
            {
                return null;
            }
            // Khởi tạo danh sách chứa các cung 
            List<int>[] transposedLst = new List<int>[numOfVertice + 1];
            for(int i = 1; i <= numOfVertice; i++)
            {
                // Init từng danh sách một
                transposedLst[i] = new List<int>();
            }
            try
            {
                for (int u = 1; u <= numOfVertice; u++)
                {
                    // Get dòng dữ liệu trong ma trận
                    List<int> matrixRowData = Helper.GetMatrixRow(u - 1);
                    if (matrixRowData == null)
                    {
                        continue;
                    }
                    foreach (int v in matrixRowData)
                    {
                        // Bỏ qua đỉnh cô lập 
                        if(v == 0)
                        {
                            continue;
                        }
                        // Đảo chiều cạnh 
                        transposedLst[v].Add(u);
                    }
                }
                // Sắp xếp các danh sách kề theo thứ tự tăng dần
                for (int i = 1; i < numOfVertice; i++)
                {
                    transposedLst[i].Sort();
                }

                return transposedLst;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Buoi3.BuildTransposedGraph() unhandled exception: ");
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

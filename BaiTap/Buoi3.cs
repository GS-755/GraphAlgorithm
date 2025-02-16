using System;
using System.IO;
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
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}

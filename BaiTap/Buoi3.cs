using System;
using System.IO;
using System.Collections.Generic;

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
            // Xuất kết quả bài 1 
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(numOfVerticles);
                int adjLstLength = adjacencyLst.Length;
                for (int i = 0; i < adjLstLength; i++)
                {
                    List<int> adjacencyLstItems = adjacencyLst[i];
                    if(adjacencyLstItems == null)
                    {
                        continue;
                    }
                    int lstItemCount = adjacencyLstItems.Count;
                    for(int j = 0; j < lstItemCount; j++)
                    {
                        sw.Write($"{adjacencyLstItems[j]} ");
                    }
                    if(i != adjLstLength - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
        public static void Run()
        {
            Bai1();
        }
    }
}

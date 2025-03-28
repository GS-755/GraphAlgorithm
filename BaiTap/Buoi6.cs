using System.Collections.Generic;
using System.IO;
using System;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi6
    {
        static void Bai1()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi6\\DFS.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi6\\DFS.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc điểm bắt đầu duyệt BFS 
            int startVertice = Helper.GetParamsValue(0, 1);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi6.Bai1() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Lấy kết quả duyệt BFS của đỉnh start được chỉ định trong input
            List<int> dfsResult = Helper.DFS(matrix, numOfVerticles, startVertice);
            if (dfsResult == null)
            {
                Console.WriteLine("Buoi6.Bai1() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 1 ra output
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(dfsResult.Count);
                foreach (int item in dfsResult)
                {
                    sw.Write($"{item} ");
                }
            }
        }
        public static void Run()
        {
            Bai1();
        }
    }
}

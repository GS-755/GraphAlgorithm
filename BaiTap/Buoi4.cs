using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi4
    {
        static void Bai1()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = "..\\..\\Assets\\Buoi4\\BFS.inp";
            string outFilePath = "..\\..\\Assets\\Buoi4\\BFS.out";
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
                Console.WriteLine("Buoi4.Bai1() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Lấy kết quả duyệt BFS của đỉnh start được chỉ định trong input
            List<int> bfsResult = Helper.BFS(matrix, numOfVerticles, startVertice);
            if(bfsResult == null)
            {
                Console.WriteLine("Buoi4.Bai1() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 1 ra output
            using(StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(bfsResult.Count); 
                foreach(int item in bfsResult)
                {
                    sw.Write($"{item} "); 
                }
            }
        }
        static void Bai2()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = "..\\..\\Assets\\Buoi4\\TimDuong.inp";
            string outFilePath = "..\\..\\Assets\\Buoi4\\TimDuong.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc điểm bắt đầu duyệt BFS 
            int startVertice = Helper.GetParamsValue(0, 1);
            int endVertice = Helper.GetParamsValue(0, 2);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi4.Bai2() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Lấy kết quả duyệt BFS của đỉnh start được chỉ định trong input
            List<int> findPathResult = Helper.BFS(matrix, numOfVerticles, startVertice, endVertice);
            if (findPathResult == null)
            {
                Console.WriteLine("Buoi4.Bai2() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 2 ra output
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(findPathResult.Count);
                foreach (int item in findPathResult)
                {
                    sw.Write($"{item} ");
                }
            }
        }
        public static void Run()
        {
            Bai1();
            Bai2();
        }
    }
}

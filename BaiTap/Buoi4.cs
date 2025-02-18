using System;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi4
    {
        static void Bai1()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi4\\BFS.inp";
            string outFilePath = "..\\..\\Assets\\Buoi4\\BFS.out";
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

        }
        public static void Run()
        {
            Bai1();
        }
    }
}

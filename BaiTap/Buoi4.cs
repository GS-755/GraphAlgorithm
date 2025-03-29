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
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\BFS.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\BFS.out";
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
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\TimDuong.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\TimDuong.out";
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
        static void Bai3()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\LienThong.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\LienThong.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi4.Bai3() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Duyệt BFS từ startVertice = 1
            List<int> bfsResult = Helper.BFS(matrix, numOfVerticles, 1);
            if(bfsResult == null)
            {
                Console.WriteLine("Buoi4.Bai3() Invalid output data!");
                return;
            }
            /*
                Lấy số lượng phần tử trong list (đại diện cho số đỉnh đã duyệt) 
                Để check đồ thị có liên thông hay không
            */
            int bfsResultCount = bfsResult.Count;
            // In ra YES: nếu đồ thị liên thông | NO: ngược lại 
            string finalResult = string.Empty;
            if(bfsResultCount == numOfVerticles)
            {
                finalResult = "YES"; 
            }
            else
            {
                finalResult = "NO"; 
            }
            // Xuất kết quả bài 3
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(finalResult);
            }
        }
        static void Bai4()
        {
            /* 
             * Khởi tạo danh sách các đỉnh để bắt đầu tìm miền liên thông 
             * (Gợi ý: 1, 3, 7)
             */
            List<int> lstStartVertice = new List<int> { 1, 3, 7 };
            // Khởi tạo đường dẫn input/output
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\DemLienThong.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi4\\DemLienThong.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi4.Bai4() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Khởi tạo biến lưu trữ trạng thái viếng thăm full đồ thị 
            bool hasVisitedAllVertices = false;
            // Loop lstStartVertice & đếm số miền liên thông 
            int countConnectGraph = 0;
            foreach (int item in lstStartVertice)
            {
                // Duyệt BFS đồ thị bằng đỉnh bắt đầu đang xét 
                List<int> bfsResult = Helper.BFS(matrix, numOfVerticles, item, 0, true);
                if (bfsResult == null)
                {
                    Console.WriteLine($"Buoi4.Bai4() BFS vertice = {item} failed!");
                    continue;
                }
                int bfsResultCount = bfsResult.Count; 
                if(bfsResultCount == 0)
                {
                    Console.WriteLine($"Buoi4.Bai4() BFS vertice = {item} no data!");
                    continue;
                }
                // Kiểm tra các đỉnh đã được viếng thăm toàn bộ hay chưa
                if(Helper.IsAllVerticesVisited())
                {
                    // Nếu các đỉnh đã được viếng thăm: DỪNG thuật toán.
                    if (hasVisitedAllVertices)
                    {
                        break;
                    }
                    // Cập nhật trạng thái: đã viếng thăm toàn bộ các đỉnh
                    hasVisitedAllVertices = true;
                }
                // Tăng số đếm miền liên thông
                countConnectGraph++;
            }
            // Xuất kết quả bài 4
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                sw.WriteLine(countConnectGraph);
            }
        }
        public static void Run()
        {
            Bai1();
            Bai2();
            Bai3();
            Bai4();
        }
    }
}

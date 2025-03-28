using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi5
    {
        static void Bai1()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi5\\MienLienThongBFS.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi5\\MienLienThongBFS.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi5.Bai1() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            /*
             * Duyệt BFS các đỉnh nguồn KHÔNG xác định 
             * Từ 1 -> numOfVerticles
             */
            List<List<int>> lstConnectedGraphs = scanConnectedGraphs(matrix, numOfVerticles);
            if(lstConnectedGraphs == null)
            {
                Console.WriteLine("Buoi5.Bai1() Invalid output data!");
                return;
            }
            // Xuất kết quả bài 1
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                int lstGraphsCount = lstConnectedGraphs.Count;
                // In ra số miền liên thông
                sw.WriteLine(lstGraphsCount);
                if(lstGraphsCount == 0)
                {
                    return;
                }
                // In ra các miền liên thông của đồ thị 
                foreach(List<int> lstItem in lstConnectedGraphs)
                {
                    foreach(int item in lstItem)
                    {
                        sw.Write($"{item} "); 
                    }
                    sw.WriteLine();
                }
            }
        }
        /// <summary>
        /// Duyệt BFS các đỉnh để tìm các miền liên thông 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numOfVerticles"></param>
        /// <returns>Danh sách các miền liên thông trong đồ thị</returns>
        static List<List<int>> scanConnectedGraphs(int[,] matrix, int numOfVerticles)
        {
            if (matrix == null || numOfVerticles <= 0)
            {
                Console.WriteLine("Buoi5.scanConnectedGraphs() Invalid params!");
                return null;
            }
            // Khởi tạo danh sách lưu lại miền liên thông 
            List<List<int>> lstConnectedGraph = null;
            // Khởi tạo biến lưu trữ trạng thái viếng thăm full đồ thị 
            bool hasVisitedAllVertices = false;
            try
            {
                /* Duyệt BFS từng đỉnh 
                 * (Điều kiện: đỉnh đó chưa được viếng thăm) 
                 * và lưu lại miền liên thông 
                 */
                lstConnectedGraph = new List<List<int>>();
                // Duyệt các đỉnh từ 1 -> numOfVerticles để thực hiện BFS 
                for (int i = 1; i <= numOfVerticles; i++)
                {
                    // Trường hợp đỉnh này KHÔNG phải đỉnh được duyệt BFS lần đầu tiên
                    if (i != 1)
                    {
                        // Kiểm tra các đỉnh đã được viếng thăm toàn bộ hay chưa
                        if (Helper.IsAllVerticesVisited())
                        {
                            // Nếu các đỉnh đã được viếng thăm: DỪNG thuật toán.
                            if (hasVisitedAllVertices)
                            {
                                break;
                            }
                            // Cập nhật trạng thái: đã viếng thăm toàn bộ các đỉnh
                            hasVisitedAllVertices = true;
                        }
                        // Trường hợp đỉnh đã viếng thăm: bỏ qua 
                        if (Helper.IsVerticeVisited(i))
                        {
                            continue;
                        }
                    }
                    // Thực hiện duyệt BFS với đỉnh bắt đầu = đỉnh đang xét 
                    List<int> bfsResult = Helper.BFS(matrix, numOfVerticles, i, 0, true); 
                    if(bfsResult == null)
                    {
                        Console.WriteLine($"Buoi5.scanConnectedGraphs() vertice = {i} execute Helper.BFS() failed!");
                        continue;
                    }
                    if(bfsResult.Count == 0)
                    {
                        Console.WriteLine($"Buoi5.scanConnectedGraphs() vertice = {i} execute Helper.BFS() no data!");
                        continue;
                    }
                    lstConnectedGraph.Add(bfsResult);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Buoi5.scanConnectedGraphs() unhandled exception: ");
                Console.WriteLine(ex);

                return null;
            }

            return lstConnectedGraph;
        }
        public static void Run()
        {
            Bai1();
        }
    }
}

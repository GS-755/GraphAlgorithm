using System;
using System.IO;
using System.Collections.Generic;
using ConsoleApp1.Models.Utils;

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
        static void Bai2()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi5\\CanhCau.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi5\\CanhCau.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi5.Bai2() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            int verticeToRemove = Helper.GetParamsValue(0, 1);
            int nextVertice = Helper.GetParamsValue(0, 2);
            /*
             * Bước 1: Duyệt BFS các đỉnh nguồn KHÔNG xác định 
             * Từ 1 -> numOfVerticles
             * TRƯỚC KHI GỠ CẠNH THEO YÊU CẦU ĐỀ BÀI
             */
            List<List<int>> step1Result = scanConnectedGraphs(matrix, numOfVerticles);
            if (step1Result == null)
            {
                Console.WriteLine("Buoi5.Bai2() - Step 1 - Invalid output data!");
                return;
            }
            OverrideMatrixParams paramObj = new OverrideMatrixParams();
            paramObj.RowIndex = verticeToRemove;
            paramObj.SourceData = nextVertice;
            // Gỡ bỏ cạnh từ danh sách kề - theo yêu cầu đề bài 
            bool removeEdgeStatus = Helper.OverrideMatrixData(paramObj);
            if (removeEdgeStatus == false)
            {
                Console.WriteLine($"Buoi5.Bai2() remove Edge[{verticeToRemove} - {nextVertice}] failed!");
                return;
            }
            /*
             * Bước 2: Duyệt BFS các đỉnh nguồn KHÔNG xác định 
             * Từ 1 -> numOfVerticles
             * SAU KHI GỠ CẠNH THEO YÊU CẦU ĐỀ BÀI
             */
            // Patch lại dữ liệu ma trận đã chỉnh sửa vào biến cục bộ matrix[,]
            matrix = Helper.ArrayMatrix;
            List<List<int>> step2Result = scanConnectedGraphs(matrix, numOfVerticles);
            if (step2Result == null)
            {
                Console.WriteLine("Buoi5.Bai2() - Step 2 - Invalid output data!");
                return;
            }
            /* 
             * Bước 3: Lấy số miền liên thông đồ thị của Bước 1 và 2
             * Kiểm tra xem sau khi gỡ cạnh theo đề bài
             * thì có xuất hiện cạnh cầu không
             */
            int cntGraphStep1 = step1Result.Count;
            int cntGraphStep2 = step2Result.Count;
            string finalResult = (cntGraphStep2 > cntGraphStep1) ? "YES" : "NO";
            // Xuất kết quả bài 2
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                // In ra kết quả: Sau khi gỡ cạnh theo đề bài thì 
                // cạnh đó có phải cạnh cầu hay không?
                sw.WriteLine(finalResult); 
            }
        }
        static void Bai3()
        {
            // Khởi tạo đường dẫn input/output
            string inpFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi5\\CanhCau.inp";
            string outFilePath = Helper.RELATIVE_ASSET_PATH + "Assets\\Buoi5\\DinhKhop.out";
            // Đọc tham số của file input
            Helper.ParseParams(inpFilePath);
            // Set dữ liệu cho Helper
            Helper.NumOfVerticles = Helper.GetParamsValue(0, 0);
            // Đọc input ma trận
            bool handleInputStatus = Helper.ReadMatrix(inpFilePath);
            if (handleInputStatus == false)
            {
                Console.WriteLine("Buoi5.Bai2() Invalid input data!");
                return;
            }
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            int verticeToRemoveEdges = Helper.GetParamsValue(0, 1);
            /*
             * Bước 1: Duyệt BFS các đỉnh nguồn KHÔNG xác định 
             * Từ 1 -> numOfVerticles
             * TRƯỚC KHI GỠ CẠNH THEO YÊU CẦU ĐỀ BÀI
             */
            List<List<int>> step1Result = scanConnectedGraphs(matrix, numOfVerticles);
            if (step1Result == null)
            {
                Console.WriteLine("Buoi5.Bai3() - Step 1 - Invalid output data!");
                return;
            }
            /* Gỡ bỏ cạnh từ danh sách kề - theo yêu cầu đề bài */
            // Gỡ bỏ các đỉnh kề với verticeToRemoveEdges
            OverrideMatrixParams paramObj = new OverrideMatrixParams();
            paramObj.RowIndex = verticeToRemoveEdges;
            paramObj.RemoveAllLines = true; 
            bool removeMatrixLineStatus = Helper.OverrideMatrixData(paramObj);
            // Duyệt & Gỡ bỏ đỉnh verticeToRemoveEdges khỏi đỉnh kề có liên quan 
            for(int i = 0; i < numOfVerticles; i++)
            {
                OverrideMatrixParams rmAdjVerticeParam = new OverrideMatrixParams();
                rmAdjVerticeParam.RowIndex = i;
                rmAdjVerticeParam.SourceData = verticeToRemoveEdges;
                Helper.OverrideMatrixData(rmAdjVerticeParam);
            }
            if (removeMatrixLineStatus == false)
            {
                Console.WriteLine($"Buoi5.Bai3() remove Edges linked to [{verticeToRemoveEdges}] failed!");
                return;
            }
            /*
             * Bước 2: Duyệt BFS các đỉnh nguồn KHÔNG xác định 
             * Từ 1 -> numOfVerticles
             * SAU KHI GỠ CẠNH THEO YÊU CẦU ĐỀ BÀI
             */
            // Patch lại dữ liệu ma trận đã chỉnh sửa vào biến cục bộ matrix[,]
            matrix = Helper.ArrayMatrix;
            List<List<int>> step2Result = scanConnectedGraphs(matrix, numOfVerticles);
            if (step2Result == null)
            {
                Console.WriteLine("Buoi5.Bai2() - Step 2 - Invalid output data!");
                return;
            }
            /* 
             * Bước 3: Lấy số miền liên thông đồ thị của Bước 1 và 2
             * Kiểm tra xem sau khi gỡ cạnh theo đề bài
             * thì có xuất hiện đỉnh khớp không
             */
            int cntGraphStep1 = step1Result.Count;
            int cntGraphStep2 = step2Result.Count;
            string finalResult = (cntGraphStep2 - cntGraphStep1 >= 2) ? "YES" : "NO";
            // Xuất kết quả bài 2
            using (StreamWriter sw = new StreamWriter(outFilePath))
            {
                // In ra kết quả: Sau khi gỡ cạnh theo đề bài thì 
                // cạnh đó có phải cạnh cầu hay không?
                sw.WriteLine(finalResult);
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
            // Reset Dictionary các đỉnh đã viếng thăm 
            Helper.ClearVisitedVerticesDict();

            return lstConnectedGraph;
        }
        public static void Run()
        {
            Bai1();
            Bai2();
            Bai3();
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1.BaiTap
{
    public static class Buoi2
    {
        static void Bai1()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyMatrix.inp";
            string outFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyMatrix.out";
            Helper.ReadMatrix(inpFilePath);
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVerticles = Helper.NumOfVerticles;
            // Tính số bậc ma trận 
            int[] degreeOfVertices = DegreeOfVertices(numOfVerticles, matrix);
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
            Helper.ReadMatrix(inpFilePath);
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int numOfVertices = Helper.NumOfVerticles; 
            // Khai báo mảng bậc ra (1 chiều, size n) 
            int[] outDegrees = new int[numOfVertices];
            // Khai báo mảng bậc vào (1 chiều, size n)
            int[] inDegrees = new int[numOfVertices];
            // Loop lồng i, j (row x col) 
            for(int i = 0; i < numOfVertices; i++)
            {
                for(int j = 0; j < numOfVertices; j++)
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
                for(int i = 0; i < numOfVertices; i++)
                {
                    sw.Write($"{inDegrees[i]} {outDegrees[i]}\n"); 
                }
            }
        }
        static void Bai3()
        {
            // Đọc input ma trận 
            string inpFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyList.inp";
            string outFilePath = "..\\..\\Assets\\Buoi2\\AdjecencyList.out";
            Helper.ReadMatrix(inpFilePath);
            // Lấy dữ liệu từ Helper
            int[,] matrix = Helper.ArrayMatrix;
            int row = Helper.Row; 
            int col = Helper.Col;   
            int numOfVerticles = Helper.NumOfVerticles;
            List<int> degreeOfVertices = new List<int>();
            for (int i = 0; i < row; i++)
            {
                List<int> degreeData = new List<int>(); 
                for (int j = 0; j < col; j++)
                {
                    if(matrix[i, j] > 0)
                    {
                        degreeData.Add(matrix[i, j]);
                    }
                }
                int degreeVal = degreeData.Count;
                degreeOfVertices.Add(degreeVal);
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

        }
        public static void Run()
        {
            Bai1();
            Bai2();
            Bai3();
            //Bai4();
        }
        static int[] DegreeOfVertices(int numOfVerticles, int[,] matrix)
        {
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
    }
}

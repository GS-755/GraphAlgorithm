namespace ConsoleApp1.Models
{ 
    /// <summary>
    /// Class Edge biểu diễn đối tượng cạnh của đồ thị. Một cạnh gồm: Đỉnh bắt đầu, đỉnh kết thúc & độ dài của cạnh.
    /// </summary>
    public class Edge
    {
        public int StartVertice { get; set; }
        public int EndVertice { get; set; }
        public int Size { get; set; }

        /// <summary>
        /// Khởi tạo đối tượng Cạnh trong đồ thị KHÔNG CÓ tham số độ dài
        /// </summary>
        /// <param name="startVertice"></param>
        /// <param name="endVertice"></param>
        public Edge(int startVertice, int endVertice)
        {
            this.StartVertice = startVertice;
            this.EndVertice = endVertice;
            this.Size = 0;
        }
        /// <summary>
        /// Khởi tạo đối tượng Cạnh trong đồ thị có tham số độ dài
        /// </summary>
        /// <param name="startVertice"></param>
        /// <param name="endVertice"></param>
        /// <param name="size"></param>
        public Edge(int startVertice, int endVertice, int size) : this(startVertice, endVertice)
        {
            this.Size = size;
        }

        /// <summary>
        /// Output Đỉnh bắt đầu, Đỉnh kết thúc và Độ dài của cạnh
        /// </summary>
        /// <returns>String theo format: StartVertice EndVertice </returns>
        public override string ToString()
        {
            string displayText = $"{this.StartVertice} {this.EndVertice}";
            if (this.Size > 0)
            {
                displayText += $" {this.Size}";
            }

            return displayText;
        }
        /// <summary>
        /// Kiểm tra hai đối tượng cạnh có bằng nhau không
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Bool: Hai cạnh bằng nhau hay không.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false; 
            }
            if(!obj.GetType().Equals(typeof(Edge)))
            {
                return false;
            }
            Edge compareEdge = obj as Edge;
            bool isEqualStartVertice = (this.StartVertice == compareEdge.StartVertice);
            bool isEqualEndVertice = (this.EndVertice == compareEdge.EndVertice);
            bool isEqualSize = (this.Size == compareEdge.Size);

            return isEqualStartVertice && isEqualEndVertice && isEqualSize; 
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

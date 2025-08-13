namespace ProyectoPROGEND.Models
{
    public class PaginatedResult<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}
namespace ApiWebFilme.Model
{
    public class Filmes
    {
        static int nextId;
        public int id { get; private set; }
        Filmes()
        {
            id = Interlocked.Increment(ref nextId);
        }
        public int year { get; set; }
        public required string title { get; set; }
        public required string studios { get; set; }
        public required string producers { get; set; }
        public string? winner { get; set; }
    }
}
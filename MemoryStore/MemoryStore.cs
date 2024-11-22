namespace MemoryStore
{
    public static class MemoryStore
    {
        public static Dictionary<string, decimal> Accounts { get; } =
        new Dictionary<string, decimal>
        {
            { "User1", 1000.00m },
            { "User2", 500.00m }
        };
    }
}

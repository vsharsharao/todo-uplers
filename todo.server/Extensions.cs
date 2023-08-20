namespace todo.server
{
    public static class Extensions
    {
        public static bool IsLocalhost(this IWebHostEnvironment webHostEnvironment)
        {
            return webHostEnvironment.IsEnvironment("Localhost");
        }
    }
}

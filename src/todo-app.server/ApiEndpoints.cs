namespace TodoApp.Server;

public class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Todos
    {
        private const string Base = $"{ApiBase}/todos";
        public const string Create = Base;
        public const string GetById = $"{Base}/{{id:long}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:long}}";
        public const string Delete = $"{Base}/{{id:long}}";
        public const string Tag = "Todos";
    }
}
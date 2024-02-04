namespace Trucks.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Trucks
    {
        private const string Base = $"{ApiBase}/trucks";

        public const string Create = Base;
        public const string Get = $"{Base}/{{code}}";
        public const string GetALl = Base;
        public const string Update = $"{Base}/{{code}}";
        public const string Delete = $"{Base}/{{code}}";
    }
}

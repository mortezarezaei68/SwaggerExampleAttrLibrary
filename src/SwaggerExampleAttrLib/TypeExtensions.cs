using Microsoft.OpenApi.Any;

namespace SwaggerExampleAttrLib
{
    public static class TypeExtensions
    {
        private static readonly HashSet<Type> DoubleOrDecimalTypes = new()
        {
            typeof(double),
            typeof(decimal),
        };

        private static readonly HashSet<Type> IntTypes = new()
        {
            typeof(Int32),
            typeof(Int64),
            typeof(int),
        };

        public static bool IsList(this Type type)
        {
            return  type is {IsGenericType: true} && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public static string CheckingTypes(this Type type)
        {
            if (DoubleOrDecimalTypes.Contains(type))
            {
                return nameof(OpenApiDouble);
            }

            if (IntTypes.Contains(type))
            {
                return nameof(OpenApiInteger);
            }
            return type.Name;
        }
        

    }
}
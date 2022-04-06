using System.Collections;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerExampleAttrLib
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            try
            {
                if (context.MemberInfo == null) return;
                var example = context.MemberInfo.GetCustomAttributes<ExampleAttribute>().FirstOrDefault();
                if (example is null) return;
                if (context.Type.IsList())
                {
                    schema.Example = CreateListExample(example, context.Type);
                }
                else
                {
                    var openApiClasses = (IOpenApiAny) GetAllEntities(context.Type.CheckingTypes(), example)!;
                    schema.Example = openApiClasses;
                }
            }
            catch (Exception e)
            {
                if (context.MemberInfo != null)
                    Console.WriteLine(context.MemberInfo.Name);
                throw;
            }
        }

        private IOpenApiAny CreateListExample(ExampleAttribute? example, Type contextType)
        {
            var itemType = contextType.GetGenericArguments()[0];
            var array = new OpenApiArray();
            if (itemType.IsClass)
            {
                throw new Exception("class not supported in this version");
                // var collection = ((IEnumerable) example).Cast<object>()
                //     .Select(a => a.ConvertToObj<type>()).Where(a=>a.status).ToList();
            }

            array.AddRange(from object? item in ((IEnumerable) example.Message)
                let type = item.GetType()
                let data = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(x => typeof(IOpenApiAny).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .FirstOrDefault(x => x.Name.Contains(type.CheckingTypes()))
                select (IOpenApiAny) GetInstance(data, item));

            return array;
        }


        private object? GetAllEntities(string type, ExampleAttribute? exampleAttribute)
        {
            var data = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IOpenApiAny).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .FirstOrDefault(x => x.Name.Contains(type));
            return GetInstance(data, exampleAttribute?.Message);
        }



        private object? GetInstance(Type type, object value)
        {
            return Activator.CreateInstance(type, value);
        }
    }
}
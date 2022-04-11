namespace SwaggerExampleAttrLib
{
    public static class EnumerableExtensions
    {
        public static T? ConvertToObj<T>(this object? input) where T : new()
        {
            var result = new T();

            if (input == null)
                return default(T);

            var allProps = input.GetType().GetProperties();
            var allResultObjProps = result.GetType().GetProperties();
            foreach (var prop in allProps)
            {
                var foundTargetProp = allResultObjProps.FirstOrDefault(t => t.Name == prop.Name);
                if (foundTargetProp == null) continue;
                var valueTarget = prop.GetValue(input);
                if (valueTarget == null) continue;
                if (foundTargetProp.GetValue(result) != null &&
                    foundTargetProp.GetValue(result)?.GetType() == typeof(DateTime))
                {
                    //foundTargetProp.SetValue(result, ValueTarget.ToEnDate());
                }
                else
                {
                    foundTargetProp.SetValue(result, valueTarget);
                }
            }

            return result;
        }
    }
}
namespace GridStatusHub.Domain.Utilities
{
    public class ConvertPropertyService 
    {

        public static TTo CopyPropertiesFrom<TFrom, TTo>(TFrom from) where TTo : new()
        {
            var to = new TTo();
            var fromProperties = typeof(TFrom).GetProperties();
            var toProperties = typeof(TTo).GetProperties();

            foreach (var fromProp in fromProperties)
            {
                var toProp = toProperties.FirstOrDefault(prop => prop.Name == fromProp.Name);
                if (toProp != null && toProp.CanWrite)
                    toProp.SetValue(to, fromProp.GetValue(from));
            }

            return to;
        }
    }
}

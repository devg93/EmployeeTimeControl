
using System.Reflection;

namespace Shared.Services.ModuleCommunication;
public class ObjectMapper
{
    public static T MapObject<T>(object resultTime) where T : new()
    {
        if (resultTime == null) throw new ArgumentNullException(nameof(resultTime));

        T mappedObject = new T();
        Type sourceType = resultTime.GetType();
        Type targetType = typeof(T);

        PropertyInfo[] sourceProperties = sourceType.GetProperties();
        PropertyInfo[] targetProperties = targetType.GetProperties();

        foreach (var sourceProp in sourceProperties)
        {
            var targetProp = Array.Find(targetProperties, p => p.Name == sourceProp.Name);
            if (targetProp != null && targetProp.CanWrite)
            {
                object value = sourceProp.GetValue(resultTime) ?? "null";
                targetProp.SetValue(mappedObject, value);
            }
        }

        return mappedObject;
    }
}
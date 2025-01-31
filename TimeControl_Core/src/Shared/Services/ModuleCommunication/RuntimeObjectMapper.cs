
using System.Reflection;
using Shared.Dto;

namespace Shared.Services.ModuleCommunication;
public class RuntimeObjectMapper
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
//**************************************Generic Nested Type Mapping **********************************************************************//
    public static TContainer MapObjectGeneric<TContainer, T>(object resultTime)
               where TContainer : new()
               where T : new()
    {
        if (resultTime == null) throw new ArgumentNullException(nameof(resultTime));


        TContainer mappedResponse = new TContainer();

        PropertyInfo[] containerProperties = typeof(TContainer).GetProperties();
        PropertyInfo? dataProperty = Array.Find(containerProperties, p => p.Name == "Data");

        if (dataProperty != null && dataProperty.CanWrite)
        {
            object mappedData = new T();
            CopyProperties(resultTime, mappedData);
            dataProperty.SetValue(mappedResponse, mappedData);
        }

        PropertyInfo? successProperty = Array.Find(containerProperties, p => p.Name == "IsSuccess");
        PropertyInfo? messageProperty = Array.Find(containerProperties, p => p.Name == "Message");

        successProperty?.SetValue(mappedResponse, true);
        messageProperty?.SetValue(mappedResponse, "Mapping successful");

        return mappedResponse;
    }

    private static void CopyProperties(object source, object destination)
    {
        PropertyInfo[] sourceProperties = source.GetType().GetProperties();
        PropertyInfo[] targetProperties = destination.GetType().GetProperties();

        foreach (var sourceProp in sourceProperties)
        {
            var targetProp = Array.Find(targetProperties, p => p.Name == sourceProp.Name);
            if (targetProp != null && targetProp.CanWrite)
            {
                object? value = sourceProp.GetValue(source);

                if (value == null)
                {
                    if (Nullable.GetUnderlyingType(targetProp.PropertyType) != null)
                    {
                        value = null;
                    }
                    else if (targetProp.PropertyType.IsValueType)
                    {
                        value = Activator.CreateInstance(targetProp.PropertyType) ?? throw new InvalidOperationException($"Cannot create an instance of {targetProp.PropertyType}");
                    }
                }

                try
                {
                    if (value != null && targetProp.PropertyType.IsAssignableFrom(value.GetType()))
                    {
                        targetProp.SetValue(destination, value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error mapping property '{sourceProp.Name}': {ex.Message}");
                }
            }
        }
    }

}



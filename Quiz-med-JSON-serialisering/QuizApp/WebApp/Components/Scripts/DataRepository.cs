using System.Text.Json;

namespace WebApp.Components.Scripts;

public sealed class DataFactory<T>
{
    public T? Data { get; }

    public static T? LoadData(string filePath, ILogger logger)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            {
                var data = JsonSerializer.Deserialize<T>(reader.BaseStream);

                if (data != null)
                {
                    logger.LogInformation("DATA LOAD!");
                    return data;
                }
                else
                {
                    logger.LogWarning("FAILED");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR");
        }
        return default;
    }
}
// See https://aka.ms/new-console-template for more information
using System.Text.Json;

public class OpenLoopsRepository
{
    private const string DirectoryName = "./openLoops/";

    public bool Add(OpenLoop newOpenLoop)
    {
        Directory.CreateDirectory(DirectoryName);

        var json = JsonSerializer.Serialize(newOpenLoop, new JsonSerializerOptions { WriteIndented = true});

        var fileName = $"{Guid.NewGuid()}.json";
        var filePath = Path.Combine(DirectoryName, fileName.ToString());
        File.WriteAllText(filePath, json);

        return true;
    }

    public OpenLoop[] Get()
    {
        var files = Directory.GetFiles(DirectoryName);

        var opneLoops = new List<OpenLoop>();

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var openLoop = JsonSerializer.Deserialize<OpenLoop>(json);
            if(openLoop == null)
            {
                throw new Exception("OpenLoop cannot be deserialized.");
            }

            opneLoops.Add(openLoop);
        }

        return opneLoops.ToArray();
    }
}
using System.Reflection;

namespace DfE.SkillsCentral.Api.Application.DocumentFormatter.Resources;

public static class ResourceHelper
{
    public static byte[] GetResource(string resourceName)
    {
        var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream($"{Assembly.GetExecutingAssembly().GetName().Name}.Resources.{resourceName}");

        if (stream == null) return Array.Empty<byte>();

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}
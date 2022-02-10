using System.Xml.Serialization;

namespace Twidlle.Library.Utility
{
    public static class XmlExtensions
    {
        public static T ReadFile<T>(string filePath, string? rootTag = null) where T : notnull
        {
            var root = new XmlRootAttribute(rootTag ?? GetDefaultRootTag<T>());
            var xr = new XmlSerializer(typeof(T), root);
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var obj = xr.Deserialize(fs);
            if (obj is null)
            {
                throw new InvalidOperationException(
                    $"Can't read instance of type {typeof(T).FullName} from file '{filePath}'.");
            }
            return (T)obj;
        }


        public static void WriteFile<T>(T source, string filePath, string? rootTag = null)
        {
            var root = new XmlRootAttribute(rootTag ?? GetDefaultRootTag<T>());
            var xr = new XmlSerializer(typeof(T), root);
            using var writer = new StreamWriter(filePath);
            xr.Serialize(writer, source);
        }


        private static string GetDefaultRootTag<T>()
        {
            return typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), inherit: true)
                       .Cast<XmlRootAttribute>()
                       .SingleOrDefault()
                       ?.ElementName
                   ?? typeof(T).Name;
        }
    }
}

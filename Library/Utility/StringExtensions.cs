using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Twidlle.Library.Utility;

/// <summary>
///  Методы-расширения для работы со строками
/// </summary>
public static class StringExtensions
{
    /// <summary> Заменяет пустую строку значением null. </summary>
    public static string? ToNullIfEmpty(this string source) =>
        source == string.Empty ? null : source;


    /// <summary> Заменяет пустую строку значением null. </summary>
    public static string? ToNullIfEmptyOrWhiteSpace(this string source) =>
        string.IsNullOrWhiteSpace(source) ? null : source;


    /// <summary> Эквивалентен отрицанию string.IsNullOrEmpty. </summary>
    /// <remarks> Позволяет использовать как lambda method group. </remarks>
    public static bool IsNeitherNullNorEmpty(string source) => 
        !string.IsNullOrEmpty(source);


    /// <summary> Возвращает true, если строка пуста или совпадает с заданной строкой. </summary>
    public static bool IsEmptyOrEqualTo(this string source, string s) =>
        source == string.Empty || source == s;


    /// <summary> Возвращает аргумент с префиксом. Если аргумент пуст или нул, возвращается null.</summary>
    public static string? AsPrefixFor(this string prefix, string? value) =>
        (value?.ToNullIfEmpty() is null) ? null : prefix + " " + value;

    /// <summary> Объединяет много-строчный текст в одну строку. </summary>
    public static string JoinLines(this string text, string separator = " ")
    {
        ThrowIfNull(text);

        return string.Join(separator, text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()));
    }


    /// <summary> Читает перечисление из его строкового представления.  </summary>
    public static TEnum Parse<TEnum>(this string source) =>
        (TEnum)Enum.Parse(typeof(TEnum), source);


    /// <summary> Читает момент времени на основе InvariantCulture.  </summary>
    public static DateTime ParseDateTime(this string source) =>
        DateTime.Parse(source, CultureInfo.InvariantCulture);


    /// <summary> Пытается прочитать целое число. Возвращает нул, если не сумел прочитать число по любым причинам. </summary>
    public static int? TryParseInt(this string source) =>
        int.TryParse(source, out var n) ? n : new int?();


    /// <summary> Пытается прочитать дату и время. Возвращает нул, если не сумел прочитать дату и время по любым причинам. </summary>
    public static DateTime? TryParseDateTime(this string source) =>
        DateTime.TryParse(source, out var d) ? d : new DateTime?();


    /// <summary> Вычисляет кэш MD5, затем преобразует его в Base64. </summary>
    public static string ToMd5Hash(this string source) =>
        Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Default.GetBytes(source)));


    /// <summary> Converts the string to its Base64 representation. </summary>
    public static string Base64Encode(this string source) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(source));

    /// <summary> Преобразовывает строку, зашифрованную для передачи по URL-адресу, в расшифрованную строку. </summary>
    public static string UrlDecode(this string source) =>
        WebUtility.UrlDecode(source);


    /// <summary> Шифрует строку для передачи по URL-адресу. </summary>
    public static string UrlEncode(this string source) =>
        WebUtility.UrlEncode(source);


    /// <summary> Converts the string to its JSON string representation. </summary>
    public static string JavaScriptStringEncode(this string source) =>
        JsonSerializer.Serialize(source);


    /// <summary> Интерпретирует JSON представление как объект заданного типа. </summary>
    public static T JsonTo<T>(this string source) =>
        JsonSerializer.Deserialize<T>(source) ?? throw new InvalidOperationException($"Can't deserialize json to {typeof(T).FullName} type.");


    /// <summary> Форматирует json c отступами. </summary>
    public static string ToJsonIndented(this string json)
    {
        var doc = JsonDocument.Parse(json);
        using var stream = new MemoryStream();

        var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
        doc.WriteTo(writer);
        writer.Flush();

        stream.Position = 0;
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    public static T XmlTo<T>(this string xml, string? rootElementName = null, string? defaultNamespace = null)
    {
        if (string.IsNullOrEmpty(rootElementName))
            rootElementName = typeof(T).Name;

        var rootAttribute = new XmlRootAttribute(rootElementName);
        var serializer = new XmlSerializer(typeof(T), null, Array.Empty<Type>(), rootAttribute, defaultNamespace);
        using var reader = new StringReader(xml);
        var obj = serializer.Deserialize(reader);
        return (T)(obj ?? throw new InvalidOperationException($"Can't deserialize xml to {typeof(T).FullName} type."));
    }
}


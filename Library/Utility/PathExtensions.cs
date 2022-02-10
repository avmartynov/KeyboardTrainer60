using System.Reflection;

namespace Twidlle.Library.Utility;

public static class PathExtensions
{
    /// <summary> Вычисляет абсолютный путь из пути, относительного к расположению файла приложения. </summary>
    /// <param name="appRelativePath"> Относительный путь файла должен быть задан по отношению к каталогу, где расположен exe-файл теста. </param>
    public static string GetRootedPath(string appRelativePath) =>
        Path.IsPathRooted(appRelativePath) ? appRelativePath : Path.Combine(GetAppDirectory(), appRelativePath).CanonicalizePath();

    public static string GetRootedPath(string appRelativePath, string path2) =>
        Path.Combine(GetAppDirectory(), appRelativePath, path2).CanonicalizePath();

    public static string GetRootedPath(string appRelativePath, string path2, string path3) =>
        Path.Combine(GetAppDirectory(), appRelativePath, path2, path3).CanonicalizePath();

    public static string GetRootedPath(string appRelativePath, string path2, string path3, string path4) =>
        Path.Combine(GetAppDirectory(), appRelativePath, path2, path3, path4).CanonicalizePath();

    public static string GetRootedPath(string appRelativePath, params string[] paths) =>
        Path.Combine(new[] { GetAppDirectory(), appRelativePath }.Concat(paths).ToArray()).CanonicalizePath();

    /// <summary> Вычисляет абсолютный путь к каталогу, где расположен exe-файл. </summary>
    public static string GetAppDirectory() =>
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";

    /// <summary> Упрощает путь к файлу. </summary>
    public static string CanonicalizePath(this string filePath) =>
        new FileInfo(filePath).FullName;
}

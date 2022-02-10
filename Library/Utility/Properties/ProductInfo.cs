namespace Twidlle.Library.Properties;

public static class ProductInfo
{
    public const string Name      = "Twidlle Base Class Library";
    public const string Version   = "6.0.0.0";
    public const string Year      = "2022";
    public const string Copyright = "Copyright © " + Year + " " + CompanyInfo.Name;

#if DEBUG
    public const string BuildConfiguration = "Debug";
#else
    public const string BuildConfiguration = "Release";
#endif
}


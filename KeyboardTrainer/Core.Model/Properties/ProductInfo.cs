namespace Twidlle.KeyboardTrainer.Core.Model.Properties;

public static class ProductInfo
{
    public const string Name      = $"{CompanyInfo.Name} Keyboard Trainer";
    public const string Version   = "3.0.0.0";
    public const string Year      = "2022";
    public const string Copyright = "Copyright © " + Year + " " + CompanyInfo.Name;

#if DEBUG
    public const string BuildConfiguration = "Debug";
#else
    public const string BuildConfiguration = "Release";
#endif
}


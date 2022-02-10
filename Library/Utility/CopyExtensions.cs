using AutoMapper;

namespace Twidlle.Library.Utility;

/// <summary>
/// Процедура копирования одноимённых свойств объектов (обёртка реализации AutoMapper).
/// </summary>
public static class CopyExtensions
{
    /// <summary> Копирует в текущий объект одноимённые свойства объекта-источника.</summary>
    /// <remarks>Копирование поверхностное (shallow copy). </remarks>
    /// <typeparam name="TDestination"> Тип возвращаемого объекта. </typeparam>
    /// <typeparam name="TSource"> Тип объекта-источника. </typeparam>
    /// <param name="destination"> Объект-приёмник значений свойств.  </param>
    /// <param name="source"> Объект-источник значений свойств.  </param>
    public static TDestination CopyFrom<TDestination, TSource>(this TDestination destination, TSource source) => 
        Helper<TDestination, TSource>.Copy(destination, source);

    /// <summary> Создает копию объекта-источника </summary>
    /// <remarks>Копирование поверхностное (shallow copy). </remarks>
    /// <typeparam name="T"> Тип объекта-источника и объекта-результата </typeparam>
    /// <param name="source"> Объект-источник </param>
    public static T Clone<T>(this T source) where T : class, new() =>
        new T().CopyFrom(source ?? throw new ArgumentNullException(nameof(source)));

    private static class Helper<TDestination, TSource>
    {
        public static TDestination Copy(TDestination destination, TSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            _copyProcedure.Value(destination, source);
            return destination;
        }

        private static Action<TDestination, TSource> CreateCopyProcedure()
        {
            var mc = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();
            return (t, s) => mc.Map(s, t);
        }

        private static readonly Lazy<Action<TDestination, TSource>> _copyProcedure =
            new Lazy<Action<TDestination, TSource>>(CreateCopyProcedure, isThreadSafe: true);
    }
}


using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Twidlle.Library.Utility;

/// <summary>
/// Методы расширения для программирования в функциональном стиле.
/// </summary>
public static class MiscExtensions
{
    /// <summary> Прерывает цепочку вызовов методов, если предикат возвращает true. </summary>
    public static T? ToDefaultIf<T>(this T obj, Predicate<T> condition) =>
        condition(obj) ? default : obj;


    /// <summary> Прерывает цепочку вызовов методов, если условие выполняется. </summary>
    public static T? ToDefaultIf<T>(this T obj, bool condition) =>
        condition ? default : obj;


    /// <summary> Прерывает цепочку вызовов методов, если предикат возвращает false. </summary>
    public static T? ToDefaultIfNot<T>(this T obj, Predicate<T> condition) =>
        condition(obj) ? obj : default;


    /// <summary> Прерывает цепочку вызовов методов, если условие не выполняется. </summary>
    public static T? DefaultIfNot<T>(this T obj, bool condition) =>
        condition ? obj : default;


    /// <summary> Заменяет на null его значение по умолчанию (только для типов-значений). </summary>
    public static TInput? NullIfDefault<TInput>(this TInput obj) where TInput : struct =>
        obj.Equals(default(TInput)) ? new TInput?() : obj;


    /// <summary> Заменяет значение по умолчанию на конкретное значение(только для типов-значений). </summary>
    public static TInput DefaultAs<TInput>(this TInput obj, TInput value) where TInput : struct =>
        obj.Equals(default(TInput)) ? value : obj;


    /// <summary> Заменяет значение на значение допускающее нул (только для типов-значений). </summary>
    public static T? ToNullable<T>(this T v, bool specified) where T : struct =>
        specified ? v : default;


    /// <summary> Выполняет действие и возвращает аргумент этого действия. </summary>
    public static T Invoke<T>(this T source, Action<T> action)
    {
        action.Invoke(source);
        return source;
    }


    /// <summary> Выполняет функцию, передавая ей текущий объект в качестве параметра. </summary>
    public static TResult Invoke<T, TResult>(this T source, Func<T, TResult> func) =>
        func.Invoke(source);

    /// <summary> Выполняет функцию, передавая ей текущий объект в качестве параметра. </summary>
    public static TEventArgs InvokeEvent<TEventArgs>(this Action<TEventArgs> action, TEventArgs args) 
        where TEventArgs : EventArgs
    {
        ThrowIfNull(action);

        action.Invoke(args);
        return args;
    }
        


    /// <summary> Выполняет действие при выполнении условия и возвращает аргумент этого действия. </summary>
    public static T InvokeIf<T>(this T source, bool condition, Action<T> action)
    {
        if (condition)
            action.Invoke(source);
        return source;
    }


    /// <summary> Выполняет функцию только при выполнении условия condition,
    /// передавая функции текущий объект в качестве параметра. </summary>
    public static T InvokeIf<T>(this T obj, bool condition, Func<T, T> func) =>
        condition ? func.Invoke(obj) : obj;


    /// <summary> Выполняет функцию только при выполнении условия condition,
    /// передавая функции текущий объект в качестве параметра. </summary>
    public static T InvokeIf<T>(this T obj, bool? condition, Func<T, T> func) =>
        (condition ?? false) ? func.Invoke(obj) : obj;


    /// <summary> Выполняет функцию, только если предикат predicate вернёт true,
    /// передавая функции текущий объект в качестве параметра. </summary>
    public static T InvokeIf<T>(this T obj, Func<bool> predicate, Func<T, T> func) =>
        obj.InvokeIf(predicate(), func);


    /// <summary> Возвращает JSON представление объекта. </summary>
    public static string ToJson(this object obj) =>
        JsonSerializer.Serialize(obj);


    /// <summary> Возвращает JSON представление объекта c отступами (в удобном для чтения виде) </summary>
    public static string ToJsonIndented(this object obj) =>
        JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });


    /// <summary> Возвращает XML представление объекта. </summary>
    public static string ToXml<T>(this T obj, string nameSpace = "", bool omitXmlDeclaration = true) where T : class
    {
        var sb = new StringBuilder();
        var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = omitXmlDeclaration };
        var writer = new StringWriter(sb);
        using var xmlWriter = XmlWriter.Create(writer, settings);

        var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", nameSpace) });
        new XmlSerializer(typeof(T)).Serialize(xmlWriter, obj, namespaces);
        return sb.ToString();
    }



    /// <summary> Возвращает имя свойства объекта на основе заданного get-выражения. </summary>
    public static string PropertyName<T, TValue>(this Expression<Func<T, TValue>> propertyGetExpression) =>
        propertyGetExpression.GetPropertyInfo().Name;


    /// <summary> Возвращает PropertyInfo свойства объекта на основе заданного get-выражения. </summary>
    public static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> propertyGetExpression)
    {
        propertyGetExpression = propertyGetExpression ?? throw new ArgumentNullException(nameof(propertyGetExpression));

        if (propertyGetExpression.Body is MemberExpression memberExpression)
            return (PropertyInfo)memberExpression.Member;

        if (!(propertyGetExpression.Body is UnaryExpression unaryExpression))
            throw new InvalidOperationException("Invalid property expression.");

        if (!(unaryExpression.Operand is MemberExpression operandMemberExpression))
            throw new InvalidOperationException("Invalid property expression.");

        return (PropertyInfo)operandMemberExpression.Member;
    }


    /// <summary> Безопасно преобразует целое число в элемент перечисления </summary>
    /// <remark> Контролирует допустимость значений перечисления. </remark> 
    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value)) throw new ArgumentOutOfRangeException(nameof(value));

        return (TEnum)Enum.ToObject(typeof(TEnum), value);
    }


    /// <summary> Возвращает признак того, что текущий объект является одним из элементов списка </summary>
    public static bool IsOneOf<T>(this T source, IEnumerable<T> list) =>
        (list ?? throw new ArgumentNullException(nameof(list))).Contains(source ?? throw new ArgumentNullException(nameof(source)));


    /// <summary> Возвращает признак того, что текущий объект является одним из элементов списка </summary>
    public static bool IsOneOf<T>(this T source, params T[] list) =>
        source.IsOneOf((IEnumerable<T>)list);


    /// <summary> Возвращает Namespace класса, исполняющего данный метод.
    /// В качестве callerMethod надо передавать MethodBase.GetCurrentMethod() </summary>
    public static string GetDeclaringNamespace(this MethodBase callerMethod) =>
        (callerMethod ?? throw new ArgumentNullException(nameof(callerMethod))).DeclaringType?.Namespace ?? "";

    /// <summary> Возвращает FullName класса, исполняющего данный метод.
    /// В качестве callerMethod надо передавать MethodBase.GetCurrentMethod() </summary>
    public static string GetDeclaringClassName(this MethodBase callerMethod) =>
        (callerMethod ?? throw new ArgumentNullException(nameof(callerMethod))).DeclaringType?.FullName ?? "";


    /// <summary> Устанавливает новое значение свойства по его выражению. </summary>
    /// <typeparam name="TTarget"> Тип объекта - владельца свойства </typeparam>
    /// <typeparam name="TProperty"> Тип свойства </typeparam>
    /// <param name="target"> Объект - владелец свойства</param>
    /// <param name="getExpression"> Свойство в виде выражения </param>
    /// <param name="value"> Новое значение свойства </param>
    public static TTarget SetProperty<TTarget, TProperty>(this TTarget target, Expression<Func<TTarget, TProperty>> getExpression, TProperty? value)
    {
        getExpression.GetPropertyInfo().SetValue(target, value);
        return target;
    }

    /// <summary> Возвращает значение свойства по его выражению. </summary>
    /// <typeparam name="TTarget"> Тип объекта - владельца свойства </typeparam>
    /// <typeparam name="TProperty"> Тип свойства </typeparam>
    /// <param name="target"> Объект - владелец свойства</param>
    /// <param name="getExpression"> Свойство в виде выражения </param>
    public static TProperty? GetProperty<TTarget, TProperty>(this TTarget target, Expression<Func<TTarget, TProperty?>> getExpression)
        where TProperty : class
    {
        return (TProperty?) getExpression.GetPropertyInfo().GetValue(target);
    }

    /// <summary> Возвращает значение свойства по его выражению. </summary>
    /// <typeparam name="TTarget"> Тип объекта - владельца свойства </typeparam>
    /// <typeparam name="TProperty"> Тип свойства </typeparam>
    /// <param name="target"> Объект - владелец свойства</param>
    /// <param name="getExpression"> Свойство в виде выражения </param>
    public static TProperty GetPropertyValue<TTarget, TProperty>(this TTarget target, Expression<Func<TTarget, TProperty>> getExpression)
        where TProperty : notnull
    {
        return (TProperty) getExpression.GetPropertyInfo().GetValue(target)!;
    }


    /// <summary> Округляет число до заданного числа значащих цифр </summary>
    /// <param name="value">Исходное число</param>
    /// <param name="digitNumber"> Требуемое число значащих цифр</param>
    public static double RoundDigits(this double value, int digitNumber)
    {
        if (value == 0)
            return 0;

        var magnitude = Math.Pow(10, digitNumber - (int)Math.Ceiling(Math.Log10(Math.Abs(value))));
        return Math.Round(value * magnitude) / magnitude;
    }
}


using System.Linq.Expressions;
using Twidlle.Library.Utility;

namespace Twidlle.Library.WinForms;

/// <summary>
/// Методы привязки свойств модели данных и свойств управляющих элементов
/// </summary>
public static class BindingExtensions
{
    /// <summary> Привязывает свойство модели данных model, к свойству управляющего элемента. </summary>
    /// <typeparam name="TModel"> Тип модели данных </typeparam>
    /// <typeparam name="TControl"> Тип управляющего элемента </typeparam>
    /// <typeparam name="TValue"> Тип свойства модели и управляющего элемента </typeparam>
    /// <param name="model"> Данные модели </param>
    /// <param name="bindingSource"> </param> 
    /// <param name="modelProperty"> Выражение-свойство модели данных </param>
    /// <param name="control"> Управляющий элемент </param>
    /// <param name="controlProperty"> Выражение-свойство управляющего элемента </param>
    public static TModel Bind<TModel, TControl, TValue>(this TModel model,
                                                        BindingSource bindingSource,
                                                        Expression<Func<TModel, TValue>> modelProperty,
                                                        TControl control,
                                                        Expression<Func<TControl, TValue>> controlProperty)
        where TControl : IBindableComponent
    {
        bindingSource.Bind(modelProperty.PropertyName(), control, controlProperty.PropertyName());
        return model;
    }

    public static TModel Bind<TModel, TValue>(this TModel model, 
                                              BindingSource bindingSource, 
                                              Expression<Func<TModel, TValue>> modelProperty, 
                                              Label label)
    {
        bindingSource.Bind(modelProperty.PropertyName(), label, nameof(label.Text));
        return model;
    }

    public static void Bind<TModel>(this BindingSource bindingSource,
                                    TModel model, 
                                    params Tuple<Expression<Func<TModel, string>>, Label>[] items)
        where TModel : class, new()
    {
        bindingSource.DataSource = model.Clone();

        foreach (var item in items)
            bindingSource.Bind(item.Item1.PropertyName(), item.Item2, nameof(item.Item2.Text));
    }


    private static void Bind(this BindingSource bindingSource,
                             string modelProperty,
                             IBindableComponent control,
                             string controlProperty)
    {
        control.DataBindings.Add(controlProperty, bindingSource, modelProperty);
    }
}
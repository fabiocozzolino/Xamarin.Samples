using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core
{
    public static class BindingExtensions
    {
        public static BindingItem<TModel, TSourceProperty> Bind<TModel, TSourceProperty>(this IView<TModel> view, Expression<Func<TModel, TSourceProperty>> sourceProperty) where TModel : ViewModel
        {
            return new BindingItem<TModel, TSourceProperty>() { SourceProperty = sourceProperty, ViewModel = view.ViewModel };
        }

        public static ObservableBindingItem<TModel, TSourcePropertyItem> Bind<TModel, TSourcePropertyItem>(this IView<TModel> view, Expression<Func<TModel, ObservableCollection<TSourcePropertyItem>>> sourceProperty) where TModel : ViewModel
        {
            return new ObservableBindingItem<TModel, TSourcePropertyItem>() { SourceProperty = sourceProperty, ViewModel = view.ViewModel };
        }

        public static ConverterBindingItem<TModel, TSourceProperty, TNewSourceProperty> Convert<TModel, TSourceProperty, TNewSourceProperty>(this BindingItem<TModel, TSourceProperty> bindingItem, Expression<Func<TSourceProperty, TNewSourceProperty>> converter) where TModel : ViewModel
        {
            return new ConverterBindingItem<TModel, TSourceProperty, TNewSourceProperty>()
            {
                SourceProperty = bindingItem.SourceProperty,
                Converter = converter,
                ViewModel = bindingItem.ViewModel
            };
        }

        public static void To<TModel, TSourceProperty>(this BindingItem<TModel, TSourceProperty> bindingItem, Expression<Func<object>> targetProperty) where TModel : ViewModel
        {
            var body = Expression.Assign(targetProperty, bindingItem.SourceProperty);
            Expression<Action> action = Expression.Lambda<Action>(body, null);
            InternalTo(bindingItem, action.Compile());
        }

        public static void To<TModel, TSourceProperty>(this BindingItem<TModel, TSourceProperty> bindingItem, Action<TSourceProperty> action) where TModel : ViewModel
        {
            InternalTo(bindingItem, action);
        }

        public static void To<TModel, TSourceProperty>(this ObservableBindingItem<TModel, TSourceProperty> bindingItem, Action<ObservableCollection<TSourceProperty>> action) where TModel : ViewModel
        {
            var observableCollection = bindingItem.SourceProperty.Compile().Invoke(bindingItem.ViewModel);
            if (observableCollection == null)
            {
                Action<ObservableCollection<TSourceProperty>> del = (property) =>
                {
                    if (property != null)
                    {
                        property.CollectionChanged += (sender, e) => action(property);
                    }
                };
                InternalTo(bindingItem, del);
            }
            else
            {
                observableCollection.CollectionChanged += (sender, e) => action(observableCollection);
            }

            InternalTo(bindingItem, action);
        }

        public static void To<TModel, TSourceProperty, TConvertedProperty>(this ConverterBindingItem<TModel, TSourceProperty, TConvertedProperty> bindingItem, Action<TConvertedProperty> action) where TModel : ViewModel
        {
            var firstFunc = bindingItem.Converter;
            Expression<Action<TConvertedProperty>> secondFunc = (cp) => action(cp);

            ParameterExpression par = Expression.Parameter(typeof(TSourceProperty), "p");

            var firstFuncArg = Expression.Variable(firstFunc.ReturnType);
            var firstFuncResult = Expression.Invoke(firstFunc, par);
            var secondFuncArg = Expression.Assign(firstFuncArg, firstFuncResult);
            var secondFuncResult = Expression.Invoke(secondFunc, firstFuncResult);
            var actionPlusConverter = Expression.Lambda<Action<TSourceProperty>>(secondFuncResult, par);

            InternalTo(bindingItem, actionPlusConverter.Compile());
        }

        public static void To<TModel, TSourceProperty>(this BindingItem<TModel, TSourceProperty> bindingItem, Action action) where TModel : ViewModel
        {
            InternalTo(bindingItem, action);
        }

        private static void InternalTo<TModel, TSourceProperty>(this BindingItem<TModel, TSourceProperty> bindingItem, Delegate action) where TModel : ViewModel
        {
            if (!PropertyObserver.Observers.ContainsKey(typeof(TModel)))
            {
                PropertyObserver.Observers.Add(typeof(TModel), new PropertyObserver(bindingItem.ViewModel));
            }

            string propertyName;
            PropertyObserver.Observers[typeof(TModel)].RegisterObserver<TModel, TSourceProperty>(bindingItem.SourceProperty, action, out propertyName);
            
            // do the first invocation
            PropertyObserver.Observers[typeof(TModel)].Invoke(propertyName);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }

    public class BindingItem<TModel, TSourceProperty> where TModel : ViewModel
    {
        public Expression<Func<TModel, TSourceProperty>> SourceProperty { get; set; }
        public TModel ViewModel { get; set; }
    }

    public class ConverterBindingItem<TModel, TSourceProperty, TConvertedSourceProperty> : BindingItem<TModel, TSourceProperty> where TModel : ViewModel
    {
        public Expression<Func<TSourceProperty, TConvertedSourceProperty>> Converter { get; set; }
    }

    public class ObservableBindingItem<TModel, TSourcePropertyItem> : BindingItem<TModel, ObservableCollection<TSourcePropertyItem>> where TModel : ViewModel
    {
    }

    public class PropertyObserver
    {
        private static IDictionary<Type, PropertyObserver> observers = new Dictionary<Type, PropertyObserver>();
        public static IDictionary<Type, PropertyObserver> Observers
        {
            get
            {
                return observers;
            }
        }

        private Dictionary<string, List<Delegate>> _handlers = new Dictionary<string, List<Delegate>>();
        private ViewModel _viewModel;

        public PropertyObserver(ViewModel viewModel)
        {
            this._viewModel = viewModel;
            this._viewModel.PropertyChanged += this.HandlePropertyChanged;
            this._viewModel.Disposed += (s, e) =>
            {
                if (s != null && Observers.ContainsKey(s.GetType()))
                    Observers.Remove(s.GetType());
            };
        }

        public void RegisterObserver(string propertyName, Delegate executor)
        {
            if (!_handlers.ContainsKey(propertyName))
            {
                _handlers.Add(propertyName, new List<Delegate>());
            }

            _handlers[propertyName].Add(executor);
        }

        public void RegisterObserver<TModel, TSourceProperty>(Expression<Func<TModel, TSourceProperty>> property, Delegate executor, out string propertyName) where TModel : ViewModel
        {
            var member = (property.Body as MemberExpression);
            if (member == null)
            {
                var unaryExpr = property.Body as UnaryExpression;
                member = unaryExpr.Operand as MemberExpression;
            }

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo != null)
            {
                propertyName = propertyInfo.Name;
                RegisterObserver(propertyInfo.Name, executor);
            }
            else
            {
                propertyName = "";
            }
        }

        public void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DeviceServices.Current.Dispatch(() => Invoke(e.PropertyName));
        }

        private void HandleCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        public void Invoke(string propertyName)
        {
            if (_handlers.ContainsKey(propertyName))
            {
                foreach (var executor in _handlers[propertyName])
                {
                    if (executor.GetMethodInfo().GetParameters().Any())
                    {
                        var methodInfo = this._viewModel.GetType().GetRuntimeProperty(propertyName);
                        var changedValue = methodInfo.GetValue(this._viewModel);
                        executor.DynamicInvoke(changedValue);
                    }
                    else
                    {
                        executor.DynamicInvoke();
                    }
                }

            }
        }
    }
}


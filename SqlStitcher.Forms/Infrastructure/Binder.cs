using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlStitcher.Forms.Helpers;

namespace SqlStitcher.Forms.Infrastructure
{
    public class Binder
    {
        public static Binder<TSource> Create<TSource>(TSource source)
            where TSource : INotifyPropertyChanged
        {
            return new Binder<TSource>(source);
        }

        internal static PropertyInfo GetProperty<TSource>(Expression<Func<TSource, object>> expression)
        {
            PropertyInfo property = null;

            if (expression.Body is MemberExpression)
            {
                property = (PropertyInfo)((MemberExpression)expression.Body).Member;
            }
            else if ((expression.Body as UnaryExpression)?.Operand is MemberExpression)
            {
                property = ((expression.Body as UnaryExpression).Operand as MemberExpression).Member as PropertyInfo;
            }

            if (property == null)
            {
                throw new InvalidOperationException("Model binding expression does not represent a property.");
            }

            return property;
        }
    }

    public class Binder<TSource> : Binder
        where TSource : INotifyPropertyChanged
    {
        private readonly TSource _source;

        private readonly List<PropertyBinding> _propertyBindings = new List<PropertyBinding>();
        private FlaggedState _isReading = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">Binding source.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public Binder(TSource source)
        {
            #region Argument Validation

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            #endregion

            _source = source;
            _source.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Initiate binding against the selected property (<paramref name="selectSourceProperty"/>).
        /// </summary>
        /// <param name="selectSourceProperty">Selector expression for source property.</param>
        /// <returns>This binder (fluent).</returns>
        public BindingConfig<TSource> Bind(Expression<Func<TSource, object>> selectSourceProperty)
        {
            var sourceProperty = GetProperty(selectSourceProperty);

            if (sourceProperty == null)
            {
                throw new InvalidOperationException("Model binding expression does not represent a property.");
            }

            if (sourceProperty.CanRead == false)
            {
                throw new InvalidOperationException("Source property does not allow reading.");
            }

            return new BindingConfig<TSource>(this, sourceProperty);
        }

        ///// <summary>
        ///// Initiate binding against the selected property (<paramref name="selectSourceProperty"/>).
        ///// </summary>
        ///// <param name="selectSourceProperty">Selector expression for source property.</param>
        ///// <returns>This binder (fluent).</returns>
        //public BindingConfig<TSource> Bind(Expression<Func<TSource, int>> selectSourceProperty)
        //{
        //    var sourceProperty = (PropertyInfo)((MemberExpression)selectSourceProperty.Body).Member;

        //    if (sourceProperty.CanRead == false)
        //    {
        //        throw new InvalidOperationException("Source property does not allow reading!");
        //    }

        //    return new BindingConfig<TSource>(this, sourceProperty);
        //}


        /// <summary>
        /// Add the given property binding (<paramref name="binding"/>).
        /// </summary>
        /// <param name="binding">Property binding.</param>
        internal void AddPropertyBinding(PropertyBinding binding)
        {
            binding.BindWrite(_source);
            _propertyBindings.Add(binding);

            if (binding.IsTwoWay)
            {
                if (binding.TargetInstance is TextBox)
                {
                    ((TextBox) binding.TargetInstance).TextChanged += (sender, e) =>
                    {
                        using (_isReading.IsTrueOver()) 
                            binding.BindRead(_source);
                    };
                }
                else if (binding.TargetInstance is ComboBox)
                {
                    ((ComboBox)binding.TargetInstance).SelectedIndexChanged += (sender, e) =>
                    {
                        using (_isReading.IsTrueOver())
                            binding.BindRead(_source);
                    };
                }
                else if (binding.TargetInstance is CheckBox)
                {
                    ((CheckBox)binding.TargetInstance).CheckStateChanged += (sender, e) =>
                    {
                        using (_isReading.IsTrueOver())
                            binding.BindRead(_source);
                    };
                }

            }
        }

        /// <summary>
        /// When the property is changed.
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Avoid write read infinite loop.
            if (_isReading)
                return;

            foreach (var binding in _propertyBindings.Where(p => p.Name == e.PropertyName))
            {
                binding.BindWrite(_source);
            }
        }


    }

    /// <summary>
    /// This is a continuation of the fluent used to select a target value and property.
    /// </summary>
    /// <typeparam name="TSource">Type of binder source.</typeparam>
    public class BindingConfig<TSource>
        where TSource : INotifyPropertyChanged
    {
        private readonly Binder<TSource> _binder;
        private readonly PropertyInfo _sourceProperty;

        private PropertyBinding _binding;

        internal BindingConfig(Binder<TSource> binder, PropertyInfo sourceProperty)
        {
            _binder = binder;
            _sourceProperty = sourceProperty;
        }

        /// <summary>
        /// Complete the binding operation by selecting a target (<paramref name="target"/>) and 
        ///     target property (<paramref name="selectTargetProperty"/>).
        /// </summary>
        /// <typeparam name="TTarget">Type of target.</typeparam>
        /// <param name="target">Binding target.</param>
        /// <param name="selectTargetProperty">Target property selector.</param>
        /// <returns>This binding configuration.</returns>
        public BindingConfig<TSource> To<TTarget>(TTarget target, Expression<Func<TTarget, object>> selectTargetProperty)
        {
            var targetProperty = Binder.GetProperty(selectTargetProperty);

            if (targetProperty.CanWrite == false)
            {
                throw new InvalidOperationException("Target property does not allow writing!");
            }

            if (!targetProperty.PropertyType.IsAssignableFrom(_sourceProperty.PropertyType))
            {
                throw new InvalidOperationException("Target property is not assignable from source property. Try using a value converter.");
            }

            _binding = new PropertyBinding
            {
                SourceProperty = _sourceProperty,
                TargetInstance = target,
                TargetProperty = targetProperty,
            };

            return this;
        }

        /// <summary>
        /// Complete the binding operation by selecting a target (<paramref name="target"/>) and 
        ///     target property (<paramref name="selectTargetProperty"/>).
        /// </summary>
        /// <typeparam name="TTarget">Type of target.</typeparam>
        /// <param name="target">Binding target.</param>
        /// <param name="selectTargetProperty">Target property selector.</param>
        /// <param name="valueConverter">Value converter.</param>
        /// <returns>Original binder.</returns>
        public BindingConfig<TSource> To<TTarget>(TTarget target, Expression<Func<TTarget, object>> selectTargetProperty, IValueConverter valueConverter)
        {
            var targetProperty = Binder.GetProperty(selectTargetProperty);

            if (targetProperty.CanWrite == false)
            {
                throw new InvalidOperationException("Cannot write to target property!");
            }

            _binding = new PropertyBinding
            {
                SourceProperty = _sourceProperty,
                TargetInstance = target,
                TargetProperty = targetProperty,
                ValueConverter = valueConverter
            };

            return this;
        }

        /// <summary>
        /// Configure the binding as two-way.
        /// </summary>
        /// <returns>This binding configuration.</returns>
        public BindingConfig<TSource> AndBack()
        {
            if (_binding == null || _binding.TargetInstance == null)
            {
                throw new InvalidOperationException("Target property was not assigned. Call To before calling IsTwoWay.");
            }

            if (_binding.TargetProperty.CanRead == false)
            {
                throw new InvalidOperationException("Target property does not allow reading!");
            }

            if (_binding.SourceProperty.CanRead == false)
            {
                throw new InvalidOperationException("Target property does not allow writing!");
            }

            if (_binding.TargetInstance is TextBox ||
                _binding.TargetInstance is ComboBox ||
                _binding.TargetInstance is CheckBox)
            {
                _binding.IsTwoWay = true;
                return this;
            }

            throw new InvalidOperationException("Target instance is not a type for which IsTwoWay binding is supported.");
        }

        /// <summary>
        /// Complete the binding configuration.
        /// </summary>
        /// <returns>Original binder.</returns>
        public Binder<TSource> Go()
        {
            _binder.AddPropertyBinding(_binding);

            return _binder;
        }
    }

    internal class PropertyBinding
    {
        public string Name
        {
            get { return SourceProperty.Name; }
        }

        public PropertyInfo SourceProperty { get; set; }

        public object TargetInstance { get; set; }

        public PropertyInfo TargetProperty { get; set; }

        public IValueConverter ValueConverter { get; set; }

        public bool IsTwoWay { get; set; }

        public void BindWrite(object source)
        {
            var value = SourceProperty.GetValue(source);

            if (ValueConverter != null)
                value = ValueConverter.Convert(value, TargetProperty.PropertyType, CultureInfo.CurrentCulture);

            TargetProperty.SetValue(TargetInstance, value);
        }

        public void BindRead(object source)
        {
            var value = TargetProperty.GetValue(TargetInstance);

            if (ValueConverter != null)
                value = ValueConverter.ConvertBack(value, TargetProperty.PropertyType, CultureInfo.CurrentCulture);

            SourceProperty.SetValue(source, value);
        }
    }

    public interface IValueConverter
    {
        object Convert(object value, Type targetType, CultureInfo culture);
        object ConvertBack(object value, Type targetType, CultureInfo culture);
    }
}

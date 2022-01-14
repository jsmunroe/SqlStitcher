using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlStitcher.Forms.Properties;

namespace SqlStitcher.Forms.Infrastructure
{
    public class FormSizer : IDisposable
    {
        private readonly Form _form;
        private readonly Settings _appSettings = App.Current.Settings;

        private PropertyInfo _locationSettingProperty;
        private PropertyInfo _sizeSettingProperty;
        private PropertyInfo _isMaximizedSettingProperty;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="form">Form to size.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        protected FormSizer(Form form)
        {
            #region Argument Validation

            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            #endregion

            _form = form;
            Attach();
        }

        /// <summary>
        /// Create a form sizer from the given form (<paramref name="form"/>).
        /// </summary>
        /// <param name="form">Form from which to create.</param>
        /// <returns>Created form sizer.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static FormSizer From(Form form)
        {
            return new FormSizer(form);
        }

        /// <summary>
        /// Select location property from settings.
        /// </summary>
        /// <param name="selector">Location selector.</param>
        /// <returns>This instance (fluent).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selector"/> is null.</exception>
        public FormSizer Location(Expression<Func<Settings, Point>> selector)
        {
            #region Argument Validation

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            #endregion

            _locationSettingProperty = BindProperty(selector);

            _form.StartPosition = FormStartPosition.Manual;
            _form.Location = GetLocationSetting();

            return this;
        }

        /// <summary>
        /// Select size property from settings.
        /// </summary>
        /// <param name="selector">Size selector.</param>
        /// <returns>This instance (fluent).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selector"/> is null.</exception>
        public FormSizer Size(Expression<Func<Settings, Size>> selector)
        {
            #region Argument Validation

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            #endregion

            _sizeSettingProperty = BindProperty(selector);

            _form.Size = GetSizeSetting();

            return this;
        }

        /// <summary>
        /// Select is-maximized property from settings.
        /// </summary>
        /// <param name="selector">Is-maximized selector.</param>
        /// <returns>This instance (fluent).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selector"/> is null.</exception>
        public FormSizer Maximized(Expression<Func<Settings, bool>> selector)
        {
            #region Argument Validation

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            #endregion

            _isMaximizedSettingProperty = BindProperty(selector);

            _form.WindowState = GetIsMaximizedSetting() ? FormWindowState.Maximized : FormWindowState.Normal;

            return this;
        }

        /// <summary>
        /// Dispose of this instance by disconnecting it from its form.
        /// </summary>
        public void Dispose()
        {
            _form.Load -= _form_Load;
            _form.Disposed -= _form_Disposed;
            _form.Resize -= _form_Resize;
            _form.Move -= _form_Move;
        }

        private void Attach()
        {
            _form.Load += _form_Load;
            _form.Disposed += _form_Disposed;
            _form.Resize += _form_Resize;
            _form.Move += _form_Move;
        }

        private static PropertyInfo BindProperty<TProperty>(Expression<Func<Settings, TProperty>> selector)
        {
            var sizeProperty = (PropertyInfo)((MemberExpression)selector.Body).Member;

            if (!sizeProperty.CanRead || !sizeProperty.CanWrite)
            {
                throw new InvalidOperationException("Size property must be able to read and write.");
            }

            if (sizeProperty.PropertyType != typeof(TProperty))
            {
                throw new InvalidOperationException(string.Format("Size property must be of type {0}.", typeof(TProperty).Name));
            }
            return sizeProperty;
        }

        private Point GetLocationSetting()
        {
            if (_locationSettingProperty == null)
                return _form.Location;

            return (Point)_locationSettingProperty.GetValue(_appSettings);
        }

        private void SetLocationSetting(Point location)
        {
            if (_locationSettingProperty == null)
                return;

            _locationSettingProperty.SetValue(_appSettings, location);
        }

        private Size GetSizeSetting()
        {
            if (_sizeSettingProperty == null)
                return _form.Size;

            return (Size)_sizeSettingProperty.GetValue(_appSettings);
        }

        private void SetSizeSetting(Size size)
        {
            if (_sizeSettingProperty == null)
                return;

            _sizeSettingProperty.SetValue(_appSettings, size);
        }

        private bool GetIsMaximizedSetting()
        {
            if (_isMaximizedSettingProperty == null)
                return _form.WindowState == FormWindowState.Maximized;

            return (bool)_isMaximizedSettingProperty.GetValue(_appSettings);
        }

        private void SetIsMaximizedSetting(bool isMaximized)
        {
            if (_isMaximizedSettingProperty == null)
                return;

            _isMaximizedSettingProperty.SetValue(_appSettings, isMaximized);
        }

        void _form_Load(object sender, EventArgs e)
        {
            _form.Location = GetLocationSetting();
        }

        void _form_Resize(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Maximized)
            {
                SetIsMaximizedSetting(true);
            }
            else if (_form.WindowState == FormWindowState.Minimized)
            {
                SetIsMaximizedSetting(false);
            }
            else
            {
                SetLocationSetting(_form.Location);
                SetSizeSetting(_form.Size);
                SetIsMaximizedSetting(false);
            }

            _appSettings.Save();
        }

        void _form_Move(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Normal)
            {
                SetLocationSetting(_form.Location);
            }

            _appSettings.Save();
        }

        void _form_Disposed(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}

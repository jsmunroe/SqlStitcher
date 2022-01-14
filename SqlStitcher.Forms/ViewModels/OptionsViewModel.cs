using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.IO;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Forms.ViewModels
{
    public class OptionsViewModel : BaseViewModel
    {
        private readonly ISettings _settings;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settings">Application settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings"/> is null.</exception>
        public OptionsViewModel(ISettings settings)
        {
            #region Argument Validation

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            #endregion

            _settings = settings;

            General = new OptionsGeneralViewModel(settings);
            Stitching = new OptionsStitchingViewModel(settings);
        }

        /// <summary>
        /// Save all settings.
        /// </summary>
        public bool Save()
        {
            ErrorMessage = "";

            if (!ValidateChild(General)) 
                return false;

            General.Save();

            if (!ValidateChild(Stitching))
                return false;

            Stitching.Save();

            _settings.Save();

            return true;
        }

        /// <summary>
        /// Validate the child view model.
        /// </summary>
        /// <param name="viewModel">Child view model.</param>
        /// <returns>Validation success.</returns>
        private bool ValidateChild(BaseViewModel viewModel)
        {
            var result = viewModel.Validate();

            if (!result)
                ErrorMessage = result.Message;

            return result;
        }

        public OptionsGeneralViewModel General { get; set; }
        public OptionsStitchingViewModel Stitching { get; set; }
    }
}

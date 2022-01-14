using System;
using System.IO;
using Helpers.Archetecture;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Forms.ViewModels
{
    public class OptionsStitchingViewModel : BaseViewModel
    {
        private readonly ISettings _settings;

        private bool _insertGoCommand;
        private PrependDescription _prependScriptComment;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settings">Application settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings"/> is null.</exception>
        public OptionsStitchingViewModel(ISettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            InsertGoCommand = _settings.InsertGoCommand;
            PrependScriptComment = _settings.PrependScriptDescription;
        }

        public PrependDescription PrependScriptComment
        {
            get { return _prependScriptComment; }
            set { _prependScriptComment = value; RaisePropertyChanged(); }
        }

        public bool InsertGoCommand
        {
            get { return _insertGoCommand; }
            set { _insertGoCommand = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// Save the settings herein.
        /// </summary>
        public void Save()
        {
            _settings.InsertGoCommand = InsertGoCommand;
            _settings.PrependScriptDescription = PrependScriptComment;
        }

        /// <summary>
        /// Validate this view model.
        /// </summary>
        /// <returns>Validation result.</returns>
        public override Result Validate()
        {
            return Result.OK;
        }
    }
}
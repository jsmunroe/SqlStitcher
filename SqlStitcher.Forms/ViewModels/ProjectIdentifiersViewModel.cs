using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Helpers.Archetecture;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Messages;
using SqlStitcher.Models;

namespace SqlStitcher.Forms.ViewModels
{
    public class ProjectIdentifiersViewModel : BaseViewModel
    {
        private readonly Project _project;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">Project.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="project"/> is null.</exception>
        public ProjectIdentifiersViewModel(Project project)
        {
            #region Argument Validation

            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            #endregion

            _project = project;
            Identifiers = _project.GetIdentifiers().Select(IdentifierViewModel.Build).ToList();
        }

        /// <summary>
        /// Script identifiers names.
        /// </summary>
        public IReadOnlyList<IdentifierViewModel> Identifiers { get; set; }

        /// <summary>
        /// Save the replacement names of the identifiers.
        /// </summary>
        public Result Save()
        {
            var badIdentifier = Identifiers.FirstOrDefault(p => !Regex.IsMatch(p.ReplacementName, @"^[\w\.]+$"));

            if (badIdentifier != null)
            {
                return Result.Fail(string.Format("\"{0}\" is an invalid identifier name!", badIdentifier.ReplacementName));
            }

            foreach (var identifier in Identifiers)
            {
                _project.ReplaceIdentifier(identifier.OriginalName, identifier.ReplacementName);
            }

            Messenger.Publish(new RefreshScriptMessage());
            
            ServiceLocator.Current.GetInstance<IProjectState>().InvalidateSave();

            return true;
        }

        /// <summary>
        /// Revert to the original names of the identifiers.
        /// </summary>
        public void Revert()
        {
            _project.Revertidentifiers();
            Identifiers = _project.GetIdentifiers().Select(IdentifierViewModel.Build).ToList();
        }


        public class IdentifierViewModel
        {
            /// <summary>
            /// Build this view model.
            /// </summary>
            /// <param name="identifier">Script identifier.</param>
            /// <returns>Built view model.</returns>
            public static IdentifierViewModel Build(ScriptIdentifier identifier)
            {
                return new IdentifierViewModel
                {
                    OriginalName = identifier.Name,
                    ReplacementName = identifier.ReplacementName
                };
            }

            /// <summary>
            /// Original identifier name.
            /// </summary>
            public string OriginalName { get; private set; }

            /// <summary>
            /// Replacement identifier name.
            /// </summary>
            public string ReplacementName { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Annotations;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Helpers;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Messages;
using SqlStitcher.Models;

namespace SqlStitcher.Forms.ViewModels
{
    public class ProjectOverviewViewModel : BaseViewModel
    {
        private readonly Project _project;
        private readonly List<ScriptEntry> _batchEntries;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">Project.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="project"/> is null.</exception>
        public ProjectOverviewViewModel([NotNull]Project project)
        {
            #region Argument Validation

            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            #endregion

            _project = project;
            Entries = _project.Entries;
            _batchEntries = _project.Entries.Where(p => p.IsSelected).OrderBy(p => p.Ordinal).ToList();
            SetDefaultBatchOrdinals();

            Messenger.Subscribe<SendProjectScriptToClipboardMessage>(this, OnMessageRecieved);
            Messenger.Subscribe<ShowIdentifiersMessage>(this, OnMessageRecieved);
            Messenger.Subscribe<ViewProjectScriptMessage>(this, OnMessageRecieved);
        }

        public IReadOnlyList<ScriptEntry> Entries { get; set; }

        public IReadOnlyList<ScriptEntry> BatchEntries
        {
            get { return _batchEntries; }
        }

        /// <summary>
        /// Build the script tree.
        /// </summary>
        /// <returns>Root node of script tree.</returns>
        public TreeNode<ScriptEntry> BuildScriptTree()
        {
            return _project.BuildTree();
        }       

        /// <summary>
        /// Copy the given entries (<paramref name="entries"/>) combined text to the clipboard.
        /// </summary>
        /// <param name="entries">Script entries.</param>
        public void CopyToClipboard(IEnumerable<ScriptEntry> entries)
        {
            var buildOptions = App.Current.GetScriptBuildOptions();

            var scriptText = _project.Build(entries, buildOptions);

            var clipboarder = Container.GetInstance<IClipboarder>();
            clipboarder.CopyText(scriptText);
        }

        /// <summary>
        /// View the given entries (<paramref name="entries"/>) combined text.
        /// </summary>
        /// <param name="entries">Script entries.</param>
        public void ViewScript(IEnumerable<ScriptEntry> entries)
        {
            var scriptBatch = new ScriptBatch(_project, entries);

            var viewModel = new ScriptViewModel(scriptBatch);

            ViewCatalog.Resolve(viewModel).Show(App.Current.MainForm);
        }

        /// <summary>
        /// Remove the given entries (<paramref name="entries"/>) from the script batch.
        /// </summary>
        /// <param name="entries">Script entries.</param>
        public void RemoveFromBatch(IEnumerable<ScriptEntry> entries)
        {
            var removedEntries = BatchEntries.Where(p => entries.Any(q => q.Equals(p))).ToArray();

            foreach (var entry in removedEntries)
            {
                Entries.First(p => entry.Equals(p)).IsSelected = false;
                entry.IsSelected = false;
                _batchEntries.Remove(entry);
            }
        }

        /// <summary>
        /// Move the given script entry (<paramref name="entry"/>) to the given index 
        ///     (<paramref name="index"/>) within the batch entries.
        /// </summary>
        /// <param name="entry">Entry to move.</param>
        /// <param name="index">Index to move it too.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entry"/> is null.</exception>
        public bool MoveInBatch(ScriptEntry entry, int index)
        {
            #region Argument Validation

            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            #endregion

            var result =  _batchEntries.MoveTo(entry, Math.Min(index, _batchEntries.Count - 1));

            SetDefaultBatchOrdinals();

            InvalidateProjectSave();

            return result;
        }

        /// <summary>
        /// Update entry selection for the given script entry (<paramref name="entry"/>).
        /// </summary>
        /// <param name="entry">Script entry.</param>
        public void UpdateEntrySelection(ScriptEntry entry)
        {
            if (entry.IsSelected)
            {
                if (!_batchEntries.Contains(entry))
                {

                    entry.Ordinal = _batchEntries.Any() ? _batchEntries.Max(p => p.Ordinal) + 1 : 0;
                    _batchEntries.Add(entry);
                }
            }
            else
            {
                if (_batchEntries.Contains(entry))
                    _batchEntries.Remove(entry);
            }
        }

        /// <summary>
        /// Invalidate project state.
        /// </summary>
        public void InvalidateProjectSave()
        {
            var projectState = ServiceLocator.Current.GetInstance<IProjectState>();

            projectState.InvalidateSave();
        }

        /// <summary>
        /// Set the entries within the batch to default consecutive ordinals.
        /// </summary>
        private void SetDefaultBatchOrdinals()
        {
            var index = 0;
            foreach (var entry in BatchEntries)
            {
                entry.Ordinal = index++;
            }
        }

        /// <summary>
        /// When a given <see cref="SendProjectScriptToClipboardMessage"/> (<paramref name="message"/>) is recieved.
        /// </summary>
        /// <param name="message">Recieved message.</param>
        private void OnMessageRecieved(SendProjectScriptToClipboardMessage message)
        {
            var buildOptions = App.Current.GetScriptBuildOptions();

            var scriptText = _project.Build(buildOptions);

            var clipboarder = Container.GetInstance<IClipboarder>();
            clipboarder.CopyText(scriptText);
        }

        /// <summary>
        /// When a given <see cref="ShowIdentifiersMessage"/> (<paramref name="message"/>) is recieved.
        /// </summary>
        /// <param name="message">Recieved message.</param>
        private void OnMessageRecieved(ShowIdentifiersMessage message)
        {
            var viewModel = new ProjectIdentifiersViewModel(_project);

            ViewCatalog.Resolve(viewModel).ShowDialog(App.Current.MainForm);
        }

        /// <summary>
        /// When a given <see cref="ViewProjectScriptMessage"/> (<paramref name="message"/>) is recieved.
        /// </summary>
        /// <param name="message">Recieved message.</param>
        private void OnMessageRecieved(ViewProjectScriptMessage message)
        {
            var viewModel = new ScriptViewModel(_project);

            ViewCatalog.Resolve(viewModel).Show(App.Current.MainForm);
        }
    }
}

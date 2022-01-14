using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;
using SqlStitcher.Forms.Messages;
using IScriptSource = SqlStitcher.Contracts.IScriptSource;

namespace SqlStitcher.Forms.ViewModels
{
    public class ScriptViewModel : BaseViewModel
    {
        private readonly IScriptSource _scriptSource;

        private string _scriptText;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scriptSource">Script source.</param>
        public ScriptViewModel(IScriptSource scriptSource)
        {
            #region Argument Validation

            if (scriptSource == null)
            {
                throw new ArgumentNullException("scriptSource");
            }

            #endregion

            _scriptSource = scriptSource;

            var buildOptions = App.Current.GetScriptBuildOptions();

            ScriptText = scriptSource.GetScript(buildOptions);

            Messenger.Subscribe<RefreshScriptMessage>(this, OnMessageRecieved);
        }

        /// <summary>
        /// Script text.
        /// </summary>
        public string ScriptText
        {
            get { return _scriptText; }
            set { _scriptText = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Copy the current script text to the clipboard.
        /// </summary>
        public void CopyToClipboard()
        {
            var clipboarder = Container.GetInstance<IClipboarder>();
            clipboarder.CopyText(ScriptText);
        }

        /// <summary>
        /// When the <see cref="RefreshScriptMessage"/> is recieved.
        /// </summary>
        /// <param name="message">Recieved message.</param>
        private void OnMessageRecieved(RefreshScriptMessage message)
        {
            var buildOptions = App.Current.GetScriptBuildOptions();

            ScriptText = _scriptSource.GetScript(buildOptions);
        }
    }
}

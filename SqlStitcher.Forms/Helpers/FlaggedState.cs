using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace SqlStitcher.Forms.Helpers
{
    public class FlaggedState
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Initial value.</param>
        public FlaggedState(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Flag value.
        /// </summary>
        public bool Value { get; private set; }

        /// <summary>
        /// Provide a means of scoping with a using statement a period over which the value herein is true.
        /// </summary>
        /// <returns>Disposable that will end the scope.</returns>
        public IDisposable IsTrueOver()
        {
            Value = true;

            return new ScopeProvider(() => Value = false);
        }

        /// <summary>
        /// Provide a means of scoping with a using statement a period over which the value herein is false.
        /// </summary>
        /// <returns>Disposable that will end the scope.</returns>
        public IDisposable IsFalseOver()
        {
            Value = false;

            return new ScopeProvider(() => Value = true);
        }

        /// <summary>
        /// Implicitly cast given flagged state (<paramref name="flaggedState"/>) as Boolean value.
        /// </summary>
        /// <param name="flaggedState">Flagged state instance.</param>
        /// <returns>Casted boolean value.</returns>
        public static implicit operator bool(FlaggedState flaggedState)
        {
            return flaggedState != null && flaggedState.Value;
        }

        /// <summary>
        /// Implicitly cast given Boolean value (<paramref name="value"/>) as a flagged state instance.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <returns>Flagged state instance.</returns>
        public static implicit operator FlaggedState(bool value)
        {
            return new FlaggedState(value);
        }
    }
}

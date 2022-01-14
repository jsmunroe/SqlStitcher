using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SqlStitcher.Contracts;

namespace SqlStitcher.Helpers
{
    public static class XmlHelpers
    {
        /// <summary>
        /// Convert each instance in the given sequence (<paramref name="sequence"/>) one-by-one to 
        ///     a sequence of XML elements.
        /// </summary>
        /// <param name="sequence">Sequenc of <see cref="IXmlSavable"/> instances.</param>
        /// <returns>Sequence of XML elements.</returns>
        public static IEnumerable<XElement> ToXml(this IEnumerable<IXmlSavable> sequence)
        {
            return sequence.Select(p => p.ToXml());
        }

        /// <summary>
        /// Convert each instance in the given sequence (<paramref name="sequence"/>) one-by-one to
        ///     a sequence of instances of type <typeparamref name="TInstance"/>.
        /// </summary>
        /// <typeparam name="TInstance">Type of instance.</typeparam>
        /// <param name="sequence">Sequence of XML elements.</param>
        /// <returns>Sequence of <typeparamref name="TInstance"/> instances.</returns>
        public static IEnumerable<TInstance> FromXml<TInstance>(this IEnumerable<XElement> sequence)
            where TInstance : class, IXmlSavable
        {
            return sequence.Select(FromXml<TInstance>);
        }

        /// <summary>
        /// Create an instance of type <typeparamref name="TInstance"/> from the given XML element 
        ///     (<paramref name="element"/>).
        /// </summary>
        /// <typeparam name="TInstance">Type of instance.</typeparam>
        /// <param name="element">XML element.</param>
        /// <returns>Created instance.</returns>
        public static TInstance FromXml<TInstance>(XElement element) 
            where TInstance : class, IXmlSavable
        {
            var type = typeof(TInstance);

            var methodInfo = type.GetMethod("FromXml", new [] { typeof(XElement) });

            if (methodInfo == null || !methodInfo.IsStatic || !methodInfo.ReturnType.IsAssignableFrom(type))
                throw new Exception(string.Format("Cannot find FromXml(XElement) method on type {0}", type.Name));

            return methodInfo.Invoke(null, new[] {element}) as TInstance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.DnBCredit.Transformation
{
    public abstract class MsgTransformerBase<TSource, TTarget> : IMsgTransformerBase<TSource, TTarget>, IDisposable
        where TSource : class
        where TTarget : class
    {
        protected TSource _source = null;
        protected TTarget _target = null;

        protected MsgTransformerBase(TSource source, TTarget target)
        {
            _source = source;
            _target = target;
        }

        public void Transform()
        {
            _checkArguments(_source, _target);

            TransformMessage();

        }

        public void Transform(TSource source, TTarget target)
        {
            _source = source;
            _target = target;

            TransformMessage();

        }

        private bool _checkArguments(TSource source, TTarget target)
        {
            if ((source == null) || (target == null))
            {
                throw new ArgumentNullException("Null references for source or targed instances");
            }

            return true;
        }

        abstract protected void TransformMessage();

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }


        #region -- other helper methods
        protected bool HasValue(object sourceValue)
        {
            if ((sourceValue != null) && (!string.IsNullOrEmpty(sourceValue.ToString())) && (!string.IsNullOrWhiteSpace(sourceValue.ToString())))
                return true;

            return false;
        }

        protected bool HasItems(IEnumerable<object> sourceValue)
        {
            if ((sourceValue != null) && (sourceValue.Count() > 0))
                return true;

            return false;
        }
        #endregion
    }
}

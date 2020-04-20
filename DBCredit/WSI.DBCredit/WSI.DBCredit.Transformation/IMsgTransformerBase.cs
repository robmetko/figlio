using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.DnBCredit.Transformation
{
    public interface IMsgTransformerBase<TSource, TTarget>
       where TSource : class
       where TTarget : class
    {

        void Transform();
        void Transform(TSource source, TTarget target);
    }
}

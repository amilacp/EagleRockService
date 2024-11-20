using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRockService.Test.Common
{
    public abstract class SpecificationForAsync<T>
    {
        protected SpecificationForAsync()
        {
            Subject = Given();
            When();
        }

        public T Subject { get; set; }

        protected abstract T Given();

        protected abstract void When();
    }
}

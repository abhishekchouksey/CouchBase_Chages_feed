using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class MyObserver : IObserver<string>
    {
        public Action<string> InterceptOnNext { private get; set; }
        public Action<Exception> InterceptOnError { private get; set; }
        public Action InterceptOnCompleted { private get; set; }

        public void OnNext(string value)
        {
            if (InterceptOnNext != null)
                InterceptOnNext(value);
        }

        public void OnError(Exception error)
        {
            if (InterceptOnError != null)
                InterceptOnError(error);
        }

        public void OnCompleted()
        {
            if (InterceptOnCompleted != null)
                InterceptOnCompleted();
        }
    }
}

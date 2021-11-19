using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface Iobserver : IDisposable
    {
        void AddObserver(Iobservable observable);
    }


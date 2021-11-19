using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine;
using TMPro;


    public class ObserverbleLogger : Iobserver
    {
        TMP_Text _text;

        public ObserverbleLogger(Iobservable observable, TMP_Text text)
        {
            observable.OnChanged += OnChanged;
            _text = text;
        }


        public void AddObserver(Iobservable observable)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void OnChanged(object o)
        {
            _text.text = o.ToString();
        }
    }


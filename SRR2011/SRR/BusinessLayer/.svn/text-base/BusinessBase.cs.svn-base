/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Base class for many of the 'Info' classes.
 * 
 */
using System;
using Disney.iDash.DataLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class BusinessBase
    {
        public DB2Factory Factory { get; private set; }

        public ExceptionHandler ExceptionHandler = new ExceptionHandler();
        public event EventHandler ChangedEvent;

		public delegate void ProgressEventHandler(string message, int percentageComplete);
		public event ProgressEventHandler ProgressEvent;
        private bool _isDirty = false;

        public BusinessBase()
        {
            Factory = new DB2Factory();

            Factory.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
            });
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                if (ChangedEvent != null)
                    ChangedEvent(this, null);
            }
        }

		public void OnProgress(string message = "", int percentageComplete = 0)
		{
			if (ProgressEvent != null)
				ProgressEvent(message, percentageComplete);
		}

    }
}

/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * System Lock class;
 * A number of menu options are only intended to be used by one user at a time.  This class will contain the lock status for a lock obtained by GetSystemLock.
 * A higher level lock if in place, will prevent a lower level lock from being established.
 * 
 */
using System.Text;
using Disney.iDash.LocalData;

namespace Disney.iDash.SRR.BusinessLayer
{
	public class SystemLock
	{
		// level at which lock currently exists.
		public enum LockTypes
		{
			Current,
			Higher,
			Lower,
			Ok
		}

		private string _area = string.Empty;
		private string _subArea = string.Empty;
		private string _lockedBy = string.Empty;

		public string Area
		{
			get { return _area; }
			internal set { _area = value.ToUpper(); }
		}

		public string SubArea
		{
			get { return _subArea; }
			internal set { _subArea = value.ToUpper(); }
		}

		public string LockedBy
		{
			get { return _lockedBy; }
			internal set { _lockedBy = value.ToUpper(); }
		}

		public LockTypes LockStatus { get; internal set; }

		public bool Success
		{
			get
			{
				return LockStatus == LockTypes.Ok;
			}

		}

		public string FailureReason
		{
			get
			{
				if (Success)
					return string.Empty;
				else
					return "In use by " + LockedBy;
			}
		}

		public SystemLock()
		{
			Area = string.Empty;
			SubArea = string.Empty;
			LockedBy = Session.User.DisplayName;
			LockStatus = LockTypes.Ok;
		}

		public SystemLock(string area, string subArea = "", string userId = "")
		{
			Area = area;
			SubArea = subArea;
			if (userId == string.Empty)
				LockedBy = Session.User.DisplayName;
			else
				LockedBy = userId;
			LockStatus = LockTypes.Ok;
		}

		public LockTypes GetLockStatus(string lockStatus)
		{
			var result = LockTypes.Ok;
			switch (lockStatus)
			{
				case "C":
					result = SystemLock.LockTypes.Current;
					break;

				case "O":
					result = SystemLock.LockTypes.Ok;
					break;

				case "H":
					result = SystemLock.LockTypes.Higher;
					break;

				case "L":
					result = SystemLock.LockTypes.Lower;
					break;
			}
			return result;
		}

		public override string ToString()
		{
			var result = new StringBuilder();
			foreach (var pi in this.GetType().GetProperties())
				result.AppendLine(string.Format("{0} = {1}", pi.Name, pi.GetValue(this, null)));
			return result.ToString();
		}

	}

}

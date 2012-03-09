using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disney.iDash.SRR.BusinessLayer
{
	public static class Constants
	{
		public const string kDaily = "Daily";
		public const string kWeekly = "Weekly";
		public const string kBoth = "Both";
		public const string kBricksAndMortar = "B&M";
		public const string kOnline = "Online";

		public enum Workbenches
		{
			Daily,
			Weekly,
			Both
		}

		private static Dictionary<Workbenches, string> _workbenches = new Dictionary<Workbenches, string>{
            {Workbenches.Daily, kDaily},
            {Workbenches.Weekly, kWeekly},
            {Workbenches.Both, kBoth}};

		public enum StoreTypes
		{
			BricksAndMortar,
			Online,
			Both
		}

		private static Dictionary<StoreTypes, string> _storeTypes = new Dictionary<StoreTypes, string>{
            {StoreTypes.BricksAndMortar, kBricksAndMortar},
            {StoreTypes.Online, kOnline},
            {StoreTypes.Both, kBoth}};

		public static string GetWorkbenchName(Workbenches workbench)
		{
			return _workbenches[workbench];
		}

		public static Dictionary<Workbenches, string> GetWorkbenches
		{
			get { return _workbenches; }
		}

		public static string GetStoreTypeName(StoreTypes storeType)
		{
			return _storeTypes[storeType];
		}

		public static Dictionary<StoreTypes, string> GetStoreTypes
		{
			get { return _storeTypes; }
		}
	}
}

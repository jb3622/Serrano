/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Source object for combo and list boxes
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using Disney.iDash.DataLayer;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public sealed class LookupSource
    {
        public enum LookupTypes
        {
            None,
            Departments,
            EventCodes,
            Franchises,
            IPUsers,
            ItemClass,
            ItemVendor,
            Markets,
            Patterns,
            Products,
            Promotions,
            Stores,
			StowawayStores,
            StoreGrades,
            StoreGroups           
        }

        public ExceptionHandler ExceptionHandler = new ExceptionHandler();

        private Dictionary<LookupTypes, LookupSourceInfo> _lookupSourceInfo = new Dictionary<LookupTypes, LookupSourceInfo>
        {
           {LookupTypes.Departments, new LookupSourceInfo(Properties.Resources.SQLLookupDepartments, "Id", "Description")},
           {LookupTypes.EventCodes, new LookupSourceInfo(Properties.Resources.SQLLookupEventCodes, "Id", "Description", false, true)},
           {LookupTypes.Franchises, new LookupSourceInfo(Properties.Resources.SQLLookupFranchises, "Id","Description", false, true)},
           {LookupTypes.IPUsers, new LookupSourceInfo(Properties.Resources.SQLLookupIPUsers, "Id","Username", false, true)},
           {LookupTypes.ItemClass, new LookupSourceInfo(Properties.Resources.SQLLookupItemClass, "ID", "DESCRIPTION", true, false)},
           {LookupTypes.ItemVendor, new LookupSourceInfo(Properties.Resources.SQLLookupItemVendors, "ID", "DESCRIPTION", true, false)},
           {LookupTypes.Markets, new LookupSourceInfo(Properties.Resources.SQLLookupMarkets, "Id","Description", false, true)},
           {LookupTypes.Patterns, new LookupSourceInfo(Properties.Resources.SQLLookupPatterns, "Id","Description", false, true)},
           {LookupTypes.Products, new LookupSourceInfo(Properties.Resources.SQLLookupProducts, "Id","Description", false, true)},
           {LookupTypes.Promotions, new LookupSourceInfo(Properties.Resources.SQLLookupPromotions, "Id","Description", false, true)},
           {LookupTypes.Stores, new LookupSourceInfo(Properties.Resources.SQLLookupStores,"Id","Description", false, true)},
           {LookupTypes.StowawayStores, new LookupSourceInfo(Properties.Resources.SQLLookupStowawayStores,"Id","Description", false, true)},
           {LookupTypes.StoreGrades, new LookupSourceInfo(Properties.Resources.SQLLookupStoreGrades, "Id","Description", false, true)},
           {LookupTypes.StoreGroups, new LookupSourceInfo(Properties.Resources.SQLLookupStoreGroups, "Id","Description", false, true)}
        };
        
        private DB2Factory _factory = new DB2Factory();
		private static Dictionary<LookupTypes, List<LookupItem>> _cache = new Dictionary<LookupTypes, List<LookupItem>>();
		private static string _userId = string.Empty;

		public LookupSource()
        {
            _factory.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
                });
        }

		public static void ClearCache()
		{
			_cache = new Dictionary<LookupTypes, List<LookupItem>>();
		}

		public static void ClearCache(LookupTypes lookupType)
		{
			if (_cache.ContainsKey(lookupType))
				_cache.Remove(lookupType);
		}

		/// <summary>
		/// Get a list of lookup items for the lookuptype
		/// </summary>
		/// <param name="lookupType"></param>
		/// <param name="whereClause"></param>
		/// <returns></returns>
		public List<LookupItem> GetItems(LookupTypes lookupType, string whereClause = "")
		{
			var items = new List<LookupItem>();
			var info = this.GetInfo(lookupType);

			// Must clear the cache if the user has logged off and logged on again with a different Id.
			if (_userId == string.Empty)
				_userId = Session.User.NetworkId.ToUpper();
			else if (_userId != Session.User.NetworkId.ToUpper())
			{
				ClearCache();
				_userId = Session.User.NetworkId.ToUpper();
			}

			// Get values from cache?
			if (whereClause == string.Empty && info.CacheAllowed && _cache.ContainsKey(lookupType))
				_cache.TryGetValue(lookupType, out items);

			// No items in the cache so get from?
			if (items.Count == 0)
			{
				var data = GetData(lookupType, whereClause);
				if (data != null && data.Rows.Count > 0)
					foreach (DataRow row in data.Rows)
						items.Add(new LookupItem(row.ItemArray[0], row[1].ToString()));

				if (info.CacheAllowed)
					_cache.Add(lookupType, items);
			}

			return items;
		}

        public LookupSourceInfo GetInfo(LookupTypes lookupType)
        {
            LookupSourceInfo info = null;
            _lookupSourceInfo.TryGetValue(lookupType, out info);
            return info;
        }

		private DataTable GetData(LookupTypes lookupType, string whereClause = "")
		{
			var table = new DataTable();
			if (_factory.OpenConnection())
				try
				{
					var info = GetInfo(lookupType);
					if (info != null)
						table = _factory.CreateTable(info.Sql.Replace("<whereClause>", whereClause));

				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "GetData: " + lookupType.ToString());
				}
			return table;
		}

    }

	public sealed class LookupItem
	{
		public object Id { get; internal set; }
		public string Description { get; internal set; }
		public object AltDescription { get; internal set; }

		public LookupItem()
		{
		}

		public LookupItem(object id, string description)
		{
			Id = id;
			Description = description;
			AltDescription = description;
		}

		public LookupItem(object id, object altDescription, string description)
		{
			Id = id;
			AltDescription = altDescription;
			Description = description;
		}
	}

    public sealed class LookupSourceInfo
    {
        public string Sql { get; set; }
        public string ValueMember { get; set; }
        public string DisplayMember { get; set; }
        public bool UseValueMemberAsDisplayMember { get; set; }
		public bool CacheAllowed { get; set; }

        public LookupSourceInfo()
        {
        }

		public LookupSourceInfo(string sql, string valueMember, string displayMember, bool cacheAllowed = false)
        {
            Sql = sql;
            ValueMember = valueMember;
            DisplayMember = displayMember;
			UseValueMemberAsDisplayMember = false;
			CacheAllowed = cacheAllowed;
        }

		public LookupSourceInfo(string sql, string valueMember, string displayMember, bool useValueMemberAsDisplayMember, bool cacheAllowed = false)
        {
            Sql = sql;
            ValueMember = valueMember;
            DisplayMember = displayMember;
			UseValueMemberAsDisplayMember = useValueMemberAsDisplayMember;
			CacheAllowed = cacheAllowed;
        }
    }
}

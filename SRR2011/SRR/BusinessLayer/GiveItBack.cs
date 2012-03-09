/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 *	Give It Back consolidation class, used to to display values for user information
 *	Used in the GiveItBackConfirmation dialog.  Any changes to properties should be reflected
 *	in the GiveItBackConfirmation dialog form.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Disney.iDash.LocalData;

namespace Disney.iDash.SRR.BusinessLayer
{
	public class GiveItBack
	{
		public StockItem StockItem { get; private set; }

		/// <summary>
		/// Current & New Total Stock Required.
		/// </summary>
		public decimal CurrentTSR { get;  set; }
		public decimal NewTSR { get;  set; }
        public decimal RingFenced { get; set; }
        public decimal DCStock { get; set; }
        public decimal StoreSOH { get; set; }
        public string  Description { get; set; }

		public GiveItBack()
		{
			StockItem = new StockItem();
			StockItem.Clear();
			CurrentTSR = 0;
			NewTSR = 0;
		}

		public string Item
		{
			get {return StockItem.ToString();}
		}

		public decimal GiveItBackValue
		{
			get 
			{
                // Max of (110 - 11, 0) -> 99
                // Min of (99, 12) -> 12
                decimal giveItBackVal = 0;

                //if (Session.Environment.Domain == "SWNA")
                switch (GiveItBackCollection.GiveItBackMethod)
                {
                    case GiveItBackCollection.GiveItBackMethods.None:
                        if (NewTSR == 0)
                        { giveItBackVal = RingFenced; }
                        else
                        { giveItBackVal = Math.Min(Math.Max(CurrentTSR - NewTSR, 0), RingFenced); }
                        break;
                    case GiveItBackCollection.GiveItBackMethods.RingFenced:
                        if (NewTSR == 0)
                        { giveItBackVal = RingFenced; }
                        else
                        { giveItBackVal = Math.Min(Math.Max(CurrentTSR - NewTSR, 0), RingFenced); }
                        break;
                    case GiveItBackCollection.GiveItBackMethods.StoreSOH:
                        if (NewTSR == 0)
                        { giveItBackVal = StoreSOH; }
                        else
                        { giveItBackVal = Math.Min(Math.Max(CurrentTSR - NewTSR, 0), StoreSOH); }   
                        break;
                    default:
                        break;
                }

                giveItBackVal = (giveItBackVal < 0 ? 0 : giveItBackVal);

				return giveItBackVal;
			}
		}

		public bool SetValues(DataRow row, decimal newTSR)
		{
			var result = false;
			var giveItBack = (row[DetailedWorkbenchInfo.colGiveItBack] ?? "N").ToString();
			var currentTSR = Convert.ToDecimal(row[DetailedWorkbenchInfo.colOriginalStockRequired] ?? 0m);
            var dcStock = Convert.ToDecimal(row[DetailedWorkbenchInfo.colDCStock ] ?? 0m);
            var storeSOH = Convert.ToDecimal(row[DetailedWorkbenchInfo.colStoreSOH ] ?? 0m);

			//if (giveItBack == "Y" && currentTSR > 0)
            if (giveItBack == "Y")
			{
				StockItem.Class = (decimal) (row[DetailedWorkbenchInfo.colClass] ?? 0m);
				StockItem.Vendor = (decimal)(row[DetailedWorkbenchInfo.colVendor] ?? 0m);
				StockItem.Style = (decimal)(row[DetailedWorkbenchInfo.colStyle] ?? 0m);
				StockItem.Colour = (decimal)(row[DetailedWorkbenchInfo.colColour] ?? 0m);
				StockItem.Size = (decimal)(row[DetailedWorkbenchInfo.colSize] ?? 0m);
                Description = row[DetailedWorkbenchInfo.colDescription].ToString();
                RingFenced = (decimal) (row[DetailedWorkbenchInfo.colRingFenced] ?? 0m);
				CurrentTSR = currentTSR;
				NewTSR = newTSR;
                DCStock = dcStock;
                StoreSOH = storeSOH;
				result = true;
			}

			return result;
		}

	}
    

	public class GiveItBackCollection : IEnumerable<GiveItBack>
	{
		private List<GiveItBack> _items = new List<GiveItBack>();

		public enum GiveItBackMethods
		{
			None,
			RingFenced,
			StoreSOH
		}

		public static GiveItBackMethods _giveItBackMethod = GiveItBackMethods.None;

		/// <summary>
		/// Determines which field is used to calculate the GiveItBack
		/// </summary>
		public static GiveItBackMethods GiveItBackMethod
		{
			get { return _giveItBackMethod; }
		}

		/// <summary>
		/// Must call this before adding items to GiveItBack collection.
		/// </summary>
		/// <param name="sysInfo"></param>
		/// <returns></returns>
		public static bool SetGiveItBackMode(SysInfo sysInfo)
		{
			var result = false;
			if (_giveItBackMethod == GiveItBackMethods.None)
			{
				var constValue = sysInfo.GetConstant("RING FENCED DC STOCK ENVIRO");
				if (constValue != string.Empty)
				{
					if (constValue == "Y")
						_giveItBackMethod = GiveItBackMethods.RingFenced;
					else
						_giveItBackMethod = GiveItBackMethods.StoreSOH;

					result = true;
				}
			}
			else
				result = true;

			return result;
		}

		/// <summary>
		///  Clear list of GiveItBack Items
		/// </summary>
		public void Clear()
		{
			_items.Clear();
		}

		/// <summary>
		/// Add a new GiveItBack item or update an existing one.
		/// </summary>
		/// <param name="newItem"></param>
		public void AddItem(GiveItBack newItem)
		{
			if (newItem != null)
			{
				var item = _items.Find((i)=>(i.Item == newItem.Item));
				if (item != null)
					item.NewTSR += newItem.NewTSR;
				else
					_items.Add(newItem);
			}
		}

		/// <summary>
		/// Return the number of GiveItBack items in the collection.
		/// </summary>
		/// <returns></returns>
		public int Count()
		{
			return _items.Count;
		}

		/// <summary>
		/// Sort the GiveItBack items by Item (class, vendor, style, colour, size)
		/// </summary>
		public void Sort()
		{
			_items.Sort((a, b) => (a.Item.CompareTo(b.Item)));
		}

		public IEnumerator<GiveItBack> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
		
		public GiveItBack this[int index]
		{
			get
			{
				if (index >= 0 && index < _items.Count)
					return _items[index];
				else
					return null;
			}
			set
			{
				if (index >= 0 && index < _items.Count)
					_items[index] = value;
			}
		}

		public List<GiveItBack> ToList()
		{
			return _items.ToList();
		}



	}
}

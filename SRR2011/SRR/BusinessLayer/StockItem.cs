/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Handles all logic relating to a stock item structure.
 * 
 * Note a stock item is a hierarchial structure with Class the highest level and Size the lowest.  At each point
 * if the current level is not supplied then all lower levels are nulled.  Likewise, the class will prevent assignmment
 * to a lower level if the preceeding level is unassigned.
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using Disney.iDash.DataLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class StockItem : ICloneable
    {
        private decimal? _class = 0;
        private decimal? _vendor = 0;
        private decimal? _style = 0;
        private decimal? _colour = 0;
        private decimal? _size = 0;

        private DB2Factory _factory = new DB2Factory();
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();

        public StockItem()
        {
            _factory.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
                });
            _factory.ExceptionHandler.AlertEvent += ((message, caption, alertType)=>
                {
                    ExceptionHandler.RaiseAlert(message, caption, alertType);
                });
            Clear();
            
        }

        /// <summary>
        /// Create a new stock item based on the individual components
        /// </summary>
        /// <param name="itemClass"></param>
        /// <param name="itemVendor"></param>
        /// <param name="itemStyle"></param>
        /// <param name="itemColour"></param>
        /// <param name="itemSize"></param>
        public StockItem(decimal? itemClass, decimal? itemVendor, decimal? itemStyle, decimal? itemColour, decimal? itemSize)
        {
            Clear();
            _class = itemClass;
            _vendor = itemVendor;
            _style = itemStyle;
            _colour = itemColour;
            _size = itemSize;
        }

        public StockItem(string appItemNo)
        {
            Clear();

            var itemSections = appItemNo.Split(new char[] { '-' });
            if (itemSections.Length == 5)
            {
                _class = Convert.ToDecimal(itemSections[0]);
                _vendor = Convert.ToDecimal(itemSections[1]);
                _style = Convert.ToDecimal(itemSections[2]);
                _colour = Convert.ToDecimal(itemSections[3]);
                _size = Convert.ToDecimal(itemSections[4]);
            }
        }

        public StockItem(DataRow row)
        {
            Clear();
            if (row.Table.Columns.Contains("CLASS"))
                _class = (decimal) row["CLASS"];
            if (row.Table.Columns.Contains("VENDOR"))
                _vendor = (decimal)row["VENDOR"];
            if (row.Table.Columns.Contains("STYLE"))
                _style = (decimal)row["STYLE"];
            if (row.Table.Columns.Contains("COLOUR"))
                _colour = (decimal)row["COLOUR"];
            if (row.Table.Columns.Contains("SIZE"))
                _size = (decimal)row["SIZE"];
        }

        public decimal? Class
        {
            get { return _class; }
            set            
            { 
                _class = value;

                if (!_class.HasValue)
                    Vendor = null;
            }
        }

        public decimal? Vendor
        {
            get { return _vendor; }
            set 
            {
                if (!_class.HasValue)
                    _vendor = null;
                else
                    _vendor = value;
                if (!_vendor.HasValue)
                    Style = null;
            }
        }

        public decimal? Style
        {
            get { return _style; }
            set 
            {
                if (!_vendor.HasValue)
                    _style = null;
                else
                    _style = value;
                if (!_style.HasValue)
                    Colour = null;
            }
        }

        public decimal? Colour
        {
            get { return _colour; }
            set 
            {
                if (!_style.HasValue)
                    _colour = null;
                else
                    _colour = value;
                if (!_colour.HasValue)
                    Size = null;
            }
        }

        public decimal? Size
        {
            get { return _size; }
            set 
            {
                if (!_colour.HasValue)
                    _size = null;
                else
                    _size = value;
            }
        }
                                       
        public string UPC { get; set; }

        public bool HasValue
        {
            get
            {
                return (Class.HasValue || Vendor.HasValue || Style.HasValue || Colour.HasValue || Size.HasValue);
            }
        }

		public bool HasAllValues
		{
			get
			{
				return (Class.HasValue && Vendor.HasValue && Style.HasValue && Colour.HasValue && Size.HasValue);
			}
		}
		
		public void Clear()
        {
            _class = null;
            _vendor = null;
            _style = null;
            _colour = null;
            _size = null;
        }

        /// <summary>
        /// ToString will return the item as 0000-00000-0000-000-0000 or without the '-' if raw parameter is true.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public string ToString(bool raw = false)
        {
            var items = new List<string>();

            if (Class.HasValue)
                items.Add(string.Format("{0:0000}", Class));
            else
                items.Add("0000");

            if (Vendor.HasValue)
                items.Add(string.Format("{0:00000}", Vendor));
            else
                items.Add("00000");

            if (Style.HasValue)
                items.Add(string.Format("{0:0000}", Style));
            else
                items.Add("0000");

            if (Colour.HasValue)
                items.Add(string.Format("{0:000}", Colour));
            else
                items.Add("000");

            if (Size.HasValue)
                items.Add(string.Format("{0:0000}", Size));
            else
                items.Add("0000");

            if (raw)
                return string.Join("", items);
            else
                return string.Join("-", items);
        }

        public object Clone()
        {
            return (StockItem)this.MemberwiseClone();
        }

        public string GetDescription()
        {
            var result = string.Empty;
            var sql = "ICLS=<class> AND IVEN=<vendor> AND ISTY=<style> AND ICLR=<colour> and ISIZ=<size>"
                .Replace("<class>", this.Class.GetValueOrDefault(0).ToString())
                .Replace("<vendor>", this.Vendor.GetValueOrDefault(0).ToString())
                .Replace("<style>", this.Style.GetValueOrDefault(0).ToString())
                .Replace("<colour>", this.Colour.GetValueOrDefault(0).ToString())
                .Replace("<size>", this.Size.GetValueOrDefault(0).ToString());

            if (this.HasValue)
                result = _factory.GetValue("IPITHDR", "IDES", sql, string.Empty).ToString();

            _factory.CloseConnection();
            return result;
        }

        public string GetUPC()
        {
            var result = string.Empty;
            var sql = "ICLS=<class> AND IVEN=<vendor> AND ISTY=<style> AND ICLR=<colour> and ISIZ=<size>"
                .Replace("<class>", this.Class.GetValueOrDefault(0).ToString())
                .Replace("<vendor>", this.Vendor.GetValueOrDefault(0).ToString())
                .Replace("<style>", this.Style.GetValueOrDefault(0).ToString())
                .Replace("<colour>", this.Colour.GetValueOrDefault(0).ToString())
                .Replace("<size>", this.Size.GetValueOrDefault(0).ToString());

            if (this.HasValue)
                result = _factory.GetValue("IPITHDR", "IUPD", sql, string.Empty).ToString();

            _factory.CloseConnection();
            return result;
        }

        public bool IsValid()
        {
            var result = false;
            var sql = "ICLS=<class> AND IVEN=<vendor> AND ISTY=<style> AND ICLR=<colour> and ISIZ=<size>"
                .Replace("<class>", this.Class.GetValueOrDefault(0).ToString())
                .Replace("<vendor>", this.Vendor.GetValueOrDefault(0).ToString())
                .Replace("<style>", this.Style.GetValueOrDefault(0).ToString())
                .Replace("<colour>", this.Colour.GetValueOrDefault(0).ToString())
                .Replace("<size>", this.Size.GetValueOrDefault(0).ToString());

            if (this.HasValue)
                result = _factory.LookupKey("IPITHDR", sql);

            _factory.CloseConnection();
            return result;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Disney.iDash.LocalData;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class StoreGroupsCollection : BusinessBase
    {
        private List<StoreGroup> _items = new List<StoreGroup>();

        public bool Refresh()
        {
            var result = false;
            _items.Clear();
            if (base.Factory.OpenConnection())
                try
                {
                    var table = base.Factory.CreateTable(Properties.Resources.SQLStoreGroupsSelect);
                    _items = base.Factory.PopulateProperties<StoreGroup>(table, typeof(StoreGroup), new DataLayer.DB2Factory.CustomConversionDelegate(ConvertType));
                    result = true;
                    IsDirty = false;
                }
                catch (Exception ex)
                {
                    base.ExceptionHandler.RaiseException(ex, "Refresh");
                }
                finally
                {
                    base.Factory.CloseConnection();
                }
            return result;
        }

        public bool Save()
        {
            var result = !base.IsDirty;

            if (base.IsDirty && base.Factory.OpenConnection())
            {
                var tran = base.Factory.BeginTransaction();

                var cmd = base.Factory.CreateCommand("DS888GS1", CommandType.StoredProcedure,
                    base.Factory.CreateParameter("In_SGpID", string.Empty, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                    base.Factory.CreateParameter("In_SGpNam", string.Empty, System.Data.OleDb.OleDbType.Char, 30, ParameterDirection.Input),
                    base.Factory.CreateParameter("In_SGpChU", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                    base.Factory.CreateParameter("In_SGpChD", System.DateTime.Now.ToString(), System.Data.OleDb.OleDbType.DBTimeStamp, 26, ParameterDirection.Input),
                    base.Factory.CreateParameter("In_Insert", 0, System.Data.OleDb.OleDbType.Decimal, 1, 0, ParameterDirection.Input));
                
                cmd.Transaction = tran;

                try
                {
                    var changes = _items.Count((i) => (i.Changed));
                    var index = 0;
                    foreach (var item in _items.Where((i) => (i.Changed)))
                    {
                        OnProgress("Saving", (int) (100.00 * ++index) / changes);
                        cmd.Parameters["In_SGpID"].Value = item.Id;
                        cmd.Parameters["In_SGpNam"].Value = item.Description;
                        cmd.Parameters["In_Insert"].Value = (item.Selected ? 1m : 0m);
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    if (tran != null && tran.Connection != null)
                        tran.Rollback();
                    base.ExceptionHandler.RaiseException(ex, "Save");
                }
            }

            return result;
        }

        public List<StoreGroup> Items
        {
            get 
            {
                return _items; 
            }
        }

        private object ConvertType(PropertyInfo property, object value)
        {
            object result = null;
            if (property.PropertyType == typeof(Boolean) && (value.GetType() == typeof(string) || value.GetType() == typeof(Int32)))
                result = value != null && (value.ToString().Contains('1') || value.ToString().ToUpper().Contains('Y'));
            else if (property.PropertyType == typeof(DateTime?) && (value.GetType() == typeof(DateTime)))
                result = value;
            return result;
        }
    }

    public class StoreGroup 
    {
        private bool _selected = false;
        private bool? _originalSelected = null;

        public string Id { get; set; }
        public string Description { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        
        public bool Selected 
        {
            get { return _selected; }
            set 
            {
                if (!_originalSelected.HasValue)
                    _originalSelected = value;

                _selected = value; 
            }
        }

        public bool Changed 
        {
            get { return _originalSelected.HasValue && _selected != _originalSelected.Value; }
        }

    }
    
}

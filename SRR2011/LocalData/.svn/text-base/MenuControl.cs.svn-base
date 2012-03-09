using System;
using System.Linq;
using Disney.iDash.Shared;
using System.Collections.Generic;

namespace Disney.iDash.LocalData
{
    public sealed class MenuControl
    {
        private LocalDataEntities _entities = null;
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();
        private List<UserMenuOption> _menuOptions = new List<UserMenuOption>();

        public bool Refresh(string networkId)
        {
            var refreshed = false;
            try
            {

                _menuOptions.Clear();
                _entities = new LocalDataEntities(Session.LocalDataConnection);

                // Add list of restricted options the user has been authorised to use.
                var qry = from user in _entities.eUsers
                          where user.NetworkId == networkId
                          from option in user.vMenuOptions
                          where option.IsRestricted == true
                          select option;               

                foreach (var option in qry.ToList())
                    _menuOptions.Add(new UserMenuOption { ApplicationId = option.ApplicationId, Tab = option.Tab.ToLower(), MenuOption = option.MenuOption.ToLower(), IsRestricted = true });

                // Add list of non-restricted options all users can use.
                qry = from o in _entities.eMenuOptions
                      where o.IsRestricted == false
                      select o;

                foreach (var option in qry.ToList())
                    _menuOptions.Add(new UserMenuOption { ApplicationId = option.ApplicationId, Tab = option.Tab.ToLower(), MenuOption = option.MenuOption.ToLower(), IsRestricted = false });


                refreshed = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "Refresh");
            }

            return refreshed;
        }

        public bool IsAuthorised(int applicationId, string tab, string menuOption)
        {
            /*var qry = from o in _menuOptions
                      where o.ApplicationId == applicationId
                        && o.Tab == tab.ToLower()
                        && o.MenuOption == menuOption.ToLower()
                      select o;
            */

            var qry1 = from apps in Session.User.vApplications
                      select apps;

            var data = qry1.ToList<eApplication>();
            for (int j = 0; j < data.Count(); j++)
            {                
                var qry2 = from o in _menuOptions
                           where o.ApplicationId == data[j].Id
                            && o.Tab == tab.ToLower()
                            && o.MenuOption == menuOption.ToLower()
                           select o;

                if (qry2.FirstOrDefault() != null)
                {
                    return true;
                }
            }

            return false;           
        }
    }

    internal class UserMenuOption
    {
        internal int ApplicationId { get; set; }
        internal string Tab { get; set; }
        internal string MenuOption { get; set; }
        internal bool IsRestricted { get; set; }
    }

}

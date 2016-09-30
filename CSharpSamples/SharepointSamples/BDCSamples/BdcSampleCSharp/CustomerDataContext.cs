// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BdcSampleCSharp
{
	public partial class CustomerDataContext
	{
        public CustomerDataContext() :
            base(global::BdcSampleCSharp.Settings.Default.NORTHWNDConnectionString, mappingSource)
        {
            OnCreated();
        }
	}
}

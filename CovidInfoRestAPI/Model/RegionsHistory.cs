//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CovidWPolsce_RestAPI.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RegionsHistory
    {
        public System.DateTime Date { get; set; }
        public string RegionID { get; set; }
        public System.DateTime Timestamp { get; set; }
        public Nullable<int> TotalCases { get; set; }
        public Nullable<int> NewCases { get; set; }
        public Nullable<int> TotalRecoveries { get; set; }
        public Nullable<int> NewRecoveries { get; set; }
        public Nullable<int> TotalDeaths { get; set; }
        public Nullable<int> NewDeaths { get; set; }
        public Nullable<int> TotalTests { get; set; }
        public Nullable<int> NewTests { get; set; }
        public Nullable<int> ActiveCases { get; set; }
    
        public virtual Region Regions { get; set; }
    }
}

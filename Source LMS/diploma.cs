//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class diploma
    {
        public int dip_id { get; set; }
        [DisplayName("Diploma Name")]
        public string dip_name { get; set; }
        [DisplayName("Duration")]
        public string dip_duration { get; set; }
        [DisplayName("Package")]
        public decimal dip_package { get; set; }
    }
}

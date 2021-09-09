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
    using System.ComponentModel.DataAnnotations;

    public partial class teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public teacher()
        {
            this.attendance = new HashSet<attendance>();
            this.batch = new HashSet<batch>();
        }
        [DisplayName("Teacher ID")]
        public string teach_id { get; set; }
        [DisplayName("Teacher Name")]
        public string teach_name { get; set; }
        [DisplayName("Email Address"), DataType(DataType.EmailAddress), RegularExpression(@"/ ^[a - zA - Z0 - 9.!#$%&'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]+$/",
        ErrorMessage = "Please enter a valid email address")]
        public string teach_email { get; set; }
        [DisplayName("Address")]
        public string teach_address { get; set; }
        [DisplayName("Contact Number"), RegularExpression("/^[0-9]{4}([-])?([0-9]{7})?$/", ErrorMessage = "Please enter a Pakistani number")]
        public long teach_contact { get; set; }
        [DisplayName("Email Address"), DataType(DataType.Currency)]
        public decimal teach_salary { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<attendance> attendance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<batch> batch { get; set; }
    }
}
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

    public partial class student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public student()
        {
            this.attendance = new HashSet<attendance>();
            this.examAttend = new HashSet<examAttend>();
        }
        [DisplayName("Student Enrollment"), Required(ErrorMessage = "Please fill this field")]
        public string stud_enrollment { get; set; }
        [DisplayName("Student Name"), Required(ErrorMessage = "Please fill this field")]
        public string stud_name { get; set; }
        [DisplayName("Father Name"), Required(ErrorMessage = "Please fill this field")]
        public string stud_fatherName { get; set; }
        [DisplayName("Email Address"), DataType(DataType.EmailAddress)]
        public string stud_email { get; set; }
        [DisplayName("Contact Number"), Required(ErrorMessage = "Please fill this field")]
        public long stud_contact { get; set; }
        [DisplayName("Student Fee"), DataType(DataType.Currency), Required(ErrorMessage = "Please fill this field")]
        public decimal stud_fee { get; set; }
        [DisplayName("Attended Exam")]
        public Nullable<int> stud_attendExam { get; set; }
        [DisplayName("Batch Code"), Required(ErrorMessage = "Please fill this field")]
        public string stud_batch { get; set; }
        [DisplayName("Address"), Required(ErrorMessage = "Please fill this field")]
        public string stud_address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<attendance> attendance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<examAttend> examAttend { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace is_takip_projesi.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblCagrilar
    {
        public int ID { get; set; }
        public Nullable<int> CagriFirma { get; set; }
        public string Konu { get; set; }
        public string Açıklama { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<bool> Durum { get; set; }
    
        public virtual TblFirmalar TblFirmalar { get; set; }
    }
}

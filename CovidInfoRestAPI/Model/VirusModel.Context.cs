﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CovidContext : DbContext
    {
        public CovidContext()
            : base("name=CovidContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries1 { get; set; }
        public virtual DbSet<CountriesHistory> CountriesHistory { get; set; }
        public virtual DbSet<Region> Regions1 { get; set; }
        public virtual DbSet<RegionsHistory> RegionsHistory { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<CountryNews> CountryNews1 { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }
}
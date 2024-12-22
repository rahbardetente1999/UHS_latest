﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UHSForm.Models.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class UHSEntities : DbContext
    {
        public UHSEntities()
            : base("name=UHSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin_Sub> Admin_Sub { get; set; }
        public virtual DbSet<CarServiceType> CarServiceTypes { get; set; }
        public virtual DbSet<CarType> CarTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAvailability> CustomerAvailabilities { get; set; }
        public virtual DbSet<CustomerCarServiceDetail> CustomerCarServiceDetails { get; set; }
        public virtual DbSet<CustomerComplaint> CustomerComplaints { get; set; }
        public virtual DbSet<CustomerConditionalType> CustomerConditionalTypes { get; set; }
        public virtual DbSet<CustomerCustomDateTime> CustomerCustomDateTimes { get; set; }
        public virtual DbSet<CustomerDateBlock> CustomerDateBlocks { get; set; }
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public virtual DbSet<CustomerInovice> CustomerInovices { get; set; }
        public virtual DbSet<CustomerIssueType> CustomerIssueTypes { get; set; }
        public virtual DbSet<CustomerOfficalDetail> CustomerOfficalDetails { get; set; }
        public virtual DbSet<CustomerOtherProperty> CustomerOtherProperties { get; set; }
        public virtual DbSet<CustomerPaymentStatu> CustomerPaymentStatus { get; set; }
        public virtual DbSet<CustomerPricing> CustomerPricings { get; set; }
        public virtual DbSet<CustomerRenewalMonth> CustomerRenewalMonths { get; set; }
        public virtual DbSet<CustomerSpecializedCleaning> CustomerSpecializedCleanings { get; set; }
        public virtual DbSet<CustomerTimeBlock> CustomerTimeBlocks { get; set; }
        public virtual DbSet<CustomerTimeline> CustomerTimelines { get; set; }
        public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<FileUse> FileUses { get; set; }
        public virtual DbSet<IncExclu> IncExclus { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MainCategory> MainCategories { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Pricing> Pricings { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyArea> PropertyAreas { get; set; }
        public virtual DbSet<PropertyResidenceType> PropertyResidenceTypes { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<RefIncExclu> RefIncExclus { get; set; }
        public virtual DbSet<RefServiceSlot> RefServiceSlots { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<ServiceSlot> ServiceSlots { get; set; }
        public virtual DbSet<ServiceSubCategory> ServiceSubCategories { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<StaffCustomerRating> StaffCustomerRatings { get; set; }
        public virtual DbSet<StaffService> StaffServices { get; set; }
        public virtual DbSet<StaffTeam> StaffTeams { get; set; }
        public virtual DbSet<SubArea> SubAreas { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Support> Supports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VehicleColor> VehicleColors { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Venture> Ventures { get; set; }
        public virtual DbSet<CarWashTimeRange> CarWashTimeRanges { get; set; }
        public virtual DbSet<TeamType> TeamTypes { get; set; }
        public virtual DbSet<ApprovalStatu> ApprovalStatus { get; set; }
        public virtual DbSet<CustomerSupport> CustomerSupports { get; set; }
        public virtual DbSet<CustomerSupportActionFor> CustomerSupportActionFors { get; set; }
        public virtual DbSet<CustomerSupportServiceType> CustomerSupportServiceTypes { get; set; }
        public virtual DbSet<CustomerSupportTicketType> CustomerSupportTicketTypes { get; set; }
        public virtual DbSet<CustomerAlert> CustomerAlerts { get; set; }
        public virtual DbSet<CustomerAltersType> CustomerAltersTypes { get; set; }
    
        public virtual int procLogs_Insert(Nullable<System.DateTime> log_date, string log_thread, string log_level, string log_source, string log_message, string username, string iPAddress, string exception)
        {
            var log_dateParameter = log_date.HasValue ?
                new ObjectParameter("log_date", log_date) :
                new ObjectParameter("log_date", typeof(System.DateTime));
    
            var log_threadParameter = log_thread != null ?
                new ObjectParameter("log_thread", log_thread) :
                new ObjectParameter("log_thread", typeof(string));
    
            var log_levelParameter = log_level != null ?
                new ObjectParameter("log_level", log_level) :
                new ObjectParameter("log_level", typeof(string));
    
            var log_sourceParameter = log_source != null ?
                new ObjectParameter("log_source", log_source) :
                new ObjectParameter("log_source", typeof(string));
    
            var log_messageParameter = log_message != null ?
                new ObjectParameter("log_message", log_message) :
                new ObjectParameter("log_message", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var iPAddressParameter = iPAddress != null ?
                new ObjectParameter("IPAddress", iPAddress) :
                new ObjectParameter("IPAddress", typeof(string));
    
            var exceptionParameter = exception != null ?
                new ObjectParameter("exception", exception) :
                new ObjectParameter("exception", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procLogs_Insert", log_dateParameter, log_threadParameter, log_levelParameter, log_sourceParameter, log_messageParameter, usernameParameter, iPAddressParameter, exceptionParameter);
        }
    
        public virtual int SPmyCreateCustomer(string password, string name, string email, string mobile, string alternativeNo, string whatsAppNo, string salutaion, Nullable<int> createdRole, Nullable<int> role, string gender, Nullable<int> uID, ObjectParameter responseMessage)
        {
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            var alternativeNoParameter = alternativeNo != null ?
                new ObjectParameter("alternativeNo", alternativeNo) :
                new ObjectParameter("alternativeNo", typeof(string));
    
            var whatsAppNoParameter = whatsAppNo != null ?
                new ObjectParameter("whatsAppNo", whatsAppNo) :
                new ObjectParameter("whatsAppNo", typeof(string));
    
            var salutaionParameter = salutaion != null ?
                new ObjectParameter("salutaion", salutaion) :
                new ObjectParameter("salutaion", typeof(string));
    
            var createdRoleParameter = createdRole.HasValue ?
                new ObjectParameter("CreatedRole", createdRole) :
                new ObjectParameter("CreatedRole", typeof(int));
    
            var roleParameter = role.HasValue ?
                new ObjectParameter("Role", role) :
                new ObjectParameter("Role", typeof(int));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var uIDParameter = uID.HasValue ?
                new ObjectParameter("uID", uID) :
                new ObjectParameter("uID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPmyCreateCustomer", passwordParameter, nameParameter, emailParameter, mobileParameter, alternativeNoParameter, whatsAppNoParameter, salutaionParameter, createdRoleParameter, roleParameter, genderParameter, uIDParameter, responseMessage);
        }
    
        public virtual int SPmyCreateStaff(string userName, string password, string name, string mobile, string email, string country, Nullable<int> phonecode, Nullable<int> createdRole, Nullable<int> role, Nullable<int> uID, Nullable<int> suID, Nullable<bool> isActive, Nullable<bool> isDelete, string createdBy, ObjectParameter responseMessage)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("country", country) :
                new ObjectParameter("country", typeof(string));
    
            var phonecodeParameter = phonecode.HasValue ?
                new ObjectParameter("phonecode", phonecode) :
                new ObjectParameter("phonecode", typeof(int));
    
            var createdRoleParameter = createdRole.HasValue ?
                new ObjectParameter("CreatedRole", createdRole) :
                new ObjectParameter("CreatedRole", typeof(int));
    
            var roleParameter = role.HasValue ?
                new ObjectParameter("Role", role) :
                new ObjectParameter("Role", typeof(int));
    
            var uIDParameter = uID.HasValue ?
                new ObjectParameter("uID", uID) :
                new ObjectParameter("uID", typeof(int));
    
            var suIDParameter = suID.HasValue ?
                new ObjectParameter("suID", suID) :
                new ObjectParameter("suID", typeof(int));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var isDeleteParameter = isDelete.HasValue ?
                new ObjectParameter("IsDelete", isDelete) :
                new ObjectParameter("IsDelete", typeof(bool));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPmyCreateStaff", userNameParameter, passwordParameter, nameParameter, mobileParameter, emailParameter, countryParameter, phonecodeParameter, createdRoleParameter, roleParameter, uIDParameter, suIDParameter, isActiveParameter, isDeleteParameter, createdByParameter, responseMessage);
        }
    
        public virtual int SPmyCreateSubUser(string userName, string password, string name, string email, string mobile, string country, Nullable<int> phonecode, Nullable<int> createdRole, Nullable<int> role, string gender, Nullable<int> uID, ObjectParameter responseMessage)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("country", country) :
                new ObjectParameter("country", typeof(string));
    
            var phonecodeParameter = phonecode.HasValue ?
                new ObjectParameter("phonecode", phonecode) :
                new ObjectParameter("phonecode", typeof(int));
    
            var createdRoleParameter = createdRole.HasValue ?
                new ObjectParameter("CreatedRole", createdRole) :
                new ObjectParameter("CreatedRole", typeof(int));
    
            var roleParameter = role.HasValue ?
                new ObjectParameter("Role", role) :
                new ObjectParameter("Role", typeof(int));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var uIDParameter = uID.HasValue ?
                new ObjectParameter("uID", uID) :
                new ObjectParameter("uID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPmyCreateSubUser", userNameParameter, passwordParameter, nameParameter, emailParameter, mobileParameter, countryParameter, phonecodeParameter, createdRoleParameter, roleParameter, genderParameter, uIDParameter, responseMessage);
        }
    
        public virtual int SPmyCreateUser(string userName, string password, string name, string email, string mobile, string country, Nullable<int> phonecode, Nullable<int> createdRole, Nullable<int> role, string gender, string usertype, ObjectParameter responseMessage)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var mobileParameter = mobile != null ?
                new ObjectParameter("mobile", mobile) :
                new ObjectParameter("mobile", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("country", country) :
                new ObjectParameter("country", typeof(string));
    
            var phonecodeParameter = phonecode.HasValue ?
                new ObjectParameter("phonecode", phonecode) :
                new ObjectParameter("phonecode", typeof(int));
    
            var createdRoleParameter = createdRole.HasValue ?
                new ObjectParameter("CreatedRole", createdRole) :
                new ObjectParameter("CreatedRole", typeof(int));
    
            var roleParameter = role.HasValue ?
                new ObjectParameter("Role", role) :
                new ObjectParameter("Role", typeof(int));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var usertypeParameter = usertype != null ?
                new ObjectParameter("usertype", usertype) :
                new ObjectParameter("usertype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPmyCreateUser", userNameParameter, passwordParameter, nameParameter, emailParameter, mobileParameter, countryParameter, phonecodeParameter, createdRoleParameter, roleParameter, genderParameter, usertypeParameter, responseMessage);
        }
    
        public virtual int SPmyLogin(string pMode, string userName, string password, string pOldPassword, ObjectParameter responseMessage)
        {
            var pModeParameter = pMode != null ?
                new ObjectParameter("pMode", pMode) :
                new ObjectParameter("pMode", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var pOldPasswordParameter = pOldPassword != null ?
                new ObjectParameter("pOldPassword", pOldPassword) :
                new ObjectParameter("pOldPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPmyLogin", pModeParameter, userNameParameter, passwordParameter, pOldPasswordParameter, responseMessage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Paw.Services.Identity;

namespace Paw.Services.Common
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            // this.Configuration.LazyLoadingEnabled = false;
        }

        #region DbSets ...

        public DbSet<Breed> BreedSet { get; set; }

        public DbSet<Chit> ChitSet { get; set; }

        public DbSet<Dimension> DimensionSet { get; set; }

        public DbSet<Employee> EmployeeSet { get; set; }

        public DbSet<Gender> GenderSet { get; set; }

        public DbSet<InvoiceItem> InvoiceItemSet { get; set; }

        public DbSet<Invoice> InvoiceSet { get; set; }

        public DbSet<Owner> OwnerSet { get; set; }

        public DbSet<Pet> PetSet { get; set; }

        public DbSet<PetReservation> PetReservationSet { get; set; }

        public DbSet<ProviderGroup> ProviderGroupSet { get; set; }

        public DbSet<Provider> ProviderSet { get; set; }

        public DbSet<Reservation> ReservationSet { get; set; }

        public DbSet<Resource> ResourceSet { get; set; }

        public DbSet<Role> RoleSet { get; set; }

        public DbSet<ScheduleBlock> ScheduleBlockSet { get; set; }

        public DbSet<ScheduleRule> ScheduleRuleSet { get; set; }
        
        public DbSet<SchedulerEvent> SchedulerEventSet { get; set; }

        public DbSet<SchedulerEventPet> SchedulerEventPetSet { get; set; }

        public DbSet<SchedulerEventType> ScheduerEventTypeSet { get; set; }

        public DbSet<SchedulerType> SchedulerTypeSet { get; set; }

        public DbSet<SkuCategory> SkuCategorySet { get; set; }

        public DbSet<Sku> SkuSet { get; set; }

        public DbSet<Timezone> TimezoneSet { get; set; }
        
        public DbSet<UserClaim> UserClaimSet { get; set; }

        public DbSet<User> UserSet { get; set; }

        public DbSet<UserLogin> UserLoginSet { get; set; }

        public DbSet<UserRole> UserRoleSet { get; set; }
        
        #endregion

        #region DbModelBuilder ...

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            // Naming Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Key Mapping
            modelBuilder.Properties<int>()
                .Where(x => x.Name == "Id")
                .Configure(p => p.IsKey());

           
            // Mapping Conventions
            modelBuilder.Properties<DateTime>().Where(x => IsDate(x)).Configure(p => p.HasColumnType("date"));
            modelBuilder.Properties<DateTime>().Where(x => !IsDate(x)).Configure(p => p.HasColumnType("datetime2"));
            
            // Asp.Net Identity FK Mappings
            modelBuilder.Entity<User>().HasMany<UserClaim>(c => c.Claims);
            modelBuilder.Entity<User>().HasMany<UserLogin>(l => l.Logins);
            modelBuilder.Entity<User>().HasMany<UserRole>(u => u.Roles);

            // TODO: UserLogin.User property
            // TODO: UserClaim.User property

            // PKs
            modelBuilder.Entity<UserLogin>().HasKey(l => new { UserId = l.UserId, LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey });
            modelBuilder.Entity<UserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId });

            // Properties
            modelBuilder.Entity<User>().Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(254);

            // Sku
            modelBuilder.Entity<Sku>().HasOptional(x => x.Parent).WithMany(x => x.ChildCollection).HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);

            // InvoiceItem
            modelBuilder.Entity<InvoiceItem>().HasOptional(x => x.Parent).WithMany(x => x.ChildCollection).HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);

        }


        #region Predicates ...

        private static bool IsDate(PropertyInfo propertyInfo)
        {
            var dataTypeAttribute = propertyInfo.GetCustomAttribute<DataTypeAttribute>();
            if (dataTypeAttribute == null)
                return false;

            return dataTypeAttribute.DataType == DataType.Date;
        }

        #endregion

        #endregion

        #region Save Changes ...

        public override int SaveChanges()
        {
            DateTime timestamp = DateTime.UtcNow;

            foreach (var entry in this.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case System.Data.Entity.EntityState.Added:
                        Added(entry.Entity, timestamp);
                        break;
                    case System.Data.Entity.EntityState.Deleted:
                        break;
                    case System.Data.Entity.EntityState.Detached:
                        break;
                    case System.Data.Entity.EntityState.Modified:
                        Modified(entry.Entity, timestamp);
                        break;
                    case System.Data.Entity.EntityState.Unchanged:
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChanges();
        }

        public void Added(object added, DateTime timestamp)
        {
            Guid currentId = this.CurrentUserId ?? Guid.Empty;

            if (currentId == Guid.Empty) throw new InvalidOperationException("IAudit User not defined.");

            // Step 1. IAudit
            IAudit audit = added as IAudit;
            if (audit != null)
            {
                audit.CreatedById = currentId;
                audit.UpdatedById = currentId;
                audit.Created = timestamp;
                audit.Updated = timestamp;
            }
        }

        public void Modified(object modifiled, DateTime timestamp)
        {
            Guid currentId = this.CurrentUserId ?? Guid.Empty;
            if (currentId == Guid.Empty) throw new InvalidOperationException("IAudit User not defined.");

            // Step 1. IAudit
            IAudit audit = modifiled as IAudit;
            if (audit != null)
            {
                audit.UpdatedById = currentId;
                audit.Updated = timestamp;
            }
        }

        #endregion

        public static DataContext Create()
        {
            return new DataContext() { CurrentUserId = IdentityHelper.GetCurrentUserId() };
        }

        public static DataContext CreateForMessage(object message)
        {
            // TODO: When creating the data context, cast message and pull properties

            return new DataContext() { CurrentUserId = IdentityHelper.GetCurrentUserId(), Message = message };
        }

        #region Audit ...

        public Guid? CurrentUserId
        {
            get { return _CurrentUserId; }
            set { _CurrentUserId = value; }
        }
        private Guid? _CurrentUserId = null;

        public object Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private object _Message = null;
        
        

        #endregion
    }
}

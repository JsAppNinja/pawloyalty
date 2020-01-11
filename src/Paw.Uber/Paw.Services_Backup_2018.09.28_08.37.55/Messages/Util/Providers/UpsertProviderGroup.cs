using Paw.Services.Common;
using Paw.Services.Messages.Util.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Util.Providers
{
    public class UpsertProviderGroup
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;
        
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        public bool TestFlag
        {
            get { return _TestFlag; }
            set { _TestFlag = value; }
        }
        private bool _TestFlag = false;
        
        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private Guid _UserId = BootstrapUser.UserId;
        

        public int Execute()
        {
            string sql = $@"
                MERGE ProviderGroup AS [Target]
                USING (SELECT '{ProviderGroupId}' AS Id) AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET [Target].Name = '{Name}', CreatedById = '{UserId}', Updated = GETUTCDATE(), UpdatedById = '{UserId}', MessageId = NEWID(), MessageType = 'SqlScript', MachineName = '', TestFlag = '{TestFlag}'
                WHEN NOT MATCHED THEN INSERT(Id, Name, Created, CreatedById, Updated, UpdatedById, MessageId, MessageType, MachineName, TestFlag) VALUES ('{ProviderGroupId}', '{Name}', GETUTCDATE(), '{UserId}', GETUTCDATE(), '{UserId}', NEWID(),  'SqlScript', '', '{TestFlag}');";

            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.Database.ExecuteSqlCommand(sql);
            }
        }

        public static Guid HulaDogProviderGroupId = new Guid("{568E3136-C325-4C62-9B2B-CC528EC6F457}");

        public static Guid BoulderDogProviderGroupId = new Guid("{C308E4C9-E9A1-4099-A948-584260C9DC1A}");
    }
}

using Paw.Services.Common;
using Paw.Services.Messages.Util.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Util.Providers
{
    public class UpsertProvider
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _PhoneNumber = String.Empty;

        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private bool _Status = false;
        

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private Guid _UserId = BootstrapUser.UserId;
        

        public int Execute()
        {
            string sql = $@"
                MERGE Provider AS [Target]
                USING (SELECT '{ProviderId}' AS Id) AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET Name = '{Name}', [Key] ='{Key}', Status = '{Status}', PhoneNumber = '{PhoneNumber}', Created = GETUTCDATE(), CreatedById = '{UserId}', Updated = GETUTCDATE(), UpdatedById = '{UserId}', MessageId = NEWID(), MessageType = 'SqlScript', MachineName = '' 
                WHEN NOT MATCHED THEN INSERT(Id, [Key], Name, Url, PhoneNumber, Status, ProviderGroupId, Created, CreatedById, Updated, UpdatedById, MessageId, MessageType, MachineName) VALUES ('{ProviderId}', '{Key}', '{Name}', '','{PhoneNumber}', '{Status}', '{ProviderGroupId}',  GETUTCDATE(), '{UserId}', GETUTCDATE(), '{UserId}', NEWID(), 'SqlScript', '');
                ";

            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.Database.ExecuteSqlCommand(sql);
            }
        }
        
        public static Guid HulaDog101ProviderId = new Guid("{41BA15BD-6BC7-4968-B434-CAC677D7DC35}");
        public static Guid HulaDog102ProviderId = new Guid("{D9AB5D7A-F3C9-4B37-AB77-1F8BF572CA9D}");


        public static Guid BoulderDogProviderId = new Guid("{6E78060E-4597-4D0F-91E3-4A14A8FD2757}");
    }
}

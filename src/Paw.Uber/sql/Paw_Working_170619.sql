SELECT * FROM [Owner]

SELECT * FROM [Provider]

SELECT * FROM [ProviderGroup]

MERGE [Owner] AS [Target]
                USING Owner AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET [Target].[FirstName] = [Source].[FirstName], [Target].[LastName] = [Source].[LastName], [Target].[Email] = [Source].[Email], [Target].[PhoneNumber] = [Source].[PhoneNumber], [Target].[AltPhoneNumber] = [Source].[AlternativePhoneNumber], [Target].[StreetAddress] = [Source].[StreetAddress], [Target].[City] = [Source].[City], [Target].[State] = [Source].[State], [Target].[PostalCode] = [Source].[PostalCode], [Target].[ProviderGroupId] = [Source].[ProviderGroupId], [Target].[LegacyId] = [Source].[Id], [Target].[ImportDate] = (GETUTCDATE()), [Target].[UpdatedDate] = (GETUTCDATE()), [Target].[UpdatedById] = ('ccfd1dae-f278-4800-b9ed-50142a963202'), [Target].[MessageId] = (NewSequentialId()), [Target].[MessageType] = ('SQL Import') 
                WHEN NOT MATCHED THEN INSERT (FirstName, LastName, Email, PhoneNumber, AltPhoneNumber, StreetAddress, City, State, PostalCode, ProviderGroupId, LegacyId, ImportDate, Id, CreatedDate, CreatedById, UpdatedDate, UpdatedById, MessageId, MessageType) VALUES ([Source].[FirstName], [Source].[LastName], [Source].[Email], [Source].[PhoneNumber], [Source].[AlternativePhoneNumber], [Source].[StreetAddress], [Source].[City], [Source].[State], [Source].[PostalCode], [Source].[ProviderGroupId], [Source].[Id], (GETUTCDATE()), [Source].[Id], (GETUTCDATE()), ('ccfd1dae-f278-4800-b9ed-50142a963202'), (GETUTCDATE()), ('ccfd1dae-f278-4800-b9ed-50142a963202'), (NewSequentialId()), ('SQL Import'));


MERGE [Provider] AS [Target]
                USING #Provider AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET [Target].[Name] = [Source].[Name], [Target].[Key] = [Source].[Key], [Target].[PhoneNumber] = [Source].[PhoneNumber], [Target].[Status] = [Source].[Status], [Target].[ProviderGroupId] = [Source].[ProviderGroupId], [Target].[ImportDate] = (GETUTCDATE()), [Target].[UpdatedDate] = (GETUTCDATE()), [Target].[UpdatedById] = ('ccfd1dae-f278-4800-b9ed-50142a963202'), [Target].[MessageId] = (NewId()), [Target].[MessageType] = ('SQL Import') 
                WHEN NOT MATCHED THEN INSERT (Name, [Key], PhoneNumber, Status, ProviderGroupId, ImportDate, Id, CreatedDate, CreatedById, UpdatedDate, UpdatedById, MessageId, MessageType) VALUES ([Source].[Name], [Source].[Key], [Source].[PhoneNumber], [Source].[Status], [Source].[ProviderGroupId], (GETUTCDATE()), [Source].[Id], (GETUTCDATE()), ('ccfd1dae-f278-4800-b9ed-50142a963202'), (GETUTCDATE()), ('ccfd1dae-f278-4800-b9ed-50142a963202'), (NewId()), ('SQL Import'));


				
                SELECT a.*, CAST('284a43dd-f676-41e4-a713-ebc848cf85db' AS UNIQUEIDENTIFIER) as ProviderGroupId FROM [Pets] a WHERE a.Id IN
                    (SELECT DISTINCT p.Id FROM [Pets] p
                    JOIN [PetProviderDetails] pp ON p.Id = pp.Pet_Id
                    JOIN [Providers] pr ON pp.Provider_Id = pr.Id
                    WHERE p.Owner_Id IS NOT NULL, pr.ProviderGroupId = '284a43dd-f676-41e4-a713-ebc848cf85db')

					MERGE [Pet] AS [Target]
                USING #Pet AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET [Target].[Name] = [Source].[Name], [Target].[DOB] = [Source].[BirthDate], [Target].[Gender] = [Source].[Gender], [Target].[Weight] = [Source].[Weight], [Target].[ProviderGroupId] = [Source].[ProviderGroupId], [Target].[OwnerId] = [Source].[Owner_Id], [Target].[ImportDate] = (GETUTCDATE()), [Target].[CreatedDate] = [Source].[Created], [Target].[UpdatedDate] = (GETUTCDATE()), [Target].[UpdatedById] = ('ccfd1dae-f278-4800-b9ed-50142a963202'), [Target].[MessageId] = (NewId()), [Target].[MessageType] = ('SQL Import') 
                WHEN NOT MATCHED THEN INSERT ([Name], [DOB], [Gender], [Weight], [ProviderGroupId], [OwnerId], [ImportDate], [CreatedDate], [Id], [CreatedById], [UpdatedDate], [UpdatedById], [MessageId], [MessageType]) VALUES ([Source].[Name], [Source].[BirthDate], [Source].[Gender], [Source].[Weight], [Source].[ProviderGroupId], [Source].[Owner_Id], (GETUTCDATE()), [Source].[Created], [Source].[Id], ('ccfd1dae-f278-4800-b9ed-50142a963202'), (GETUTCDATE()), ('ccfd1dae-f278-4800-b9ed-50142a963202'), (NewId()), ('SQL Import'));


				SELECT * FROM PetImport

				SELECT i.Id, o.Id
				DELETE PetImport
					FROM PetImport i
					LEFT JOIN [Owner] o ON i.Owner_ID = o.Id
					WHERE o.Id IS NULL

				SELECT * FROM PetImport i

				DROP TABLE SearchTemp

				DELETE [PetImport] i LEFT JOIN

				SELECT * FROM Pet

				sp_spaceused 'Pet'

				SELECT p.ID, p.Name, o.FirstName, o.LastName, o.ProviderGroupId, 'UNKNOWN' as Breed
				INTO SearchTemp
				FROM Pet p
					JOIN [Owner] o
					ON p.OwnerId = o.Id
				
				sp_spaceused 'SearchTemp'	

				SELECT * FROM SearchTemp where 

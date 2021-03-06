
 

Use master 
Go
 
  
Create Master Key Encryption BY Password = '45Gme*3^fwu';
Go


Create Certificate EndPointTargetCertificate
WITH Subject = 'Target.Server.Local',
       START_DATE = '01/01/2018',
       EXPIRY_DATE = '01/01/2019'
ACTIVE FOR BEGIN_DIALOG = ON;
GO
 

CREATE ENDPOINT InstTargetEndpoint
      STATE=STARTED
      AS TCP (LISTENER_PORT = 4022)
      FOR SERVICE_BROKER
      ( 
         AUTHENTICATION = CERTIFICATE EndPointTargetCertificate,
         ENCRYPTION = SUPPORTED
      );
	    


BACKUP CERTIFICATE EndPointTargetCertificate TO FILE=
  'D:\storedcerts\$ampleSSBCerts\EndPointTargetCertificate.cer';
GO


--  Part1


Create Certificate EndPointInitiatorCertificate
 From FILE = 
 'D:\storedcerts\$ampleSSBCerts\EndPointInitiatorCertificate.cer';
GO

CREATE LOGIN SbLogin
 FROM CERTIFICATE EndPointInitiatorCertificate;
GO

GRANT CONNECT ON ENDPOINT::InstTargetEndpoint To SbLogin
GO
 
CREATE DATABASE TargetDb;
GO
use TargetDb;
Create Master Key Encryption BY
Password = '45Gme*3^fwu';
Go
Create Certificate TargetDatabaseUserCertificate
 WITH Subject = 'Target.Server.Local',
    START_DATE = '01/01/2018',
    EXPIRY_DATE = '01/01/2019' 
ACTIVE FOR BEGIN_DIALOG = ON;
GO

use TargetDb;

BACKUP CERTIFICATE TargetDatabaseUserCertificate TO
FILE='D:\storedcerts\$ampleSSBCerts\TargetDatabaseUserCertificate.cer';
GO

Create User InitiatorDatabaseUser WITHOUT LOGIN
GO

-- Part 2

use TargetDb;

CREATE CERTIFICATE InitiatorDatabaseUserCertificate
 AUTHORIZATION InitiatorDatabaseUser
FROM FILE = 'D:\storedcerts\$ampleSSBCerts\InitiatorDatabaseUserCertificate.cer';
GO

GRANT CONNECT TO InitiatorDatabaseUser;
go

Create Queue TargetQueue
 WITH status = ON

 go

Create Service [//Target/InternalAct/TargetService] ON QUEUE [TargetQueue]  ([//BothDB/2InstSample/SimpleContract])

GRANT SEND ON SERVICE::[//Target/InternalAct/TargetService] To InitiatorDatabaseUser;
GO

CREATE REMOTE SERVICE BINDING [//Initiator/InternalAct/InitiatorServiceBinding[]
 TO SERVICE '//Initiator/InternalAct/InitiatorService'
 WITH USER = InitiatorDatabaseUser

 /*
 Выполнить запрос на InitiatorDb
 select service_broker_guid
 from sys.databases
 where name = 'InitiatorDb'

 Для определения BROKER_INSTANCE  
 */

 Create Route TargetRoute
WITH
  SERVICE_NAME = '//Initiator/InternalAct/InitiatorService',
  BROKER_INSTANCE='...-...-...-...-...',
 ADDRESS = 'TCP://10.200.0.42:4022'
GO



 

	  ----------
/*
	  USE master ;
GO

-- Create a login for the remote instance.

CREATE LOGIN RemoteInstanceLogin
    WITH PASSWORD = '#gh!3A%!1@f' ;
GO

-- Create a user for the login in the master database.

CREATE USER RemoteInstanceUser
    FOR LOGIN RemoteInstanceLogin ;
GO

-- Load the certificate from the file system. Notice that
-- the login owns the certificate.

CREATE CERTIFICATE RemoteInstanceCertificate
    AUTHORIZATION RemoteInstanceUser
    FROM FILE='C:\Certificates\AceBikeComponentsCertificate.cer' ;
GO
GRANT CONNECT ON ENDPOINT::ThisInstanceEndpoint to RemoteInstanceLogin ;
GO
-- Write the certificate from this instance
-- to the file system. This command assumes
-- that the certificate used by the Service Broker
-- endpoint is named TransportSecurity.
 
BACKUP CERTIFICATE TransportSecurity
    TO FILE = 'C:\Certificates\ThisInstanceCertificate.cer' ;
GO*/
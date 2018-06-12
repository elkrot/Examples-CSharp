
 

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
  'C:\Documents and Settings\Santhi\Desktop\Service Broker\Session\EndPointTargetCertificate.cer';
GO

Create Certificate EndPointInitiatorCertificate
 From FILE = 
 'C:\Documents and Settings\Santhi\Desktop\Service Broker\Session\EndPointInitiatorCertificate.cer';
GO

CREATE LOGIN SbLogin
 FROM CERTIFICATE EndPointInitiatorCertificate;
GO

GRANT CONNECT ON ENDPOINT::InstTargetEndpoint To SbLogin
GO
 
CREATE DATABASE TargetDb;
GO
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
FILE='C:\Documents and Settings\Santhi\Desktop\Service Broker\Session\TargetDatabaseUserCertificate.cer';
GO

Create User InitiatorDatabaseUser WITHOUT LOGIN
GO


CREATE CERTIFICATE InitiatorDatabaseUserCertificate
 AUTHORIZATION InitiatorDatabaseUser
FROM FILE = 'C:\Documents and Settings\Santhi\Desktop\Service Broker\Session\InitiatorDatabaseUserCertificate.cer';
GO

GRANT CONNECT TO InitiatorDatabaseUser;

GRANT SEND ON SERVICE:://AWDB/InternalAct/TargetService To InitiatorDatabaseUser;
GO

CREATE REMOTE SERVICE BINDING //AWDB/InternalAct/InitiatorServiceBinding
 TO SERVICE '//AWDB/InternalAct/InitiatorService'
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
  SERVICE_NAME = '//AWDB/InternalAct/InitiatorService',
  BROKER_INSTANCE='...-...-...-...-...',
 ADDRESS = 'TCP://192.168.1.11:1434'
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
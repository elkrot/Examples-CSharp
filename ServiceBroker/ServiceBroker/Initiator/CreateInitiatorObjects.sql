
 

Use master 
Go 
 

Create Master Key Encryption BY Password = '45Gme*3^fwu';
Go


Create Certificate EndPointInitiatorCertificate
WITH Subject = 'Initiator.Server.Local',
       START_DATE = '01/01/2018',
       EXPIRY_DATE = '01/01/2019'
ACTIVE FOR BEGIN_DIALOG = ON;
GO
 

CREATE ENDPOINT InstInitiatorEndpoint
      STATE=STARTED
      AS TCP (LISTENER_PORT = 4022)
      FOR SERVICE_BROKER
      ( 
         AUTHENTICATION = CERTIFICATE EndPointInitiatorCertificate,
         ENCRYPTION = SUPPORTED
      );
	    


BACKUP CERTIFICATE EndPointInitiatorCertificate TO FILE=
  'D:\storedcerts\$ampleSSBCerts\EndPointInitiatorCertificate.cer';
GO

-- Part 1


Create Certificate EndPointTargetCertificate
 From FILE = 
 'D:\storedcerts\$ampleSSBCerts\EndPointTargetCertificate.cer';
GO

CREATE LOGIN SbLogin
 FROM CERTIFICATE EndPointTargetCertificate;
GO

GRANT CONNECT ON ENDPOINT::InstInitiatorEndpoint To SbLogin
GO
 
CREATE DATABASE InitiatorDb;
GO

use InitiatorDb;

Create Master Key Encryption BY
Password = '45Gme*3^fwu';
Go
Create Certificate InitiatorDatabaseUserCertificate
 WITH Subject = 'Initiator.Server.Local',
    START_DATE = '01/01/2018',
    EXPIRY_DATE = '01/01/2019' 
ACTIVE FOR BEGIN_DIALOG = ON;
GO

use InitiatorDb;

BACKUP CERTIFICATE InitiatorDatabaseUserCertificate TO
FILE='D:\storedcerts\$ampleSSBCerts\InitiatorDatabaseUserCertificate.cer';
GO

Create User TargetDatabaseUser WITHOUT LOGIN
GO

-- Part 2


CREATE CERTIFICATE TargetDatabaseUserCertificate
 AUTHORIZATION TargetDatabaseUser
FROM FILE = 'D:\storedcerts\$ampleSSBCerts\TargetDatabaseUserCertificate.cer';
GO

GRANT CONNECT TO TargetDatabaseUser;

Create Queue InitiatorQueue
 WITH status = ON

 go

Create Service [//Initiator/InternalAct/InitiatorService] ON QUEUE [InitiatorQueue]  ([//BothDB/2InstSample/SimpleContract])


GRANT SEND ON SERVICE::[//Initiator/InternalAct/InitiatorService] To TargetDatabaseUser;
GO

CREATE REMOTE SERVICE BINDING [//Target/InternalAct/TargetServiceBinding]
 TO SERVICE '//Target/InternalAct/TargetService'
 WITH USER = TargetDatabaseUser
  
 /*
 Выполнить запрос на TargetDb
 select service_broker_guid
 from sys.databases
 where name = 'TargetDb'

 Для определения BROKER_INSTANCE  
 */

 Create Route InitiatorRoute
WITH
  SERVICE_NAME = '//Target/InternalAct/TargetService',
  BROKER_INSTANCE='...-...-...-...-...',
 ADDRESS = 'TCP://10.200.0.41:4022'  
GO


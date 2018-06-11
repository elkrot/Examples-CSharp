
 

Use master
Go
 
--1. Create a master key for master database.
Create Master Key Encryption BY Password = '45Gme*3^&fwu';
Go
--2.Create certificate and End Point that support certificate based authentication.
Create Certificate EndPointCertificateB
WITH Subject = 'B.Server.Local',
       START_DATE = '01/01/2006',
       EXPIRY_DATE = '01/01/2008'
ACTIVE FOR BEGIN_DIALOG = ON;
GO
CREATE ENDPOINT ServiceBrokerEndPoint
      STATE=STARTED
      AS TCP (LISTENER_PORT = 4022)
      FOR SERVICE_BROKER
      ( 
         AUTHENTICATION = CERTIFICATE EndPointCertificateB,
         ENCRYPTION = SUPPORTED
      );





	  ----------

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
GO
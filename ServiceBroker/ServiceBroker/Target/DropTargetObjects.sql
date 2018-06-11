
 

IF EXISTS (SELECT * FROM sys.services
           WHERE name = N'//AWDB/InternalAct/TargetService')
     DROP SERVICE
     [//AWDB/InternalAct/TargetService];

IF EXISTS (SELECT * FROM sys.service_queues
           WHERE name = N'TargetQueue')
     DROP QUEUE TargetQueue;

	 USE master;
GO

IF EXISTS (SELECT * FROM master.sys.endpoints
           WHERE name = N'InstTargetEndpoint')
     DROP ENDPOINT InstTargetEndpoint;
GO
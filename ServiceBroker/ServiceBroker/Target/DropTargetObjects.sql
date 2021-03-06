
 

IF EXISTS (SELECT * FROM sys.services
           WHERE name = N'//Target/InternalAct/TargetService')
     DROP SERVICE
     [//Target/InternalAct/TargetService];
	  
IF EXISTS (SELECT * FROM sys.service_queues
           WHERE name = N'TargetQueue')
     DROP QUEUE TargetQueue;

	 USE master;
GO

IF EXISTS (SELECT * FROM master.sys.endpoints
           WHERE name = N'InstTargetEndpoint')
     DROP ENDPOINT InstTargetEndpoint;
GO
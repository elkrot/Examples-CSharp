
 

Declare @ConversationHandle uniqueidentifier

Begin Transaction
Begin Dialog @ConversationHandle
 From Service [//Initiator/InternalAct/InitiatorService]
 To Service '//Target/InternalAct/TargetService'
 On Contract [//BothDB/2InstSample/SimpleContract]
 WITH Encryption=off;
SEND 
      ON CONVERSATION @ConversationHandle
      Message Type [//BothDB/2InstSample/RequestMessage]
  ('<test>test</test>')
Commit


select cast(message_body as xml) as [Мое неотправленное сообщение], transmission_status,* from sys.transmission_queue


select * from sys.conversation_endpoints ce join sys.services s on ce.service_id = s.service_id join sys.service_queues sq on s.service_queue_id = sq.object_id order by ce.conversation_id, ce.is_initiator desc

select * from sys.dm_broker_activated_tasks
SELECT is_broker_enabled FROM sys.databases
WHERE database_id = DB_ID() 

select * from sys.routes

select is_broker_enabled,* from sys.databases

Dialog security is unavailable for this conversation because there is no security certificate bound to the database principal (Id: 1). Either create a certificate for the principal, or specify ENCRYPTION = OFF when beginning the conversation.

SELECT * FROM sys.service_message_types ; 

SELECT * FROM sys.service_contracts ; 
 
SELECT * FROM sys.service_queues ; 


SELECT * FROM sys.services ; 

SELECT * FROM sys.dm_broker_connections 

use master
SELECT name, role_desc, state_desc FROM sys.database_mirroring_endpoints; 



SELECT N'No Exact Match' = tq.to_service_name
FROM sys.transmission_queue AS tq
WHERE NOT EXISTS
    (SELECT remote_service_name
     FROM sys.routes AS routes
     WHERE tq.to_service_name = routes.remote_service_name) ;

END CONVERSATION 'E6D86D27-CB63-E811-A2F1-08606E6D35A8' WITH CLEANUP;

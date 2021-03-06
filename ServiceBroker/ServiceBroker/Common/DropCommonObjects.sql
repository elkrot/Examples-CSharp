
  


-- Drop contract and message type if they already exist.
IF EXISTS (SELECT * FROM sys.service_contracts
           WHERE name =
           N'//BothDB/2InstSample/SimpleContract')
     DROP CONTRACT
     [//BothDB/2InstSample/SimpleContract];
	  
IF EXISTS (SELECT * FROM sys.service_message_types
           WHERE name =
           N'//BothDB/2InstSample/RequestMessage')
     DROP MESSAGE TYPE
     [//BothDB/2InstSample/RequestMessage];

IF EXISTS (SELECT * FROM sys.service_message_types
           WHERE name =
           N'//BothDB/2InstSample/ReplyMessage')
     DROP MESSAGE TYPE
     [//BothDB/2InstSample/ReplyMessage];
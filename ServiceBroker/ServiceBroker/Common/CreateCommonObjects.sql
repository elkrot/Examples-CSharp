
  


CREATE MESSAGE TYPE [//BothDB/2InstSample/RequestMessage]
       VALIDATION = WELL_FORMED_XML;
CREATE MESSAGE TYPE [//BothDB/2InstSample/ReplyMessage]
       VALIDATION = WELL_FORMED_XML;
GO
 

CREATE CONTRACT [//BothDB/2InstSample/SimpleContract]   
      ([//BothDB/2InstSample/RequestMessage]
         SENT BY INITIATOR,
       [//BothDB/2InstSample/ReplyMessage]
         SENT BY TARGET
      );
GO

 



Declare @ConversationHandle as uniqueidentifier
Declare @MessageBody as nvarchar(max)
Declare @MessageType as sysname

Begin Transaction
Print 'Started Receiving ';

RECEIVE top (1)
      @MessageType = message_type_name,
      @ConversationHandle = conversation_handle,
    @MessageBody = message_body
FROM TargetQueue;

if @MessageType = '//BothDB/2InstSample/RequestMessage'
      Begin
            SEND 
                  ON CONVERSATION @ConversationHandle
                  Message Type [//BothDB/2InstSample/ReplyMessage]
                  ('Message is received')
            END Conversation @ConversationHandle
      END

Commit
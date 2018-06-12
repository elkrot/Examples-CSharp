
 



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

if @MessageType = 'SenderMessageType'
      Begin
            SEND 
                  ON CONVERSATION @ConversationHandle
                  Message Type ReceiverMessageType
                  ('Message is received')
            END Conversation @ConversationHandle
      END

Commit
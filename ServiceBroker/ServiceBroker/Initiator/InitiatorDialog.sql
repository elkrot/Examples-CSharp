
 

Declare @ConversationHandle uniqueidentifier

Begin Transaction
Begin Dialog @ConversationHandle
 From Service SenderService
 To Service 'ReceiverService'
 On Contract SampleContract
 WITH Encryption=off;
SEND 
      ON CONVERSATION @ConversationHandle
      Message Type SenderMessageType
  ('<test>test</test>')
Commit
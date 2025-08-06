export interface Message {
    messageId: number;
    messageContent: string;
    senderId: number;
    receiverId: number;
    sentAt: Date;
    isRead: boolean;
  }

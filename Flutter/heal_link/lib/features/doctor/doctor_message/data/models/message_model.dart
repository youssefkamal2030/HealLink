import 'package:heal_link/core/utils/app_images.dart';

class MessageModel {
  final String name;
  final String message;
  final String time;
  final String image;
  final bool isRead;
  final String unreadCount;

  MessageModel({
    required this.name,
    required this.message,
    required this.time,
    required this.image,
    required this.isRead,
    required this.unreadCount,
  });
}

List<MessageModel> allMessages = [
  MessageModel(
    name: "Ahmed Omar",
    message: "The pain came back",
    time: "10:12 PM",
    image: AppImages.user1,
    isRead: false,
    unreadCount: "2",
  ),
  MessageModel(
    name: "Mohamed Ahmed",
    message: "I've updated your prescription...",
    time: "3:33 PM",
    image: AppImages.user2,
    isRead: true,
    unreadCount: "0",
  ),
  MessageModel(
    name: "Sara Amr",
    message: "Don't forget to do the test before...",
    time: "Yesterday",
    image: AppImages.user3,
    isRead: true,
    unreadCount: "0",
  ),
  MessageModel(
    name: "Ali Mohamed",
    message: "Is this normal after the injection?",
    time: "10:12 PM",
    image: AppImages.user4,
    isRead: false,
    unreadCount: "4",
  ),
];

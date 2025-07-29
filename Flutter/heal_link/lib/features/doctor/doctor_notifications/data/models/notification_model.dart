import 'package:heal_link/core/utils/app_images.dart';

class NotificationModel {
  final String imgUrl;
  final String title;
  final String description;
  final String time;
  final bool isNew;
  final String category;
  NotificationModel({
    required this.imgUrl,
    required this.title,
    required this.description,
    required this.time,
    this.isNew = false,
    required this.category,
  });
}

List<NotificationModel> notifications = [
  // New Notifications
  NotificationModel(
    imgUrl: AppImages.person , 
    title: 'New Case Submitted',
    description: 'A new patient has submitted a medical case for your review.',
    time: '30m',
    isNew: true,
    category: "None",
  ),
  NotificationModel(
     imgUrl: AppImages.person , 
    title: 'You Received a New Review',
    description: 'A patient has rated and reviewed your recent session',
    time: '30m',
    isNew: true,
     category: "Star",
  ),

  // Earlier Notifications
  NotificationModel(
   imgUrl: AppImages.person , 
    title: 'Payment Transferred',
    description:
        'An amount of \$250 has been transferred to your account for completed consultations',
    time: '30m ',
    isNew: false,
     category: "Payment",
  ),
  NotificationModel(
 imgUrl: AppImages.person , 
    title: 'New Message Received',
    description: 'You’ve received a new message from a patient',
    time: '30m ',
    isNew: false,
     category: "Message",
  ),
];

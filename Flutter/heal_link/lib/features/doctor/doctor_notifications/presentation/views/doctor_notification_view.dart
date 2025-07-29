import 'package:flutter/material.dart';

import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/doctor_notifications/data/models/notification_model.dart';
import 'package:heal_link/features/doctor/doctor_notifications/presentation/widgets/review_notification_card.dart';

class DoctorNotificationsView extends StatelessWidget {
   DoctorNotificationsView({super.key});
final newNotifications = notifications.where((n) => n.isNew).toList();
final eailerNotifications = notifications.where((n) => !n.isNew).toList();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: ListView(
        padding: const EdgeInsets.symmetric(horizontal: 16),
        physics: BouncingScrollPhysics(),
        children: [
          CustomAppBarPopWidget(text: "Notifications"),
          SizedBox(height: 24),
          NotificationSectionTitle(title: "New",),
          SizedBox(height: 24),
          NewNotificationsListView(passedNotifications: newNotifications,),
          SizedBox(height: 16),
          NotificationSectionTitle(title: 'Earlier',),
          SizedBox(height: 8),
          NewNotificationsListView(passedNotifications: eailerNotifications,),
        ],
      ),
    );
  }
}

class NotificationSectionTitle extends StatelessWidget {
  const NotificationSectionTitle({
    super.key, required this.title,
  });
final String title;
  @override
  Widget build(BuildContext context) {
    return Text(
      title,
      textAlign: TextAlign.start,
      style: AppTextStyles.popins500style16PrimaryColor,
    );
  }
}

class NewNotificationsListView extends StatelessWidget {
  const NewNotificationsListView({
    super.key,
    required this.passedNotifications,
  });
final List <NotificationModel> passedNotifications ;  
  @override
  Widget build(BuildContext context) {
    return ListView.separated(
      padding: EdgeInsets.zero,
      physics: const NeverScrollableScrollPhysics(),
      scrollDirection: Axis.vertical,
      shrinkWrap: true,
      itemBuilder: (context, index) => ReviewNotificationCard( notificationModel:  passedNotifications[index]),
      separatorBuilder: (context, index) => SizedBox(height: 8),
      itemCount: passedNotifications.length,
    );
  }
}

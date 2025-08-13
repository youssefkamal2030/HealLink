import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_notifications/data/models/notification_model.dart';

class ReviewNotificationCard extends StatelessWidget {
  const ReviewNotificationCard({super.key, required this.notificationModel});
  final NotificationModel notificationModel;
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(8),

      decoration: BoxDecoration(
        color: AppColors.kWhiteColor,
        borderRadius: BorderRadius.circular(12),
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          // Profile image with star icon
          Stack(
            children: [
              CircleAvatar(
                radius: 24,
                child: Image.asset(notificationModel.imgUrl, fit: BoxFit.cover),
              ),
              notificationModel.category == "None"
                  ? SizedBox()
                  : Positioned(
                bottom: 0,
                right: 0,
                child: Container(
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: Colors.white,
                  ),
                  padding: const EdgeInsets.all(2),
                  child:  SvgPicture.asset(
                    height: 10,
                    width: 10,
                     notificationModel.category == "Star"
                        ? AppImages.starAmberIcon
                        : notificationModel.category == "Payment"
                            ? AppImages.walletAmberIcon
                            : notificationModel.category == "Message"
                                ? AppImages.messageAmberIcon
                                : AppImages.notification,

                  )
                ),
              )
            ],
          ),
          const SizedBox(width: 12),
          // Texts
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  notificationModel.title,
                  style: AppTextStyles.popins400style16LightDarkGrey.copyWith(
                    color: AppColors.kPrimaryColor,
                  ),
                ),
                SizedBox(height: 4),
                Text(
                  notificationModel.description,
                  style: AppTextStyles.popins400style12kPrimaryColor.copyWith(
                    color: AppColors.kDarkGreyColor,
                  ),
                ),
              ],
            ),
          ),
          // Time
          SizedBox(
            height: 48,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Text(
                  notificationModel.time,
                  style: AppTextStyles.popins400style12kPrimaryColor.copyWith(
                    color: AppColors.kDarkGreyColor,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
